#coding:gbk

class TranStr:

  def __init__(self):
    self.proto_str = ''

  def get_protocol_str(self):
    return self.proto_str

  def write(self, buff):
    self.proto_str += buff

    
class UnTranStr:

  def __init__(self, un_proto_str = '', cur = 0):
    self.un_proto_str = un_proto_str
    self.cur = cur 

  def readAll(self,sz):
    buff = ''
    buff = self.un_proto_str[self.cur:self.cur+sz]
    self.cur += sz
    return buff
