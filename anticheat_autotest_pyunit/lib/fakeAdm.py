from baseLib import base_server,base_log

import os,re,time,commands
import string

fakeAdmMap = {}
admRecvLog = {}

execfile('conf/antiCheat.cfg')

class fakeAdm(base_server.Server):
        def __init__(self):
                base_server.Server.__init__(self,fakeAdmMap['start'],'DumpADMInfo')
                self.err = base_log.Log(fakeAdmMap['path'], fakeAdmMap['err'])
		self.std = base_log.Log(fakeAdmMap['path'], fakeAdmMap['std'])
	
	def checkBillingCount(self):
		return 	str(self.err.__getSection__())

	def checkValidBillingCount(self):
		sum = self.err.__getSection__()	
		pacNum = 0
		for i in range(sum):
			if self.err.get(i,admRecvLog['pacType']) == '0':
				pacNum = pacNum + 1
		return str(pacNum)
	
	def getField(self, line, field):
		return str(self.err.get(line, admRecvLog[field]))

	def getContent(self, line, field):
		content = self.err.get(line, admRecvLog['content'])
		return content.split('&')[field]
	
	def getSpSogouNum(self):
                sum = self.err.__getSection__()
                spNum = 0
                for i in range(sum):
                        if self.getField(i, 'pid') == 'sp_sogou':
                                spNum = spNum + 1
                return spNum

