import socket
import random
import time


def socket_init(port = 13000, host_ip = '127.0.0.1'):
	client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
	client.connect((host_ip, port))
	return client

def socket_send(data, client):
	# sends data over the specified port and ip (should usually leave default)
	# assumes that the input data has already been formatted as a string
	client.send(data.encode('ascii'))

def socket_close(client):
	client.close()

# testing

# testCli = socket_init()
# i = 0
# while i < 9: # 30 seconds running max
	# a = random.random()
	# a = '%.2f' % a
	# socket_send(a, testCli)
	# print('sent message:' + a)
	# i += 1
	# time.sleep(1)

# #from_server = client.recv(4096)
# socket_close(testCli)
# print('Done!')
# #print(from_server.decode('ascii'))

