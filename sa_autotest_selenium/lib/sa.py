#coding:GBK

from selenium import selenium
from datetime import *
from time import *
import ConfigParser
import MySQLdb
import logging

class Sa():
    def __init__(self):
        self.verificationErrors = []
     
        #初始化配置文件
        self.cfg = ConfigParser.ConfigParser()   
        self.cfg.read("..\conf\sa_test.ini")
        #self.logfile=self.cfg.get('log_file','path')+self.cfg.get('log_file','prefix')+time.strftime('%Y_%m_%d',time.localtime(time.time()))
        #self.logger = None

        #设置日志文件   
        #self.logger = logging.getLogger()
        #self.hdlr = logging.FileHandler(self.logfile)
        #self.formatter = logging.Formatter('%(asctime)s %(levelname)s %(message)s')
        #self.logger.addHandler(self.hdlr)
        #self.logger.setLevel(logging.NOTSET)

        #连接selenium-server服务器
        self.sel = selenium(host = self.cfg.get('sel_server', 'host'), port = self.cfg.get('sel_server','port'), browserStartCommand = self.cfg.get('sel_server','browser'), browserURL = self.cfg.get('base_page','baseURL'))
        self.sel.start()
        self.sel.open('/login_userLogin.action')
        #self.logger.info(u"成功连接到selenium-server:" + self.cfg.get('sel_server','host'))

        #连接my_sql
        self.conn = MySQLdb.Connect(host = self.cfg.get('mysql_server','host'), user = self.cfg.get('mysql_server','user'), passwd = self.cfg.get('mysql_server','passwd'), db = self.cfg.get('mysql_server','maindb') )
        self.cur = self.conn.cursor();   
        #if self.cur == None:
        #    self.logger.error(u'连接mysql服务器:' + self.cfg.get('mysql_server','host')+u'失败')
        #self.logger.info(u"成功连接到mysql服务器:"+self.cfg.get('sel_server', 'host'))                   

    def __del__(self):
        #disconnect with db
        #self.logger.info(u"断开与mysql服务器的连接:")   
        self.conn.close()
        #self.logger.info(u"断开与selenium-server的连接.")  
        #self.sel.stop()
        self.sel.close()   

    def get_sel(self):
        return self.sel

    def get_cursor(self):
        return self.cur

    def get_log(self):
        return self.logger

    def get_cfg(self):
        return self.cfg
        
    def login_sa(self):
        self.sel.open(self.cfg.get('loginSA','loginURL'))
        self.sel.type("username", self.cfg.get('loginSA','username'))
        self.sel.type("password", self.cfg.get('loginSA', 'password'))
        self.sel.click("css=input[type=\"submit\"]")
        self.sel.wait_for_page_to_load("30000")
    
    def query(self,sql):
        self.cur.execute(sql)
        #self.logger.info(u"断开与selenium-server的连接." + self.cfg.get('sel_server','host')  )
        return self.cur.fetchall()
    

    
    
    
    