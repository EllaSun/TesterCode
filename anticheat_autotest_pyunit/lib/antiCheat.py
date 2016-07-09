from baseLib import base_server
from baseLib import base_conf

from lib import antiCheatLog
from lib import antiCheatData
from lib import cheatingClient

import time,commands,os

antiCheatMap = {}
anticheatReLoadMap = {}

execfile("conf/antiCheat.cfg")

class antiCheat(base_server.Server):
	def __init__(self):
		base_server.Server.__init__(self,antiCheatMap['start'],'anti_cheat')
		self.add_conf=base_conf.Config("CheatingDeath",antiCheatMap['add_cfg'],antiCheatMap['add_cfg_bak'])
		self.op_conf=base_conf.Config("CheatingDeath",antiCheatMap['op_cfg'],antiCheatMap['op_cfg_bak'])
		self.adr_conf=base_conf.Config("CheatingDeath",antiCheatMap['adr_cfg'],antiCheatMap['adr_cfg_bak'])
		self.log =antiCheatLog.antiCheatLog()
		self.data=antiCheatData.antiCheatData()


	def recover(self):
		self.stop()
		#os.system("sh /search/autoTest/antiCheat/stop_anticheat_gcov.sh &")
		self.add_conf.recover()
		self.op_conf.recover()
		self.adr_conf.recover()
		self.log.rmAll()
		self.data.recover()
	
	def reload(self, type, subType=''):
		subStr = ''
		if subType != '':
			subStr = subStr + '=' + subType
		cmd = 'wget -q "' + anticheatReLoadMap[type] + subStr + '" -O p1.html '
		print cmd
        	os.system(cmd)
		time.sleep(0.2)
	
	def tearDown(self):
		lm_err = ["LM_ERROR", "LM_CRTIC"]
		for lm in lm_err:
			if(int(self.log.find_error_log(key = lm)) > 0):
				print "[!]Check antiCheat error_log, there is \"", lm, "\"."
		



