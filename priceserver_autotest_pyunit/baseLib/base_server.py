# coding:GBK
import os,time

class Server:
	def __init__(self,start_script,app_name):
		self.start_script = start_script
		self.app_name = app_name

	def start(self):
		'''启动Server'''
		if self.start_script == None:
			return -1
		os.system(self.start_script)
		pid = os.popen("pidof " + self.app_name).readline().strip('\n')
		n = 0
		while self.getAppStatus().find("S")<0:
			time.sleep(1)
			if n > 30:
				return -1
			n += 1
		time.sleep(0.3)
		return pid

	def stop(self):
		'''停止Server'''
		os.system("killall -9 -q " + self.app_name)
		pid = os.popen("pidof " + self.app_name).readline().strip('\n')
		if pid == None or pid == "":
			return 1
		else:
			return -1

	def getAppStatus(self):
		'''获取应用程序运行状态'''
		cmd = "ps aux|grep " + self.app_name + "|grep -v grep |awk '{print $8}'"
		return os.popen(cmd).readline()

