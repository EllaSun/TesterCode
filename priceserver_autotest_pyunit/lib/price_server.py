# coding:GBK
import os, re, time
from baseLib import base_server
from baseLib import base_conf
from baseLib import base_sys
from lib import price_init
from lib import price_log
from lib import price_data

class PriceServer:
	def __init__(self):
		self.init = price_init.priceserver_init
		self.server = base_server.Server(self.init.start_script, self.init.app_name)
		self.conf = base_conf.Config("PriceServer", self.init.server_cfg, self.init.server_cfg_bak)
		self.log = price_log.PriceLog()
		self.data = price_data.PriceData()
		self.recover()

	def recover(self):
		'''停止Server'''
		self.server.stop()
		# 恢复配置文件
		self.conf.recover()
		# 删除日志
		self.log.rmAll()
		# 清空数据文件
		dataList = [
			'data_file',
		]
		self.data.clear_price_files(dataList)

	def tearDown(self):
		'''停止Server'''
		lm_err = ["LM_ERROR", "LM_CRTIC"]
		for lm in lm_err:
			if(int(self.log.find_error_log(key = lm)) > 0):
				print "[!]Check PriceServer error_log, there is \"", lm, "\"."
		#self.server.stop()
		#恢复配置文件
		#self.conf.recover()

	def start(self):
		'''启动Server'''
		if self.server.start() < 0:
			return -2
		isok = 1
		lm_err = ["LM_ERROR", "LM_CRTIC"]
		for lm in lm_err:
			if(int(self.log.find_error_log(key = lm)) > 0):
				isok = -2
				print "[!]Check PriceServer error_log, there is \"", lm, "\"."
		time.sleep(0.8)
		return isok

	def reload(self, act):
		'''执行reload'''
		ip = base_sys.getLocalIP()
		port = self.conf.get("http_server_port")
		dir = self.init.getOpt("ext", "autotmp_dir")
		out = self.init.getOpt("ext", "reload_log")
		out = dir + "/" + out
		url = "http://" + ip + ":" + port
		url += "/reload?act="
		url += str(act)
		cmd = 'wget -q ' +  '"' + url + '" -O ' + out
		#print 'x'*3,cmd
		os.system(cmd)

	def query(self, req_param = {}):
		'''执行query'''
		ip = base_sys.getLocalIP()
		port = self.conf.get("http_server_port")
		dir = self.init.getOpt("ext", "autotmp_dir")
		out = self.init.getOpt("ext", "query_log")
		out = dir + "/" + out
		req = "http://" + ip + ":" + port
		req += "/query?"
		i = 0
		for k in req_param.keys():
			if i != 0:
				req += "&"
			i = 1
			req += k + "=" + str(req_param[k])
		cmd = 'wget -q ' +  '"' + req + '" -O ' + out
		#print '*'*3,cmd
		os.system(cmd)

	def parse_response(self, rsp):
		pass


