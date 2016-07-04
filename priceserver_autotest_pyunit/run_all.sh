#! /bin/bash

STD="log/std.log"
RESULT="log/result.log"

mkdir -p log
rm -rf log/*
touch $STD
touch $RESULT
for file in `ls case/price_*.py`
do
	module=${file%.*}
	module=${module//\//.}
	echo -n `date "+%F-%H:%M:%S"`
	echo "	Running $module"
	python run_test.py -v $module 1>>$STD 2>>$RESULT
done
