import paho.mqtt.client as mqtt

# motionClassifierTest.py
# Simple classifier to differentiate between forward push (Y) and upward lift (Z)

import sys
import time
import math
import IMU
import datetime
import os
import numpy as np
import csv

a = datetime.datetime.now()

IMU.detectIMU()	 #Detect if BerryIMU is connected.
if(IMU.BerryIMUversion == 99):
	print(" No BerryIMU found... exiting ")
	sys.exit()
IMU.initIMU()	   #Initialise the accelerometer, gyroscope and compass


axs = []
ays = []
azs = []
gxs = []
gys = []
gzs = []

# Thresholds
# Vertical
z_th_up = 6000
z_th_down = 3000

# Side to side
x_th_right = 2000
x_th_left = -2500

# Front back (likely not used)
y_th_front = 2000
y_th_back = -2000

movements = []
motion_detected = False
cooldown = 0.6

t = 0

shape = "square"	 # default

# MQTT

# 0. define callbacks - functions that run when events happen
# The callback for when teh client receives a CONNACK respose from the server
def on_connect(client, userdata, flags, rc):
	print("Connection returned result: " + str(rc))

	# Subscribing in on_connect() means that if we lose the connection and
	# reconnect then subscriptions will be renewed
	client.subscribe("ece180d/team8/unity", qos = 1)

# The callback of the client when it disconnects.
def on_disconnect(client, userdata, rc):
	if rc != 0:
		print('Unexpected Disconnect')
	else:
		print('Expected Disconnect')

# The default message callback.
# (won't be used if only publishing, but can still exist)
def on_message(client, userdata, message):
	#print('Received message: "' + str(message.payload) + '" on topic "' +
	#	message.topic + '" with QoS ' + str(message.qos))
	if message.payload


client = mqtt.Client()

client.on_connect = on_connect
client.on_disconnect = on_disconnect
client.on_message = on_message

client.connect_async('mqtt.eclipseprojects.io')
client.loop_start()


# Cycle
# one minute: 1200
while t < 1200:

	#Read the accelerometer,gyroscope and magnetometer values
	ACCx = IMU.readACCx()
	ACCy = IMU.readACCy()
	ACCz = IMU.readACCz()
	GYRx = IMU.readGYRx()
	GYRy = IMU.readGYRy()
	GYRz = IMU.readGYRz()
	MAGx = IMU.readMAGx()
	MAGy = IMU.readMAGy()
	MAGz = IMU.readMAGz()

	#axs.append(ACCx)
	#ays.append(ACCy)
	#azs.append(ACCz)
	#gxs.append(GYRx)
	#gys.append(GYRy)
	#gzs.append(GYRz)

	# Vertical classification
	if ACCz > z_th_up:
		#print("Up!")
		movements.append("U")
		time.sleep(cooldown)
	elif ACCz < z_th_down:
		#print("Down!")
		movements.append("D")
		time.sleep(cooldown)

	# Left right classification
	elif ACCx > x_th_right:
		#print("Right!")
		movements.append("R")
		time.sleep(cooldown)
	elif ACCx < x_th_left:
		#print("Left!")
		movements.append("L")
		time.sleep(cooldown)

	# Front back classification
	elif ACCy > y_th_front:
		#print("Forward!")
		movements.append("F")
		time.sleep(cooldown)
	elif ACCy < y_th_back:
		#print("Back!")
		movements.append("B")
		time.sleep(cooldown)

	# Square recognition (RDLU)
	pure_square = ["R","D","L","U"]
	shape = movements[-4:]

	if pure_square == shape:
		print("\tPure Square!")
		client.publish('ece180d/team8/motion', str("square"), qos=1)
	

	#slow program down a bit, makes the output more readable
	time.sleep(0.05)
	t += 1

client.loop_stop()
client.disconnect()