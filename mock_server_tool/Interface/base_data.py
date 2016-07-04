#coding:gbk

from lib import proto_data,thrift_data

class Data:

    def __init__(self,data_type):
        #1-protobuf,2-thrift
        if "protobuf" == data_type:
            self.data = proto_data.ProtoData()
        elif "thrift" == data_type:
            self.data = thrift_data.TriftData()

    def package(self,dict_data,stub_type):
        self.string_data = self.data.package(dict_data,stub_type)
        return self.string_data

    def unpackage(self,string_data,stub_type):
        self.dict_data = self.data.unpackage(string_data,stub_type)
        return self.dict_data

