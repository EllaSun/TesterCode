import imaplib
import email
import os
import time
import sys

if len(sys.argv) != 4:
    print len(sys.argv)
    print "arg number should be equal to 4"
    exit(-1)


username = sys.argv[1]
password = sys.argv[2]

mail_service = "outlook.office365.com"
detach_dir = "C:\Ella\mycode\performancereport\\input\\"
#strTM = time.strftime('%d%m%Y',time.localtime(time.time()))
#strTM1 = time.strftime('%d-%m-%Y',time.localtime(time.time()))
strTM = sys.argv[3]
strTM1 = strTM.replace('/','-')

m = imaplib.IMAP4_SSL(mail_service)
m.login(username, password)
m.select("Inbox/TimeSheet")

subject_title= "TimeSheet-" + strTM
print subject_title
resp, items = m.search(None, '(Subject '+subject_title+ ')')
items = items[0].split()

names = []



for emailid in items:
    resp, data = m.fetch(emailid, "(RFC822)") 
    email_body = data[0][1] 
    mail = email.message_from_string(email_body) 


    if mail.get_content_maintype() != 'multipart':
        continue

    #print "["+mail["From"]+"] :" + mail["Subject"]

    for part in mail.walk():
        if part.get_content_maintype() == 'multipart':
            continue
        if part.get('Content-Disposition') is None:
            continue

        authorName = mail["From"].split(' ')[0]
        print authorName
        filename = authorName + '_' + strTM1 + '.xlsx'
        if authorName in names:
            names.remove(authorName)
            
            #delete the file
            os.remove(detach_dir+filename)
            print "delete:", detach_dir+filename
        names.append(authorName)

 
        att_path = os.path.join(detach_dir, filename)
        print att_path

        if not os.path.isfile(att_path) :
            fp = open(att_path, 'wb+')
            fp.write(part.get_payload(decode=True))
            fp.close()
m.logout()
#m.close()
