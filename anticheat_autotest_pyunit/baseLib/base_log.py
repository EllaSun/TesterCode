# coding:GBK
import os, re, time
import string
import commands

"""日志类"""
class Log:
	def __init__(self, path_, log_, head=0, tail=0):
		self.path = path_
		self.log = log_
		if log_.find('\\') != -1:
			self.log += str(path_)
		self.log_head = head
		self.log_tail = tail

	def __getSection__(self, all = True):
		'''确定本次读取日志的范围'''
		cmd = "cat " + self.log + "*|wc -l"
		log_tail_n = os.popen(cmd).read()
		if log_tail_n != self.log_tail:
			self.log_head = self.log_tail
			self.log_tail = log_tail_n
		elif log_tail_n == self.log_tail and all == False:
			return 0
		return int(self.log_tail) - int(self.log_head)

	def find(self, key1="", key2=""):
		'''包含字符串key1和key2的行数'''
		t = self.__getSection__()
		if self.log_head == 0:
			cmd = "cat " + self.log + "* | grep \"" + str(key1) + "\" | grep \"" + str(key2) + "\" | wc -l"
		else:
			cmd = "cat " + self.log + "* | tail -" + str(t) + " | grep \"" + str(key1) + "\" | grep \"" + str(key2) + "\" | wc -l"
		res = os.popen(cmd).read()
		return int(res)

	def getAll(self, key="", all = True):
		'''返回日志某行某列的数据'''
		t = self.__getSection__(all = all)
		#读取范围内的日志
		if self.log_head == 0 and t != 0:
			cmd = "cat " + self.log + "* | grep \"" + key + "\""
		else:
			cmd = "cat " + self.log + "* | tail -" + str(t) + " | grep \"" + key + "\""
		content = os.popen(cmd).read()
		return content

	def getLine(self, line=None, key="", all = True):
		content = self.getAll(key, all = all)
		lines = content.split('\n')
		if line == None:
			return lines 
		return lines[int(line)]

	def get(self, line=None, column=None, key="", seg="\t"):
		lines = self.getLine(line, key)
		if line == None:
			res = []
			for l in range(len(lines) - 1):
				value = lines[l].split(seg)[column]
				res.append(str(value))
		else:
			res = str(lines.split(seg)[column])
		return res

	def getMid(self, line=None, column=None, key="", begin="[", end="]"):
		lines = self.getLine(line, key)
		if line == None:
			res = []
			l = 0
			for l in range(len(lines) - 1):
				value = (lines[l].split(begin)[column]).split(end)[0]
				res.append(str(value))
		else:
			res = str((lines[line].split(begin)[column]).split(end)[0])
		return res

	def getBit(self, content, begin, end):
		if content == None:
			return 0
		rr = int(content) & int(2**(int(end)+1)-2**(int(begin)))
		return rr>>int(begin)

	def rmAll(self):
		'''清空全部日志'''
		cmd = "rm " + self.path + "/* -rf"  
		res = os.popen(cmd).read()
	
	def getLineNum(self):
		cmd = "cat " + self.log + "*|wc -l"
		status, data = commands.getstatusoutput(cmd)
		return data
