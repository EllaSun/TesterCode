#encoding=gbk
#! /usr/bin/python

import ConfigParser
import os
import struct

def get_conf_value(section,key):
    '''获取配置项'''
    cf = ConfigParser.ConfigParser()
    conf_file = os.path.dirname(os.path.realpath(__file__))+'/../conf/mock_server.conf'
    cf.read(conf_file)
    return cf.get(section,key)

