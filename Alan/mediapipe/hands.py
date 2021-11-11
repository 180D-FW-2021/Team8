import cv2
import mediapipe as mp
import csv
mp_drawing = mp.solutions.drawing_utils
mp_drawing_styles = mp.solutions.drawing_styles
mp_hands = mp.solutions.hands


# For webcam input:
cap = cv2.VideoCapture(0)
cap.set(cv2.CAP_PROP_FPS, 10)
with open('wrist_sequential.csv', 'w', newline='') as f:
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
							cx, cy = landmrk.x, landmrk.y
							# print(cx, cy)
							# print (ids, cx, cy)
							writer.writerow([cx, cy])
					mp_drawing.draw_landmarks(
						image,
						hand_landmarks,
						mp_hands.HAND_CONNECTIONS,
						mp_drawing_styles.get_default_hand_landmarks_style(),
						mp_drawing_styles.get_default_hand_connections_style())
			# Flip the image horizontally for a selfie-view display.
			cv2.imshow('MediaPipe Hands', cv2.flip(image, 1))
			if cv2.waitKey(5) & 0xFF == 27:
			  break
	cap.release()