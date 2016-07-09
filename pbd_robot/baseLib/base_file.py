import os,re,time
import string

# cofiguration
class Config:
	def __init__(self, id):
		self.id = id
		self.path = self.id.get('path_conf')
		self.path_bak = self.id.get('path_conf_bak')

	#set the configuration item
	def setConfig(self, item, value, mode=1):
		f = file(self.path,'r')
		cfg = f.read()
		if mode == 1:
			cp = re.compile('"' + str(item) + '"=(.*)')
			cont = '"' + str(item) + '"=' + '"' + str(value) + '"'
		elif mode == 2:
			cp = re.compile(str(item) + '=(.*)')
			cont = str(item) + '=' + str(value) 
		fd = re.findall(cp,cfg)
		f.close()
		if fd:
			new = re.sub(cp,cont,cfg)
			f = file(self.path,'w')
			f.write(new)
		else:
			f = file(self.path,'a')
			cont = cont + "\n"
			f.write(cont)
		f.close()


	#get configuration item
	def getConfig(self, item):
		f = file(self.path,'r')
		cfg = f.read()
		cp = re.compile('"{0,1}' + str(item) + '"{0,1}"=(.*)')
		fd = re.findall(cp,cfg)
		f.close()
		return fd[0]

	#remove the configuration item
	def removeConfig(self, item):
		f = file(self.path,'r')
		cfg = f.read()
		cp =re.compile('"{0,1}'+item+'"{0,1}=(.*)')
		cont = ''
		new = re.sub(cp,cont,cfg)
		f.close()
		f = file(self.path,'w')
		f.write(new)
		f.close()

	#recover the configuration file
	def recoverConfig(self):
		cmd = 'cp -f ' + self.path_bak + ' ' + self.path
		os.popen(cmd).read()



