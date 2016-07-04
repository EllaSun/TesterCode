#coding:gbk

import sys

from Interface import base_comm,base_data,base_print
from mock_lib.conf import get_conf_value
from optparse import OptionParser


usage = """%prog [options] [dict_data]\n       \
options -s -p -d must be given\n       \
the default dict_data will be get from the conf file"""

def main():
    '''usage for the virtual stub. use -h option to view the options definition'''

    parser = OptionParser(usage = usage, version = "%prog " + '1.0.0')
    parser.add_option( "-s", "--stub", action = "store", dest = "stub_type", help = "option for stub type. \"server\" or \"client\"")
    parser.add_option( "-p", "--protocol", action = "store", dest = "protocol_type", help = "option for protocol type. \"protobuf\" and \"thrift\" are supported in this version.")
    parser.add_option( "-t", "--transport", action = "store", dest = "tran_type", help = "option for trans type. \"tcp\" and \"udp\" are supproted in this version, and when the protocol type is \"thrift\", the trans type is always \"tcp\".")
    parser.add_option( "-d", "--data", action = "store", dest = "dict_data", help = "dict data for stub.")
    parser.add_option( "-o", "--out_type", action = "store", dest = "out_type", help = "option for out type. \"standard\" will output to standard device. \"slience\" will return the value without display in the screen. ")
    (options, args) = parser.parse_args(sys.argv)

    stub = Stub(options.stub_type,options.protocol_type,options.tran_type,options.dict_data,options.out_type)
    stub.start()

class Stub:

    def __init__(self,stub_type,proto_type,tran_type,dict_data,out_type):
        
        if dict_data == None:
            self.resp_data = eval(get_conf_value('mock_server','response_value'))
        else:
            self.resp_data = eval(dict_data)
        if dict_data == None:
            self.req_data = eval(get_conf_value('mock_client','request_value'))
        else:
            self.req_data = eval(dict_data)
        
        self.comm = base_comm.Comm(tran_type,stub_type)
        self.data = base_data.Data(proto_type)
        self.printlog = base_print.Print()
        self.stub_type = stub_type

        if out_type == None:
            self.out_type = 'standard'
        else:
            self.out_type = out_type


    def start(self):
        '''struct the virtual stub'''
        #server
        if "server" == self.stub_type:
            print "server start.."
            while True:
                self.recv_data,self.recv_addr = self.comm.recv()
                self.recv_dict = self.data.unpackage(self.recv_data,1)
                print "recv from:",self.recv_addr
                self.send_data = self.data.package(self.resp_data,1)
                print "send data to:",self.recv_addr
                self.comm.send(self.send_data, self.recv_addr)
                print "send over!"
                if self.out_type == 'standard':
                    self.printlog.print_log("req data from client",self.recv_dict)
                elif self.out_type == 'silence':
                    return self.recv_dict
        elif "client" == self.stub_type:
            print "send data to server!"
            self.send_data = self.data.package(self.req_data,2)
            self.comm.send(self.send_data,None)
            print "send over!"
            self.recv_data,self.recv_addr = self.comm.recv()
            self.recv_dict = self.data.unpackage(self.recv_data,2)
            if self.out_type == 'standard':
                self.printlog.print_log("resp data from server",self.recv_dict)
            elif self.out_type == 'silence':
                return self.recv_dict





if __name__ == '__main__':
    main()
