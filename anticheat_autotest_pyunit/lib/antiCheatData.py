#coding: gbk
from baseLib import base_data
import copy

emergeParam = {}
ieTemplate = {}
antiCheatMap = {}

execfile("conf/antiCheat.cfg")
path= antiCheatMap['path'] + "/data/"
list=["bidding_ie_log",
	"ie_lead_stat_log",
	"acid_pass_ratio_file",
	"pid_pass_ratio_file",
	" pid_level_pass_ratio_file",
	"error_code_pass_ratio_file",
	"acid_consume_file",
	"pid_acid_protect_file",
	"force_query_pass_ratio_file",
	"spsogou_type_file",
	"ac_type_protect_file",
	"pid_protect_file",
	"type_pass_ratio_file",
	"xml_ip_black_file",
	"acid_double_clicks_file",
	"emerg_rule_file",
	"pid_max_consume_file",
	"bd_pid_pass_rate_file",
	"pid_real_time_protect_file",
	"reload_ip_file",
	"white_ip_file",
	"acid_five_white_acid_file",
	"cost_control_2_kwd_file",
	"cost_control_2_acc_file"]

def setEmergeParam(key = None, value = None, m = emergeParam):
	n = copy.deepcopy(m)
	n[key] = value
	return n


class antiCheatData(base_data.Data):
	def __init__(self):
		base_data.Data.__init__(self)
	
	def recover(self):
		for i in list:
			self.clear(path + i)
		self.append(path + "reload_ip_file", "127.0.0.1")
		self.append(path + "white_ip_file", "127.0.0.1")
	
	def clearData(self, file):
		self.clear(path + file)
	
	def setBadContent(self, file, content):
        	self.append(path + file, content)
	 
	def rmFile(self, file):
		self.rm(path + file)

	def addAccIdConsume(self, accId, type, price = None):
		newLine=''
		if price:
			newLine = str(accId) + '\t' + str(type) + '\t' + str(price) 
		else:
			newLine = str(accId) + '\t' + str(type) 
		self.append(path + 'acid_consume_file', newLine)

	def setAccIdConsume(self, accId, type, price = None):
		self.clearData('acid_consume_file')
        	self.addAccIdConsume(accId, type, price)

	def setPidAccidProtectFile(self, pid, accId, ratio, type=None):
		if type:
                	newLine = str(pid) + '\t' + str(type) + '\t' + str(accId) + '\t' + str(ratio)  
		else:
                	newLine = str(pid) + '\t1\t' + str(accId) + '\t' + str(ratio) + '\n'
                	newLine += str(pid) + '\t2\t' + str(accId) + '\t' + str(ratio) 
        	self.append(path + 'pid_acid_protect_file', newLine)

	def setPidProtectFile(self, pid, ratio, type = None):
        	if type:
                	newLine = str(pid) + '\t' + str(type) + '\t' + str(ratio) 
        	else:
                	newLine = str(pid) + '\t1\t' + str(ratio) + '\n' 
                	newLine += str(pid) + '\t2\t' + str(ratio) 
		self.append(path + 'pid_protect_file', newLine)
	
	def setAcTypeProtectFile(self, cpcId, isSearch, type, price, min_intv = '-1'):
        	newLine = str(cpcId) + '\t' + str(isSearch) + '\t' + str(type) + '\t' + str(price) + '\t' + str(min_intv) 
        	self.append(path + 'ac_type_protect_file', newLine)

	def setSpSogouTypeFile(self, isSearch, type, ratio):
		newLine = str(isSearch) + '\t' + str(type) + '\t' + str(ratio) 
		self.append(path + 'spsogou_type_file', newLine)


	def setTypePassRatioFile(self, isContext, letGoType, errorCode, ratio):
		newLine = str(isContext) + '\t' + str(letGoType) + '\t' + str(errorCode) + '\t' + str(ratio)
		self.append(path + 'type_pass_ratio_file', newLine)

	def setWhiteIp(self, ip):
		newLine = ip
		self.append(path + 'white_ip_file', newLine)

	def setReloadWhiteIp(self,ip):
		newLine = ip
		self.append(path + 'reload_ip_file', newLine)

	def setPidLevel(self, pid, level):
		newLine = pid + '\t' + level
		self.append(path + 'pid_level', newLine)

	#指定格式的数据，添加紧急上线文件
	def setEmergeFile(self, emergeParam = emergeParam):
		data  = emergeParam['ip']  + '\t' 
		data += emergeParam['pid'] + '\t'	
		data += emergeParam['suid'] + '\t'	
		data += emergeParam['yyid'] + '\t'	
		data += emergeParam['accId'] + '\t'	
		data += emergeParam['adId'] + '\t'	
		data += emergeParam['groupId'] + '\t'	
		data += emergeParam['searchKey'] + '\t'	
		data += emergeParam['keyword'] + '\t'	
		data += emergeParam['url'] + '\t'	
		data += emergeParam['clickId'] + '\t'	
		data += emergeParam['letGoType'] + '\t'	
		data += emergeParam['pidLevel'] 
		self.append(path+'emerg_rule_file', data)


	def setPidConsume(self, pid="sohu", consume="2000"):
		data = pid + "\t" + str(consume)
		self.append(path + 'pid_max_consume_file',data)

	def setFileContent(self, fileName, content):
		self.append(path + fileName, content)

	def setPidPassRatio(self, errCode, pid, ratio):
		data = errCode + '\t' + pid + '\t' + ratio
		self.setFileContent('bd_pid_pass_rate_file', data)

	def setPidRealTimeProtectFile(self, pid):
		self.append(path + 'pid_real_time_protect_file', pid)

	def setAcidDoubleClicksFile(self, acid = ""):
		self.append(path + 'acid_double_clicks_file', acid)

	def setXmlIpBlackFile(self, ip):
		self.append(path + 'xml_ip_black_file', ip)

	def setAcidFiveWhiteFile(self,acid):
		self.append(path + 'acid_five_white_acid_file', acid)

	def makeRTM(self, pid, startTime, n = 1):
		data = ieTemplate.replace('{[(pid)]}', pid)
		data = data.replace('{[(time)]}', startTime)
		for i in range(n):
			self.append(path+'bidding_ie_log', data + '\n')
	def setCostControl2KwdFile(self, kwd, counts=3):
		data =  kwd + "\t" + str(counts);
		self.append(path + 'cost_control_2_kwd_file', data)
	def setCostControl2AccFile(self, accountId, consume=100):
		data = accountId + "\t" + str(consume);
		self.append(path + "cost_control_2_acc_file", data)

		
