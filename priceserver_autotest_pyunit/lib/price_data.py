# coding:GBK
import os
import struct
from baseLib import base_data
from baseLib import base_conf
from baseLib import base_sys
from lib import price_init

class PriceData(base_data.Data):

	def __init__(self):
		base_data.Data.__init__(self)
		self.init = price_init.priceserver_init
		self.conf = base_conf.Config("PriceServer", self.init.server_cfg, self.init.server_cfg_bak)

	def clear_price_files(self, fileList):
		for f in fileList:
			file = self.conf.get(f)
			self.clear(file)

	def set_white_ip_file(self, ip = None):
		'''设置ip白名单'''
		file = self.conf.get("white_ip_file")
		if ip == None:
			ip = base_sys.getLocalIP()
			content = str(ip)
		self.append(file,content)

	def clear_white_ip_file(self):
		'''清空ip白名单'''
		file = self.conf.get("white_ip_file")
		self.clear(file)

	def rm_white_ip_file(self):
		'''删除ip白名单'''
		file = self.conf.get("white_ip_file")
		self.rm(file)

	def set_data_file(self, data = ""):
		'''设置数据文件'''
		file = self.conf.get("data_file")
		content = str(data)
		self.append(file,content)

	def clear_data_file(self):
		'''清空数据文件'''
		file = self.conf.get("data_file")
		self.clear(file)

	def rm_data_file(self):
		'''删除数据文件'''
		file = self.conf.get("data_file")
		self.rm(file)

