# coding:GBK
import os,re,time
import string

# 数据文件类
class Data:
	def __init__(self):
		pass

	def append(self, file, content):
		'''追加数据文件'''
		file = open(file, 'a')
		file.write(content + "\n")
		file.close()

	def write(self, file, content):
		'''写数据文件'''
		file = open(file, 'w')
		file.write(content + "\n")
		file.close()

	def read(self, file):
		'''读数据文件'''
		file = open(file, 'r')
		content = file.read().strip('\n').split('\n')
		file.close()
		return content

	def readAll(self, file):
		'''读数据文件'''
		file = open(file, 'r')
		content = file.read()
		file.close()
		return content

	def readLine(self, file, line):
		'''读数据文件中某一行'''
		line = self.read(file)[int(line)]
		return str(line)

	def clear(self, file):
		'''清空数据文件'''
		file = open(file, 'w')
		file.write('')
		file.close()

	def clearFiles(self, files):
		'''批量清空指定数据文件'''
		for f in files:
			self.clear(f)
	
	def clearBatch(self, dir, files=None):
		'''批量清空指定目录下数据文件'''
		if files == None:
			files = os.listdir(str(dir))
		for f in files:
			file = str(dir) + "/" + str(f)
			self.clear(file)

	def rm(self, file):
		'''删除数据文件'''
		os.remove(file)
