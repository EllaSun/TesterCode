# coding:GBK
import os,re,time
import string

# 配置文件
class Config:
	def __init__(self, id):
		self.id = id
		self.path = self.id.get('path_conf')
		self.path_bak = self.id.get('path_conf_bak')

	#设置配置项
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


	#获取配置项
	def getConfig(self, item):
		f = file(self.path,'r')
		cfg = f.read()
		cp = re.compile('"{0,1}' + str(item) + '"{0,1}"=(.*)')
		fd = re.findall(cp,cfg)
		f.close()
		return fd[0]

	#删除配置项
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

	#恢复配置文件
	def recoverConfig(self):
		cmd = 'cp -f ' + self.path_bak + ' ' + self.path
		os.popen(cmd).read()



