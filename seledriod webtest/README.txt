运行环境为10.136.119.4 用户名：sunying 密码：sunny
运行方式命令行下输入python C:\webtest1\main.py C:\webtest1\webtest.cfg 或者点击快捷键runWeb.bat。
运行结果如图：

配置文件说明：
[webtest]
domain=wap.sogou.com
log=D:\webtest1\log.txt
data=D:\webtest1\data.txt
pic=D:\webtest1\screenshot\

domain为访问主域名，可支持线上线下两种方式。线上为wap.sogou.com, 线下直接写ip即可，如10.136.20.28

log为日志文件地址。目前日志的报警只有一种，在找不到广告的情况下报警。如在安卓下搜索“市场”，无广告，输出 “市场 has no ad!”


data为数据文件地址。 数据文件格式后续再说明


pic为截图保存文件夹路径。



数据文件说明：
格式为[terminal][queryword][ad_type][下方广告截图方式]，以\t分割
各字段说明：
terminal：终端。仅限于ipone4,iphone5,note3, S4, E63,MI1S,MI2S,HuaWeiG750
queryword: 查询词
ad_type:附加创意类型
下方广告截图方式：0或1，默认1。 0值主要用于note3，note3手机屏过大，导致截图方式与其他机型略有差异

示例如下：
#[terminal][queryword][ad_type][下方广告截图方式]
iphone4	火车票	无图皇冠	1
note3	火车票	无图皇冠	0
iphone4	信用卡	有图皇冠	1
note3	信用卡	有图皇冠	0
S4	暗黑战神	APP_强样式	1
S4	失眠	TEL	1
E63	鲜花	炫版	1
S4	机票	无图皇冠_APP普通_电话	1



目前该自动化支持：
1. 版式：炫版和触屏版/移动版
2. 手机型号：小米（1S、2S）、华为（G750）、三星（NOTE，S4，S3）,苹果（iphone4,iphone5),Nokia(E63)
3. 各种附加创意（这个是由数据文件中查询词决定）
4. 环境：线上线下
5. 广告位置：首页上方，首页下方。目前默认为上方下方均截图


后续todo：
1.支持简版。
2.智能判断页面元素，以此附加创意是否满足预期，截断，标红等。这个可以替代部分目前的自动化测试。
3.添加到平台上，增加用户友好性。


安装步骤：
1.安装active-python, http://www.activestate.com/activepython/downloads,分32位操作系统和64位操作系统
2.安装selenium，命令行下运行pip install selenium
3. 安装chrome
4. 安卓chrome driver
5. 安装PIL，这是一种图像处理python包，截图用的。 http://www.pythonware.com/products/pil/index.htm