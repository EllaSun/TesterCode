import unittest

from case import antiCheatRollbackSogou
from case import antiCheatRollbackBd
from case import antiCheatRollbackOther
from case import antiCheatUcweb
from case import antiCheatBlackList
from case import antiCheatNAPSogou
from case import antiCheatNAPBd
from case import antiCheatNAPOther
from case import antiCheatNAPLetgo
from case import antiCheatAllRules
from case import antiCheatDt
from case import antiCheatInterface
from case import antiCheatPidProtect
from case import antiCheatFrame
from case import antiCheat5AP
from case import antiCheatAP1
from case import antiCheatAP2
from case import antiCheatEmerge
from case import antiCheatLetgo
from case import antiCheatPidRealTimeProtect 
from case import antiCheatCtr
from case import antiCheatIPC 
from case import antiCheatPidConsume
from case import antiCheatOther
from case import antiCheatRulesCall
from case import antiCheatLu
from case import antiCheatMd5
from case import antiCheatXmlWhiteIp
from case import antiCheatUser
from case import antiCheatHash
from case import antiCheatLt
from case import antiCheatReload
from case import antiCheatCC

if __name__ == '__main__':
	runner = unittest.TextTestRunner(verbosity = 2)
	#runner.run(antiCheatRollbackSogou.suite())
	#runner.run(antiCheatRollbackBd.suite())
	#runner.run(antiCheatRollbackOther.suite())
	#runner.run(antiCheatPidProtect.suite())
	#runner.run(antiCheatUcweb.suite())
	#runner.run(antiCheatNAPSogou.suite())
	#runner.run(antiCheatNAPBd.suite())
	#runner.run(antiCheatNAPLetgo.suite())
	#runner.run(antiCheatAllRules.suite())
	#runner.run(antiCheat5AP.suite())
	#runner.run(antiCheatBlackList.suite())
	#runner.run(antiCheatAP1.suite())
	runner.run(antiCheatCC.suite())
	#runner.run(antiCheatAP2.suite())
	#runner.run(antiCheatDt.suite())
	#runner.run(antiCheatEmerge.suite())
	#runner.run(antiCheatLetgo.suite())
	#runner.run(antiCheatFrame.suite())
	#runner.run(antiCheatInterface.suite())
	#runner.run(antiCheatMd5.suite())
	#runner.run(antiCheatPidRealTimeProtect.suite())
	#runner.run(antiCheatCtr.suite())
	#runner.run(antiCheatIPC.suite())
	#runner.run(antiCheatPidConsume.suite())
	#runner.run(antiCheatOther.suite())
	#runner.run(antiCheatRulesCall.suite())
	#runner.run(antiCheatLu.suite())
	#runner.run(antiCheatXmlWhiteIp.suite())
	#runner.run(antiCheatUser.suite())
	#runner.run(antiCheatHash.suite())
	#runner.run(antiCheatNAPOther.suite())
	#runner.run(antiCheatReload.suite())
