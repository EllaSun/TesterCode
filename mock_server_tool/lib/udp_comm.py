#coding:gbk

import socket

from mock_lib.conf import get_conf_value

class UdpComm:

    def __init__(self,s_type):
        self.send_addr = (get_conf_value('mock_client','server_ip'),\
                int(get_conf_value('mock_client','server_port')))
        self.recv_addr = ('',int(get_conf_value('mock_server','listen_port')))
        self.buffersize = 10240000
        self.s_type = s_type

        #udp socket
        self.comm_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        if self.s_type == "server":
            self.comm_socket.bind(self.recv_addr)

    def send(self,string_data,addr):
        if self.s_type == "server":
            self.comm_socket.sendto(string_data, addr)
        elif self.s_type == "client":
            self.comm_socket.sendto(string_data, self.send_addr)

    def recv(self):
        self.string_data,self.recv_from_addr = self.comm_socket.recvfrom(self.buffersize)
        return (self.string_data,self.recv_from_addr)


    def __del__(self):
        self.comm_socket.close()

