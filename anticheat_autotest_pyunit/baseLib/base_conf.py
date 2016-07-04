# coding:GBK
import os,re,time
import string
import ConfigParser
import commands
import base_data

# �����ļ�
class Config:
	def __init__(self, section, cfg, cfg_bak = None, mode = 1):
		self.cf = ConfigParser.ConfigParser()
		self.section = section
		self.cfg = cfg
		self.cfg_bak = cfg_bak
		self.mode = mode
		self.cf.read(self.cfg)
		self.data = base_data.Data()

	def set(self, item, value, isStrip = 1):
		'''����������'''
		if self.mode == 1:
			item = "\"" + item + "\""
			value = "\"" + value + "\""
		self.cf.set(self.section, item, value)
		self.cf.write(open(self.cfg,"w"))
		if isStrip == 1:
			self.strip()

	def get(self, item):
		'''��ȡ������'''
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
		'''ɾ��������'''
		if self.mode == 1:
			item = "\"" + item + "\""
		try:
			self.cf.remove_option(self.section, item)
			self.cf.write(open(self.cfg,"w"))
			if isStrip == 1:
				self.strip()
		except:
			print "Error"

	def recover(self):
		'''�ָ������ļ�'''
		cmd = '\cp -f ' + self.cfg_bak + ' ' + self.cfg
		#os.system(cmd)
		status, data = commands.getstatusoutput(cmd)
		self.cf.read(self.cfg)

	def strip(self):
		'''ȥ��������Ⱥ����ҿո�'''
		'''ע��ģ���Զ���ӿո�server��ȡʧ��'''
		lines = self.data.read(self.cfg)
		newLines = []
		for l in lines:
			l = l.replace(' ','')
			newLines.append(l)
		self.data.clear(self.cfg)
		for nl in newLines:
			self.data.append(self.cfg, nl)

