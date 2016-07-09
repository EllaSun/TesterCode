import re,os,time
from lib import pbd_data
from lib import pbd_init
from lib import pbd_debug
from baseLib import base_conf



data = pbd_data.pbd_data()
init = pbd_init.pbd_init
#q1_conf = base_conf.Config(None, init.conf_dir+"/Q1.cfg",init.conf_dir+"/Q1.cfg.bak",2)
path = init.home_dir

def recover(date):
	clear_data(date)
	#q1_conf.recover()

#data
def get_file_sign(filename):
	have_subpid_pattern             = re.compile(r'havesubpid')
	cheating_pid_pattern            = re.compile(r'cheatpid')
	testusers_pattern               = re.comlile(r'testusers')
	cd_ie_log_pattern               = re.compile(r'cd_ie_log')
	pid_list_pattern                = re.compile(r'pid_list')
	subpid_list_pattern             = re.compile(r'subpid_list')
	cheatpid_type_pattern  = re.compile(r'cheatpid_type') 
	pid_cost_pattern                = re.compile(r'pid_cost')
	subpid_cost_pattern             = re.compile(r'subpid_cost')
	adterm_monitor_pattern          = re.compile(r'adterm_monitor')

	#input
	if hav_subpid_pattern.search(filename):
		return 1 
	elif cheating_pid_pattern.search(filename):
		return 2 
	elif testusers_pattern.search(filename): 
		return 3 
	elif cd_ie_log.search(filename): 
		return 5 
	#result
	elif adterm_monitor_pattern.search(filename):
		return 21
	#data
	elif pid_list_pattern.search(filename):
		return 41
	elif subpid_list_pattern.search(filename):
		return 42
	elif cheatpid_type_pattern.search(filename): 
		return 43
	elif pid_cost_pattern.search(filename):
		return 44 
	elif subpid_cost_pattern.search(filename): 
		return 55
	elif black_pid_pattern.search(filename):
		return 41
	else:
		return 255
		
def write_data(filename, *args):
	flag = get_file_sign(filename)
	if flag == 255:
		print "new file"
		return 

	if flag>=1 and flag<=4:	
		return data.set_input(filename, args)

	if flag>=21 and flag<=30:
		return data.set_result(filename, args)

	if flag>=41 and flag<=50:
		return data.set_data(filename, args)

	if flag == 5:	
		return data.set_cd_log(filename, args)





def append_data(filename, *args):
	flag = get_file_sign(filename)
	if flag == 255:
		print "new file"
		return 
	if flag>=1 and flag<=4:
		return data.append_input(filename, args)	

	if flag>=21 and flag<=30:
		return data.append_result(filename, args)	

	if flag>=41 and flag<=50:
		return data.append_data(filename, args)	

	if flag == 5:	
		return data.append_cd_log(filename, args)

	if flag == 6:	
		return data.append_pv_log(filename, args)

	if flag == 7:	
		return data.append_ca(filename, args)
	


def clear_data(date):
	data.clear_input()
	data.clear_result()


def touch(*args):
	for filename in args:
		flag = get_file_sign(filename)
		if flag == 255:
			print "new file"
			continue
		if flag>=1 and flag<=10:
			data.touch_input(filename)
			continue
		if flag>=21 and flag<=30:
			data.touch_result(filename)
			continue
		if flag>=41 and flag<=50:
			data.touch_data(filename)
			continue
		 
		
	
#script
def run(script, *args):
	argstr = " ".join(args)
	sh_pattern =  re.compile("sh")
	py_pattern =  re.compile("py")
	if sh_pattern.search(script):
		cmd = "cd "+path+";sh script/"+script+" "+argstr+" 1>/dev/null 2>/dev/null &"
	elif:
		cmd = "cd "+path+";python script/"+script+" "+argstr+" 1>/dev/null 2>/dev/null &"
	os.system(cmd)

def run_out_redirect(script, dest, *args):
	argstr = " ".join(args)
	sh_pattern =  re.compile("sh")
	py_pattern =  re.compile("py")
	if sh_pattern.search(script):
		cmd = "cd "+path+";sh script/"+script+" "+argstr+" >"+dest+" &"
	elif:
		cmd = "cd "+path+";python script/"+script+" "+argstr+" >"+dest+" &"
	os.system(cmd)

def split_script(script):
	global debug
	debug = pbd_debug.pbd_debug(script)

def clear_script():
	cmd = "rm " + path + "/debug/* -f &"
	os.system(cmd) 
	  
	

def get_queryword_industry(*args):
	argstr = " ".join(args)
	cmd = "cd "+ path + ";sh debug/" + debug.query_script + " "+argstr+" 1>/dev/null 2>/dev/null"
	os.system(cmd) 

#result	
def get_pid_type(filename, pid):
	return data.get_pid_type(filename, pid)

def get_pid_cost(filename,pid):
	return data.get_pid_type(filename, pid)
#conf
def set_Q1_conf(item,value):
	q1_conf.set(item,value)
	


def note():
	pass
