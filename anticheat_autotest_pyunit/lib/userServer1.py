from baseLib import base_server,base_log

import os,re,time,commands
import string

userServer1Map = {}

execfile('conf/antiCheat.cfg')

class userServer1(base_server.Server):
        def __init__(self):
                base_server.Server.__init__(self,userServer1Map['start'],'user_server')
		self.udp = base_log.Log(userServer1Map['log'], userServer1Map['log']+'udp_update_log*')

	def checkUserLogMsg(self, column = 0, index = 0):
		print self.udp.get(index, column)
		return self.udp.get(index, column)
	
	def recover(self):
		cmd="rm " + userServer1Map['log'] +"/* -f"
		os.system(cmd)	
