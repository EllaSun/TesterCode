import smtplib
from os import listdir
import sys
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
from email.mime.image import MIMEImage

sender = 'ella.sun@plexure.com'
subject = 'QA Team TimesSheet Check Result'
smtpserver = 'smtp.office365.com'

username = sys.argv[1]
password = sys.argv[2]
receiver_str = sys.argv[3]
receiver = receiver_str.split(",")
strTM = sys.argv[4]

msgRoot = MIMEMultipart('related')
msgRoot['Subject'] = subject  +'(' + strTM + ')'

title = '<p><font size="6" face="Microsoft YaHei" align="center">Data Source Check Result('+strTM+'):</font></p><br><br>'
sub_str0 = ''
file_list = listdir("input")
for file_name in file_list:
    sub_str0 += "<br>"+file_name+"</br>"


sub_str1 = '<p><font size="3" face="Microsoft YaHei">Regards</font></p><p><font size="3" face="Microsoft YaHei">Ella</font></p><br><br>'

contentStr = title + sub_str0 + sub_str1
msgText = MIMEText(contentStr,'html','utf-8')


msgRoot.attach(msgText)
smtp = smtplib.SMTP(smtpserver, 587)
smtp.starttls()
smtp.login(username, password)
smtp.sendmail(sender, receiver, msgRoot.as_string())
smtp.quit()
    
