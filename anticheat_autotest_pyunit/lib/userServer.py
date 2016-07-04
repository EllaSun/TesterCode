from baseLib import base_server,base_log

import os,re,time,commands
import string

userServerMap = {}

execfile('conf/antiCheat.cfg')

class userServer(base_server.Server):
        def __init__(self):
                base_server.Server.__init__(self,userServerMap['start'],'user_server')
		self.udp = base_log.Log(userServerMap['log'], userServerMap['log']+'udp_update_log*')

	def checkUserLogMsg(self, column = 0, index = 0):
		return self.udp.get(index, column)

	def recover(self):
		cmd="rm " + userServerMap['log'] +"/* -f"
		os.system(cmd)	
