import os,time,sys
import string
import ConfigParser
from baseLib import base_sys

class Init:
	def __init__(self, cfg):
		'''load configeruation'''
		self.config = ConfigParser.ConfigParser()
		self.config.read(cfg)
		'''init diretory configuration'''
		self.home_dir = self.getHomeDir()
		self.conf_dir = self.getConfDir()
		self.data_dir = self.getDataDir()
		self.bin_dir = self.getBinDir()
		self.lib_dir = self.getLibDir()
		self.log_dir = self.getLogDir()
		'''init automation framework's configuration'''
		self.server_cfg = self.getServerConfig()
		self.server_cfg_bak = self.getServerConfigBak()
		self.adlog_cfg = self.getAdlogCfg()
		''' init test objective'''
		self.app_name = self.getAppName()
		self.app_full_path = self.getAppFullPath()
		'''init start script'''
		self.start_script = self.getStartScript()

	def getOpt(self, section, option):
		'''get the option in the section'''
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
		'''get the binary directory'''
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
		'''get configration directory'''
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

	def getLogDir(self):
		'''get the log directory'''
		dir = self.getOpt("base", "log_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getAppName(self):
		'''get the excutable application's name'''
		return self.getOpt("app", "app_name")

	def getAppFullPath(self):
		'''get the absolute path of the excutable application'''
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
		'''get the server's backup configuration'''
		cfg = self.getOpt("conf", "server_cfg_bak")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getAdlogCfg(self):
		'''get the adlog configuration'''
		cfg = self.getOpt("conf", "adlog_cfg")
		if cfg == None:
			return None
		return self.conf_dir + cfg

	def getStartScript(self):
		'''get the start script'''
		sh = self.getOpt("script", "start_script")
		if sh == None:
			return None
		return self.home_dir + sh
