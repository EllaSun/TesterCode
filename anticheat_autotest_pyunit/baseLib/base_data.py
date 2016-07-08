import os,re,time
import string

# Data
class Data:
	def __init__(self):
		pass

	def append(self, file, content):
		'''append data to a data file'''
		file = open(file, 'a')
		file.write(content + "\n")
		file.close()

	def write(self, file, content):
		'''write a data file'''
		file = open(file, 'w')
		file.write(content + "\n")
		file.close()

	def read(self, file):
		'''read a data file.'''
		'''return a list. Each item of the list is a line of the data file'''
		file = open(file, 'r')
		content = file.read().strip('\n').split('\n')
		file.close()
		return content

	def readAll(self, file):
	        '''read a data file.'''
	        '''return a string which include whole content of the data file with carriage return characters'''
		file = open(file, 'r')
		content = file.read()
		file.close()
		return content

	def readLine(self, file, line):
		'''get the specified line in the data file'''
		line = self.read(file)[int(line)]
		return str(line)

	def clear(self, file):
		''' clear the data file'''
		file = open(file, 'w')
		file.write('')
		file.close()

	def clearFiles(self, files):
		'''clear batch of data files'''
		for f in files:
			self.clear(f)
	
	def clearBatch(self, dir, files=None):
		'''clear batch of data files in specified directory'''
		if files == None:
			files = os.listdir(str(dir))
		for f in files:
			file = str(dir) + "/" + str(f)
			self.clear(file)

	def rm(self, file):
		'''delete a data file'''
		os.remove(file)
