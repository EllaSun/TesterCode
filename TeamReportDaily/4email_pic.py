#!/usr/bin/env python3
#coding: utf-8
import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
from email.mime.image import MIMEImage
from email.mime.application import MIMEApplication
import time
import sys
from os import listdir
print len(sys.argv)
if len(sys.argv) != 5:
    print "should equal to 5"
    exit(-1)
    

file_list = listdir("img")
personal_time_distribute = []
create_testcase_time_cost = []
execute_testcase_time_cost = []
ticket_time_cost = []
#get list of personal time distribution
for file_name in file_list:
    if file_name.find('TimeDistribute') > 0:
        personal_time_distribute.append(file_name)
    if file_name.find('CreateTestCaseTimeCost') > 0:
        create_testcase_time_cost.append(file_name)
    if file_name.find('ExecuteTestCaseTimeCost') > 0:
        execute_testcase_time_cost.append(file_name)
    if file_name.find('TicketTimeCost') > 0:
        ticket_time_cost.append(file_name)





sender = 'ella.sun@plexure.com'
#receiver = ['ella.sun@plexure.com', 'jasmine.zhang@plexure.com', 'leon.liu@plexure.com']
#receiver = ['ella.sun@plexure.com', 'sowmya.velicherla@plexure.com']
subject = 'QA Team Daily Report'
smtpserver = 'smtp.office365.com'
username = sys.argv[1]
password = sys.argv[2]
receiver_str = sys.argv[3]
receiver = receiver_str.split(",")




msgRoot = MIMEMultipart('related')



#strTM = time.strftime('%d/%m/%Y',time.localtime(time.time()))
strTM = sys.argv[4]
msgRoot['Subject'] = subject  +'(' + strTM + ')'

title = '<p><font size="6" face="Microsoft YaHei" align="center">QA Team Daily Report('+strTM+')</font></p><br><br>'



sub_str0 = '<p><font size="4" face="Microsoft YaHei"><li>Team time distribution</font></p><br><img src="cid:TeamTimeDistribution.png"></li><br><br>\<br>'

sub_str1 = ""
index = 0
if personal_time_distribute:
    sub_str1 = '<p><font size="4" face="Microsoft YaHei"><li>Team member time distribution</li></font></p><br>'
    sub_str1 += '<table>'
    
    for img in personal_time_distribute:
        print 'img'
        print img
        index = index + 1
        if index % 2 != 0:
            sub_str1 += "<tr>"
            sub_str1 += '<td><img src="cid:' + img +'"></tb>'
        else:
            sub_str1 += '<td><img src="cid:' + img +'"></tb>'
            sub_str1 += '</tr>'
            

    sub_str1 += '</table><br><br>'

print sub_str1 





sub_str2 =""
index = 0
if create_testcase_time_cost != []:
    sub_str2 = '<p><font size="4" face="Microsoft YaHei"><li>Team member time cost of creating test case</li></font></p><br>'
    sub_str2 += '<table>'
    for img in create_testcase_time_cost:
        index = index + 1
        if index % 2 != 0:
            sub_str2 += "<tr>"
            sub_str2 += '<td><img src="cid:' + img +'"></tb>'
        else:
            sub_str2 += '<td><img src="cid:' + img +'"></tb>'
            sub_str2 += '</tr>'
            

    sub_str2 += '</table><br><br>'





sub_str3 =""
index = 0
if execute_testcase_time_cost != []:
    sub_str3 = '<p><font size="4" face="Microsoft YaHei"><li>Team member time cost of executing test cases</li></font></p><br>'
    sub_str3 += '<table>'
    
    for img in execute_testcase_time_cost:
        index = index + 1
        if index % 2 != 0:
            sub_str3 += "<tr>"
            sub_str3 += '<td><img src="cid:' + img +'"></tb>'
        else:
            sub_str3 += '<td><img src="cid:' + img +'"></tb>'
            sub_str3 += '</tr>'
            

    sub_str3 += '</table><br><br>'



    
sub_str4 = ""
index = 0
if ticket_time_cost != []:
    sub_str4 = '<p><font size="4" face="Microsoft YaHei"><li>Team member time cost of picking up tickets</font></li></p><br>'
    sub_str4 += '<table>'
    for img in ticket_time_cost:
        index = index + 1
        if index % 2 != 0:
            sub_str4 += "<tr>"
            sub_str4 += '<td><img src="cid:' + img +'"></tb>'
        else:
            sub_str4 += '<td><img src="cid:' + img +'"></tb>'
            sub_str4 += '</tr>'
            

    sub_str4 += '</table><br><br>'


    
sub_str5 = '<p><font size="3" face="Microsoft YaHei">Regards</font></p><p><font size="3" face="Microsoft YaHei">Ella</font></p><br><br>'


contentStr = title + sub_str0 + sub_str1 + sub_str2 + sub_str3 + sub_str4 + sub_str5



msgText = MIMEText(contentStr,'html','utf-8')


msgRoot.attach(msgText)
xlsxpart = MIMEApplication(open('TeamTimeSheet.xlsx', 'rb').read())
xlsxpart.add_header('Content-Disposition', 'attachment', filename='TeamTimeSheet.xlsx')
msgRoot.attach(xlsxpart)


def addImage(fileName):
    fp = open("img\\"+fileName, 'rb')
    msgImage = MIMEImage(fp.read())
    fp.close()
    msgImage.add_header('Content-ID', '<%s>' %(fileName))
    msgRoot.attach(msgImage)
    
for file_name in file_list:
    print 'list'
    print file_name
    addImage(file_name)



smtp = smtplib.SMTP(smtpserver, 587)
smtp.starttls()
smtp.login(username, password)
smtp.sendmail(sender, receiver, msgRoot.as_string())
smtp.quit()
