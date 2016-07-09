import os,time,sys
import string
from baseLib import base_init

class Server:
	def __init__(self,start_script,app_name):
		self.start_script = start_script
		self.app_name = app_name

	def start(self):
		'''start server'''
		if self.start_script == None:
			return -1
		os.system(self.start_script)
		pid = os.popen("pidof " + self.app_name).readline().strip('\n')
		n = 0
		while self.getAppStatus().find("S")<0:
			time.sleep(2)
			if n>3:
				return -1
			n+=1
		time.sleep(2)
		return pid

	def stop(self):
		'''stop server'''
		os.system("killall -9 -q " + self.app_name)
		pid = os.popen("pidof " + self.app_name).readline().strip('\n')
		if pid == None or pid == "":
			return 1
		else:
			return -1

	def getAppStatus(self):
		'''get application runing status'''
		cmd = "ps aux|grep " + self.app_name + "|grep -v grep |awk '{print $8}'"
		return os.popen(cmd).readline()

