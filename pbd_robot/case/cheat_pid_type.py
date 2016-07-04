| *** Settings *** |
| Test Setup     | clear data  |
| Library        | lib/pbd.py |
| Resource       | conf/pbd_var.txt |

| *** Test Cases *** |
| brand protect |
|    | [Documentation] | 若keyword在品专词白名单里，则非Q1Q2广告 |
|    | note | #Q1_list.$CUR_TIME.tmp2 | star | adid | accid | query | top service |
|    | ... | #click | pv |
|    | write data | ${CUR_DATE}/Q1/Q1_list.${CUR_TIME}.tmp2 | 1 | 1111 | 2222 | 酷讯旅游 | 103 |
|    | ... | 1 | 15 |
|    | note | #acc_keyword_white | accoutid | keyword |
|    | write data | ${CUR_DATE}/acc_keyword_white | 2222 | 酷讯旅游 |
|    | #run script |
|    | run | cheat_pid_type.py | DATA_PATH/cheatpid_type/cheatpid_type_$date | ${input_path}/havesubpid_{date} | ${input_path}/cheatpid_${date} | ${data_path}/reserved/$date/pid_info/pid_list | ${data_path}/reserved/$date/subpid_info/pid_list |
|    | #check result |
|    | ${star}= | get_star | ${CUR_DATE}/Q1/Q1_list.${CUR_TIME}.tmp3 | 1111 |
|    | Should Be equal | ${star} | ${None} |
