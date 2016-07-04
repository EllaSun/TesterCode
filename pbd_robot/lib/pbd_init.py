# coding:GBK
from baseLib import base_init

pbd_cfg_g = "/search/odin/autotest/pbd/pybot/conf/pbd.cfg"

class Pbd_init(base_init.Init):
	def __init__(self):
		base_init.Init.__init__(self, pbd_cfg_g)



pbd_init = Pbd_init()
