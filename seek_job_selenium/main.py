# coding:utf-8


from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
import time
import xlwt
workbook = xlwt.Workbook(encoding='utf-8')
booksheet = workbook.add_sheet('Sheet 1', cell_overwrite_ok=True)
booksheet.write(0,0, "title")
booksheet.write(0,1, "company")
booksheet.write(0,2, "status")
booksheet.write(0,3, "date")
booksheet.write(0,4, "work-type")
booksheet.write(0,5, "relevant" )

#try other words like "QA"
chromedriver_path="C:\chromedriver_win32\chromedriver.exe"
dest_url_head = 'http://www.seek.co.nz/jobs/in-auckland/#dateRange=999&workType=0&industry=&occupation=&graduateSearch=false&salaryFrom=0&salaryTo=999999&salaryType=annual&companyID=&advertiserID=&advertiserGroup=&keywords=test&page='
dest_url_tail = '&displaySuburb=&seoSuburb=&where=All+Auckland&whereId=1018&whereIsDirty=false&isAreaUnspecified=false&location=1018&area=&nation=&sortMode=KeywordRelevance&searchFrom=quick&searchType='

browser_driver = webdriver.Chrome(chromedriver_path)

#login
browser_driver.get('https://www.seek.co.nz/Login/SignIn?returnUrl=%2F&feature=Navigation&inUse=False')
browser_driver.find_element_by_xpath('//*[@id="Email"]').send_keys('sunyingms@gmail.com')
browser_driver.find_element_by_xpath('//*[@id="Password"]').send_keys('newBaby@Apr')
browser_driver.find_element_by_xpath('//*[@id="signInForm"]/div/button').click()
time.sleep(10)
line=1
for page in range(10):
    dest_url = dest_url_head + str(page) + dest_url_tail
#   #collect information
    browser_driver.get(dest_url)
    time.sleep(10)
       
    jobs= browser_driver.find_elements_by_xpath('//*[@id="jobsListing"]/div[*]/article[1]/dl/dd[1]')

    job_url_map={}
    url_status_map={}
    job_titles = jobs[0].find_elements_by_xpath('//h2/a')
    job_companys = jobs[0].find_elements_by_xpath('//h2/em')
    for i in range(len(job_titles)): 
        value = job_titles[i].text+"\t"+job_companys[i].text
        key = job_titles[i].get_attribute('href')
        job_url_map[key] = value
        url_status_map[key] = "NULL"
     
    for url in url_status_map:
        browser_driver.get(url)
        time.sleep(3)
        element = browser_driver.find_element_by_xpath('//*[@id="bodyContainer"]/div[2]/div[4]/div[2]/div[2]/div[1]/a[1]/div')
        url_status_map[url] = element.text
        element_1 = browser_driver.find_element_by_xpath('//*[@id="bodyContainer"]/div[2]/div[4]/div[1]/strong')
        date = element_1.text
        element_2 = browser_driver.find_element_by_xpath('//*[@id="bodyContainer"]/div[2]/div[4]/div[1]/ul/li[2]/div')
        work_type = element_2.text
        print job_url_map[url], url_status_map[url]
        title = job_url_map[url].split('\t')[0]
        company = job_url_map[url].split('\t')[1]
        booksheet.write(line,0, title)
        booksheet.write(line,1, company)
        booksheet.write(line,2, url_status_map[url])
        booksheet.write(line,3, date)
        booksheet.write(line,4,work_type)
        booksheet.write(line,5, page)
        #booksheet.write(line,3, url)
        line = line + 1

workbook.save('D:\\seek\\result.xls')
        
          
        
      
browser_driver.quit()

    


