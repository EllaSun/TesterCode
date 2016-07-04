from baseLib import base_server,base_log

import os,re,time,commands
import string

userServer2Map = {}

execfile('conf/antiCheat.cfg')

class userServer2(base_server.Server):
        def __init__(self):
                base_server.Server.__init__(self,userServer2Map['start'],'user_server')
		self.udp = base_log.Log(userServer2Map['log'], userServer2Map['log']+'udp_update_log*')

	def checkUserLogMsg(self, column = 0, index = 0):
		return self.udp.get(index, column)	
	def recover(self):
		cmd="rm " + userServer2Map['log'] +"/* -f"
		os.system(cmd)	
