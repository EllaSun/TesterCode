from baseLib import base_server, base_data
import os, re, time, commands, struct
import string, copy, md5

antiCheatMap = {}
cheatingClientMap = {}
bidClickParam = {}

execfile("conf/antiCheat.cfg")
increaseId = 0
def __getdeferentId__():
	global increaseId
	increaseId += 1
	return increaseId

validTime = 0
def getValidTime():
	global validTime
	validTime = validTime +1
	return (500 + validTime * (validTime - 1) * 100)
	
def addMassiveValidClicks(n, isUnion = False):
	if isUnion:
		dataFileName = antiCheatMap['path']+'/p.clicks'
	else:
		dataFileName = antiCheatMap['path']+ '/s.clicks'
	f1 = open(dataFileName)
	f2 = open(cheatingClientMap['clickFile'], 'a')
	lines = f1.readlines()
	for i in range(n):
		f2.write(lines[i])
	f1.close()
	f2.close()



def makeCookie(firstViewTime = 1249142400, lastClickTime = 0, firstClickTime = 0, lastViewTime = 1249142400, \
               adClickCount = 0, ip = 2030902265, adViewCount = 1):
	cookieString =  str(firstViewTime) + '#'
	cookieString += str(firstClickTime) + '#'
	cookieString += str(lastViewTime) + '#'
	cookieString += str(lastClickTime) + '#'
	cookieString += str(ip) + '#'
	cookieString += str(adViewCount) + '#'
	cookieString += str(adClickCount)
	return cookieString

def setClickParam(key, value, m =  bidClickParam ):
	n = copy.deepcopy(m)
	n[str(key)] = value
	return n

execfile('conf/antiCheat.cfg')
class cheatingClient(base_server.Server):
	def __init__(self):
		id = 'cheatingClient'
		base_server.Server.__init__(self,cheatingClientMap['start'],'CheatingClient')
		self.clickFile = base_data.Data()

	def clearClick(self):
		self.clickFile.clear(cheatingClientMap['clickFile'])

	def makeLevel(self, levl, leftMove = 15):
		reserved = 8421889
		#15-18 level is the union channel
		if leftMove == 15:
			#将15-18置0
			reserved = reserved & 4294721535
		#set 15-19 bit
		reserved = reserved | (levl << leftMove)
        	return reserved

	def addClick(self, adId='1111', accId='2222', groupId='3333', serverTime='100000', ip='11.22.33.44', pid='sogou',\
			suid='7EA0010A81430A0A00000000574E63C6', yyid='7E43B0FD2EA99B7DD88F7CA2E22E25D1', cookie=makeCookie(),\
			flag = '00000001', reserved = '08421889', s_type = '1', price = '32', url='http://www.abc.com', letGoType = 1,\
			ml = -1, mc = -1, isBack = 0, serviceType="20100",isInvalid='0',newXmlCookie='',extendReserved='8', \
			creativeId='4444', groupTemp='0',cookieTime=0, pvTime=0, searchKeyWord="鲜花",clickId=None, planId="0", \
			md5Res='ffffffffffffffff', ma="2375,1344,238,553,250,560,1424,723", cx_type="0", cx_indus="0", lu="", upos="0", \
			queryReserved="0", maxPrice="0", keyword="鲜花"):
		data = clickTemplate.replace('{[(flag)]}', str(flag))
		data = data.replace('{[(reserved)]}', str(reserved))
		data = data.replace('{[(serverTime)]}', str(serverTime))
		data = data.replace('{[(cookie)]}', cookie)
		data = data.replace('{[(adId)]}', str(adId))
		data = data.replace('{[(md5Res)]}', md5Res)
		data = data.replace('{[(ip)]}', ip)
		data = data.replace('{[(pid)]}', pid)
		data = data.replace('{[(isInvalid)]}', isInvalid)
		data = data.replace('{[(regionPublic)]}', '8')
		data = data.replace('{[(isSearchOrSohu)]}', str(s_type))
		data = data.replace('{[(price)]}', str(price))
		data = data.replace('{[(groupId)]}', str(groupId))
		data = data.replace('{[(keyword)]}', keyword)
		data = data.replace('{[(accId)]}', str(accId))
		data = data.replace('{[(searchKeyword)]}', searchKeyWord)
		data = data.replace('{[(pvRefer)]}', url)
		data = data.replace('{[(suid)]}', str(suid))
		data = data.replace('{[(yyid)]}', str(yyid))
		if clickId == None:
			data = data.replace('{[(clickId)]}', str(int(time.time()) + __getdeferentId__()))
		else:
			data = data.replace('{[(clickId)]}', clickId)
		data = data.replace('{[(uip)]}','1.2.3.4')
		data = data.replace('{[(passType)]}',str(letGoType))
		data = data.replace('{[(ml)]}',str(ml))
		data = data.replace('{[(mc)]}',str(mc))
		data = data.replace('{[(isBack)]}',str(isBack))
		data = data.replace('{[(serviceType)]}',str(serviceType))
		data = data.replace('{[(newXmlCookie)]}', newXmlCookie)
		data = data.replace('{[(extendReserved)]}', str(extendReserved))
		data = data.replace('{[(creativeId)]}', str(creativeId))
		data = data.replace('{[(groupTemp)]}', str(groupTemp))
		data = data.replace('{[(cookieTime)]}', str(cookieTime))
		data = data.replace('{[(pvTime)]}', str(pvTime))
		data = data.replace('{[(planId)]}', str(planId))
		data = data.replace('{[(ma)]}', str(ma))
		data = data.replace('{[(cx_type)]}', str(cx_type))
		data = data.replace('{[(cx_indus)]}', str(cx_indus))
		data = data.replace('{[(lu)]}', str(lu))
		data = data.replace('{[(upos)]}', str(upos))
		data = data.replace('{[(queryReserved)]}', str(queryReserved))
		data = data.replace('{[(maxPrice)]}', str(maxPrice))
		if cookie != "":
			cmd  = 'cd ' + msgToolMap['path']  + ';echo "' + data + '" | sh ' + msgToolMap['start'] + ' 1'
			status, data = commands.getstatusoutput(cmd)
		self.clickFile.append(cheatingClientMap['clickFile'], data)

	def addClickMap(self, clickParam = bidClickParam):
		clickId = str(int(time.time()) + __getdeferentId__())
		data = clickTemplate.replace('{[(flag)]}',  clickParam['flag'])
		data = data.replace('{[(reserved)]}',       clickParam['reserved'])
		data = data.replace('{[(serverTime)]}',     clickParam['serverTime'])
		data = data.replace('{[(cookie)]}',         clickParam['cookie'])
		data = data.replace('{[(adId)]}',           clickParam['adId'])
		data = data.replace('{[(md5Res)]}',  	    'ffffffffffffffff')
		data = data.replace('{[(ip)]}',             clickParam['ip'])
		data = data.replace('{[(pid)]}',            clickParam['pid'])
		data = data.replace('{[(isInvalid)]}',      clickParam['isInvalid'])
		data = data.replace('{[(regionPublic)]}',   '8')
		data = data.replace('{[(isSearchOrSohu)]}', clickParam['searchOrSohu'])
		data = data.replace('{[(price)]}',          clickParam['price'])
		data = data.replace('{[(groupId)]}',        clickParam['groupId'])
		data = data.replace('{[(keyword)]}',        clickParam['keyword'])
		data = data.replace('{[(accId)]}',          clickParam['accId'])
		data = data.replace('{[(searchKeyword)]}',  clickParam['searchKey'])
		data = data.replace('{[(pvRefer)]}',        clickParam['url'])
		data = data.replace('{[(suid)]}',           clickParam['suid'])
		data = data.replace('{[(yyid)]}',           clickParam['yyid'])
		data = data.replace('{[(clickId)]}',        clickId)
		data = data.replace('{[(uip)]}',            '1.2.3.4')
		data = data.replace('{[(passType)]}',       clickParam['letGoType'])
		data = data.replace('{[(ml)]}',             clickParam['ml'])
		data = data.replace('{[(mc)]}',             clickParam['mc'])
		data = data.replace('{[(isBack)]}',         clickParam['isBack'])
		data = data.replace('{[(serviceType)]}',    clickParam['serviceType'])
		if clickParam['cookie'] != "":
			cmd  = 'cd ' + msgToolMap['path']  + ';echo "' + data + '" | sh ' + msgToolMap['start'] + ' 1'
			status, data = commands.getstatusoutput(cmd)
		self.clickFile.append(cheatingClientMap['clickFile'], data)
		return clickId
	
	def sendRequest(self):
		os.system(cheatingClientMap['start'])
		
	def addMassiveNoIPCRepeatClicks(self, n, cpcId, accId, pid='sogou', flag = '00000001', reserved = '08421889', s_type = '3'):
		cookie = makeCookie()
		#generate no repeating ip addresses
		ipN = 100100100100
		for i in range(n):
			serverTime = getValidTime()
			url = 'http://www.' + str(i) + '.com'
			suid = 'DE0D0D794B400A0A4A3C218F0004E1A' + str(i)
			ipN += 1001
			ip = struct.pack('I', ipN)
			ip = struct.unpack('BBBB', ip)
			ip = str(ip[0]) + '.' + str(ip[1]) + '.'  + str(ip[2]) + '.' + str(ip[3])
			self.addClick(str(cpcId), str(accId), '222222222222', serverTime, ip, pid, suid, '0', cookie, flag, reserved, s_type, 32, url)

	def md5Digest(self, content):
		m = md5.new()
		m.update(content)
		return  m.hexdigest() 	

	def addClickUrlMd5(self, pid, url, flag='00000001', reserved='08421889', s_type='3', serviceType='20100',serverTime="565656", accId="1111"):	
		self.addClick(pid=pid, url=url, flag=flag, reserved=reserved, s_type = s_type, serviceType=serviceType, serverTime=serverTime, md5Res=self.md5Digest(str(url))[:16], accId=accId)
		
