# coding:GBK
import os,re
import ConfigParser
from baseLib import base_data

# 配置文件
class Config:
	def __init__(self, section, cfg, cfg_bak = None, mode = 1):
		self.cf = ConfigParser.ConfigParser()
		self.cfg = cfg
		self.cfg_bak = cfg_bak
		self.mode = mode
		if section != None:
			self.section = section
			self.cf.read(self.cfg)
		else:
			self.section = None
		self.data = base_data.Data()

	def set(self, item, value, isStrip=1):
		if self.section != None:
			self.setSection(item, value, isStrip)
		else:
			self.setWithoutSection(item, value)

	def setSection(self, item, value, isStrip):
		'''设置配置项'''
		if self.mode == 1:
			item = "\"" + item + "\""
			value = "\"" + value + "\""
		self.cf.set(self.section, item, value)
		self.cf.write(open(self.cfg,"w"))
		if isStrip == 1:
			self.strip()

	def setWithoutSection(self, item, value):
		f = file(self.cfg,'r')
		cfg = f.read()
		if self.mode == 1:
			cp = re.compile('"' + str(item) + '"=(.*)')
			cont = '"' + str(item) + '"=' + '"' + str(value) + '"'
		elif self.mode == 2:
			cp = re.compile(str(item) + '=(.*)')
			cont = str(item) + '=' + str(value) 
		fd = re.findall(cp,cfg)
		f.close()
		if fd:
			new = re.sub(cp,cont,cfg)
			f = file(self.cfg,'w')
			f.write(new)
		else:
			f = file(self.cfg,'a')
			cont = cont + "\n"
			f.write(cont)
		f.close()

	def get(self, item):
		'''获取配置项'''
		if self.mode == 1:
			item = "\"" + item + "\""
		try:
			res = self.cf.get(self.section, item)
		except:
			return None
		if self.mode == 1:
			return res.strip("\"")
		return str(res)

	def getKeys(self):
		res = self.cf.options(self.section)
		if self.mode == 1:
			return res.strip("\"")
		return res

	def remove(self, item, isStrip = 1):
		'''删除配置项'''
		if self.mode == 1:
			item = "\"" + item + "\""
		try:
			self.cf.remove_option(self.section, item)
			self.cf.write(open(self.cfg,"w"))
			self.cf.read(self.cfg)
			if isStrip == 1:
				self.strip()
		except:
			print "Error"

	def recover(self):
		'''恢复配置文件'''
		cmd = 'cp -f ' + self.cfg_bak + ' ' + self.cfg
		os.system(cmd)
		self.cf.read(self.cfg)

	def strip(self):
		'''去掉配置项等号左右空格,注：模块自动添加空格，server读取失败'''
		lines = self.data.read(self.cfg)
		newLines = []
		for l in lines:
			l = l.replace(' ','')
			newLines.append(l)
		self.data.clear(self.cfg)
		for nl in newLines:
			self.data.append(self.cfg, nl)

