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

t = 0
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
		print("Up!")
		movements.append("U")
		time.sleep(0.75)
	elif ACCz < z_th_down:
		print("Down!")
		movements.append("D")
		time.sleep(0.75)

	# Left right classification
	if ACCx > x_th_right:
		print("Right!")
		movements.append("R")
		time.sleep(0.75)
	elif ACCx < x_th_left:
		print("Left!")
		movements.append("L")
		time.sleep(0.75)

	# Front back classification
	if ACCy > y_th_front:
		print("Forward!")
		movements.append("F")
		time.sleep(0.75)
	elif ACCy < y_th_back:
		print("Back!")
		movements.append("B")
		time.sleep(0.75)

	# Square recognition (RDLU)
	pure_square = ["R","D","L","U"]
	shape = movements[-4:]

	if pure_square == shape:
		print("\tPure Square!")
	

	#slow program down a bit, makes the output more readable
	time.sleep(0.05)
	t += 1
print(movements)