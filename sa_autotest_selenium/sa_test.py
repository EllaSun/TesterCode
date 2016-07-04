# coding:GBK
import unittest
from case import data_verify

if __name__ == "__main__":
    unittest.main()
suite = unittest.makeSuite(DataVerifyCase,'test') 
runner = unittest.TextTestRunner(verbosity=1)
runner.run(suite)
