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
#import numpy as np
#from scipy.optimize import curve_fit

RAD_TO_DEG = 57.29578
M_PI = 3.14159265358979323846
G_GAIN = 0.070  # [deg/s/LSB]  If you change the dps for gyro, you need to update this value accordingly
AA =  0.40      # Complementary filter constant


def kalmanFilterY ( accAngle, gyroRate, DT):
    y=0.0
    S=0.0

    global KFangleY
    global Q_angle
    global Q_gyro
    global y_bias
    global YP_00
    global YP_01
    global YP_10
    global YP_11

    KFangleY = KFangleY + DT * (gyroRate - y_bias)

    YP_00 = YP_00 + ( - DT * (YP_10 + YP_01) + Q_angle * DT )
    YP_01 = YP_01 + ( - DT * YP_11 )
    YP_10 = YP_10 + ( - DT * YP_11 )
    YP_11 = YP_11 + ( + Q_gyro * DT )

    y = accAngle - KFangleY
    S = YP_00 + R_angle
    K_0 = YP_00 / S
    K_1 = YP_10 / S

    KFangleY = KFangleY + ( K_0 * y )
    y_bias = y_bias + ( K_1 * y )

    YP_00 = YP_00 - ( K_0 * YP_00 )
    YP_01 = YP_01 - ( K_0 * YP_01 )
    YP_10 = YP_10 - ( K_1 * YP_00 )
    YP_11 = YP_11 - ( K_1 * YP_01 )

    return KFangleY

def kalmanFilterX ( accAngle, gyroRate, DT):
    x=0.0
    S=0.0

    global KFangleX
    global Q_angle
    global Q_gyro
    global x_bias
    global XP_00
    global XP_01
    global XP_10
    global XP_11


    KFangleX = KFangleX + DT * (gyroRate - x_bias)

    XP_00 = XP_00 + ( - DT * (XP_10 + XP_01) + Q_angle * DT )
    XP_01 = XP_01 + ( - DT * XP_11 )
    XP_10 = XP_10 + ( - DT * XP_11 )
    XP_11 = XP_11 + ( + Q_gyro * DT )

    x = accAngle - KFangleX
    S = XP_00 + R_angle
    K_0 = XP_00 / S
    K_1 = XP_10 / S

    KFangleX = KFangleX + ( K_0 * x )
    x_bias = x_bias + ( K_1 * x )

    XP_00 = XP_00 - ( K_0 * XP_00 )
    XP_01 = XP_01 - ( K_0 * XP_01 )
    XP_10 = XP_10 - ( K_1 * XP_00 )
    XP_11 = XP_11 - ( K_1 * XP_01 )

    return KFangleX

# Berry IMU setup
IMU.detectIMU()	 #Detect if BerryIMU is connected.
if(IMU.BerryIMUversion == 99):
	print(" No BerryIMU found... exiting ")
	sys.exit()
IMU.initIMU()	   #Initialise the accelerometer, gyroscope and compass

# Thresholds
# Vertical
z_th_up = 3.5
z_th_down = -3.7

# Side to side
x_th_right = 2.8
x_th_left = -2.8

xa_th_front = 20
xa_th_back = -20

# Front back (likely not used)
y_th_front = 4
y_th_back = -4

ya_th_right = 20
ya_th_left = -20

#Kalman filter variables
Q_angle = 0.02
Q_gyro = 0.0015
R_angle = 0.005
y_bias = 0.0
x_bias = 0.0
XP_00 = 0.0
XP_01 = 0.0
XP_10 = 0.0
XP_11 = 0.0
YP_00 = 0.0
YP_01 = 0.0
YP_10 = 0.0
YP_11 = 0.0
KFangleX = 0.0
KFangleY = 0.0

magXmin =  0
magYmin =  0
magZmin =  0
magXmax =  0
magYmax =  0
magZmax =  0

# Shape classification
pure_square = ["L","D","R","U"]

motion_detected = False
cooldown = 0.5
move = ""

t = 0

detect_shape = "square"	 # default
shape_stage = 0

game_running = True		# False
is_calibrating = False

gyroXangle = 0.0
gyroYangle = 0.0
gyroZangle = 0.0
CFangleX = 0.0
CFangleY = 0.0
kalmanX = 0.0
kalmanY = 0.0

x_vals = []
y_vals = []
z_vals = []

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

client = mqtt.Client("rpi", True, None, mqtt.MQTTv31)

client.on_connect = on_connect
client.on_disconnect = on_disconnect
client.on_message = on_message

client.connect_async('test.mosquitto.org', 1883, 60)
client.loop_start()

print("Connected")

a = datetime.datetime.now()

# Cycle
# one minute: 1200
x = 0
while x < 1200:
	x = x + 1
	
	#if game_running:
	if game_running:
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

		#Apply compass calibration
		MAGx -= (magXmin + magXmax) /2
		MAGy -= (magYmin + magYmax) /2
		MAGz -= (magZmin + magZmax) /2

		##Calculate loop Period(LP). How long between Gyro Reads
		b = datetime.datetime.now() - a
		a = datetime.datetime.now()
		LP = b.microseconds/(1000000*1.0)
		outputString = "Loop Time %5.2f " % ( LP )

		#Convert Gyro raw to degrees per second
		rate_gyr_x =  GYRx * G_GAIN
		rate_gyr_y =  GYRy * G_GAIN
		rate_gyr_z =  GYRz * G_GAIN

		#Calculate the angles from the gyro.
		gyroXangle += rate_gyr_x*LP
		gyroYangle += rate_gyr_y*LP
		gyroZangle += rate_gyr_z*LP

		#Convert Accelerometer values to degrees
		AccXangle =  (math.atan2(ACCy,ACCz)*RAD_TO_DEG)
		AccYangle =  (math.atan2(ACCz,ACCx)+M_PI)*RAD_TO_DEG

		#convert the values to -180 and +180
		if AccYangle > 90:
			AccYangle -= 270.0
		else:
			AccYangle += 90.0

		#Complementary filter used to combine the accelerometer and gyro values.
		CFangleX = AA*(CFangleX+rate_gyr_x*LP) +(1 - AA) * AccXangle
		CFangleY = AA*(CFangleY+rate_gyr_y*LP) +(1 - AA) * AccYangle

		#Kalman filter used to combine the accelerometer and gyro values.
		kalmanY = kalmanFilterY(AccYangle, rate_gyr_y,LP)
		kalmanX = kalmanFilterX(AccXangle, rate_gyr_x,LP)


		#Calculate heading
		heading = 180 * math.atan2(MAGy,MAGx)/M_PI

		#Only have our heading between 0 and 360
		if heading < 0:
			heading += 360

		###################Tilt compensated heading#########################
		#Normalize accelerometer raw values.
		accXnorm = ACCx/math.sqrt(ACCx * ACCx + ACCy * ACCy + ACCz * ACCz)
		accYnorm = ACCy/math.sqrt(ACCx * ACCx + ACCy * ACCy + ACCz * ACCz)

		#Calculate pitch and roll
		pitch = math.asin(accXnorm)
		roll = -math.asin(accYnorm/math.cos(pitch))

		#Calculate the new tilt compensated values
		#The compass and accelerometer are orientated differently on the the BerryIMUv1, v2 and v3.
		#This needs to be taken into consideration when performing the calculations

		#X compensation
		if(IMU.BerryIMUversion == 1 or IMU.BerryIMUversion == 3):			#LSM9DS0 and (LSM6DSL & LIS2MDL)
			magXcomp = MAGx*math.cos(pitch)+MAGz*math.sin(pitch)
		else:																#LSM9DS1
			magXcomp = MAGx*math.cos(pitch)-MAGz*math.sin(pitch)

		#Y compensation
		if(IMU.BerryIMUversion == 1 or IMU.BerryIMUversion == 3):			#LSM9DS0 and (LSM6DSL & LIS2MDL)
			magYcomp = MAGx*math.sin(roll)*math.sin(pitch)+MAGy*math.cos(roll)-MAGz*math.sin(roll)*math.cos(pitch)
		else:                                                                #LSM9DS1
			magYcomp = MAGx*math.sin(roll)*math.sin(pitch)+MAGy*math.cos(roll)+MAGz*math.sin(roll)*math.cos(pitch)

		#Calculate tilt compensated heading
		tiltCompensatedHeading = 180 * math.atan2(magYcomp,magXcomp)/M_PI

		if tiltCompensatedHeading < 0:
			tiltCompensatedHeading += 360

		##################### END Tilt Compensation ########################

		# Left right classification
		if kalmanX > xa_th_front:
			print("D")
			move = "D"
			time.sleep(cooldown)
		elif kalmanX < xa_th_back:
			print("U")
			move = "U"
			time.sleep(cooldown)

		#if move == pure_square[shape_stage]:
		#	print("Square motion found")
		#	shape_stage = shape_stage + 1
		#	client.publish('ece180d/team8/imu', shape_stage, qos=1)

		# Vertical classification (invert up and down)
		if kalmanY > ya_th_right:
			print("R")
			move = "R"
			time.sleep(cooldown)
		elif kalmanY < ya_th_left:
			print("L")
			move = "L"
			time.sleep(cooldown)

		#if move == pure_square[shape_stage]:
		#	print("Square motion found")
		#	shape_stage = shape_stage + 1
		#	client.publish('ece180d/team8/imu', shape_stage, qos=1)

		# slow program down a bit, makes the output more readable
		time.sleep(0.05)
	if shape_stage > 3:
		shape_stage = 0

client.loop_stop()
client.disconnect()
