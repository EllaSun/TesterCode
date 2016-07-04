#coding=gbk
import struct
import socket


class x_server_header(object) :

    '''
    目前先通过类的形式进行变量的空间管理，后续可进行配置项的动态加载。
    头结构如下
           unit16_t id, //id  1
           unit16_t version, //版本号 1
           unit32_t log_id, //由apche产生的logid，贯穿一次请求的所有网络交互 111
           char provider[16], //客户端标实，如"client" 
           uint32_t magic_num //特殊标示，一个包的起始 0xffee7799
           unit32_t reserved; //保留
           unit32_t body_len; //head后请求的总长度，也就是protobuf序列号后的长度
    '''
        
    def __init__(self,id=1,version=1,log_id=111,provider='client',magic_num=socket.htonl(0xffee7799),reserved=0):
        '''创建实例的时候需要传入body_len这个变量'''
        self.id=id
        self.version=version
        self.log_id=log_id
        self.provider=provider
        self.magic_num=magic_num
        self.reserved=reserved

    def package_header(self,body_len):
        '''先转成网络序，再进行struct'''
        self.id = socket.htons(self.id)
        self.version = socket.htons(self.version)
        self.log_id = socket.htonl(self.log_id)
        self.reserved = socket.htons(self.reserved)
        self.body_len = socket.htonl(body_len)

        #序列化，返回
        return struct.pack("!HHI16sIII",self.id,self.version,self.log_id,\
                self.provider,self.magic_num,self.reserved,self.body_len)

    def unpackage_header(self,head):
        '''反解收到的header,返回dict,供调试信息用'''
        (self.id,self.version,self.log_id,self.provider,self.magic_num,self.reserved,self.body_len)=struct.unpack("!HHI16sIII",head)
        self.id = socket.ntohs(self.id)
        self.version = socket.ntohs(self.version)
        self.log_id = socket.ntohl(self.log_id)
        self.reserved = socket.ntohs(self.reserved)
        self.body_len = socket.ntohl(self.body_len)

        self.result = {}
        self.result["id"]=self.id
        self.result["version"]=self.version
        self.result["log_id"]=self.log_id
        self.result["provider"]=self.provider
        self.result["magic_num"]=self.magic_num
        self.result["reserved"]=self.reserved
        self.result["body_len"]=self.body_len
        return self.result
        
