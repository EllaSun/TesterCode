#coding:GBK
from datetime import *
from time import *

def get_yesterday_uv(account = 'tuan.27@sogou.com'):
        #generate SQL for query UV
        sql = "SELECT count(distinct(session_id)) from  sa_account_info acc, sa_visit_for_flow_statics_for_view sv where acc.account_id = sv.acid and acc.c_mail='"
        day =  datetime.now() - timedelta(days=1)
        sql = sql+ account + "' and day = '" + day.strftime("%Y%m%d")+"'"    
        return sql
        #sql = "select count(*) from (select distinct session_id from sa_visit_for_flow_statics where domain = '" + domain+ "'" 
        #sql_end = ") summery"
        
        #yesterday = datetime.now() - timedelta(days=1)
        #sql = sql + "and day = '" + yesterday.strftime("%Y%m%d") + "'"
#       for i in range(1,days + 1):
#            that_day = datetime.now() - timedelta(days=i)
#            if i==1:
#            sql = sql + "and day = '" + that_day.strftime("%Y%m%d") + "'"
#        else:
#           sql = sql + " or day = '" + that_day.strftime("%Y%m%d") + "'"
        #sql = sql+sql_end
        return sql

def get_yesterday_pv(account = 'tuan.27@sogou.com'):
        #generate sql for query PV
        #select sum(page_nums) from sa_account_info acc, sa_visit_for_flow_statics_for_view sv
        #where acc.account_id = sv.acid and acc.c_mail='tuan.27@sogou.com' and day='20111007'
        #generate SQL for query UV
        sql = "SELECT sum(page_nums) from  sa_account_info acc, sa_visit_for_flow_statics_for_view sv where acc.account_id = sv.acid and acc.c_mail='"
        day =  datetime.now() - timedelta(days=1)
        sql = sql+ account + "' and day = '" + day.strftime("%Y%m%d")+"'"    
        return sql
#        for i in range(1,days + 1):
#            that_day = datetime.now() - timedelta(days=i)
#            if i==1:
#                sql = sql + "and day = '" + that_day.strftime("%Y%m%d") + "'"
#            else:
#                sql = sql + " or day = '" + that_day.strftime("%Y%m%d") + "'"
        return sql
    
def get_yesterday_avg_time(domain='27.cn'):
    sql="select sum(stay_time) /( count(distinct(session_id))- sum(is_bounce))  from sa_visit_for_flow_statics_for_view where domain='"
    yesterday = datetime.now() - timedelta(days=1)
    return sql+domain+"' and day='" + yesterday.strftime("%Y%m%d")+"'"

#获得昨天的平均访问页面数
def get_yesterday_avg_page(domain='27.cn'):
    sql="select sum(page_nums)/count(distinct(session_id)) from sa_visit_for_flow_statics_for_view where domain='"
    yesterday = datetime.now() - timedelta(days=1)
    return sql+domain+"' and day='" + yesterday.strftime("%Y%m%d")+"'"

#获得昨天的转化率
def get_yesterday_(domain='27.cn'):
    sql="select sum(goalid_convert_num)  from sa_visit_for_flow_statics where domain ='"
    yesterday = datetime.now() - timedelta(days=1)
    return sql+domain+"' and day='" + yesterday.strftime("%Y%m%d")+"'"

#获得昨天的跳出率
def get_yesterday_bound(domain='27.cn'):
    sql="select sum(is_bounce)/count(distinct(session_id)) from sa_visit_for_flow_statics_for_view where domain='" 
    yesterday = datetime.now() - timedelta(days=1)
    return sql+domain+"' and day='" + yesterday.strftime("%Y%m%d")+"'"

def get_date_str(day = 1, date_start='', date_end=''):
    if day == 1:
        date_end = datetime.now() - timedelta(days = 1)
        days = "day = '" + date_end.strftime("%Y%m%d") + "' "
    elif day == 7:
        date_end = datetime.now() - timedelta(days = 1)
        date_start = datetime.now() - timedelta(days = 7)
        days = "day >= '" + date_start.strftime("%Y%m%d") + "' and day <= '" + date_end.strftime("%Y%m%d") + "'"
    elif day == 30:
        date_start = datetime.now() - timedelta(days = 1)
        date_end = datetime.now() - timedelta(days = 30)
        days = "day >= '" + date_start.strftime("%Y%m%d") + "' and day <= '" + date_end.strftime("%Y%m%d") + "'"
    elif day == 0:
        days = "day >= '" + date_start + "' and day <= '" + date_end + "'"
    else:
        print "illegal date"
        return
    
    return days

#获得搜狗推广关键词查询的信息的SQL语句  
#day为往前多少天的统计数据，day大于0时有效。day=0时则从date_start到date_end输入统计信息
def get_pro_keyword(account='tuan.27@sogou.com', day = 1, is_sum = 0, date_start='', date_end=''):
    sql="""SELECT
        pro.keyword as keyword,
        Sum(pro.visit_num) as visitNum,
        Sum(pro.stay_time_num)/(Sum(pro.visit_num) - sum(pro.is_bounce_num)) as stayTime,
        Sum(pro.page_num)/Sum(pro.visit_num) as bounceRate,
        Sum(pro.is_bounce_num)/Sum(pro.visit_num),
        Sum(pro.consume)/100,
        Sum(pro.clicknum)
    FROM sa_promotion_nogoal pro, sa_account_info acc
    where pro.acid=acc.account_id
    and acc.c_mail='"""   
    sql_end='''
        group by pro.keyword
        order by sum(pro.visit_num)  desc
    '''
    sql_sum="""
        SELECT
            Sum(pro.visit_num) as visitNum,
            Sum(pro.stay_time_num)/(Sum(pro.visit_num) - sum(pro.is_bounce_num)) as stayTime,
            Sum(pro.page_num)/Sum(pro.visit_num) as bounceRate,
            Sum(pro.is_bounce_num)/Sum(pro.visit_num),
            Sum(pro.consume)/100,
            Sum(pro.clicknum)
        FROM sa_promotion_nogoal pro, sa_account_info acc
        where pro.acid=acc.account_id
        and acc.c_mail='""" 
    sql_sum_end=""" order by sum(pro.visit_num)  desc"""
    sql_cohere="' and "

    days = get_date_str(day, date_start, date_end)
    if is_sum==1:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end

#def get_pro_keyword_convert(account='tuan.27@sogou.com', day = 1, is_sum=0, date_start='', date_end=''):
#获得广告组数据信息的SQL语句
#day为往前多少天的统计数据，day大于0时有效。day=0时则从date_start到date_end输入统计信息
def get_ad_group(account='tuan.27@sogou.com', day = 1, is_sum=0, date_start='', date_end=''):
    sql="""SELECT
    grp.name,
    sum(pro.visit_num),
    Sum(pro.stay_time_num)/(Sum(pro.visit_num)-sum(pro.is_bounce_num)),
    sum(pro.page_num)/Sum(pro.visit_num),
    sum(pro.is_bounce_num)/Sum(pro.visit_num),
    sum(pro.consume)/100,
    sum(pro.clicknum)
FROM sa_adgroup grp, sa_promotion_nogoal pro, sa_account_info acc
WHERE grp.gpid=pro.adgroup_id 
    and grp.acid=acc.account_id
    and acc.c_mail= '"""
    sql_cohere="' and "
    sql_end=""" group by grp.name
    order by sum(pro.visit_num) desc"""
    
    sql_sum="""SELECT
    sum(pro.visit_num),
    Sum(pro.stay_time_num)/(Sum(pro.visit_num)-sum(pro.is_bounce_num)),
    sum(pro.page_num)/Sum(pro.visit_num),
    sum(pro.is_bounce_num)/Sum(pro.visit_num),
    sum(pro.consume)/100,
    sum(pro.clicknum)
FROM sa_adgroup grp, sa_promotion_nogoal pro, sa_account_info acc
WHERE grp.gpid=pro.adgroup_id 
    and grp.acid=acc.account_id
    and acc.c_mail= '"""
    
    sql_sum_end="""
    order by sum(pro.visit_num) desc"""
    
    days = get_date_str(day, date_start, date_end)
    
    if is_sum==1:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end

#def get_ad_group_convert(account='tuan.27@sogou.com', day = 1, is_sum=0, date_start='', date_end=''):
    
#获得时段分析部分的SQL语句
#day为往前多少天的统计数据，day大于0时有效。day=0时则从date_start到date_end输入统计信息
def get_hour_analysis(account='tuan.27@sogou.com', day = 1, is_sum=0, date_start='', date_end=''):
    sql="""SELECT
    flow.`hour`,
    sum(flow.visit_num),
    sum(flow.page_num),
    sum(flow.stay_time_num)/(sum(flow.visit_num)-sum(flow.is_bounce_num)),
    sum(flow.page_num)/Sum(flow.visit_num),
    Sum(flow.is_bounce_num)/Sum(flow.visit_num)
FROM sa_flow_date_nogoal flow, sa_account_info acc
where flow.acid = acc.account_id
    and acc.c_mail='"""
    sql_cohere="' and "
    sql_end="""
GROUP BY flow.`hour`
ORDER BY flow.`hour` desc
"""
    sql_sum="""SELECT
    flow.`hour`,
    sum(flow.visit_num),
    sum(flow.page_num),
    sum(flow.stay_time_num)/(sum(flow.visit_num)-sum(flow.is_bounce_num)),
  sum(flow.page_num)/Sum(flow.visit_num),
    Sum(flow.is_bounce_num)/Sum(flow.visit_num)
FROM sa_flow_date_nogoal flow, sa_account_info acc
where flow.acid = acc.account_id
    and acc.c_mail='"""
    sql_sum_end="""
ORDER BY flow.`hour` desc
"""     
    
    days = get_date_str(day, date_start, date_end)
    
    if is_sum==1:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end

#def get_trend_analysis_convert(account='tuan.27@sogou.com', day = 1, is_sum=0, date_start='', date_end=''):

#获得趋势分析部分转化次数的SQL语句
def get_trend_analysis_convert(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
    sql="""SELECT
    flow.`day`,
    sum(flow.convert_num)
FROM sa_flow_date_goal_convertnum flow, sa_account_info acc
where flow.acid = acc.account_id
    and acc.c_mail='"""
    sql_cohere="' and "
    sql_end="""
GROUP BY flow.`day`
ORDER BY flow.`day` desc
"""    
    sql_sum="""SELECT
    sum(flow.convert_num)
FROM sa_flow_date_goal_convertnum flow, sa_account_info acc
where flow.acid = acc.account_id
    and acc.c_mail='"""
    sql_sum_end="""
    ORDER BY flow.`day` desc
    """
    
    days = get_date_str(day, date_start, date_end)
    
    if is_sum==1:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end

#获得趋势分析部分
def get_trend_analysis(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
    sql="""SELECT
    flow.`day`,
    sum(flow.visit_num),
    sum(flow.page_num),
    sum(flow.stay_time_num)/(sum(flow.visit_num)-sum(flow.is_bounce_num)),
    sum(flow.page_num)/Sum(flow.visit_num),
    Sum(flow.is_bounce_num)/Sum(flow.visit_num)
FROM sa_flow_date_nogoal flow, sa_account_info acc
where flow.acid = acc.account_id
    and acc.c_mail='"""
    sql_cohere="' and "
    sql_end="""
GROUP BY flow.`day`
ORDER BY flow.`day` desc
"""
    sql_sum="""SELECT
    flow.`day`,
    sum(flow.visit_num),
    sum(flow.page_num),
    sum(flow.stay_time_num)/(sum(flow.visit_num)-sum(flow.is_bounce_num)),
  sum(flow.page_num)/Sum(flow.visit_num),
    Sum(flow.is_bounce_num)/Sum(flow.visit_num)
FROM sa_flow_date_nogoal flow, sa_account_info acc
where flow.acid = acc.account_id
    and acc.c_mail='"""
    sql_sum_end="""
ORDER BY flow.`day` desc
"""     
    
    days = get_date_str(day, date_start, date_end)
    
    if is_sum==1:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end
    
#def get_source_keyword(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
#def get_source_all(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
#def get_source_se(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
#def get_source_external(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):

def get_visit_location(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
    sql="""SELECT
    sf.location_id,
    sum(sf.visit_num) as vsum,
        sum(sf.pv) as pvsum,
        sum(sf.stay_time_num)/(sum(sf.visit_num)-sum(sf.is_bounce_num)),
    sum(sf.page_num)/Sum(sf.visit_num),
    Sum(sf.is_bounce_num)/Sum(sf.visit_num)
FROM sa_flow_locationnogoal sf, sa_account_info acc
WHERE sf.acid = acc.account_id and acc.c_mail= '"""
    sql_cohere = "' and "
    sql_end = """ 
    GROUP BY sf.location_id
    ORDER BY vsum desc"""
    sql_sum="""SELECT
    sum(sf.visit_num) as vsum,
        sum(sf.pv) as pvsum,
        sum(sf.stay_time_num)/(sum(sf.visit_num)-sum(sf.is_bounce_num)),
    sum(sf.page_num)/Sum(sf.visit_num),
    Sum(sf.is_bounce_num)/Sum(sf.visit_num)
FROM sa_flow_locationnogoal sf, sa_account_info acc
WHERE sf.acid = acc.account_id and acc.c_mail= '"""
    sql_sum_end = """ 
    ORDER BY vsum desc"""
    
    days = get_date_str(day, date_start, date_end)
    
    if is_sum==1:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end
      
def get_visit_location_convert(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
    sql="""SELECT
    sum(sf.visit_num) as vsum,
    sum(sf.convert_num) as cvtsum
FROM sa_flow_location_goal_convertnum sf, sa_account_info acc
WHERE sf.acid = acc.account_id and acc.c_mail= '"""
    sql_cohere="' and "
    sql_end="""
    GROUP BY sf.location_id
    ORDER BY vsum desc
    """
    sql_sum="""SELECT
    sum(sf.convert_num) as cvtsum
FROM sa_flow_location_goal_convertnum sf, sa_account_info acc
WHERE sf.acid = acc.account_id and acc.c_mail= '"""
    sql_sum_end=""
    
    days = get_date_str(day, date_start, date_end)
    
    if is_sum==1:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end 
     
def get_visit_page(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
    sql="""SELECT
    vp.page_num_id,
    sum(vp.visit_num) as vsum
FROM sa_visitor_page vp, sa_account_info acc
WHERE vp.acid = acc.account_id and acc.c_mail= '"""
    sql_cohere = "' and "
    sql_end="""
    GROUP BY vp.page_num_id
    ORDER BY vsum desc"""

    sql_sum="""SELECT
    vp.page_num_id,
    sum(vp.visit_num) as vsum
FROM sa_visitor_page vp, sa_account_info acc
WHERE vp.acid = acc.account_id and acc.c_mail= '"""    
    sql_sum_end="""
    ORDER BY vsum desc"""
    
    days = get_date_str(day, date_start, date_end)
    
    if is_sum==1:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end
    
def get_visit_time(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
    sql="""SELECT
    vs.staytime_id,
    sum(vs.visit_num) as vsum
FROM sa_visitor_staytime vs, sa_account_info acc
WHERE vs.acid = acc.account_id and acc.c_mail= '"""
    sql_cohere = "' and "
    sql_end="""
    GROUP BY vs.staytime_id
    ORDER BY vsum desc"""

    sql_sum="""SELECT
    sum(vs.visit_num) as vsum
FROM sa_visitor_staytime vs, sa_account_info acc
WHERE vs.acid = acc.account_id and acc.c_mail= '"""    
    sql_sum_end="""
    ORDER BY vsum desc"""
    days = get_date_str(day, date_start, date_end)
    if is_sum==1:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end
    
def get_source_keyword(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end='', with_location = False, with_se=False):
    sql = """SELECT
    src.`query`,
    sum(src.visit_num) as vsum,
    sum(src.page_num) as pvsum,
    sum(src.stay_time_num)/(sum(src.visit_num)-sum(src.is_bounce_num)),
    sum(src.page_num)/Sum(src.visit_num),
    Sum(src.is_bounce_num)/Sum(src.visit_num)*100
FROM sa_nolocation_flow_query_nogoal src, sa_account_info acc
WHERE src.acid = acc.account_id and acc.c_mail='"""
    sql_cohere = "' and src."
    sql_end = """
    GROUP BY src.`query`
    ORDER BY vsum desc
    """
    sql_sum = """SELECT
    sum(src.visit_num) as vsum,
    sum(src.page_num) as pvsum,
    sum(src.stay_time_num)/(sum(src.visit_num)-sum(src.is_bounce_num)),
    sum(src.page_num)/Sum(src.visit_num),
    Sum(src.is_bounce_num)/Sum(src.visit_num)*100
FROM sa_nolocation_flow_query_nogoal src, sa_account_info acc
WHERE src.acid = acc.account_id and acc.c_mail='"""
    sql_sum_end = "ORDER BY vsum desc"
    
    days = get_date_str(day, date_start, date_end)
    
    if is_sum:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end 
    
def get_source_se(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
    sql="""SELECT
    src.se_code,
    sum(src.visit_num) as vsum,
    sum(src.page_num) as pvsum,
    sum(src.stay_time_num)/(sum(src.visit_num)-sum(src.is_bounce_num)),
    sum(src.page_num)/Sum(src.visit_num),
    sum(src.is_bounce_num)/sum(src.visit_num)*100
FROM sa_se_query_nogoal src, sa_account_info acc
WHERE src.acid = acc.account_id and acc.c_mail='"""
    sql_cohere = "' and src.day = '"
    sql_end="""
    GROUP BY src.se_code
    ORDER BY vsum desc"""
    sql_sum="""SELECT
    src.se_code,
    sum(src.visit_num) as vsum,
    sum(src.page_num) as pvsum,
    sum(src.stay_time_num)/(sum(src.visit_num)-sum(src.is_bounce_num)),
    sum(src.page_num)/Sum(src.visit_num),
    sum(src.is_bounce_num)/sum(src.visit_num)*100
FROM sa_se_query_nogoal src, sa_account_info acc
WHERE src.acid = acc.account_id and acc.c_mail='"""
    sql_sum_end = " ORDER BY vsum desc"
    
    days = get_date_str(day, date_start, date_end)
    
    if is_sum:
        return sql_sum+account+sql_cohere+days+sql_sum_end
    else:
        return sql+account+sql_cohere+days+sql_end 
    
#print get_pro_keyword()
#def get_source_directaccess(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):
#def get_source_external(account='tuan.27@sogou.com', day = 30, is_sum=0, date_start='', date_end=''):