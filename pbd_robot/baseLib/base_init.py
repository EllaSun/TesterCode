import os
import ConfigParser

class Init:
	def __init__(self, cfg):
		#load the configuration file
		self.config = ConfigParser.ConfigParser()
		self.config.read(cfg)
		#init the data flows directory
		#according to the data flow specification, different module should be in the specified directory
		self.home_dir = self.getHomeDir()
		self.conf_dir = self.getConfDir()
		self.data_dir = self.getDataDir()
		self.input_dir = self.getInputDir()
		self.result_dir = self.getResultDir()
		self.bin_dir = self.getBinDir()
		self.lib_dir = self.getLibDir()
		self.log_dir = self.getLogDir()
		self.script_dir = self.getScriptDir()
		#init the configuration
		self.server_cfg = self.getServerConfig()
		self.server_cfg_bak = self.getServerConfigBak()
		self.query_server_cfg = self.getQueryConfig()
		self.query_server_cfg_bak = self.getQueryConfigBak()
		self.adlog_cfg = self.getAdlogCfg()
		#init the application
		self.app_name = self.getAppName()
		self.app_full_path = self.getAppFullPath()
		#init the start script
		self.start_script = self.getStartScript()

	def getOpt(self, section, option):
		'''get the option of the section'''
		try:
			dir = self.config.get(section, option)
		except:
			return None
		return dir

	def getHomeDir(self):
		'''get the home directory'''
		dir = self.getOpt("base", "home_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return dir

	def getBinDir(self):
		'''get the bin directory'''
		dir = self.getOpt("base", "bin_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getLibDir(self):
		'''get the library directory'''
		dir = self.getOpt("base", "lib_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getConfDir(self):
		'''get the configuration directory'''
		dir = self.getOpt("base", "conf_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getDataDir(self):
		'''get the data directory'''
		dir = self.getOpt("base", "data_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getInputDir(self):
		'''get the data directory'''
		dir = self.getOpt("base", "input_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getResultDir(self):
		'''get the data directory'''
		dir = self.getOpt("base", "result_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getLogDir(self):
		'''get the log diretory'''
		dir = self.getOpt("base", "log_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir


	def getScriptDir(self):
		'''get the log directory'''
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
		'''get the server's configuration'''
		cfg = self.getOpt("conf", "server_cfg")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getServerConfigBak(self):
		'''get the backup server configuration'''
		cfg = self.getOpt("conf", "server_cfg_bak")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getQueryConfig(self):
		'''get the query server's configuration'''
		cfg = self.getOpt("conf", "query_cfg")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getQueryConfigBak(self):
		'''get query server's configuration'''
		cfg = self.getOpt("conf", "query_cfg_bak")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getAdlogCfg(self):
		'''get adlog.cfg'''
		cfg = self.getOpt("conf", "adlog_cfg")
		if cfg == None:
			return None
		return self.conf_dir + cfg

	def getStartScript(self):
		'''get start script'''
		sh = self.getOpt("script", "start_script")
		if sh == None:
			return None
		return self.home_dir + sh

