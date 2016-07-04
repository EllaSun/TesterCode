# coding:GBK
from baseLib import base_init

priceservercfg_g = "/opt/autoTest/pyunit/priceserver/conf/priceserver.cfg"
priceclientcfg_g = "/opt/autoTest/pyunit/priceserver/conf/priceclient.cfg"

class PriceServerInit(base_init.Init):
	def __init__(self):
		base_init.Init.__init__(self, priceservercfg_g)

class PriceClientInit(base_init.Init):
	def __init__(self):
		base_init.Init.__init__(self, priceclientcfg_g)


priceserver_init = PriceServerInit()
priceclient_init = PriceClientInit()
