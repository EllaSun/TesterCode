#coding:gbk

from lib import udp_comm,tcp_comm

class Comm:

    def __init__(self,comm_type,s_type):
        #1-tcp,2-udp..
        if "udp" == comm_type:
            self.comm = udp_comm.UdpComm(s_type)
        elif "tcp" == comm_type:
            self.comm = tcp_comm.TcpComm(s_type)

    def send(self,string_data,addr):
        self.comm.send(string_data,addr)

    def recv(self):
        self.data,self.addr = self.comm.recv()
        return (self.data,self.addr)
