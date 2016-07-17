using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Net;
using System.IO;
using System.Windows.Forms;
namespace VMS
{
    class Function
    {


        //门卫身份验证
        //参数为用户名密码
        public int CheckGuardINFO(string username, string pw)
        {
            //http连接，得到对应数据
            int flag = http_user_login(username, pw);
            //int flag = 1;
            //bool flag = true;

            return flag;
        }

        //访客信息查询
        //参数ID为客户证件号
        //返回值为访客信息(唯一)
        public ArrayList GetDBInfo(string VistorID)
        {
            ArrayList checkresult = new ArrayList();
            //http连接，若得到对应数据放于ArrayList checkresult中

            string response_str = null;
            int flag = http_get_vistor(out response_str, VistorID.ToString());
            if (flag == 1)// 请求成功
            {

                ArrayList temp_array = new ArrayList();
                temp_array = parse_rows(response_str);
                DataTable tb = array_to_datatable(temp_array);

                //mainpanel mainpanel1 = mainpanel.Getmainpanel();
                //DataTable dataFromDB = mainpanel1.dtvistor_today.Clone();

                //DateTime test1 = new DateTime(2011, 8, 23, 9, 0, 0);//
                //DateTime test2 = new DateTime(2011, 8, 23, 11, 0, 0);//
                //dataFromDB.Rows.Add("1", "G1", "AMTP", "1234567", "John", "IBM", "A", "Tom", "System 5", "", test1, test2, "", "");



                //checkresult = UniqueCheck(dataFromDB);

                checkresult = UniqueCheck(tb);
                if (checkresult.Count == 0)
                {
                    checkresult.Add("无该访客记录");
                }

                return checkresult;

            }
            else if (flag == 0)//无记录
            {
                checkresult.Add("无该访客记录");
                return checkresult;
            }
            else //连接不成功
            {
                //若没有取得对应数据,不做任何操作
                checkresult.Add("连接失败");
                return checkresult;
            }



        }

        //访客信息唯一性确认
        //参数为DB中取出记录
        //
        public ArrayList UniqueCheck(DataTable dataFromDB)
        {
            DateTime datetime3 = DateTime.Now;
            ArrayList result = new ArrayList();
            string status = "";
            //判定记录条数

            //没有记录
            if (dataFromDB.Rows.Count == 0)
            {

            }
            //唯一记录
            else if (dataFromDB.Rows.Count == 1)
            {
                DateTime datetime1 = stringtodatetime(dataFromDB.Rows[0]["申请访问时间"].ToString());
                DateTime datetime2 = stringtodatetime(dataFromDB.Rows[0]["申请离开时间"].ToString());
               

                    result.Add(dataFromDB.Rows[0]["序号"].ToString());
                    result.Add(dataFromDB.Rows[0]["群组序号"].ToString());
                    result.Add(dataFromDB.Rows[0]["群组名称"].ToString());
                    result.Add(dataFromDB.Rows[0]["访客证件号"].ToString());
                    result.Add(dataFromDB.Rows[0]["访客姓名"].ToString());
                    result.Add(dataFromDB.Rows[0]["访客单位"].ToString());
                    result.Add(dataFromDB.Rows[0]["允许访问楼层"].ToString());
                    result.Add(dataFromDB.Rows[0]["接待人姓名"].ToString());
                    result.Add(dataFromDB.Rows[0]["接待人部门"].ToString());
                    result.Add(dataFromDB.Rows[0]["携带物"].ToString());



                    result.Add(dataFromDB.Rows[0]["申请访问时间"].ToString());
                    result.Add(dataFromDB.Rows[0]["申请离开时间"].ToString());
                    result.Add(dataFromDB.Rows[0]["实际进入时间"].ToString());
                    result.Add(dataFromDB.Rows[0]["实际离开时间"].ToString());

                    result.Add(dataFromDB.Rows[0]["record_ID"].ToString());
                    result.Add(dataFromDB.Rows[0]["临时卡号"].ToString());

                    //result.Add(dataFromDB.Rows[0]["事由"].ToString());


                    //判断是否为群组用户
                    if (dataFromDB.Rows[0]["群组序号"].ToString() != "0")
                    {
                        status = "群组";
                    }
                    else
                    {
                        status = "个人";
                    }


                    //判断leave_time是否为空
                    if (dataFromDB.Rows[0]["实际离开时间"].ToString() != "")
                    {
                        if (datetime1.Subtract(datetime3).TotalSeconds <= 1800 && datetime2.Subtract(datetime3).TotalSeconds >= 0)
                        {
                        status = status + "登陆";
                        }
                    }
                    else
                    {
                        //判断enter_time是否为空

                        if (dataFromDB.Rows[0]["实际进入时间"].ToString() != "")
                        {
                            status = status + "登出";
                        }
                        else
                        {
                            if (datetime1.Subtract(datetime3).TotalSeconds <= 1800 && datetime2.Subtract(datetime3).TotalSeconds >= 0)
                            {
                                status = status + "登陆";
                            }
                        }

                    }

                    result.Add(status);
                    result.Add(dataFromDB.Rows[0]["事由"].ToString());//add by jinxing

                }
            
            //多条记录
            else
            {
               //增加时间过滤0319
                for (int i = 0; i < dataFromDB.Rows.Count; i++)
                {
                    DateTime datetime1 = stringtodatetime(dataFromDB.Rows[i]["申请访问时间"].ToString());
                    DateTime datetime2 = stringtodatetime(dataFromDB.Rows[i]["申请离开时间"].ToString());
                    //如果当前日期不在申请访问时间和申请离开时间之间（以day计）
                    if (datetime3.Date .Subtract(datetime1.Date ).TotalDays < 0 || datetime3.Date .Subtract(datetime2.Date ).TotalDays > 0)
                    {
                        dataFromDB.Rows.RemoveAt(i);

                        i--;
                    }

                }
                //add  0517
                if (dataFromDB.Rows.Count == 0)
                {
                    return result;
                }

                //判断实际离开时间是否为空
                int f = 0;
                for (int i = 0; i < dataFromDB.Rows.Count; i++)
                {
                    if (dataFromDB.Rows[i]["实际离开时间"].ToString() == "")
                    {
                        f = 1;
                        break;
                    }
                }
                if(f==0)
                   
                    {
                        DateTime datetime1 = stringtodatetime(dataFromDB.Rows[0]["申请访问时间"].ToString());
                        DateTime datetime2 = stringtodatetime(dataFromDB.Rows[0]["申请离开时间"].ToString());

                        if (datetime1.Subtract(datetime3).TotalSeconds <= 1800 && datetime2.Subtract(datetime3).TotalSeconds >= 0)
                        {
                            //判断是否为群组用户
                            if (dataFromDB.Rows[0]["群组序号"].ToString() != "0")
                            {
                                status = "群组";
                            }
                            else
                            {
                                status = "个人";
                            }

                            status = status + "登陆";
                            result.Add(dataFromDB.Rows[0]["序号"].ToString());
                            result.Add(dataFromDB.Rows[0]["群组序号"].ToString());
                            result.Add(dataFromDB.Rows[0]["群组名称"].ToString());
                            result.Add(dataFromDB.Rows[0]["访客证件号"].ToString());
                            result.Add(dataFromDB.Rows[0]["访客姓名"].ToString());
                            result.Add(dataFromDB.Rows[0]["访客单位"].ToString());
                            result.Add(dataFromDB.Rows[0]["允许访问楼层"].ToString());
                            result.Add(dataFromDB.Rows[0]["接待人姓名"].ToString());
                            result.Add(dataFromDB.Rows[0]["接待人部门"].ToString());
                            result.Add(dataFromDB.Rows[0]["携带物"].ToString());
                            result.Add(dataFromDB.Rows[0]["申请访问时间"].ToString());
                            result.Add(dataFromDB.Rows[0]["申请离开时间"].ToString());
                            result.Add(dataFromDB.Rows[0]["实际进入时间"].ToString());
                            result.Add(dataFromDB.Rows[0]["实际离开时间"].ToString());

                            result.Add(dataFromDB.Rows[0]["record_ID"].ToString());
                            result.Add(dataFromDB.Rows[0]["临时卡号"].ToString());

                            result.Add(status);
                            result.Add(dataFromDB.Rows[0]["事由"].ToString()); //add by jinxing 
                        }
                    }
                

                if (f == 1)
                {
                    //判断enter_time是否为空
                    int flag = 1;
                    for (int i = 0; i < dataFromDB.Rows.Count; i++)
                    {
                        //如果enter_time不为空，则认为是登出操作
                        if (dataFromDB.Rows[i]["实际进入时间"].ToString() != "" && dataFromDB.Rows[i]["实际离开时间"].ToString() == "")
                        {
                            DateTime datetime1 = stringtodatetime(dataFromDB.Rows[i]["申请访问时间"].ToString());
                            DateTime datetime2 = stringtodatetime(dataFromDB.Rows[i]["申请离开时间"].ToString());

                            if (datetime1.Subtract(datetime3).TotalSeconds <= 1800  && datetime2.Subtract(datetime3).TotalSeconds >= 0)
                            {
                                //判断是否为群组用户
                                if (dataFromDB.Rows[0]["群组序号"].ToString() != "0")
                                {
                                    status = "群组";
                                }
                                else
                                {
                                    status = "个人";
                                }
                                status = status + "登出";
                                result.Add(dataFromDB.Rows[i]["序号"].ToString());
                                result.Add(dataFromDB.Rows[i]["群组序号"].ToString());
                                result.Add(dataFromDB.Rows[i]["群组名称"].ToString());
                                result.Add(dataFromDB.Rows[i]["访客证件号"].ToString());
                                result.Add(dataFromDB.Rows[i]["访客姓名"].ToString());
                                result.Add(dataFromDB.Rows[i]["访客单位"].ToString());
                                result.Add(dataFromDB.Rows[i]["允许访问楼层"].ToString());
                                result.Add(dataFromDB.Rows[i]["接待人姓名"].ToString());
                                result.Add(dataFromDB.Rows[i]["接待人部门"].ToString());
                                result.Add(dataFromDB.Rows[i]["携带物"].ToString());
                                result.Add(dataFromDB.Rows[i]["申请访问时间"].ToString());
                                result.Add(dataFromDB.Rows[i]["申请离开时间"].ToString());
                                result.Add(dataFromDB.Rows[i]["实际进入时间"].ToString());
                                result.Add(dataFromDB.Rows[i]["实际离开时间"].ToString());

                                result.Add(dataFromDB.Rows[i]["record_ID"].ToString());
                                result.Add(dataFromDB.Rows[i]["临时卡号"].ToString());

                                result.Add(status);

                                result.Add(dataFromDB.Rows[0]["事由"].ToString()); //add  by jinxing
                                flag = 0;                                
                            }
                        }
                        
                    }

                    //如果enter_time为空
                    //比较与datetime更接近的start_time
                    if (flag == 1)
                    {
                        //DateTime enter_time = DateTime.Now;
                        //TimeSpan time = datetime2.Subtract(datetime1);
                        int k = 0;
                        double a = 864000 * 2;
                        for (int j = 0; j < dataFromDB.Rows.Count; j++)
                        {
                            TimeSpan time = (Convert.ToDateTime(dataFromDB.Rows[j]["申请访问时间"])).Subtract(datetime3);
                            //a = System.Math.Abs(time.TotalSeconds) > a ? a : System.Math.Abs(time.TotalSeconds);
                            if (System.Math.Abs(time.TotalSeconds) < a)
                            {
                                a = System.Math.Abs(time.TotalSeconds);
                                k = j;
                            }
                        }
                        DateTime datetime1 = stringtodatetime(dataFromDB.Rows[k]["申请访问时间"].ToString());
                        DateTime datetime2 = stringtodatetime(dataFromDB.Rows[k]["申请离开时间"].ToString());

                        if (datetime1.Subtract(datetime3).TotalSeconds <= 1800 && datetime2.Subtract(datetime3).TotalSeconds >= 0)
                        {
                            //判断是否为群组用户
                            if (dataFromDB.Rows[0]["群组序号"].ToString() != "0")
                            {
                                status = "群组";
                            }
                            else
                            {
                                status = "个人";
                            }
                            status = status + "登陆";
                            result.Add(dataFromDB.Rows[k]["序号"].ToString());
                            result.Add(dataFromDB.Rows[k]["群组序号"].ToString());
                            result.Add(dataFromDB.Rows[k]["群组名称"].ToString());
                            result.Add(dataFromDB.Rows[k]["访客证件号"].ToString());
                            result.Add(dataFromDB.Rows[k]["访客姓名"].ToString());
                            result.Add(dataFromDB.Rows[k]["访客单位"].ToString());
                            result.Add(dataFromDB.Rows[k]["允许访问楼层"].ToString());
                            result.Add(dataFromDB.Rows[k]["接待人姓名"].ToString());
                            result.Add(dataFromDB.Rows[k]["接待人部门"].ToString());
                            result.Add(dataFromDB.Rows[k]["携带物"].ToString());
                            result.Add(dataFromDB.Rows[k]["申请访问时间"].ToString());
                            result.Add(dataFromDB.Rows[k]["申请离开时间"].ToString());
                            result.Add(dataFromDB.Rows[k]["实际进入时间"].ToString());
                            result.Add(dataFromDB.Rows[k]["实际离开时间"].ToString());

                            result.Add(dataFromDB.Rows[k]["record_ID"].ToString());
                            result.Add(dataFromDB.Rows[k]["临时卡号"].ToString());


                            result.Add(status);

                            result.Add(dataFromDB.Rows[0]["事由"].ToString()); //add by jinxing 

                            //result.Add(dataFromDB.Rows[i]["id"].ToString());
                            //result.Add(dataFromDB.Rows[i]["group_id"].ToString());
                            //result.Add(dataFromDB.Rows[i]["group_name"].ToString());
                            //result.Add(dataFromDB.Rows[i]["id_name"].ToString());
                            //result.Add(dataFromDB.Rows[i]["name"].ToString());
                            //result.Add(dataFromDB.Rows[i]["company"].ToString());
                            //result.Add(dataFromDB.Rows[i]["building"].ToString());
                            //result.Add(dataFromDB.Rows[i]["visit_person"].ToString());
                            //result.Add(dataFromDB.Rows[i]["visit_person"].ToString());
                            //result.Add(dataFromDB.Rows[i]["luggage"].ToString());
                            //result.Add(dataFromDB.Rows[i]["strat_time"].ToString());
                            //result.Add(dataFromDB.Rows[i]["end_time"].ToString());
                            //result.Add(dataFromDB.Rows[i]["enter_time"].ToString());
                            //result.Add(dataFromDB.Rows[i]["leave_time"].ToString());
                            //result.Add(status);

                        }
                    }
                    //如果实际离开时间都不为空
                    else
                    {

                    }

                }
            }
            //判定是否为群组用户
            //判定leave_time是否为空
            //判定enter_time是否为空
            //判定最接近datetime的ExpectTimeIn
            //返回唯一记录           
            return result;
        }



        //访客信息更新(登陆)
        //参数一为客户端输入值
        //返回值为1，成功，0失败
        public int UpdateDBInfo(string vistor_id, string luggage)
        {
            //http请求         


            bool flag = http_user_check_in(vistor_id, luggage);
            if (flag)// 请求成功
            {

                //成功 返回1            

                return 1;
            }

            //失败，返回0
            else
            {
                return 0;
            }
        }


        //访客信息更新(登出)
        //参数一为客户端输入值
        //返回值为1，成功，0失败
        public int  UpdateDBInfo2(string id)
        {
            //http请求         


            bool flag = http_user_check_out(id);
            
            if (flag)// 请求成功
            {

                //成功 返回1            

                return 1;
            }

            //失败，返回0
            else
            {
                return 0;
            }
        }
       







       

        //http连接，尝试3次连接，失败后转备用IP地址，重新3次连接，失败后，连接失败？？


      


        /*
      *  函数名: http_request
      *  功能:  向服务器发送http请求，请求获取某访客的信息
      *  参数： response_str为描述访客信息的字符串,response_str为输出参数 id 为访客身份证号。
      *  返回值: false 为http请求失败，true 为请求成功
      */
        public bool http_request(out string response_str, string id)
        {
            string http_str = null;
            http_str = string.Format("http://ip:port/real_times/check_coming_person?id_number={0:s}", id);
            // http://localhost:3000/real_times/check_guard?name=sunying&pwd=sunying
            // HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create("http://www.sogou.com");
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(http_str);
            HttpWebResponse HttpWResp = null;
            response_str = null;
            try
            {
                HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            }
            catch (WebException ee)
            {
                //MessageBox.Show(ee.Status.ToString());
                //MessageBox.Show(ee.Message);
                return false;
            }
            Stream reader = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(reader, Encoding.GetEncoding("utf-8"));
            response_str = sr.ReadToEnd();
            reader.Close();
            HttpWResp.Close();
            return true;

        }

        /*
      *  函数名: http_get_visitor
      *  功能:  向服务器发送http请求，请求获取某访客的信息
      *  参数： response_str为描述访客信息的字符串,response_str为输出参数 id 为访客身份证号。
      *  返回值: false 为http请求失败，true 为请求成功
         *  1为成功，2为失败，0为无记录
      */
        public int http_get_vistor(out string response_str, string id)
        {
            string http_str = null;
            string a = TxtToString() + "/real_times/check_visitor?id_number={0:s}";
            http_str = string.Format(a, id);

            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(http_str);

            HttpWebResponse HttpWResp = null;
            response_str = null;
            try
            {
                HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            }
            catch (WebException ee)
            {

                return 2;
            }
            Stream reader = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(reader, Encoding.GetEncoding("utf-8"));
            response_str = sr.ReadToEnd();
            reader.Close();
            HttpWResp.Close();
            if ("0" == response_str)
                return 0;
            else
                return 1;

        }

        /*
       *  函数名: http_user_login
       *  功能:  向服务器发送http请求，验证警卫登录。
       *  返回值: false 验证未通过，true 为验证通过
       */
        public int http_user_login(string user_name, string password)
        {
            string http_str = null;
            int flag = 0;
            string a = TxtToString() + "/real_times/check_guard?name={0:s}&pwd={1:s}";
            http_str = string.Format(a, user_name, password);

            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(http_str);
            HttpWebResponse HttpWResp = null;
            string response_str = null;
            try
            {
                HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            }
            catch (WebException ee)
            {

                return 2;
            }
            Stream reader = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(reader, Encoding.GetEncoding("utf-8"));
            response_str = sr.ReadToEnd();
            response_str.Trim();
            if ("0" == response_str)
                flag = 0;
            if ("1" == response_str)
                flag = 1;
            reader.Close();
            HttpWResp.Close();
            return flag;

        }

        /*
      *  函数名: http_check_in
      *  功能:  向服务器发送http请求，请求更新登入时间。
      *  返回值: false 更新成功，true 更新失败
      */
        public bool http_user_check_in(string vistor_id, string luggage)
        {
            string http_str = null;
            bool flag = false;
            string a = TxtToString() + "/real_times/insert_visitor?vid={0:s}&l={1:s}&cn={2:s}";
            http_str = string.Format(a, vistor_id, luggage);

            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(http_str);
            HttpWebResponse HttpWResp = null;
            string response_str = null;
            try
            {
                HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            }
            catch (WebException ee)
            {

                return false;
            }
            Stream reader = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(reader, Encoding.GetEncoding("utf-8"));
            response_str = sr.ReadToEnd();
            response_str.Trim();
            if ("insert failed" == response_str)
                flag = false;
            else
            { flag = true; }
            reader.Close();
            HttpWResp.Close();
            return flag;

        }

        /*
      *  函数名: http_check_out
      *  功能:  向服务器发送http请求，请求更新登出时间。
      *  返回值: false 更新成功，true 更新失败
      */
        public bool http_user_check_out(string id)
        {
            string http_str = null;
            bool flag = false;
            string a = TxtToString() + "/real_times/update_visitor?id={0:s}";
            http_str = string.Format(a,id);

            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(http_str);
            HttpWebResponse HttpWResp = null;
            string response_str = null;
            try
            {
                HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            }
            catch (WebException ee)
            {

                return false;
            }
            Stream reader = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(reader, Encoding.GetEncoding("utf-8"));
            response_str = sr.ReadToEnd();
            response_str.Trim();
            reader.Close();
            HttpWResp.Close();
            flag = true;
            return flag;

        }
        /*
    *  函数名: parse_a_row
    *  功能:  解析描述一行数据的字符串
    *  参数： row_str为从服务器中获取的一行的字符串
    */
        public ArrayList parse_a_row(string row_str)
        {
            ArrayList column_arraylist = new ArrayList(); //存储每一列，例如"id=1"
            row_str = row_str.Trim();

            int index = -1;
            int start_index = -1;
            string temp = null;
            do
            {
                index++;
                start_index = index;
                index = row_str.IndexOf("%", index); //modify by jinxing 3.5 将列的通配符由,改为 %
                if (index >= 0)
                {
                    temp = null;
                    temp = row_str.Substring(start_index, index - start_index);
                    column_arraylist.Add(temp);

                }
                else
                {
                    temp = null;
                    temp = row_str.Substring(start_index);
                    column_arraylist.Add(temp);
                }


            }
            while (index >= 0);
            ArrayList value_arraylist = new ArrayList();//存储列值，例如"1"
            index = 0;
            string column_str_temp = null;
            for (int i = 0; i < column_arraylist.Count; i++)
            {
                temp = null;
                column_str_temp = null;
                column_str_temp = column_arraylist[i] as string;
                index = column_str_temp.IndexOf("=");
                temp = column_str_temp.Substring(index + 1);
                value_arraylist.Add(temp);

            }
            return value_arraylist;

        }
        /*
         *  函数名: parse_rows
         *  功能:  解析描述数据的字符串,可能是一行的数据或者2行以上的数据
         *  参数： rows_str为从服务器中获取的数据的字符串
         */
        public ArrayList parse_rows(string rows_str)
        {
            int index = 0;
            ArrayList table_array = new ArrayList();
            ArrayList row_array = new ArrayList();
            index = rows_str.IndexOf("#");
            if (index < 0)//一行数据
            {
                row_array = parse_a_row(rows_str);
                table_array.Add(row_array);
            }//end 一行数据
            else //多行数据
            {
                rows_str = rows_str.Trim();
                index = -1;
                int start_index = -1;
                string temp = null;
                do
                {
                    index++;
                    start_index = index;
                    index = rows_str.IndexOf("#", index);
                    if (index >= 0)
                    {
                        temp = null;
                        temp = rows_str.Substring(start_index, index - start_index);
                        row_array = parse_a_row(temp);
                        table_array.Add(row_array);

                    }
                    else
                    {
                        temp = null;
                        temp = rows_str.Substring(start_index);
                        row_array = parse_a_row(temp);
                        table_array.Add(row_array);
                    }


                }
                while (index >= 0);


            }//end 多行数据
            return table_array;


        }
        /*人*/
        public DataTable array_to_datatable(ArrayList table_array)
        {
            DataTable vistor_tb = new DataTable();
            ArrayList row_array = new ArrayList();
            vistor_tb.Columns.Add("序号", typeof(string));    //0
            vistor_tb.Columns.Add("群组序号", typeof(string));//1
            vistor_tb.Columns.Add("群组名称", typeof(string));//2
            vistor_tb.Columns.Add("访客证件号", typeof(string));//3
            vistor_tb.Columns.Add("访客姓名", typeof(string));//4
            vistor_tb.Columns.Add("访客单位", typeof(string));//5
            vistor_tb.Columns.Add("允许访问楼层", typeof(string));//6;
            vistor_tb.Columns.Add("接待人姓名", typeof(string));//7
            vistor_tb.Columns.Add("接待人部门", typeof(string));//8
            vistor_tb.Columns.Add("携带物", typeof(string));//9
            vistor_tb.Columns.Add("申请访问时间", typeof(string));//10
            vistor_tb.Columns.Add("申请离开时间", typeof(string));//11
            vistor_tb.Columns.Add("实际进入时间", typeof(string));//12
            vistor_tb.Columns.Add("实际离开时间", typeof(string));//13


            vistor_tb.Columns.Add("record_ID", typeof(string));//14
            vistor_tb.Columns.Add("临时卡号", typeof(string));//15 
            vistor_tb.Columns.Add("事由", typeof(string));//16 

           
            //vistor_tb.Columns.Add("result", typeof(string));

            for (int i = 0; i < table_array.Count; i++)
            {
                row_array = table_array[i] as ArrayList;
                DataRow newRow = vistor_tb.NewRow();
                newRow["序号"] = row_array[0] as string;
                newRow["群组序号"] = row_array[6] as string;
                newRow["群组名称"] = row_array[7] as string;
                newRow["访客证件号"] = row_array[2] as string;
                newRow["访客姓名"] = row_array[1] as string;
                newRow["访客单位"] = row_array[9] as string;
                newRow["允许访问楼层"] = row_array[3] as string;
                newRow["接待人姓名"] = row_array[8] as string;
                //newRow["接待人部门"] = row_array[3] as string;
                newRow["接待人部门"] = "";
                newRow["携带物"] = row_array[12+1] as string;
                newRow["申请访问时间"] = row_array[4] as string;
                newRow["申请离开时间"] = row_array[5] as string;
                newRow["实际进入时间"] = row_array[10+1] as string;
                newRow["实际离开时间"] = row_array[11+1] as string;

                newRow["record_ID"] = row_array[13+1] as string;
                newRow["临时卡号"] = row_array[14+1] as string;
                newRow["事由"] = row_array[10] as string;
                vistor_tb.Rows.Add(newRow);

            }
            return vistor_tb;
        }

        /*车*/
        public DataTable car_array_to_datatable(ArrayList table_array)
        {
            DataTable vistor_tb = new DataTable();
            ArrayList row_array = new ArrayList();
            vistor_tb.Columns.Add("id", typeof(string));    //0
            vistor_tb.Columns.Add("name", typeof(string));//1
            vistor_tb.Columns.Add("id_number", typeof(string));//2
            vistor_tb.Columns.Add("building_name", typeof(string));//3
            vistor_tb.Columns.Add("start_time", typeof(string));//4
            vistor_tb.Columns.Add("end_time", typeof(string));//5
            vistor_tb.Columns.Add("phone", typeof(string));//6;
            vistor_tb.Columns.Add("matter", typeof(string));//7
            vistor_tb.Columns.Add("vp", typeof(string));//8
            vistor_tb.Columns.Add("vc", typeof(string));//9
            


           // vistor_tb.Columns.Add("record_ID", typeof(string));//14


            //vistor_tb.Columns.Add("result", typeof(string));

            for (int i = 0; i < table_array.Count; i++)
            {
                row_array = table_array[i] as ArrayList;
                DataRow newRow = vistor_tb.NewRow();
                newRow["id"] = row_array[0] as string;
                newRow["name"] = row_array[1] as string;
                newRow["id_number"] = row_array[2] as string;
                newRow["building_name"] = row_array[3] as string;
                newRow["start_time"] = row_array[4] as string;
                newRow["end_time"] = row_array[5] as string;
                newRow["phone"] = row_array[6] as string;
                newRow["matter"] = row_array[7] as string;
                //newRow["接待人部门"] = row_array[3] as string;
                newRow["vp"] = row_array[8] as string;
                newRow["vc"] = row_array[9] as string;


               // newRow["record_ID"] = row_array[13] as string;

                vistor_tb.Rows.Add(newRow);

            }
            return vistor_tb;
        }

        //将标准格式的string改成标准格式的datetime
        //标准格式的string格式为YYYY-MM-DD hh:mm:ss
        public DateTime stringtodatetime(string a)
        {
            
            //读取字符串
            int YYYY =Convert .ToInt32 ( a.Substring(0, 4));
            int MM = Convert.ToInt32(a.Substring(5, 2));
            int DD = Convert.ToInt32(a.Substring(8, 2));
            int hh= Convert.ToInt32(a.Substring(11, 2));
            int mm = Convert.ToInt32(a.Substring(14, 2));
            int ss = Convert.ToInt32(a.Substring(17, 2));
            DateTime datetime = new DateTime(YYYY, MM, DD, hh, mm,ss);

            return datetime;
        }

        public static void DataTableToTXT(DataTable dt, string pathTXT)
        {
            StreamWriter SW = new StreamWriter(pathTXT, false, System.Text.Encoding.Default);

            StringBuilder SB = new StringBuilder();
            int dtColumnsLength = dt.Columns.Count;
            //添加列名
            for (int j = 0; j < dtColumnsLength - 1; j++)
            {
                SB.Append(dt.Columns[j].ToString().Trim() + "\t");
            }
            SB.Append(dt.Columns[dtColumnsLength - 1].ToString().Trim());
            SW.WriteLine(SB.ToString());
            SB = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dtColumnsLength - 1; i++)
                {
                    SB.Append(row[i].ToString().Trim() + "\t");

                }
                SB.Append(row[dtColumnsLength - 1].ToString().Trim());
                SW.WriteLine(SB.ToString());
                SB = new StringBuilder();
            }

            SW.Close();
        }

        public static DataTable TxtToDataTable(string txtFilePath, int deleteStartRows)//deleteStartRows从文件起始位置删除的行数
        {
           
                StreamReader SR = new StreamReader(txtFilePath, System.Text.Encoding.Default);
                SR.BaseStream.Seek(0, SeekOrigin.Begin);
                for (int i = 0; i < deleteStartRows; i++)
                {
                    SR.ReadLine();
                }
                string strLine = SR.ReadLine();
                while (strLine.Trim() == "")
                {
                    strLine = SR.ReadLine();
                }
                string[] strLineAll = strLine.Split('\t');
                DataTable DT = new DataTable();
                foreach (string columnName in strLineAll)
                {
                    DT.Columns.Add(columnName, typeof(string));
                }
                strLine = SR.ReadLine();
                while (strLine != null)
                {
                    strLineAll = strLine.Split('\t');
                    if (strLine.Trim() == "" || strLineAll.Length < 1)
                    {
                        strLine = SR.ReadLine();
                        continue;
                    }
                    DataRow row = DT.NewRow();
                    for (int i = 0; i < DT.Columns.Count; i++)
                    {
                        if (i < strLineAll.Length)
                        {
                            row[i] = strLineAll[i];
                        }
                        else
                        {
                            row[i] = "";
                        }
                    }
                    DT.Rows.Add(row);
                    strLine = SR.ReadLine();
                    
                }
                SR.Close();
                SR.Dispose();
            
            

            return DT;


        }

        public static string process_person_id_str(string person_id)
        {
            string result_str = null;
            int pos = person_id.IndexOf("\0");
            int str_length = pos - 0;
            if (pos >= 0)
                result_str = person_id.Substring(0, str_length);
            return result_str;

        }

        private  string TxtToString()
        {
            try
            {
                string  startpath = Application.StartupPath;
                //MessageBox.Show(startpath.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                startpath = System.Environment.CurrentDirectory;
                //MessageBox.Show(startpath.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                string txtFilePath = "";
                if (startpath == "C:\\usr\\" + System.Environment.UserName)
                {
                    txtFilePath = "C:\\programme files\\CCB\\vms2.1\\访客管理系统\\media\\url.txt";
                    
                }
                else
                {
                    txtFilePath = startpath.Replace("bin\\Release", "") + "media\\url.txt";
                }
                StreamReader SR = new StreamReader(txtFilePath, System.Text.Encoding.Default);
                string urlstr = SR.ReadToEnd().ToString().Trim();
                SR.Close();
                return urlstr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString (), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }


        }


        public static void IDcheck(string person_ID)
        {
            try
            {
                if (person_ID == "")
                {
                    MessageBox.Show("证件号不能为空值，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //连接服务器搜索
                    Function checkfunction = new Function();
                    string atemp = person_ID;
                    ArrayList a = checkfunction.GetDBInfo(atemp);

                    //test code
                    //个人登陆
                    /*String[] personalLoginArray = { "success", "G1", "AMTP", "1234567", "John", "IBM", "A", "Mandy","Commercial Ad Depart","project A","个人登陆","" };
                    //个人登出
                    String[] personalLogoutArray = { "success", "G1", "AMTP", "1234567", "John", "IBM", "A", "Mandy", "Commercial Ad Depart", "背包", "个人登出", "" };
                    //团体登陆
                    String[] GroupLoginArray = { "success", "G1", "AMTP", "1234567", "John", "IBM", "A", "Mandy", "Commercial Ad Depart", "背包", "", "","","","","","群组登陆","Project A" };
                    //团体登出
                    String[] GroupLogoutArray = { "success", "G1", "AMTP", "1234567", "John", "IBM", "A", "Mandy", "Commercial Ad Depart", "背包", "", "", "", "", "", "", "群组登出", "Project A" };
                    //no result
                    String[] zeroArray = { };

                    ArrayList a = new ArrayList(personalLoginArray);*/

                    if (a.Count == 0)
                    {
                      
                        
                        VistorReject vistorreject = VistorReject.GetVistorReject();
                        vistorreject.Show();
                    }
                    else if (a[0].ToString().Trim() == "连接失败")
                    {
                        MessageBox.Show("连接失败，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                    }
                    else if (a[0].ToString().Trim() == "无该访客记录")
                    {
                       
                        VistorReject vistorreject = VistorReject.GetVistorReject();
                        vistorreject.Show();
                    }

                    else
                    {
                        //核对信息，正确
                        //匹配窗口种类
                        string status = "";
                        status = a[a.Count - 2].ToString().Trim();

                        switch (status)
                        {
                            case "个人登出":
                                VisitorLogout vistorlogout = VisitorLogout.GetVistorLogout();
                                vistorlogoutcheck(a);
                                break;
                            case "个人登陆":
                                VisitorLogin vistorlogin = VisitorLogin.GetVistorLogin();
                                vistorlogincheck(a);
                                break;
                            case "群组登出":
                                grouplogoutcheck(a);
                                break;
                            case "群组登陆":
                                grouplogincheck(a);
                                break;
                            default:
                                break;


                        }
                    }

                }
                //正确连接
            }
            catch (Exception ex)
            {

                MessageBox.Show("数据库连接错误，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

          
        }


        //检查是否正在进行群组登记，如果是，则稍候进行操作。
        private static void vistorlogoutcheck(ArrayList a)
        {
            if (GroupLogin.instance != null)
            {
                MessageBox.Show("正在进行团体登记中，请稍候登记。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
            
                    VisitorLogout vistorlogout = VisitorLogout.GetVistorLogout();
                    vistorlogout.VistorInfo(a);
                    vistorlogout.Show();
              
              
            }
        }

        //检查是否正在进行群组登记，如果是，则稍候进行操作。
        private static void vistorlogincheck(ArrayList a)
        {
            if (GroupLogin.instance != null)
            {
                MessageBox.Show("正在进行团体登记中，请稍候登记。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                VisitorLogin vistorlogin = VisitorLogin.GetVistorLogin();
                vistorlogin.VistorInfo(a);
                vistorlogin.Show();
            }
        }

        //检查是否正在进行群组登记，如果是，则稍候进行操作。
        private static void grouplogoutcheck(ArrayList a)
        {
            if (GroupLogin.instance != null)
            {
                MessageBox.Show("正在进行团体登记中，请稍候登记。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                GroupLogout grouplogout = GroupLogout.GetGroupLogout();
                grouplogout.GroupInfo(a);
                grouplogout.Show();
            }
        }

        //检查是否正在进行群组登记，如果不是，正常开始群组登记；如果是，则检查该访客是否归属与该群组，不是则拒绝，是则登记
        private static void grouplogincheck(ArrayList a)
        {
            if (GroupLogin.instance == null)
            {
                GroupLogin grouplogin = GroupLogin.GetGroupLogin();
                grouplogin.GroupInfo(a);
                grouplogin.Show();
                grouplogin.CallMemInfo();
                //this.Close();
                //MessageBox.Show("正在进行团体登记中，请稍候登记。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                GroupLogin grouplogin = GroupLogin.GetGroupLogin();

                int checkgroupid = grouplogin.checkISGROUP(a[1].ToString().Trim());
                if(checkgroupid !=0)
                {
                    MessageBox.Show("该访客不属于正在登记的团体，请稍候登记。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    GroupMember groupmember = GroupMember.GetGroupMember();
                    if (a[6].ToString() == "A")
                    {
                        groupmember.visitorinfo2(a);
                    }
                    else
                    {
                        groupmember.visitorinfo(a);
                    }
                    groupmember.Show();
                    //this.Close();

                }

            }
        }

        //定时调用批量写入
        //public void 

        //批量向数据库写入信息
        public  void BATToWrite()
        {
            //个人登入
            string visitorlogin_request = System.Environment.CurrentDirectory.Replace("bin\\Release", "") + "temp\\visitorlogin.txt";
            DataTable visitorlogindt = new DataTable();
            if (File.Exists(visitorlogin_request))
            {
                visitorlogindt= TxtToDataTable ( visitorlogin_request,0);
            }
            if (visitorlogindt.Rows.Count != 0)
            {
                //进行数据库连接
                for (int i = 0; i < visitorlogindt.Rows.Count; i++)
                {
                    int j = UpdateDBInfo(visitorlogindt.Rows[i][0].ToString().Trim(), visitorlogindt.Rows[i][1].ToString().Trim());
                    if (j == 1)
                    {
                        visitorlogindt.Rows.RemoveAt(i);
                        i = i - 1; 
                    }
                }
            }
            if (visitorlogindt.Rows.Count != 0)
            {
                //更新临时文件
                File.Delete(visitorlogin_request);
                DataTableToTXT(visitorlogindt,visitorlogin_request);
            }
            else if (visitorlogindt.Rows.Count == 0)
            {
                File.Delete(visitorlogin_request);
            }


            //个人登出
            string visitorlogout_request = System.Environment.CurrentDirectory.Replace("bin\\Release", "") + "temp\\visitorlogin.txt";
            DataTable visitorlogoutdt = new DataTable();
            if (File.Exists(visitorlogout_request))
            {
                visitorlogoutdt = TxtToDataTable(visitorlogout_request, 0);
            }
            if (visitorlogoutdt.Rows.Count != 0)
            {
                //进行数据库连接
                for (int i = 0; i < visitorlogoutdt.Rows.Count; i++)
                {
                    int j = UpdateDBInfo2(visitorlogoutdt.Rows[i][0].ToString().Trim());
                    if (j == 1)
                    {
                        visitorlogoutdt.Rows.RemoveAt(i);
                        i = i - 1;
                    }
                }
            }
            if (visitorlogoutdt.Rows.Count != 0)
            {
                //更新临时文件
                File.Delete(visitorlogout_request);
                DataTableToTXT(visitorlogoutdt, visitorlogout_request);
            }
            else if (visitorlogoutdt.Rows.Count == 0)
            {
                File.Delete(visitorlogout_request);
            }

            //群组登入
            string grouplogin_request = System.Environment.CurrentDirectory.Replace("bin\\Release", "") + "temp\\visitorlogin.txt";
            DataTable grouplogindt = new DataTable();
            if (File.Exists(grouplogin_request))
            {
                grouplogindt = TxtToDataTable(grouplogin_request, 0);
            }
            if (grouplogindt.Rows.Count != 0)
            {
                //进行数据库连接
                for (int i = 0; i < grouplogindt.Rows.Count; i++)
                {
                    int j = UpdateDBInfo(grouplogindt.Rows[i][0].ToString().Trim(), grouplogindt.Rows[i][1].ToString().Trim());
                    if (j == 1)
                    {
                        grouplogindt.Rows.RemoveAt(i);
                        i = i - 1;
                    }
                }
            }
            if (grouplogindt.Rows.Count != 0)
            {
                //更新临时文件
                File.Delete(grouplogin_request);
                DataTableToTXT(grouplogindt, grouplogin_request);
            }
            else if (grouplogindt.Rows.Count == 0)
            {
                File.Delete(grouplogin_request);
            }

            //群组登出
            string grouplogout_request = System.Environment.CurrentDirectory.Replace("bin\\Release", "") + "temp\\visitorlogin.txt";
            DataTable grouplogoutdt = new DataTable();
            if (File.Exists(grouplogout_request))
            {
                grouplogoutdt = TxtToDataTable(grouplogout_request, 0);
            }
            if (grouplogoutdt.Rows.Count != 0)
            {
                //进行数据库连接
                for (int i = 0; i < grouplogoutdt.Rows.Count; i++)
                {
                    int j = UpdateDBInfo2(grouplogoutdt.Rows[i][0].ToString().Trim());
                    if (j == 1)
                    {
                        grouplogoutdt.Rows.RemoveAt(i);
                        i = i - 1;
                    }
                }
            }
            if (grouplogoutdt.Rows.Count != 0)
            {
                //更新临时文件
                File.Delete(grouplogout_request);
                DataTableToTXT(grouplogoutdt, grouplogout_request);
            }
            else if (grouplogoutdt.Rows.Count == 0)
            {
                File.Delete(grouplogout_request);
            }
            //车辆
        }

        //车辆信息查询
        //返回值为车辆信息
        public DataTable GetCardDBInfo(out int get_flag)
        {
            ArrayList checkresult = new ArrayList();
            //http连接，若得到对应数据放于ArrayList checkresult中
            DataTable tb = new DataTable();
            string response_str = null;

            int flag = http_get_car_id(out response_str);
            if (flag == 1)// 请求成功
            {

                ArrayList temp_array = new ArrayList();
                temp_array = parse_rows(response_str);
                 tb = car_array_to_datatable(temp_array);

                //mainpanel mainpanel1 = mainpanel.Getmainpanel();
                //DataTable dataFromDB = mainpanel1.dtvistor_today.Clone();

                //DateTime test1 = new DateTime(2011, 8, 23, 9, 0, 0);//
                //DateTime test2 = new DateTime(2011, 8, 23, 11, 0, 0);//
                //dataFromDB.Rows.Add("1", "G1", "AMTP", "1234567", "John", "IBM", "A", "Tom", "System 5", "", test1, test2, "", "");



                //checkresult = UniqueCheck(dataFromDB);

               // checkresult = UniqueCheck(tb);
                 get_flag = 1;
                return tb;

            }
            else if (flag == 0)//无记录
            {
                //tb.Rows[0][0] = "无车辆记录";
                get_flag = 0;
                return tb;
            }
            else //连接不成功
            {
                //若没有取得对应数据,不做任何操作
                //tb.Rows[0][0] = "连接失败";
                get_flag = -1;
                return tb;
            }



        }
        /*
         *  函数名: http_get_car_id
         *  功能:  向服务器发送http请求，请求获取某访客的信息
         *  参数： response_str为描述车辆信息的字符串,response_str为输出参数。
         *  返回值: false 为http请求失败，true 为请求成功
            *  1为成功，2为失败，0为无记录
         */
        public int http_get_car_id(out string response_str)
        {
            string http_str = null;

            string a = TxtToString() + "/real_times/check_car";
             http_str = a;
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(http_str);

            HttpWebResponse HttpWResp = null;
            response_str = null;
            try
            {
                HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            }
            catch (WebException ee)
            {
                //MessageBox.Show(ee.Status.ToString());
                MessageBox.Show(ee.Message);
                return 2;
            }
            Stream reader = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(reader, Encoding.GetEncoding("utf-8"));
            response_str = sr.ReadToEnd();
            reader.Close();
            HttpWResp.Close();
            if ("0" == response_str)
                return 0;
            else
                return 1;

        }



        //特定车辆详细信息查询
        //返回值为车辆信息
        public ArrayList Get_Car_Detail(string car_id)
        {
            ArrayList temp_array = new ArrayList();
            //http连接，若得到对应数据放于ArrayList checkresult中
            DataTable tb = new DataTable();
            string response_str = null;
            response_str = http_get_car_detail(car_id);

            temp_array = parse_a_row(response_str);
            //tb = car_array_to_datatable(temp_array);
           return temp_array;

        }


        /*
 *  函数名: http_get_car_detail
 *  功能:  向服务器发送http请求，请求获取某车辆的详细信息
 *  参数： response_str为描述车辆信息的字符串,response_str为输出参数。
 *  返回值: false 为http请求失败，true 为请求成功
    *  1为成功，2为失败，0为无记录
 */
        public string  http_get_car_detail(string car_id)
        {
            string response_str = null;
            response_str = "京123456%王三%吴猛%123456789%20131202%阿斯蒂芬了空间啊阿斯顿福建阿隆索的房间";
            return response_str;
            
            string http_str = null;
            //string a = TxtToString() + "/real_times/check_visitor?id_number={0:s}";
            string a = TxtToString() + "/real_times/check_car_detail?car_id={0:s}";
            http_str = string.Format(a, car_id);
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(http_str);
            HttpWebResponse HttpWResp = null;
            try
            {
                HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            }
            catch (WebException ee)
            {
                //MessageBox.Show(ee.Status.ToString());
                MessageBox.Show(ee.Message);
                return "1";
            }
            Stream reader = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(reader, Encoding.GetEncoding("utf-8"));
            response_str = sr.ReadToEnd();
            reader.Close();
            HttpWResp.Close();
            if ("0" == response_str)

                return "0";
            else
                //temp_array = parse_rows(response_str);
                //return temp_array;
                return response_str;

        }


        /// <summary>
        /// 延时函数
        /// </summary>
        /// <param name="delayTime">需要延时多少秒</param>
        /// <returns></returns>
        public  bool Delay(int delayTime)
        {
            DateTime now = DateTime.Now;
            int s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.Seconds;
                Application.DoEvents();
            }
            while (s < delayTime);
            return true;
        }

    }
}
