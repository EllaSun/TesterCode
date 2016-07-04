#coding:GBK
from lib import sa, sa_sql
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
        sql=self.sa.get_yesterday_uv();
        self.cur.execute(sql)
        result=self.cur.fetchone()
        self.assertEqual(string.atoi(yesterday_total_uv), result[0])
        
        yesterday_total_pv = self.sel.get_text("//td[3]")
        sql=self.sa.get_yesterday_pv();
        self.cur.execute(sql)
        result=self.cur.fetchone()
        self.assertEqual(string.atoi(yesterday_total_pv), result[0])
        
        yesterday_visitpage = self.sel.get_text("//td[6]")
        sql=self.sa.get_yesterday_avg_page()
        self.cur.execute(sql)
        result=self.cur.fetchone()
        self.assertEqual(string.atof(yesterday_visitpage), round(result[0],2))
    
        yesterday_bound = self.sel.get_text("//td[4]")
        sql=self.sa.get_yesterday_bound()
        self.cur.execute(sql) 
        result=self.cur.fetchone()
        self.assertEqual(string.atof(yesterday_bound), round(result[0]*100,2))
        
        yesterday_transtimes = self.sel.get_text("//td[7]")
        sql=self.sa.get_yesterday_transform()
        self.cur.execute(sql) 
        result=self.cur.fetchone()
        self.assertEqual(round(string.atof(yesterday_transtimes),2), round(result[0], 2))
        
        #计算时间，时间格式是hh:mm:ss 将数据库结果转化为时间格式字符串并加以验证。
        yesterday_avgtime = self.sel.get_text("//td[5]")
        sql=self.sa.get_yesterday_avg_time()
        self.cur.execute(sql)
        result=self.cur.fetchone()
        now = datetime.now()
        now = now - timedelta(hours=now.hour, minutes=now.minute, seconds=now.second-int(result[0]))
        self.assertEqual(yesterday_avgtime, unicode(now.strftime("%H:%M:%S")))
                
    def testSevenGeneral(self):
        self.sel.open("/overview_overView.action?username=tuan.27@sogou.com")
        self.sel.wait_for_page_to_load("30000")
         
        sevendays_total_uv = self.sel.get_text("//tr[3]/td[2]")
        sevendays_total_pv = self.sel.get_text("//tr[3]/td[3]")
        sevendays_visitpage = self.sel.get_text("//tr[3]/td[6]")
        sevendays_transtimes = self.sel.get_text("//tr[3]/td[7]")
        sevendays_jr = self.sel.get_text("//tr[3]/td[4]")
        sevendays_visittime = self.sel.get_text("//tr[3]/td[5]")
        
    def testMonthGeneral(self):
        self.sel.open("/overview_overView.action?username=tuan.27@sogou.com")
        self.sel.wait_for_page_to_load("30000")
        
        thirtydays_total_uv = self.sel.get_text("//tr[4]/td[2]")
        thirtydays_total_pv = self.sel.get_text("//tr[4]/td[3]")
        thirtydays_jr = self.sel.get_text("//tr[4]/td[4]")
        thirtydays_visittime = self.sel.get_text("//tr[4]/td[5]")
        thirtydays_visitpage = self.sel.get_text("//tr[4]/td[6]")
        thirtydays_transtimes = self.sel.get_text("//tr[4]/td[7]")
       
    def testProKeyword(self): 
        self.sel.open("/overview_overView.action?username=tuan.27@sogou.com")
        self.sel.wait_for_page_to_load("30000")
        self.sel.open("sp_keyword.action?username=tuan.27@sogou.com")
        self.sel.wait_for_page_to_load("30000")
        
        sql=self.sa.get_pro_keyword(account="tuan.27@sogou.com", day=1)
        result_num = self.cur.execute(sql)
        
        for i in range(1, 10):
            row = self.cur.fetchone()
            if row == None:
                break
            #访问数
            visit_num = self.sel.get_text("//tr[" + str(i+2) + "]/td[2]")
            self.assertEqual(string.atoi(visit_num), row[1])
            #平均访问页数
            avg_pages= self.sel.get_text("//tr[" + str(i+2) + "]/td[4]")
            self.assertEqual(string.atof(avg_pages), round(row[3],2))
            #跳出率
            bounceRatio = self.sel.get_text("//tr[" + str(i+2) + "]/td[5]")
            self.assertEqual(round(string.atof(bounceRatio)/100, 4), float(row[4]))  
            #消费
            cosume = self.sel.get_text("//tr[" + str(i+2) + "]/td[7]")
            self.assertEquals(round(string.atof(cosume), 2), float(row[5]))
            #点击量    
            clicksum = self.sel.get_text("//tr[" + str(i+2) + "]/td[8]")
            self.assertEquals(string.atoi(clicksum), int(row[6]))
            #平均访问时间。
            avg_time = self.sel.get_text("//tr[" + str(i+2) + "]/td[3]")
            now = datetime.now()
            print row[2]
            now = now - timedelta(hours=now.hour, minutes=now.minute, seconds=now.second-int(row[2]))
            self.assertEqual(avg_time, unicode(now.strftime("%H:%M:%S")))
    
    #转化次数部分，查询转化次数相关表
    
    
    def testAdGroup(self):
        print "OK"
            
#    def testTimeAnalysis(self):
#        print "OK"
            
#    def testTrendAnalysis(self):
#        print "OK"
            
#    def testSearchEngine(self):
#        print "OK"
            
#    def testSourceKeyword(self):
#        print "OK"
            
#    def testSourceSearchEngine(self):
#        print "OK"
            
#    def testSourceDirect(self):
#        print "OK"

#    def testSourceOutsideRefer(self):
#        print "OK"
        
#    def testVisitorLocation(self):
#        print "OK"
            
#    def testVisitorPagenums(self):
#        print "OK"
            
#    def testVisitorPagetimes(self):
#        print "OK"
            
#    def tearDown(self):
#        self.log.info("dataverify tear down!")

if __name__ == "__main__":
    unittest.main()
    #suite = unittest.makeSuite(DataVerifyCase,'test') 
    suite = unittest.TestSuite()
    suite.addTest(DataVerifyCase("testYesterdayGeneral"))
    runner = unittest.TextTestRunner(verbosity=1)
    runner.run(suite)
    