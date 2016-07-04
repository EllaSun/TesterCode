#coding:gbk

import os,sys,subprocess,struct,string

try:
    import thrift
except:
    path = os.popen('pwd').read().strip() + '/mock_protocol/body/'
    os.system('sh '+path+'install_thrift.sh')
    

from ThriftProtocol import BinaryProtocol
from thrift.Thrift import TType,TMessageType
from thrift.protocol.TBinaryProtocol import TBinaryProtocol
from mock_lib.conf import get_conf_value



class TriftData:

    def __init__(self):
        self.file_path = os.path.dirname(os.path.abspath(__file__))
        self.thrift_file = get_conf_value('thrift','thrift_idl')
        subprocess.call(['thrift','--gen','py','-o',self.file_path+'/../mock_protocol/body/',self.file_path+'/../mock_protocol/body/'+self.thrift_file])
        self.fun_name = get_conf_value('thrift','thrift_function')
        self.service_name = os.popen('grep "^service" '+self.file_path+"/../mock_protocol/body/"+self.thrift_file).read().strip().split(' ')[1]
        __import__("mock_protocol.body.gen-py."+self.thrift_file.split(".")[0]+"."+self.service_name)
        self.service_object = sys.modules["mock_protocol.body.gen-py."+self.thrift_file.split(".")[0]+"."+self.service_name]
        self.req_thrift = getattr(self.service_object,self.fun_name+'_args')
        self.resp_thrift = getattr(self.service_object,self.fun_name+'_result')
        self.proto_type = BinaryProtocol(int(get_conf_value('thrift','thrift_proto_type')))



    def package(self,dict_data,stub_type):
        '''transfer dict data to binary string var thrift protocol'''
        data_str=''
        if 2 == stub_type:
            data_str+=self.proto_type.thrift_proto(self.req_thrift,dict_data,self.fun_name,stub_type)
        elif 1 == stub_type:
            data_str+=self.proto_type.thrift_proto(self.resp_thrift,dict_data,self.fun_name,stub_type)
        return data_str


    def unpackage(self,data_str,stub_type):
        '''transfer binary string to dict data var thrift protocol'''
        dict_data={}
        cur=0
        if stub_type == 2:
            dict_data = self.proto_type.list_thrift_proto(self.resp_thrift,data_str,cur)
        elif stub_type == 1:
            dict_data = self.proto_type.list_thrift_proto(self.req_thrift,data_str,cur)
        return dict_data
            

