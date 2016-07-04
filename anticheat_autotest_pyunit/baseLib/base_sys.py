# coding:GBK
import os,time,sys
import string


def show(res, sg ='#', num=30):
	'''按格式打印字符'''
	print sg * num
	print res
	print sg * num

def getLocalIP():
	cmd = "ifconfig |grep 'inet addr:' |awk '{print $2}'"
	res = os.popen(cmd).read()
	return res.split('\n')[0].split(':')[1]

def getRegionIP(region):
	regionDic = {}
	regionDic['0']	= "1.2.3.4"			# 火星 
	regionDic['1']	= "124.205.93.2"	# 北京
	regionDic['2']	= "114.86.23.56"	# 上海
	regionDic['3']	= "123.150.135.121"	# 天津
	regionDic['4']	= "222.183.16.208"	# 重庆
	regionDic['5']	= "60.167.230.178"	# 安徽 
	regionDic['6']	= "218.5.17.134"	# 福建
	regionDic['7']	= "61.178.88.236"	# 甘肃 
	regionDic['8']	= "116.23.220.77"	# 广东
	regionDic['9']	= "219.159.231.161"	# 广西 
	regionDic['10']	= "58.42.246.28"	# 贵州
	regionDic['11']	= "59.50.72.82"		# 海南 
	regionDic['12']	= "219.148.83.41"	# 河北 
	regionDic['13']	= "123.55.51.84"	# 河南
	regionDic['14']	= "222.171.24.214"	# 黑龙江 
	regionDic['15']	= "119.99.26.226"	# 湖北 
	regionDic['16']	= "220.170.15.98"	# 湖南 
	regionDic['17']	= "222.169.187.232"	# 吉林
	regionDic['18']	= "117.84.168.20"	# 江苏
	regionDic['19']	= "117.40.129.160"	# 江西
	regionDic['20']	= "59.44.142.118"	# 辽宁
	regionDic['21']	= "123.178.247.186"	# 内蒙古
	regionDic['22']	= "119.60.215.71"	# 宁夏
	regionDic['23']	= "125.72.155.230"	# 青海
	regionDic['24']	= "113.125.40.55"	# 山东
	regionDic['25']	= "110.183.149.23"	# 山西
	regionDic['26']	= "117.34.163.214"	# 陕西
	regionDic['27']	= "125.67.231.103"	# 四川
	regionDic['28']	= "220.182.20.237"	# 西藏
	regionDic['29']	= "124.118.58.134"	# 新疆
	regionDic['30']	= "112.112.5.146"	# 云南
	regionDic['31']	= "125.110.75.156"	# 浙江
	regionDic['33']	= "124.9.49.66"		# 台湾
	return str(regionDic.get(region))

def getTime():
	return time.strftime('%Y%m%d %H:%M:%S',time.localtime(time.time()))
