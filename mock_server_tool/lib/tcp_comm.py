#coding:gbk

import socket

from mock_lib.conf import get_conf_value

class TcpComm:

    def __init__(self,s_type):
        self.send_addr = (get_conf_value('mock_client','server_ip'),\
                int(get_conf_value('mock_client','server_port')))
        self.recv_addr = ('',int(get_conf_value('mock_server','listen_port')))
        self.buffersize = 10240000

        self.s_type = s_type
        
        self.comm_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        
        if self.s_type == "server":
            self.comm_socket.bind(self.recv_addr)
            self.comm_socket.listen(10)
        elif self.s_type == "client":
            self.comm_socket.connect(self.send_addr)
            self.recv_from_addr=()


    def send(self,string_data,addr):
        if "server" == self.s_type:
            self.client_socket.send(string_data)
        elif "client" == self.s_type:
            self.comm_socket.send(string_data)

    def recv(self):
        if "server" == self.s_type:
            self.client_sock,self.recv_from_addr = self.comm_socket.accept()
            self.client_socket = self.client_sock
            self.string_data = self.client_sock.recv(self.buffersize)
        elif "client" == self.s_type:
            self.string_data = self.comm_socket.recv(self.buffersize)
        return (self.string_data,self.recv_from_addr)

    def __del__(self):
        self.comm_socket.close()
        self.client_socket.close()

