#coding: gbk
from baseLib import base_log,base_conf
import os
import commands

antiCheatMap = {}
cdIeMap = {}

execfile("conf/antiCheat.cfg")

class antiCheatLog(base_log.Log):
	def __init__(self):
		base_log.Log.__init__(self,antiCheatMap['log'],'*')
		self.access = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/access*')
		self.ie     = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/ie*')
		self.stat   = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/stat*')
		self.cookie = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/cookie*')
		self.adm    = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/adm*')
		self.cc     = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/consume_control*')
		self.star   = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/star_*')
		self.error  = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/error_log*')
		self.err    = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/err.log')
		self.click  = base_log.Log(antiCheatMap['log'],antiCheatMap['log']+'/click_log')
	
	def getIeField(self, line, field):
		return self.ie.get(line,cdIeMap[field])

	def getClickField(self, line, field):
		return self.click.get(line, 1).split('&')[field]	

	def getIeErrMsg(self, line, ruleOrPass, testType):
		if ruleOrPass == 0:
			errMsg = self.getIeField(line, 'ruleErrMsg')
		elif ruleOrPass == 1:
			errMsg = self.getIeField(line, 'passErrMsg')
		else:
			return "func testType err"
		results = errMsg.split(',')
		for i in range(32):
                	if int(testType) & (1 << i) != 0:
                        	break
        	return results[i]

	def getRollRet(self, line, move):
		actualCode = int(self.getIeField(line, 'rollPolicy'))
		if ((actualCode>>move)&1) != 0:
			return True
		else:
			return False	

	def getPassedNumber(self):
		sum = self.ie.__getSection__()
		passNum = 0
		for i in range(sum):
			if self.ie.get(i,cdIeMap['ret']) == '0':
				passNum = passNum + 1
		return passNum

	def checkUnvalidPac(self, line):
		ret = self.getIeField(line, 'ret')
		packType = self.getIeField(line, 'pacType')
		rollRet = self.getIeField(line, 'rollRet')
		rollPolicy = self.getIeField(line, 'rollPolicy')
		result = False
		if (int(ret) != 0 ) and (int(packType) == 1) \
			and (int(rollPolicy) == -1) and (int(rollRet) == -1):
			result = True
		return result

	def checkValidPac(self, line):
		ret = self.getIeField(line, 'ret')
		packType = self.getIeField(line, 'pacType')
		rollRet = self.getIeField(line, 'rollRet')
		rollPolicy = self.getIeField(line, 'rollPolicy')
		result = False
		if (int(ret) == 0) and (int(packType) == 0) \
                        and (int(rollPolicy) == -1) and (int(rollRet) == -1):
			result = True
		return result

	def getSpSogouNum(self):
		sum = self.ie.__getSection__()
		spNum = 0
		for i in range(sum):
			if self.getIeField(i,'pid') == 'sp_sogou':
				spNum = spNum + 1
		return spNum

	def getAdmChargDetail(self, index, column, secondColumn,  separator='\t'):
        	line = self.adm.get(index, column, seg='\4')
        	return line.split(separator)[secondColumn]
	
	def getCcChargDetail(self, index, column, secondColumn,  separator='\t'):
        	line = self.cc.get(index, column, seg='\4')
        	return line.split(separator)[secondColumn]

	def getStarChargDetail(self, index, column, secondColumn,  separator='\t'):
        	line = self.star.get(index, column, seg='\4')
        	return line.split(separator)[secondColumn]

	def getErrCodeNum(self, errCode = '103011', col=8):
		sum = self.ie.__getSection__()
		cheatNum = 0
		for i in range(sum):
			errRule = self.getIeField(i, 'ruleErrMsg')
			if errRule.split(',')[col] == errCode:
				cheatNum = cheatNum  + 1
		return cheatNum

	def getRBErrCodeNum(self, errCode= '103011'):
		sum = self.ie.__getSection__()
		cheatNum = 0
		for i in range(sum):
			errRule = self.getIeField(i, 'rollPolicy')
			if errRule == errCode:
				cheatNum = cheatNum + 1
		return cheatNum

	def find_error_log(self, key="", key2=""):
		'''查找error日志中某关键词的出现行数'''
		return self.error.find(key1=key, key2=key2) + self.err.find(key1=key, key2=key2)
	
	def makeRollBackClick(self, type):
		if type == "set":
			cmd = 'mv ' + antiCheatMap['log']+'/click_log* '  + antiCheatMap['log_bak'] + '/click_log'
			os.system(cmd)
			self.rmAll()
		if type == "get":
			cmd = 'mv ' + antiCheatMap['log_bak'] + '/click_log' + ' ' + antiCheatMap['data']
			os.system(cmd)

	def getDebugInfo(self, hash_name, row, col):
		cmd = 'grep ' + hash_name + ' ' + antiCheatMap['log'] + '/error_log_2* | awk \'{if(NR==' + row + ')print $' + col + '}\' | awk -F ":" \'{print $2}\''
		status, data = commands.getstatusoutput(cmd)
		return data
