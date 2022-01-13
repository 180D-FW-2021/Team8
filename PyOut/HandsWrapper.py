import cv2
import mediapipe as mp
import csv
import socket

'''
The following functions are set up to track wrist positions and record them in .csv files.
These should be more than sufficient for the needs of our game(s) and seem to be light on resource use.

TODO: show_video = False prevents the escape key from stopping the program. This is because waitKey() only works if there is an active window.
Might create a dummy window of no size to allow waitKey() to work. But this is not a deployable solution-- the game should be the window in focus at all times.
I think we'll be able to integrate this more seamlessly with the game logic than with keyboard input.

TODO: filename might need to have "PyOut/" prepended to it. New requirement since the Unity external process
starts in the project root folder (Hands Game). For some reason just prepending doesn't work. Investigate...
'''

def hands_sequential(filename = 'PyOut/wrist_sequential.csv', show_video = True, show_nodes = True, frame_wait = 10):
	'''
	This function will write the wrist node position on every frame that the node is present.
	There are no timestamps as of now (but that should be pretty easy to do).
	This will create large files if used for a long time!
	
	filename: string filename ending in .csv to write wrist node position. Overwrites if existing.
	show_video: Set to True to display cv2.VideoCapture() webcam output.
	show_nodes: Set to False to stop Mediapipe from drawing node positions on the video window. 
	Also requires show_video = True.
	frame_wait: Sets the delay (in ms) between loop iterations. This is simply the time passed to waitKey() to wait for the esc key and to preserve resources.
	Higher values result in speedup at the cost of detection speed.
	'''
	mp_drawing = mp.solutions.drawing_utils
	mp_drawing_styles = mp.solutions.drawing_styles
	mp_hands = mp.solutions.hands
	# For webcam input:
	cap = cv2.VideoCapture(0)
	
	if(frame_wait <= 1):
		frame_wait = 5 # set minimum frame delay to avoid locking program up
	
	with open(filename, 'w', newline='') as f:
		writer = csv.writer(f)
		with mp_hands.Hands(
			model_complexity=0,
			min_detection_confidence=0.5,
			min_tracking_confidence=0.5) as hands:
			while cap.isOpened():
				success, image = cap.read()
				if not success:
				  print("Ignoring empty camera frame.")
				  # If loading a video, use 'break' instead of 'continue'.
				  continue

				# To improve performance, optionally mark the image as not writeable to
				# pass by reference.
				image.flags.writeable = False
				image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
				results = hands.process(image)

				# Draw the hand annotations on the image.
				image.flags.writeable = True
				image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
				if results.multi_hand_landmarks:
					for hand_landmarks in results.multi_hand_landmarks:
					#id pulling code below from https://arkalsekar.medium.com/how-to-get-all-the-co-ordinates-of-hand-using-mediapipe-hand-solutions-ac7e2742f702
						for ids, landmrk in enumerate(hand_landmarks.landmark):
							# print(ids, landmrk)
							if ids == 0: #wrist node. But for which hand if 2 are onscreen?
								cx, cy, cz = landmrk.x, landmrk.y, landmrk.z
								writer.writerow([cx, cy, cz])
						if(show_video and show_nodes):
							mp_drawing.draw_landmarks(
								image,
								hand_landmarks,
								mp_hands.HAND_CONNECTIONS,
								mp_drawing_styles.get_default_hand_landmarks_style(),
								mp_drawing_styles.get_default_hand_connections_style())
				if(show_video):
				# Flip the image horizontally for a selfie-view display.
					cv2.imshow('MediaPipe Hands', cv2.flip(image, 1))
				if cv2.waitKey(frame_wait) & 0xFF == 27:
				# press esc (ASCII 27) to stop
					break
		cap.release()
		
def hands_single(filename = 'PyOut/wrist_single.csv', show_video = True, show_nodes = True, frame_wait = 10):
	'''
	This function will save only the last detected wrist node position.
	
	filename: string filename ending in .csv to write wrist node position. Overwrites if existing.
	show_video: Set to True to display cv2.VideoCapture() webcam output.
	show_nodes: Set to False to stop Mediapipe from drawing node positions on the video window. 
	Also requires show_video = True.
	frame_wait: Sets the delay (in ms) between loop iterations. This is simply the time passed to waitKey() to wait for the esc key and to preserve resources.
	Higher values result in speedup at the cost of detection speed.
	'''
	mp_drawing = mp.solutions.drawing_utils
	mp_drawing_styles = mp.solutions.drawing_styles
	mp_hands = mp.solutions.hands
	# For webcam input:
	cap = cv2.VideoCapture(0)
	
	if(frame_wait <= 1):
		frame_wait = 5 # set minimum frame delay to avoid locking program up
	
	with mp_hands.Hands(
		model_complexity=0,
		min_detection_confidence=0.5,
		min_tracking_confidence=0.5) as hands:
		while cap.isOpened():
			success, image = cap.read()
			if not success:
			  print("Ignoring empty camera frame.")
			  # If loading a video, use 'break' instead of 'continue'.
			  continue

			# To improve performance, optionally mark the image as not writeable to
			# pass by reference.
			image.flags.writeable = False
			image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
			results = hands.process(image)

			# Draw the hand annotations on the image.
			image.flags.writeable = True
			image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
			if results.multi_hand_landmarks:
				for hand_landmarks in results.multi_hand_landmarks:
				#id pulling code below from https://arkalsekar.medium.com/how-to-get-all-the-co-ordinates-of-hand-using-mediapipe-hand-solutions-ac7e2742f702
					for ids, landmrk in enumerate(hand_landmarks.landmark):
						# print(ids, landmrk)
						if ids == 0: #wrist node. But for which hand if 2 are onscreen?
							f = open(filename, 'w', newline='')
							writer = csv.writer(f)
							cx, cy, cz = landmrk.x, landmrk.y, landmrk.z
							writer.writerow([cx, cy, cz])
							f.close()
							# TODO: add break here to keep the loop from acting on the other nodes for potential speedup.
					if(show_video and show_nodes):
						mp_drawing.draw_landmarks(
							image,
							hand_landmarks,
							mp_hands.HAND_CONNECTIONS,
							mp_drawing_styles.get_default_hand_landmarks_style(),
							mp_drawing_styles.get_default_hand_connections_style())
			if(show_video):
			# Flip the image horizontally for a selfie-view display.
				cv2.imshow('MediaPipe Hands', cv2.flip(image, 1))
			if cv2.waitKey(frame_wait) & 0xFF == 27:
			# press esc (ASCII 27) to stop
				break
	cap.release()
	
def hands_tcp(port = 13000, host_ip = '127.0.0.1', show_video = True, show_nodes = True, frame_wait = 10):
	'''
	This function will save only the last detected wrist node position.
	
	port: Port number to use TCP over. Must match setting used in Unity
	show_video: Set to True to display cv2.VideoCapture() webcam output.
	show_nodes: Set to False to stop Mediapipe from drawing node positions on the video window. 
	Also requires show_video = True.
	frame_wait: Sets the delay (in ms) between loop iterations. This is simply the time passed to waitKey() to wait for the esc key and to preserve resources.
	Higher values result in speedup at the cost of detection speed.
	'''
	mp_drawing = mp.solutions.drawing_utils
	mp_drawing_styles = mp.solutions.drawing_styles
	mp_hands = mp.solutions.hands
	# For webcam input:
	cap = cv2.VideoCapture(0)
	
	# initiate TCP client
	client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
	client.connect((host_ip, port))
	
	if(frame_wait <= 1):
		frame_wait = 5 # set minimum frame delay to avoid locking program up
	
	with mp_hands.Hands(
		model_complexity=0,
		min_detection_confidence=0.5,
		min_tracking_confidence=0.5) as hands:
		while cap.isOpened():
			success, image = cap.read()
			if not success:
			  print("Ignoring empty camera frame.")
			  # If loading a video, use 'break' instead of 'continue'.
			  continue

			# To improve performance, optionally mark the image as not writeable to
			# pass by reference.
			image.flags.writeable = False
			image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
			results = hands.process(image)

			# Draw the hand annotations on the image.
			image.flags.writeable = True
			image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
			if results.multi_hand_landmarks:
				for hand_landmarks in results.multi_hand_landmarks:
				#id pulling code below from https://arkalsekar.medium.com/how-to-get-all-the-co-ordinates-of-hand-using-mediapipe-hand-solutions-ac7e2742f702
					for ids, landmrk in enumerate(hand_landmarks.landmark):
						# print(ids, landmrk)
						if ids == 0: #wrist node. But for which hand if 2 are onscreen?
							cx, cy, cz = '%.3f' % landmrk.x, '%.3f' % landmrk.y, '%.3f' % landmrk.z
							coords = ",".join([cx, cy, cz])
							client.send(coords.encode('ascii'))
							# TODO: add break here to keep the loop from acting on the other nodes for potential speedup.
					if(show_video and show_nodes):
						mp_drawing.draw_landmarks(
							image,
							hand_landmarks,
							mp_hands.HAND_CONNECTIONS,
							mp_drawing_styles.get_default_hand_landmarks_style(),
							mp_drawing_styles.get_default_hand_connections_style())
			if(show_video):
			# Flip the image horizontally for a selfie-view display.
				cv2.imshow('MediaPipe Hands', cv2.flip(image, 1))
			if cv2.waitKey(frame_wait) & 0xFF == 27:
			# press esc (ASCII 27) to stop
				break
	client.close()
	cap.release()