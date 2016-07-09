import os,time,sys
import string


def show(res, sg ='#', num=30):
	'''print in a easyreadable way'''
	print sg * num
	print res
	print sg * num

def getLocalIP():
	cmd = "ifconfig |grep 'inet addr:' |awk '{print $2}'"
	res = os.popen(cmd).read()
	return res.split('\n')[0].split(':')[1]

def getRegionIP(region):
	regionDic = {}
	regionDic['0']	= "1.2.3.4"			# out of earth 
	regionDic['1']	= "124.205.93.2"	# beijing
	regionDic['2']	= "114.86.23.56"	# shanghai
	regionDic['3']	= "123.150.135.121"	# tianjin
	regionDic['4']	= "222.183.16.208"	# chongqing
	regionDic['5']	= "60.167.230.178"	# anhui
	regionDic['6']	= "218.5.17.134"	# fujian
	regionDic['7']	= "61.178.88.236"	# gansu
	regionDic['8']	= "116.23.220.77"	# guangdong
	regionDic['9']	= "219.159.231.161"	# guangxi
	regionDic['10']	= "58.42.246.28"	# guizhou
	regionDic['11']	= "59.50.72.82"		# hainan
	regionDic['12']	= "219.148.83.41"	# hebei
	regionDic['13']	= "123.55.51.84"	# henan
	regionDic['14']	= "222.171.24.214"	# heilongjiang 
	regionDic['15']	= "119.99.26.226"	# hubei
	regionDic['16']	= "220.170.15.98"	# hunan
	regionDic['17']	= "222.169.187.232"	# jilin
	regionDic['18']	= "117.84.168.20"	# jiangsu
	regionDic['19']	= "117.40.129.160"	# jiangxi
	regionDic['20']	= "59.44.142.118"	# liaoning
	regionDic['21']	= "123.178.247.186"	# neimenggu
	regionDic['22']	= "119.60.215.71"	# ningxia
	regionDic['23']	= "125.72.155.230"	# qianghai
	regionDic['24']	= "113.125.40.55"	# shandong
	regionDic['25']	= "110.183.149.23"	# shanxi1
	regionDic['26']	= "117.34.163.214"	# shanxi2 
	regionDic['27']	= "125.67.231.103"	# sichuan
	regionDic['28']	= "220.182.20.237"	# qingzang
	regionDic['29']	= "124.118.58.134"	# xinjiang
	regionDic['30']	= "112.112.5.146"	# yunnan
	regionDic['31']	= "125.110.75.156"	# zhejiang
	regionDic['33']	= "124.9.49.66"		# taiwan
	return str(regionDic.get(region))

def getTime():
	return time.strftime('%Y%m%d %H:%M:%S',time.localtime(time.time()))
