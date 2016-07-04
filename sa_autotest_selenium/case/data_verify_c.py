#coding:GBK
from lib import sa
from lib import sa_sql
import unittest
from datetime import *
from time import *
import string

class DataVerifyCase(unittest.TestCase):
    def setUp(self):
        self.sa = sa.Sa()
        self.sa.login_sa()
        #self.log=self.sa.get_log()
        self.cfg=self.sa.get_cfg()
        self.sel=self.sa.get_sel()
        self.cur=self.sa.get_cursor()
        #self.log.info("Now starting testcase DataVerify")
        
    def testYesterdayGeneral(self):
#        self.cur_account = self.sel.get_text("//div[@id='wrapper']/div[2]/div[2]/table/tbody/tr[2]/td[4]");
        self.sel.open("/overview_overView.action?username=tuan.27@sogou.com")
        self.sel.wait_for_page_to_load("30000")
        
        yesterday_total_uv = self.sel.get_text("//td[2]")
        sql=sa_sql.get_yesterday_uv();
        self.cur.execute(sql)
        result=self.cur.fetchone()
        self.assertEqual(string.atoi(yesterday_total_uv), result[0])
        
        yesterday_total_pv = self.sel.get_text("//td[3]")
        sql=sa_sql.get_yesterday_pv();
        self.cur.execute(sql)
        result=self.cur.fetchone()
        self.assertEqual(string.atoi(yesterday_total_pv), result[0])
        
        yesterday_visitpage = self.sel.get_text("//td[6]")
        sql=sa_sql.get_yesterday_avg_page()
        self.cur.execute(sql)
        result=self.cur.fetchone()
        self.assertEqual(string.atof(yesterday_visitpage), round(result[0],2))
    
        yesterday_bound = self.sel.get_text("//td[4]")
        sql=sa_sql.get_yesterday_bound()
        self.cur.execute(sql) 
        result=self.cur.fetchone()
        self.assertEqual(string.atof(yesterday_bound), round(result[0]*100,2))
        
        yesterday_transtimes = self.sel.get_text("//td[7]")
        sql=sa_sql.get_yesterday_transform()
        self.cur.execute(sql) 
        result=self.cur.fetchone()
        self.assertEqual(round(string.atof(yesterday_transtimes),2), round(result[0], 2))
        
        #计算时间，时间格式是hh:mm:ss 将数据库结果转化为时间格式字符串并加以验证。
        yesterday_avgtime = self.sel.get_text("//td[5]")
        sql=sa_sql.get_yesterday_avg_time()
        self.cur.execute(sql)
        result=self.cur.fetchone()
        now = datetime.now()
        now = now - timedelta(hours=now.hour, minutes=now.minute, seconds=now.second-int(result[0]))
        self.assertEqual(yesterday_avgtime, unicode(now.strftime("%H:%M:%S")))
                
def suite():
    return unittest.makeSuite(DataVerifyCase, "test")        

if __name__ == "__main__":
    unittest.main()
    #suite = unittest.makeSuite(DataVerifyCase,'test') 
    suite = unittest.TestSuite()
    suite.addTest(DataVerifyCase("testProKeyword"))
    runner = unittest.TextTestRunner(verbosity=1)
    runner.run(suite)
    