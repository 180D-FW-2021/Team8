# IMUComms.py
# Meant to run on host computer

import paho.mqtt.client as mqtt
import time


numbers = [0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16]

game_running = False
shape_written = False

loop_delay = 0.1
file_path = "../../IMUCommsTxt.txt"

# The callback for when the client receives a CONNACK response from the server.
def on_connect(client, userdata, flags, rc):
	print("Connection returned result: " + str(rc))

	# Subscribing in on_connect() means that if we lose the ocnnection and
	# reconnect, then subscriptions will be renews
	client.subscribe("ece180d/team8/imu", qos = 1)

# The callback of the client when it disconnects
def on_disconnect(client, userdata, rc):
	if rc != 0:
		print('Unexpected Disconnect')
	else:
		print("Expected Disconnect")

# The default message callback
# (can create separate callbacks per subscribed topic)
def on_message(client, userdata, message):
	print("Received message: " + str(message.payload) + '" on topic "' +
		message.topic + '" with QoS' + str(message.qos))

	# If message is if the shape is recognized: true or false
	if "True" in str(message.payload):
		with open(file_path, "r") as file:
			data = file.readlines()
		data[1] = "True\n"
		with open(file_path, "w") as file:
			file.writelines(data)

	elif message.payload == "X":
		return

	elif int(message.payload) in numbers:
		with open(file_path, "r") as file:
			data = file.readlines()
		data[3] = str(int(message.payload))
		with open(file_path, "w") as file:
			file.writelines(data)

client = mqtt.Client("", True, None, mqtt.MQTTv31)
#client = mqtt.Client()

client.on_connect = on_connect
client.on_disconnect = on_disconnect
client.on_message = on_message

# 2. connect to a broker using one of the connect*() functions
client.connect_async('test.mosquitto.org', 1883, 60)
#client.connect_async('mqtt.eclipseprojects.io', 1883)

#3. call one of the loop*() functions to maintain network traffic flow with the broker.
client.loop_start()

while True:
	# try:
	# 	file = open(file_path, "r")
	# 	data = file.readlines()
	# 	print(data)

	# 	if data[0] != "N/A\n" and not shape_written:
	# 		client.publish("ece180d/team8/unity", data[0], qos=1)
	# 		shape_written = True
	# 		game_running = True
	# 	elif data[0] == "N/A\n":
	# 		client.publish("ece180d/team8/unity", "stop", qos=1)
	# 		shape_written = False
	# 		game_running = False

	# 	if data[2] == "True\n":
	# 		client.publish("ece180d/team8/unity", "stop", qos=1)
	# 		game_running = False
	# 	elif data[2] == "False\n":
	# 		client.publish("ece180d/team8/unity", "start", qos=1)
	# 		game_running = True

	# 	file.close()

	# except (OSError, PermissionError):
	# 	print("Could not open")


	with open(file_path, "r") as file:
		data = file.readlines()
		print(data)

		if data[0] != "N/A\n" and not shape_written:
			client.publish("ece180d/team8/unity", data[0], qos=1)
			shape_written = True
			game_running = True
		elif data[0] == "N/A\n":
			client.publish("ece180d/team8/unity", "stop", qos=1)
			shape_written = False
			game_running = False

		if data[2] == "True\n":
			client.publish("ece180d/team8/unity", "stop", qos=1)
			game_running = False
		elif data[2] == "False\n":
			client.publish("ece180d/team8/unity", "start", qos=1)
			game_running = True

	time.sleep(loop_delay)


client.loop_stop()
client.disconnect()