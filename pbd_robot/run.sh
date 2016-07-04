export LANG="zh_CN.gb2312"
pybot --logtitle ShowQ_q1_hour --reporttitle showQ_q1_hour  -N showQ_q1_hour  case/* 
iconv -f utf-8 -t gbk output.xml > output11.xml
mv output11.xml output.xml
cp -f *ml /search/odin/autotest/showQ_q1_hour/workspace/showQ_q1_hour/
