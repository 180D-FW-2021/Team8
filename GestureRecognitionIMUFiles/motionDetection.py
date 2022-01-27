import paho.mqtt.client as mqtt

# motionClassifierTest.py
# Simple classifier to differentiate between forward push (Y) and upward lift (Z)

# Accelerometer calibration code taken from: https://makersportal.com/blog/calibration-of-an-inertial-measurement-unit-imu-with-raspberry-pi-part-ii

import sys, signal
import time
import math
import IMU
import datetime
import os
import csv
import numpy as np
from scipy.optimize import curve_fit

# Accelerometer calibration functions

def accel_fit(x_input,m_x,b):
	return (m_x*x_input)+b # fit equation for accel calibration

def get_accel():
	ax = IMU.readACCx()
	ay = IMU.readACCy()
	az = IMU.readACCz()
	return ax,ay,az

cal_size = 1000

def accel_cal():
	print("-"*50)
	print("Accelerometer Calibration")
	mpu_offsets = [[],[],[]] # offset array to be printed
	axis_vec = ['z','y','x'] # axis labels
	cal_directions = ["upward","downward","perpendicular to gravity"] # direction for IMU cal
	cal_indices = [2,1,0] # axis indices
	for qq,ax_qq in enumerate(axis_vec):
		ax_offsets = [[],[],[]]
		print("-"*50)
		for direc_ii,direc in enumerate(cal_directions):
			#input("-"*8+" Press Enter and Keep IMU Steady to Calibrate the Accelerometer with the -"+\
			#  ax_qq+"-axis pointed "+direc)
			[IMU.readACCx() for ii in range(0,cal_size)] # clear buffer between readings
			mpu_array = []
			while len(mpu_array)<cal_size:
				try:
					ax,ay,az = get_accel()
					mpu_array.append([ax,ay,az]) # append to array
				except:
					continue
			ax_offsets[direc_ii] = np.array(mpu_array)[:,cal_indices[qq]] # offsets for direction

		# Use three calibrations (+1g, -1g, 0g) for linear fit
		popts,_ = curve_fit(accel_fit,np.append(np.append(ax_offsets[0],
								ax_offsets[1]),ax_offsets[2]),
								np.append(np.append(1.0*np.ones(np.shape(ax_offsets[0])),
								-1.0*np.ones(np.shape(ax_offsets[1]))),
								0.0*np.ones(np.shape(ax_offsets[2]))),
								maxfev=10000)
		mpu_offsets[cal_indices[qq]] = popts # place slope and intercept in offset array
	print('Accelerometer Calibrations Complete')
	return mpu_offsets



IMU.detectIMU()	 #Detect if BerryIMU is connected.
if(IMU.BerryIMUversion == 99):
	print(" No BerryIMU found... exiting ")
	sys.exit()
IMU.initIMU()	   #Initialise the accelerometer, gyroscope and compass
accel_coeffs = accel_cal()

print(accel_coeffs)

axs = []
ays = []
azs = []
gxs = []
gys = []
gzs = []

accel_coeffs = []

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

# Shape classification
pure_square = ["R","D","L","U"]

movements = []
motion_detected = False
cooldown = 0.5

t = 0

detect_shape = "square"	 # default
shape_stage = 0

game_running = True		# False
is_calibrating = False

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
	global game_running
	print('Received message: "' + str(message.payload) + '" on topic "' +
		message.topic + '" with QoS ' + str(message.qos))

	# Told to stop or no shape to detect
	if "stop" in str(message.payload):
		game_running = False
	elif "start" in str(message.payload):
		print("Start detected")
		if game_running is False:
			shape_stage = 0
		game_running = True
	elif "square" in str(message.payload):
		detect_shape = "square"

print("About to connect...")

client = mqtt.Client()

client.on_connect = on_connect
client.on_disconnect = on_disconnect
client.on_message = on_message

client.connect_async('mqtt.eclipseprojects.io')
client.loop_start()

print("Connected")

client.publish('ece180d/team8/imu', "Test message", qos=1)



# Cycle
# one minute: 1200
while True:

	if is_calibrating:
		accel_coeffs = accel_cal()
	
	if game_running:
		#Read the accelerometer,gyroscope and magnetometer values
		ACCx = IMU.readACCx()
		ACCy = IMU.readACCy()
		ACCz = IMU.readACCz()

		# calibrated values
		cal_x = accel_fit(ACCx, accel_coeffs[0][0], accel_coeffs[0][1])
		cal_y = accel_fit(ACCy, accel_coeffs[1][0], accel_coeffs[1][1])
		cal_y = accel_fit(ACCz, accel_coeffs[2][0], accel_coeffs[2][1])

		# print calibrated values
		print(str(cal_x) + "\t" + str(cal_y) + "\t" + str(cal_z))

		#GYRx = IMU.readGYRx()
		#GYRy = IMU.readGYRy()
		#GYRz = IMU.readGYRz()
		#MAGx = IMU.readMAGx()
		#MAGy = IMU.readMAGy()
		#MAGz = IMU.readMAGz()
		move = ""

		# Vertical classification
		if ACCz > z_th_up:
			print("U")
			movements.append("U")
			move = "U"
			time.sleep(cooldown)
		elif ACCz < z_th_down:
			print("D")
			movements.append("D")
			move = "D"
			time.sleep(cooldown)

		# Left right classification
		elif ACCx > x_th_right:
			print("R")
			movements.append("R")
			move = "R"
			time.sleep(cooldown)
		elif ACCx < x_th_left:
			print("L")
			movements.append("L")
			move = "L"
			time.sleep(cooldown)

		# Front back classification
		# elif ACCy > y_th_front:
			# print("Forward!")
			# movements.append("F")
			# time.sleep(cooldown)
		# elif ACCy < y_th_back:
			# print("Back!")
			# movements.append("B")
			# time.sleep(cooldown)

		if move == pure_square[shape_stage]:
			print("Square motion found")
			shape_stage = shape_stage + 1
			client.publish('ece180d/team8/imu', shape_stage, qos=1)

		# if pure_square == shape and detect_shape == "square":
			# print("\tPure Square!")
			# client.publish('ece180d/team8/imu', "True", qos=1)
		

		#slow program down a bit, makes the output more readable
		time.sleep(0.05)
	if shape_stage > 3:
		shape_stage = 0

client.loop_stop()
client.disconnect()