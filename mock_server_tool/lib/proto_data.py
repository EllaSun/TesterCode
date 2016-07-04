#coding:gbk

import subprocess,os,sys,re

from mock_lib.conf import get_conf_value

class ProtoData:

    def __init__(self):
        self.file_path = os.path.dirname(os.path.abspath(__file__))
        self.proto_file=get_conf_value('proto_buf','proto_buf')
        subprocess.call(["sh",self.file_path+"/../mock_protocol/body/create_pro_for_python.sh",self.proto_file])
        (self.file_name,self.ext)=os.path.splitext(self.proto_file)
        self.proto_import_module=self.file_name+"_pb2"
        __import__("mock_protocol.body."+self.proto_import_module)
        self.proto_py = sys.modules["mock_protocol.body."+self.proto_import_module]
        #protobuf object
        self.req_proto = getattr(self.proto_py,get_conf_value('proto_buf','proto_request'))()
        self.resp_proto = getattr(self.proto_py,get_conf_value('proto_buf','proto_response'))()
        #output
        self.out_put_str=[""]


    def package(self,dict_data,stub_type):
        '''transfer dict data to string data var protobuffer protocol'''
        if stub_type == 1:
            self.__proto_buf(self.resp_proto,dict_data)
            self.string_data = getattr(self.resp_proto,"SerializeToString")()
        elif stub_type == 2:
            self.__proto_buf(self.req_proto,dict_data)
            self.string_data = getattr(self.req_proto,"SerializeToString")()
        return self.string_data

    def unpackage(self,string_data,stub_type):
        '''transfer string data to dict data var protobuffer protocol'''
        self.dict_recv = {}
        if stub_type == 1:
            getattr(self.req_proto,"ParseFromString")(string_data)
            self.__list_proto_buf(self.req_proto, self.dict_recv)
        elif stub_type == 2:
            getattr(self.resp_proto,"ParseFromString")(string_data)
            self.__list_proto_buf(self.resp_proto, self.dict_recv)
        return self.dict_recv
 
    def __proto_buf(self, message_object, value={}):
        for key in value.keys():
            self.message_item_type=type(getattr(message_object,key)).__name__
            if self.message_item_type in ('str','int','float','bool','long'):
                setattr(message_object,key,eval(str(self.message_item_type)+"('"+str(value[key])+"')"))
                continue
            if self.message_item_type == "RepeatedCompositeFieldContainer":
                for repeat_item in value[key]:
                    self.message_object_add = getattr(message_object,key).add()
                    self.__proto_buf(self.message_object_add,repeat_item)
                    continue
            elif self.message_item_type == "RepeatedScalarFieldContainer":
                if not isinstance(value[key],list):
                    raise "the parameter : "+str(key)+" in protobuf the value type should be list not "+type(value[key]).__name__
                else: getattr(message_object,key).extend(value[key])
            else:
                self.message_object_proto = getattr(message_object,key)
                self.__proto_buf(self.message_object_proto,value[key])
                continue
        return 


    def __list_proto_buf(self,proto_buf_instance,result_info={}):
        items = filter(lambda x : re.match('^[a-z]',x) , dir(proto_buf_instance))
        for i in items:
            if isinstance(getattr(proto_buf_instance,i),(int,str,bool,float,long)):
                self.out_put_str[0] += str(i)+" : "+str(getattr(proto_buf_instance,i))+"\n"
                result_info[str(i)]=str(getattr(proto_buf_instance,i))
            elif type(getattr(proto_buf_instance,i)).__name__ == "RepeatedCompositeFieldContainer":
                self.out_put_str[0] += str(i) + " : \n"
                self.out_put_str[0] += '*'*40+"\n"
                result_info[str(i)]=[]
                for vec_item in getattr(proto_buf_instance,i):
                    #self.out_put_str[0] += '*'*20+"\n"
                    self.repeat_info_temp={}
                    self.__list_proto_buf(vec_item,self.repeat_info_temp)
                    result_info[str(i)].append(self.repeat_info_temp)
                    self.out_put_str[0] += '+'*30+"\n"
                self.out_put_str[0] += '*'*40+"\n"
            elif type(getattr(proto_buf_instance,i)).__name__ == "RepeatedScalarFieldContainer":
                self.out_put_str[0] += str(i) + " : \n"
                self.out_put_str[0] += '*'*40+"\n"
                self.list_info_temp=[]
                for list_item in getattr(proto_buf_instance,i):
                    self.out_put_str[0] += str(list_item)+"\n"
                    self.list_info_temp.append(str(list_item))
                self.out_put_str[0] += '*'*40+"\n"
                result_info[str(i)]=self.list_info_temp
                continue
            elif type(getattr(proto_buf_instance,i)).__name__=="GeneratedProtocolMessageType":
                continue
            else :
                self.out_put_str[0] += str(i) + "  : \n"
                self.out_put_str[0] += '*'*40+"\n"
                result_info[str(i)]={}
                self.__list_proto_buf(getattr(proto_buf_instance,i),result_info[str(i)])
                self.out_put_str[0] += '*'*40+"\n"


