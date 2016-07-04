
from baseLib import base_debug
from baseLib import base_conf
from lib import pbd_init


class pbd_debug(base_debug.Debug):
	def __init__(self,script):
		self.init = pbd_init.pbd_init
		self.script_dir = self.init.script_dir


		full_path = self.script_dir + "/" + script
		section="test"
		debug_dir = self.init.getOpt(section, "debug_dir")
		base_debug.Debug.__init__(self,full_path, debug_dir)


		head_search = self.init.getOpt(section, "head")
		ctr_begin_search = self.init.getOpt(section, "get_Q1Q2_by_pv_ctr_begin")
		ctr_end_search = self.init.getOpt(section, "get_Q1Q2_by_pv_ctr_end")
		query_b_search = self.init.getOpt(section, "get_queryword_industry_begin")
		query_e_search = self.init.getOpt(section, "get_queryword_industry_end")
		account_b_search = self.init.getOpt(section, "get_account_industry_begin")
		account_e_search = self.init.getOpt(section, "get_account_industry_end")
		history_b_search = self.init.getOpt(section, "history_protect_begin")
		history_e_search = self.init.getOpt(section, "history_protect_end")
		brand_b_search = self.init.getOpt(section, "brand_protect_begin")
		brand_e_search = self.init.getOpt(section, "brand_protect_end")
		reverse_b_search = self.init.getOpt(section, "reverse_1hourbefore_Q1Q2_begin")
		reverse_e_search = self.init.getOpt(section, "reverse_1hourbefore_Q1Q2_end")
		filter_b_search = self.init.getOpt(section, "filter_black_begin")
		filter_e_search = self.init.getOpt(section, "filter_black_end")
 
		#head
		head_line = self.locate(head_search)
		#get_Q1Q2_by_pv_ctr
		ctr_section = self.section(ctr_begin_search, ctr_end_search)
		self.ctr_script = self.sub_script(head_line, ctr_section, "ctr")
		#get_queryword_insdustry
		query_section = self.section(query_b_search, query_e_search)
		self.query_script = self.sub_script(head_line, query_section,"query")
		#get_account_industry
		account_section = self.section(account_b_search, account_e_search)
		self.account_script = self.sub_script(head_line, account_section, "account")
		#history_protect
		history_section = self.section(history_b_search, history_e_search)
		self.history_script = self.sub_script(head_line, history_section, "history")
		#brand_protect
		brand_section = self.section(brand_b_search, brand_e_search)
		self.brand_script = self.sub_script(head_line, brand_section, "brand")
		#reverse_1hourbefore_Q1Q1
		reverse_section = self.section(reverse_b_search, reverse_e_search)
		self.reverse_script = self.sub_script(head_line, reverse_section, "reverse")
		#filter_black
		filter_section = self.section(filter_b_search, filter_e_search)
		self.filter_script = self.sub_script(head_line, filter_section, "filter")
		

 
