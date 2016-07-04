#!/bin/sh


tar xzvf mock_protocol/body/thrift-0.9.0.tar.gz -C mock_protocol/body

cd mock_protocol/body/thrift-0.9.0
./configure --without-php ;make ;make install
cd -

cd mock_protocol/body/thrift-0.9.0/lib/py
python setup.py install
cd -
