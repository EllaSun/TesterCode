# coding:GBK
import os
import ConfigParser

class Init:
	def __init__(self, cfg):
		#加载配置文件
		self.config = ConfigParser.ConfigParser()
		self.config.read(cfg)
		#初始化目录配置信息
		self.home_dir = self.getHomeDir()
		self.conf_dir = self.getConfDir()
		self.data_dir = self.getDataDir()
		self.input_dir = self.getInputDir()
		self.result_dir = self.getResultDir()
		self.bin_dir = self.getBinDir()
		self.lib_dir = self.getLibDir()
		self.log_dir = self.getLogDir()
		self.script_dir = self.getScriptDir()
		#初始化配置文件配置信息
		self.server_cfg = self.getServerConfig()
		self.server_cfg_bak = self.getServerConfigBak()
		self.query_server_cfg = self.getQueryConfig()
		self.query_server_cfg_bak = self.getQueryConfigBak()
		self.adlog_cfg = self.getAdlogCfg()
		#初始化app
		self.app_name = self.getAppName()
		self.app_full_path = self.getAppFullPath()
		#启动脚本
		self.start_script = self.getStartScript()

	def getOpt(self, section, option):
		'''获取某个option'''
		try:
			dir = self.config.get(section, option)
		except:
			return None
		return dir

	def getHomeDir(self):
		'''获取home_dir'''
		dir = self.getOpt("base", "home_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return dir

	def getBinDir(self):
		'''获取bin目录'''
		dir = self.getOpt("base", "bin_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getLibDir(self):
		'''获取lib目录'''
		dir = self.getOpt("base", "lib_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getConfDir(self):
		'''获取conf目录'''
		dir = self.getOpt("base", "conf_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getDataDir(self):
		'''获取data目录'''
		dir = self.getOpt("base", "data_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getInputDir(self):
		'''获取data目录'''
		dir = self.getOpt("base", "input_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getResultDir(self):
		'''获取data目录'''
		dir = self.getOpt("base", "result_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getLogDir(self):
		'''获取log目录'''
		dir = self.getOpt("base", "log_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir


	def getScriptDir(self):
		'''获取log目录'''
		dir = self.getOpt("base", "script_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getAppName(self):
		'''获取可执行程序名'''
		return self.getOpt("app", "app_name")

	def getAppFullPath(self):
		'''获取可执行程序的全路径'''
		return str(self.bin_dir) + str(self.app_name)

	def getServerConfig(self):
		'''获取配置文件'''
		cfg = self.getOpt("conf", "server_cfg")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getServerConfigBak(self):
		'''获取备份配置文件'''
		cfg = self.getOpt("conf", "server_cfg_bak")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getQueryConfig(self):
		'''获取配置文件'''
		cfg = self.getOpt("conf", "query_cfg")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getQueryConfigBak(self):
		'''获取备份配置文件'''
		cfg = self.getOpt("conf", "query_cfg_bak")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getAdlogCfg(self):
		'''获取Adlog.cfg'''
		cfg = self.getOpt("conf", "adlog_cfg")
		if cfg == None:
			return None
		return self.conf_dir + cfg

	def getStartScript(self):
		'''获取启动脚本'''
		sh = self.getOpt("script", "start_script")
		if sh == None:
			return None
		return self.home_dir + sh

