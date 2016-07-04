#coding:gbk

import struct
import TranStr
from thrift.Thrift import TType,TMessageType
from thrift.protocol import TBinaryProtocol,TCompactProtocol
from thrift.protocol.TBinaryProtocol import TBinaryProtocol
from thrift.protocol.TCompactProtocol import TCompactProtocol,CompactType


class BinaryProtocol:
    '''thrift protocol'''


    def __init__(self,proto_type):
        self.proto_type = proto_type

    def thrift_proto(self, struct_object, data_dict, fun_name, mock_type):
        self.tran_str = TranStr.TranStr()
        print self.proto_type
        if self.proto_type == 1:
            self.protocol = TBinaryProtocol(self.tran_str)
        elif self.proto_type == 2:
            self.protocol = TCompactProtocol(self.tran_str)

        if mock_type == 2:
            self.protocol.writeMessageBegin(fun_name, TMessageType.CALL, 0)
        elif mock_type == 1:
            self.protocol.writeMessageBegin(fun_name, TMessageType.REPLY, 0)
        self.__tran_struct(struct_object, data_dict)
        self.protocol.writeMessageEnd()
        data_str = self.tran_str.get_protocol_str()
        data_str = struct.pack('!i',len(data_str)) + data_str
        return data_str


    def list_thrift_proto(self, struct_object, data_str, cur):
        self.un_tran_str = TranStr.UnTranStr(data_str,cur+4) 
        if self.proto_type == 1:
            self.list_proto = TBinaryProtocol(self.un_tran_str)
        elif self.proto_type == 2: 
            self.list_proto = TCompactProtocol(self.un_tran_str)

        data_dict = {}
        self.list_proto.readMessageBegin()
        data_dict = self.__un_tran_struct(struct_object)
        self.list_proto.readMessageEnd()
        return data_dict



    def __tran_struct(self, struct_object, data_dict):
        req_spec = struct_object()
        self.protocol.writeStructBegin(data_dict.keys()[0])
        for i in range(len(req_spec.thrift_spec)):
            if req_spec.thrift_spec[i]==None:
                continue
            if req_spec.thrift_spec[i][2] in data_dict:
                #struct
                if req_spec.thrift_spec[i][1]==TType.STRUCT:
                    self.protocol.writeFieldBegin(req_spec.thrift_spec[i][2],TType.STRUCT,i)
                    self.__tran_struct(req_spec.thrift_spec[i][3][0],data_dict[req_spec.thrift_spec[i][2]])
                    self.protocol.writeFieldEnd()
                #string
                elif req_spec.thrift_spec[i][1]==TType.STRING:
                    self.protocol.writeFieldBegin(req_spec.thrift_spec[i][2],TType.STRING,i)
                    self.protocol.writeString(data_dict[req_spec.thrift_spec[i][2]])
                    self.protocol.writeFieldEnd()
                #i32
                elif req_spec.thrift_spec[i][1]==TType.I32:
                    self.protocol.writeFieldBegin(req_spec.thrift_spec[i][2],TType.I32,i)
                    self.protocol.writeI32(data_dict[req_spec.thrift_spec[i][2]])
                    self.protocol.writeFieldEnd()
                #i16
                elif req_spec.thrift_spec[i][1]==TType.I16:
                    self.protocol.writeFieldBegin(req_spec.thrift_spec[i][2],TType.I16,i)
                    self.protocol.writeI16(data_dict[req_spec.thrift_spec[i][2]])
                    self.protocol.writeFieldEnd()
                #byte
                elif req_spec.thrift_spec[i][1]==TType.BYTE:
                    self.protocol.writeFieldBegin(req_spec.thrift_spec[i][2],TType.BYTE,i)
                    self.protocol.writeByte(data_dict[req_spec.thrift_spec[i][2]])
                    self.protocol.writeFieldEnd()
                #i64
                elif req_spec.thrift_spec[i][1]==TType.I64:
                    self.protocol.writeFieldBegin(req_spec.thrift_spec[i][2],TType.I64,i)
                    self.protocol.writeI64(data_dict[req_spec.thrift_spec[i][2]])
                    self.protocol.writeFieldEnd()
                #bool
                elif req_spec.thrift_spec[i][1]==TType.BOOL:
                    self.protocol.writeFieldBegin(req_spec.thrift_spec[i][2],TType.BOOL,i)
                    self.protocol.writeBool(data_dict[req_spec.thrift_spec[i][2]])
                    self.protocol.writeFieldEnd()
                #double
                elif req_spec.thrift_spec[i][1]==TType.DOUBLE:
                    self.protocol.writeFieldBegin(req_spec.thrift_spec[i][2],TType.DOUBLE,i)
                    self.protocol.writeDouble(data_dict[req_spec.thrift_spec[i][2]])
                    self.protocol.writeFieldEnd()
                #list
                elif req_spec.thrift_spec[i][1]==TType.LIST:
                    self.protocol.writeFieldBegin(req_spec.thrift_spec[i][2],TType.LIST,i)
                    self.protocol.writeListBegin(etype = req_spec.thrift_spec[i][3][0], size = len(data_dict[req_spec.thrift_spec[i][2]]))
                    if req_spec.thrift_spec[i][3][0] == TType.STRUCT:
                        self.__tran_list(data_dict[req_spec.thrift_spec[i][2]],req_spec.thrift_spec[i][3][0],req_spec.thrift_spec[i][3][1][0])
                    else:
                        self.__tran_list(data_dict[req_spec.thrift_spec[i][2]],req_spec.thrift_spec[i][3][0])
                    self.protocol.writeListEnd()
                    self.protocol.writeFieldEnd()
        self.protocol.writeFieldStop()
        self.protocol.writeStructEnd()


    def __un_tran_struct(self, struct_object):
        thrift_object = struct_object()
        data_dict = {}
        self.list_proto.readStructBegin()
        for i in range(len(thrift_object.thrift_spec)):
            if thrift_object.thrift_spec[i] == None:
                continue
            #i32
            if thrift_object.thrift_spec[i][1] == TType.I32:
                self.list_proto.readFieldBegin()
                data_dict[thrift_object.thrift_spec[i][2]] = self.list_proto.readI32()
                self.list_proto.readFieldEnd()
            #string
            elif thrift_object.thrift_spec[i][1] == TType.STRING:
                self.list_proto.readFieldBegin()
                data_dict[thrift_object.thrift_spec[i][2]] = self.list_proto.readString()
                self.list_proto.readFieldEnd()
            #i64
            elif thrift_object.thrift_spec[i][1] == TType.I64:
                self.list_proto.readFieldBegin()
                data_dict[thrift_object.thrift_spec[i][2]] = self.list_proto.readI64()
                self.list_proto.readFieldEnd()
            #i16
            elif thrift_object.thrift_spec[i][1] == TType.I16:
                self.list_proto.readFieldBegin()
                data_dict[thrift_object.thrift_spec[i][2]] = self.list_proto.readI16()
                self.list_proto.readFieldEnd()
            #byte
            elif thrift_object.thrift_spec[i][1] == TType.BYTE:
                self.list_proto.readFieldBegin()
                data_dict[thrift_object.thrift_spec[i][2]] = self.list_proto.readByte()
                self.list_proto.readFieldEnd()
            #bool
            elif thrift_object.thrift_spec[i][1] == TType.BOOL:
                self.list_proto.readFieldBegin()
                data_dict[thrift_object.thrift_spec[i][2]] = self.list_proto.readBool()
                self.list_proto.readFieldEnd()
            #struct
            elif thrift_object.thrift_spec[i][1] == TType.STRUCT:
                self.list_proto.readFieldBegin()
                data_dict[thrift_object.thrift_spec[i][2]] = self.__un_tran_struct(thrift_object.thrift_spec[i][3][0])
                self.list_proto.readFieldEnd()
            #list
            elif thrift_object.thrift_spec[i][1] == TType.LIST:
                self.list_proto.readFieldBegin()
                (etype,size) = self.list_proto.readListBegin()
                data_dict[thrift_object.thrift_spec[i][2]] = []#init list to dict
                if etype == TType.STRUCT:
                    data_dict[thrift_object.thrift_spec[i][2]] = self.__un_tran_list(size, etype, thrift_object.thrift_spec[i][3][1][0])
                else:
                    data_dict[thrift_object.thrift_spec[i][2]] = self.__un_tran_list(size, etype)
                self.list_proto.readListEnd()
                self.list_proto.readFieldEnd()
        self.list_proto.readFieldBegin()
        self.list_proto.readStructEnd()
        return data_dict


    def __un_tran_list(self, list_len, list_type, *struct_object):
        data_list=[]
        for i in range(list_len):
            if list_type == TType.DOUBLE:
                list_proto_data = self.list_proto.readDouble()
            elif list_type == TType.I32:
                list_proto_data = self.list_proto.readI32()
            elif list_type == TType.I16:
                list_proto_data = self.list_proto.readI16()
            elif list_type == TType.I64:
                list_proto_data = self.list_proto.readI64()
            elif list_type == TType.BOOL:
                list_proto_data = self.list_proto.readBool()
            elif list_type == TType.STRING:
                list_proto_data = self.list_proto.readString()
            elif list_type == TType.BYTE:
                list_proto_data = self.list_proto.readByte()
            elif list_type == TType.STRUCT:
                list_proto_data = self.__un_tran_struct(struct_object[0])
            data_list = data_list + [list_proto_data,]
        return data_list


    def __tran_list(self, data_list, list_type, *struct_object):
        for i in range(len(data_list)):
            if list_type == TType.STRUCT:
                self.__tran_struct(struct_object[0],data_list[i])
            elif list_type == TType.I32:
                self.protocol.writeI32(data_list[i])
            elif list_type == TType.I64:
                self.protocol.writeI64(data_list[i])
            elif list_type == TType.BOOL:
                self.protocol.writeBool(data_list[i])
            elif list_type == TType.DOUBLE:
                self.protocol.writeDouble(data_list[i])
            elif list_type == TType.I16:
                self.protocol.writeI16(data_list[i])
            elif list_type == TType.BYTE:
                self.protocol.writeByte(data_list[i])
            elif list_type == TType.STRING:
                self.protocol.writeString(data_list[i])
 
