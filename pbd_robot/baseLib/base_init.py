# coding:GBK
import os
import ConfigParser

class Init:
	def __init__(self, cfg):
		#���������ļ�
		self.config = ConfigParser.ConfigParser()
		self.config.read(cfg)
		#��ʼ��Ŀ¼������Ϣ
		self.home_dir = self.getHomeDir()
		self.conf_dir = self.getConfDir()
		self.data_dir = self.getDataDir()
		self.input_dir = self.getInputDir()
		self.result_dir = self.getResultDir()
		self.bin_dir = self.getBinDir()
		self.lib_dir = self.getLibDir()
		self.log_dir = self.getLogDir()
		self.script_dir = self.getScriptDir()
		#��ʼ�������ļ�������Ϣ
		self.server_cfg = self.getServerConfig()
		self.server_cfg_bak = self.getServerConfigBak()
		self.query_server_cfg = self.getQueryConfig()
		self.query_server_cfg_bak = self.getQueryConfigBak()
		self.adlog_cfg = self.getAdlogCfg()
		#��ʼ��app
		self.app_name = self.getAppName()
		self.app_full_path = self.getAppFullPath()
		#�����ű�
		self.start_script = self.getStartScript()

	def getOpt(self, section, option):
		'''��ȡĳ��option'''
		try:
			dir = self.config.get(section, option)
		except:
			return None
		return dir

	def getHomeDir(self):
		'''��ȡhome_dir'''
		dir = self.getOpt("base", "home_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return dir

	def getBinDir(self):
		'''��ȡbinĿ¼'''
		dir = self.getOpt("base", "bin_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getLibDir(self):
		'''��ȡlibĿ¼'''
		dir = self.getOpt("base", "lib_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getConfDir(self):
		'''��ȡconfĿ¼'''
		dir = self.getOpt("base", "conf_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getDataDir(self):
		'''��ȡdataĿ¼'''
		dir = self.getOpt("base", "data_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getInputDir(self):
		'''��ȡdataĿ¼'''
		dir = self.getOpt("base", "input_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getResultDir(self):
		'''��ȡdataĿ¼'''
		dir = self.getOpt("base", "result_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getLogDir(self):
		'''��ȡlogĿ¼'''
		dir = self.getOpt("base", "log_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir


	def getScriptDir(self):
		'''��ȡlogĿ¼'''
		dir = self.getOpt("base", "script_dir")
		if dir == None:
			return None
		if not dir.endswith(os.path.sep):
			dir += os.path.sep
		return self.home_dir + dir

	def getAppName(self):
		'''��ȡ��ִ�г�����'''
		return self.getOpt("app", "app_name")

	def getAppFullPath(self):
		'''��ȡ��ִ�г����ȫ·��'''
		return str(self.bin_dir) + str(self.app_name)

	def getServerConfig(self):
		'''��ȡ�����ļ�'''
		cfg = self.getOpt("conf", "server_cfg")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getServerConfigBak(self):
		'''��ȡ���������ļ�'''
		cfg = self.getOpt("conf", "server_cfg_bak")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getQueryConfig(self):
		'''��ȡ�����ļ�'''
		cfg = self.getOpt("conf", "query_cfg")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getQueryConfigBak(self):
		'''��ȡ���������ļ�'''
		cfg = self.getOpt("conf", "query_cfg_bak")
		if cfg == None:
			return None
		if self.conf_dir == None:
			return str(self.home_dir) + cfg
		return str(self.conf_dir) + cfg

	def getAdlogCfg(self):
		'''��ȡAdlog.cfg'''
		cfg = self.getOpt("conf", "adlog_cfg")
		if cfg == None:
			return None
		return self.conf_dir + cfg

	def getStartScript(self):
		'''��ȡ�����ű�'''
		sh = self.getOpt("script", "start_script")
		if sh == None:
			return None
		return self.home_dir + sh

