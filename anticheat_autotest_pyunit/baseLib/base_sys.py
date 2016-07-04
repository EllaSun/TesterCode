# coding:GBK
import os,time,sys
import string


def show(res, sg ='#', num=30):
	'''����ʽ��ӡ�ַ�'''
	print sg * num
	print res
	print sg * num

def getLocalIP():
	cmd = "ifconfig |grep 'inet addr:' |awk '{print $2}'"
	res = os.popen(cmd).read()
	return res.split('\n')[0].split(':')[1]

def getRegionIP(region):
	regionDic = {}
	regionDic['0']	= "1.2.3.4"			# ���� 
	regionDic['1']	= "124.205.93.2"	# ����
	regionDic['2']	= "114.86.23.56"	# �Ϻ�
	regionDic['3']	= "123.150.135.121"	# ���
	regionDic['4']	= "222.183.16.208"	# ����
	regionDic['5']	= "60.167.230.178"	# ���� 
	regionDic['6']	= "218.5.17.134"	# ����
	regionDic['7']	= "61.178.88.236"	# ���� 
	regionDic['8']	= "116.23.220.77"	# �㶫
	regionDic['9']	= "219.159.231.161"	# ���� 
	regionDic['10']	= "58.42.246.28"	# ����
	regionDic['11']	= "59.50.72.82"		# ���� 
	regionDic['12']	= "219.148.83.41"	# �ӱ� 
	regionDic['13']	= "123.55.51.84"	# ����
	regionDic['14']	= "222.171.24.214"	# ������ 
	regionDic['15']	= "119.99.26.226"	# ���� 
	regionDic['16']	= "220.170.15.98"	# ���� 
	regionDic['17']	= "222.169.187.232"	# ����
	regionDic['18']	= "117.84.168.20"	# ����
	regionDic['19']	= "117.40.129.160"	# ����
	regionDic['20']	= "59.44.142.118"	# ����
	regionDic['21']	= "123.178.247.186"	# ���ɹ�
	regionDic['22']	= "119.60.215.71"	# ����
	regionDic['23']	= "125.72.155.230"	# �ຣ
	regionDic['24']	= "113.125.40.55"	# ɽ��
	regionDic['25']	= "110.183.149.23"	# ɽ��
	regionDic['26']	= "117.34.163.214"	# ����
	regionDic['27']	= "125.67.231.103"	# �Ĵ�
	regionDic['28']	= "220.182.20.237"	# ����
	regionDic['29']	= "124.118.58.134"	# �½�
	regionDic['30']	= "112.112.5.146"	# ����
	regionDic['31']	= "125.110.75.156"	# �㽭
	regionDic['33']	= "124.9.49.66"		# ̨��
	return str(regionDic.get(region))

def getTime():
	return time.strftime('%Y%m%d %H:%M:%S',time.localtime(time.time()))
