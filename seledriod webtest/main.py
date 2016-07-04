#coding: gbk
from selenium import webdriver
import time
import sys
import ConfigParser
#from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.touch_actions import TouchActions
from selenium.webdriver.common.keys import  Keys

def getUrl(queryWord,  domain):
    quoteWord = queryWord.decode('gbk').encode('utf-8')
    url='http://'+domain+'/web/searchList.jsp?keyword='+quoteWord+'&s_1=%E6%90%9C%E7%B4%A2&pg=webSearchList&uID=fUGRg9mRJNJF9rXl&w=1244'
    return url

def testAd(driver, orign, ad_type, log,  pic_store,  screenDownType="1",  domain="wap.sogou.com"):
    test_name = ad_type + "_" + orign
    url = getUrl(orign, domain)

    
    driver.get(url)
    time.sleep(10)
    screenFilename_1 = ad_type + '_' + orign + '_' + '上方' + '.jpg'
    screenFilename_2 = ad_type + '_' + orign + '_' + '下方' + '.jpg'
    
    chain = TouchActions(driver)
    
    elements=driver.find_elements_by_class_name('ec_ad_results')
    print elements
    
    if len(elements) == 0:
        #上方
        driver.save_screenshot(pic_store+screenFilename_1)
        #下方
        chain.scroll(0, 3000).perform()
        #chain.send_keys(Keys.END).send_keys(Keys.PAGE_UP).perform()
        #time.sleep(1)
        driver.save_screenshot(pic_store+screenFilename_2)
        
        str=test_name + "  cant not find ad!\n"
        log.write(str)
        return 
    #存在广告块
    element = elements[0]
    x=int(element.location['x'])
    y=int(element.location['y'])
    chain.scroll(x, y).perform()
   # print 'test1', x, y
    time.sleep(5)
    #上方广告
    driver.save_screenshot(pic_store+screenFilename_1)
    x=-1*x
    y=-1*y
    chain.scroll(x, y).perform()
    if len(elements) == 2:
        #下方广告
        element = elements[1]
        #chain = ActionChains(driver)
        #chain.move_to_element(element).perform()
        #chain.flick_element(element, 0, 0, 0).perform()
        x=int(element.location['x'])
        y=int(element.location['y'])
        print '1111'
        chain.scroll(x, y).perform()
        chain.scroll(0, 0).perform()
        time.sleep(5)
        driver.save_screenshot(pic_store+screenFilename_2)
    #关闭
    #driver.quit()
    

if __name__=="__main__":
    #sys language
    reload(sys)
    sys.setdefaultencoding('UTF-8')
    if len(sys.argv) != 2:
        print "python main.py testweb.cfg"
        sys.exit(1)
    else:
        conf = ConfigParser.ConfigParser()
        conf.read(sys.argv[1])
        domain = conf.get("webtest", "domain")
        log_file = conf.get("webtest", "log")
        log = open(log_file, 'w')
        data_file  = conf.get("webtest", "data")
        data = open(data_file, 'r')
        pic_store = conf.get("webtest", "pic")
    driver  = webdriver.Remote(desired_capabilities=webdriver.DesiredCapabilities.ANDROID)
    for line in data:
        content = line.strip('\n').split('\t')
        if len(content) < 3:
            continue
        else:
            terminal = content[0]
            orign = content[1]
            ad_type = content[2]
        if len(content) >= 4:
            screenDownType = content[3]
        testAd(driver, orign, ad_type, log,  pic_store,  screenDownType,  domain)
        
    #driver.quit()
