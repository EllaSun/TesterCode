# coding:GBK
import os,re,time
import string

# �����ļ���
class Data:
	def __init__(self):
		pass

	def append(self, file, content):
		'''׷�������ļ�'''
		file = open(file, 'a')
		file.write(content + "\n")
		file.close()

	def write(self, file, content):
		'''д�����ļ�'''
		file = open(file, 'w')
		file.write(content + "\n")
		file.close()

	def read(self, file):
		'''�������ļ�'''
		file = open(file, 'r')
		content = file.read().strip('\n').split('\n')
		file.close()
		return content

	def readAll(self, file):
		'''�������ļ�'''
		file = open(file, 'r')
		content = file.read()
		file.close()
		return content

	def readLine(self, file, line):
		'''�������ļ���ĳһ��'''
		line = self.read(file)[int(line)]
		return str(line)

	def clear(self, file):
		'''��������ļ�'''
		file = open(file, 'w')
		file.write('')
		file.close()

	def clearFiles(self, files):
		'''�������ָ�������ļ�'''
		for f in files:
			self.clear(f)
	
	def clearBatch(self, dir, files=None):
		'''�������ָ��Ŀ¼�������ļ�'''
		if files == None:
			files = os.listdir(str(dir))
		for f in files:
			file = str(dir) + "/" + str(f)
			self.clear(file)

	def rm(self, file):
		'''ɾ�������ļ�'''
		os.remove(file)
