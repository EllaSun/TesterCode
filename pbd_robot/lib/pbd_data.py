#coding: gbk
import sys
import time
import commands
from baseLib import base_data
from baseLib import base_conf
from lib import pbd_init


reload(sys)
sys.setdefaultencoding('gbk') 


sleepTime = 0.5
class pbd_data(base_data.Data):
	def __init__(self):
		base_data.Data.__init__(self)
		self.init = pbd_init.pbd_init
		self.conf = self.init.conf_dir
		self.path = self.init.home_dir
		self.input = self.init.input_dir
		self.data = self.init.data_dir
		self.result = self.init.result_dir
	#set
	def set_input(self, filename, args):
		line = "\t".join(args)
		self.write(self.input+filename, line)
	
	def set_result(self, filename, args):
		line = "\t".join(args) 
		self.write(self.result+filename, line)

	def set_data(self, filename, args):
		line = "\t".join(args)
		self.write(self.data+filename, line)


	def set_ca(self, filename, args):
		line = '\3'.join(args)
		self.write(self.input+filename, line)
	
	def set_cd_log(self, filename, args):
		pid = args[0]
		adid = args[1]
		accountid = args[2]
		reserve = args[3]
		keyword = args[4]
		ret = args[5]
		search_word = args[6]
		service_type = args[7]
		line = "1375413744\t123.1.1.1\t" + pid + "\t3EB46ACA35E20D0A0000000050FF9F20|FBB55B6C3446C93C413A23CB58AA693A\t25747645\t" 
		line+= adid + "\t" + accountid + "\t93\t" + reserve + "\t" + keyword + "\t2\thttp://www.sogou.com\t" + ret +"\t" 
		line+= search_word + "\tffffffffffffffff\thttp://www.sogou.com/\t0ffec29d021374b4248827157a1db21a\t"
		line+= "http://www.XianHua.com.cn/vip.php?u=1253\t3\t10.11.194.157\t0\t8,109\t"
		line+= service_type + "\t-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,\t0\t257\t246677225\t135019\t2\tbill\t"
		line+= "406,0,375,152,372,155,1419,687\t0\t0\t\t0\t612902677\t456\t338690049\t0\t1100\t456\t0\t1\t0\t2120605\t2120605\t"
		line+= "1\t\t123.126.51.81\t0\t0\t7A88A7F396632089641A134B2824D0D7" 
		self.write(self.input+filename, line)
	
	def set_pv_log(self, filename, args):
		pid = args[0]
		adid_list = args[1]
		accountid_list =  args[2]
		reserve_list = args[3]
		key_list = args[4]
		query_list = args[5]
		service_type = "10100"
		if len(args) > 6:
			service_type = args[6]
		line = "1372087800\t116.16.132.78\t" + pid + "\t4E8410747A430E0A0000000051C863B7|0\t\t"+adid_list+"\t"+accountid_list
		line+= "\t\t"+reserve_list+"\t"+""+key_list+"\t356\t\t"+query_list+"\t10.14.18.18\te5d94051-69aa-44ad-8616-d150390deb7e\t"
		line+= "\t807797640\t210c1025b3f98014\t\t\t\t0\t0\t0\t\t0\t\t\t\t"+service_type+"\t\t\t\t\t\t\t\t\t\t0\t\t657272"
		line+= "\t\t255\tweb\t\t\t\ta2;a\t0\t\t0" 
		self.write(self.input+filename, line)

	#clear
	def clear_input(self):
		self.rmBatch(self.input+"/")

	def clear_result(self):
		self.rmBatch(self.result+"/")

	#touch
	def touch_input(self, filename):
		self.touch(self.input+filename)

	def touch_result(self, filename):
		self.touch(self.result+filename)

	def touch_data(self, filename):
		self.touch(self.data+filename)	


	#append
	def append_input(self, filename, args):
		line = "\t".join(args)
		self.append(self.input+filename, line)
	
	def append_result(self, filename, args):
		line = "\t".join(args)
		self.append(self.result+filename, line)
	
	def append_data(self, filename, args):
		line = "\t".join(args)
		self.append(self.data+filename, line)


	def append_cd_log(self, filename, args):
		pid = args[0]
		accountid = args[1]
		keyword = args[2]
		ret = args[3]
		price = args[4]
		service_type = args[5]
		line = "1375413744\t123.1.1.1\t" + pid + "\t3EB46ACA35E20D0A0000000050FF9F20|FBB55B6C3446C93C413A23CB58AA693A\t25747645\t" 
		line+= "22222" + "\t" + accountid + "\t93\t" + "2334" + "\t" + keyword + "\t2\thttp://www.sogou.com\t" + ret +"\t" 
		line+= search_word + "\tffffffffffffffff\thttp://www.sogou.com/\t0ffec29d021374b4248827157a1db21a\t"
		line+= "http://www.XianHua.com.cn/vip.php?u=1253\t"+price+"\t10.11.194.157\t0\t8,109\t"
		line+= service_type + "\t-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,\t0\t257\t246677225\t135019\t2\tbill\t"
		line+= "406,0,375,152,372,155,1419,687\t0\t0\t\t0\t612902677\t456\t338690049\t0\t1100\t456\t0\t1\t0\t2120605\t2120605\t"
		line+= "1\t\t123.126.51.81\t0\t0\t7A88A7F396632089641A134B2824D0D7" 
		self.append(self.input+filename, line)
	
	def append_pv_log(self, filename, args):
		pid = args[0]
		adid_list = args[1]
		accountid_list =  args[2]
		reserve_list = args[3]
		key_list = args[4]
		query_list = args[5]
		if len(args) > 6:
			service_type = args[6]
		line = "1372087800\t116.16.132.78\t" + pid + "\t4E8410747A430E0A0000000051C863B7|0\t\t"+adid_list+"\t"+accountid_list+"\t\t"+reserve_list+"\t"+""+key_list+"\t356\t\t"+query_list+"\t10.14.18.18\te5d94051-69aa-44ad-8616-d150390deb7e\t\t807797640\t210c1025b3f98014\t\t\t\t0\t0\t0\t\t0\t\t\t\t"+service_type+"\t\t\t\t\t\t\t\t\t\t0\t\t657272\t\t255\tweb\t\t\t\ta2;a\t0\t\t0" 
		self.append(self.input+filename, line)

	def append_ca(self, filename, args):
		line = '\3'.join(args)
		self.append(self.input+filename, line)

	def get_new_click(self, filename):
		time.sleep(sleepTime)
		content = self.readLine(self.result+filename, 0)
		item =content.split("\t")[4]
		return item

	def get_pid_type(self, filename, pid):
		time.sleep(sleepTime)
		cmd = "grep " + pid + " " +  self.result + filename + "|awk '{print $2}'"
		cmd1 = "grep " + adid + " " +  self.result + filename 
		status, output = commands.getstatusoutput(cmd1)
		if status != 0:
			return None
		status, output = commands.getstatusoutput(cmd)
		if status == 0:
			return output
		else:
			return None	

	def get_industry_list(self, filename):
		time.sleep(sleepTime)
		content = self.readLine(self.result+filename, 0)
		adid = content.split("\t")[0]
		accid = content.split("\t")[1]
		keyword = content.split("\t")[2] 
		return adid, accid, keyword

	def get_industry_kwd(self, filename):
		time.sleep(sleepTime)
		content = self.readLine(self.result+filename, 0)
		return content 

	def get_kwd_industry(self, filename):
		time.sleep(sleepTime)
		content = self.readLine(self.result+filename, 0)
		kwd = content.strip("\t")[0]
		industry = content.split('\t')[1]
		return kwd,industry

	def get_acc_industry(self, filename):
		time.sleep(0.5)
		line = self.readLine(data.result+filename, 0)
		industry = line.split('\t')[3] 
		return industry

	def get_pv_click(self, filename, type):
		time.sleep(sleepTime)
		cmd = "awk '$5==" + type + "{print $6}'" + " " + self.result + filename
		print cmd
		status, output = commands.getstatusoutput(cmd)
		if status == 0:
			return output
		else:
			return None
		
	def get_acc_industry(self, filename):
		time.sleep(sleepTime)
		content = self.readLine(self.result+filename, 0)
		return content.split('\t')[3]		

	def get_industry_list_tmp3(self, filename):
		time.sleep(sleepTime)
		content = self.readLine(self.result+filename, 0)
		return content.split('\t')[-2],content.split('\t')[-1]
		


