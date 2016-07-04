# coding:GBK
from baseLib import base_log
from baseLib import base_conf
from baseLib import *
from lib import price_init

class PriceLog(base_log.Log):
	def __init__(self):
		self.init = price_init.priceserver_init
		base_log.Log.__init__(self, self.init.log_dir, '*')
		self.conf = base_conf.Config("AdLog", self.init.adlog_cfg)
		# error_log
		log = self.conf.get("log.lvl.all")
		self.error = base_log.Log(self.init.log_dir, log)
		# access_log
		log = self.conf.get("log.access")
		self.access = base_log.Log(self.init.log_dir, log)
		self.accessRecvDict = {}
		self.accessRetDict = {}
		self._set_access_log()
		# ie_log
		log = self.conf.get("log.ie")
		self.ie = base_log.Log(self.init.log_dir, log)
		self.ieDict = {}
		self._set_ie_log()
		# stat_log
		log = self.conf.get("log.stat")
		self.stat = base_log.Log(self.init.log_dir, log)
		self.statDict = {}
		self._set_stat_log()

	def find_error_log(self, key="", key2=""):
		'''查找error日志中某关键词的出现行数'''
		return self.error.find(key1=key, key2=key2)

	def _set_access_log(self):
		'''设置access_log 日志内容与列的对应关系'''
		#'''recv'''
		self.accessRecvDict['url'] = 3
		#'''ret'''
		self.accessRetDict['cache'] = 6
		self.accessRetDict['ori_filt'] = 8
		self.accessRetDict['extend_filt'] = 9
		self.accessRetDict['ad_num'] = 10
		self.accessRetDict['free'] = 11
		self.accessRetDict['shield'] = 12
		self.accessRetDict['black'] = 13
		self.accessRetDict['grd_shield'] = 14
		self.accessRetDict['ext_server'] = 15
		self.accessRetDict['key'] = 16

	def get_access_log(self, item, type="ret"):
		'''获取access_log指定数据'''
		if str(type) == "ret":
			res = self.access.getMid(column = int(self.accessRetDict[item]), key="Weblogic Ret", begin="[", end="]")
		elif str(type) == "recv":
			res = self.access.getMid(column = int(self.accessRecvDict[item]), key="RequestReceiver Recv", begin="[", end="]")
		return res
	
	def _set_ie_log(self):
		'''设置ie_log 日志内容与列的对应关系'''
		self.ieDict['uip'] = 1
		self.ieDict['pid'] = 2
		self.ieDict['suid'] = 3
		self.ieDict['gid'] = 4
		self.ieDict['adid'] = 5
		self.ieDict['acid'] = 6
		self.ieDict['flag'] = 7
		self.ieDict['reserved'] = 8
		self.ieDict['key'] = 9
		self.ieDict['cost'] = 10
		self.ieDict['refer'] = 11
		self.ieDict['query'] = 12
		self.ieDict['fip'] = 13
		self.ieDict['uuid'] = 14
		self.ieDict['cache'] = 15
		self.ieDict['query_reserved'] = 16
		self.ieDict['pvid'] = 17
		self.ieDict['weight'] = 18
		self.ieDict['price'] = 19
		self.ieDict['extend_key'] = 20
		self.ieDict['gg_up'] = 21
		self.ieDict['gg_down'] = 22
		self.ieDict['gg_right'] = 23
		self.ieDict['gg_tile'] = 24
		self.ieDict['gg_cost'] = 25
		self.ieDict['p'] = 26
		self.ieDict['w'] = 27
		self.ieDict['rel_weight'] = 28
		self.ieDict['service_type'] = 29
		self.ieDict['ctr'] = 30
		self.ieDict['extend_reserved'] = 31
		self.ieDict['ccid'] = 32
		self.ieDict['showprice'] = 33
		self.ieDict['quality'] = 34

	def get_ie_log(self, item, line = 0):
		'''获取ie_log指定数据'''
		res = self.ie.get(line = line, column = int(self.ieDict[item]))
		return res

	def _set_stat_log(self):
		'''设置stat_log 日志内容与列的对应关系'''
		self.statDict['req_num'] = 2
		self.statDict['timeout_num'] = 4
		self.statDict['sohu_num'] = 6
		self.statDict['sogou_num'] = 7
		self.statDict['bd_num'] = 8
		self.statDict['xml_num'] = 9
		self.statDict['normal_timeout_num'] = 22
		self.statDict['instant_timeout_num'] = 23
		self.statDict['gg_timeout_num'] = 24
		self.statDict['qc_timeout_num'] = 25
		self.statDict['user_timeout_num'] = 26
		self.statDict['left_timeout_num'] = 27

	def getStatLog(self, item, line = None):
		'''获取stat_log指定数据'''
		res = self.stat.get(line = line, column = int(self.statDict[item]))
		return res

	def getStatLogSum(self, item, line = None):
		'''获取stat_log指定数据和'''
		res = self.getStatLog(item = item, line = line)
		sum = 0
		for l in range(len(res)):
			sum += int(res[l])
		return sum
