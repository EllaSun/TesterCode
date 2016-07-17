using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Media; 
using System.IO ;


namespace VMS
{

    public partial class VisitorLogin : Form
    {
        DateTime datetime = DateTime.Now;
        ArrayList visitorinfo = new ArrayList();
        Function checkfunction = new Function();
        static string startpath = System.Environment.CurrentDirectory;
        public string error_path = startpath.Replace("bin\\Release", "") + "media\\error.wav";
        public string success_path = startpath.Replace("bin\\Release", "") + "media\\success.wav";
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static VisitorLogin instance = null;
        internal static VisitorLogin GetVistorLogin()
        {
            if (VisitorLogin.instance == null)
            {
                VisitorLogin.instance = new VisitorLogin();
            }
            return VisitorLogin.instance;
        }
        #endregion 获取主窗体
        public VisitorLogin()
        {
            InitializeComponent();
        }

        private void VistorInfoCheck_Load(object sender, EventArgs e)
        {

            PaperSize ps = new PaperSize("Your Paper Name", 693, 950);
            ps.RawKind = 13;
            this.printDocument1.DefaultPageSettings.PaperSize = ps;


            //打印开始前           
            this.printDocument1.BeginPrint += new PrintEventHandler(printDocument_BeginPrint);
            ///打印输出（过程）           
            this.printDocument1.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);
            //打印结束            
            this.printPreviewDialog1.Document = this.printDocument1;
        }
        void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {            //也可以把一些打印的参数放在此处设置   

            this.printDocument1.DefaultPageSettings.Landscape = true;
        }

        public void VistorInfo(ArrayList a)
        {


            label16.Text = "访客信息核对正确！\r\n请允许通过。";
            button_Pass.Visible = false;
            button_Print.Visible = true;

            visitorinfo = a;
            //textBox_ID.Text = a[0].ToString().Trim();
            textBox_VistorID.Text = a[3].ToString().Trim();
            textBox_VistorName.Text = a[4].ToString().Trim();
            textBox_VistorDepart.Text = a[5].ToString().Trim();
            textBox_VistorBuilding.Text = a[6].ToString().Trim();
            //textBox_ExpectTimeIn.Text = a[10].ToString().Trim();
            //textBox_ExpectTimeOut.Text = a[11].ToString().Trim();
            textBox_HostName.Text = a[7].ToString().Trim();
            textBox_HostDepart.Text = a[8].ToString().Trim();
            textBox_resaon.Text = a[9].ToString().Trim();
          
        }
        private void button_Print_Click(object sender, EventArgs e)
        {

 
               //PASS_Check();
               visitorinfo[9] = comboBox_VistorLuggage.Text;
                //调用打印函数
                this.printPreviewDialog1.ShowDialog();
                this.Close();
            
        }

        private void button_Pass_Click(object sender, EventArgs e)
        {

        }

        //同意用户进入
        private void PASS_Check()
        {
            mainpanel mainpanel1 = mainpanel.Getmainpanel();
            //MessageBox.Show("Pass", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int success_check = httprequest();
            if (success_check == 0)
            {
              
                 //数据库请求失败，将请求写入临时文件，准备下一次请求。
                DataTable dt=new DataTable ();
                
                dt.Columns.Add("id");
                dt.Columns.Add("luggage");
                dt.Columns.Add("linshika");
                DataRow dr1 = dt.NewRow();
                dt.Rows.Add(dr1);
                dt.Rows [0][0] = visitorinfo[0];
                dt.Rows[0][1] = comboBox_VistorLuggage.Text;
                //dt.Rows[0][2] = textBox_linshika.Text;
                string unsuccessful_request = System.Environment.CurrentDirectory.Replace("bin\\Release", "") + "temp\\visitorlogin.txt"; 
                if(!File.Exists(unsuccessful_request))
                {
                    Function.DataTableToTXT(dt, unsuccessful_request);
                }
                else if (File.Exists(unsuccessful_request))
                {
                    DataTable dttemp = Function.TxtToDataTable(unsuccessful_request, 0);
                    //dttemp.Rows.Add(dt.Rows[0][0].ToString().Trim(), dt.Rows[0][1].ToString().Trim());
                    DataRow dr = dttemp.NewRow();
                    dr[0] = dt.Rows[0][0].ToString().Trim();
                    dr[1] = dt.Rows[0][1].ToString().Trim();
                    dttemp.Rows.Add(dr);
                    File.Delete(unsuccessful_request);
                    Function.DataTableToTXT(dttemp, unsuccessful_request);
                }
            }
            //else if (success_check == 1)
            //{
               
            //}
        }

        private int httprequest()
        {
            DataTable a_in = new DataTable();
            a_in.Columns.Add("id");            
            a_in.Columns.Add("luggage");
            //访客ID号                           
            //实际进入时间     
            //携带物
            a_in.Rows.Add(visitorinfo[0].ToString(), comboBox_VistorLuggage.Text.ToString());
            //写入数据库，并通过返回值检查            
            int success_check = checkfunction.UpdateDBInfo(a_in.Rows[0][0].ToString(), a_in.Rows[0][1].ToString());
            return success_check;
        }

        private void button_Reject_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void docToPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)//设置打印机开始打印的事件处理函数  
        {
            print_person(e);
        }
        private void print_person(PrintPageEventArgs e)
        {

            float x_factor = (693.0f / (210.0f)) * 1.38f;
            float y_factor = (950.0f / (75.0f)) * 0.75f;
            string text = "进  楼  证  存  根 ";
            string text1 = "进      楼      证";
            System.Drawing.Font header_font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font content_font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular );
            System.Drawing.Font time_font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold );
            string name, sex, num, address, vistor_building, meet_person, luggage, person_id, enter_time, leave_time, guard,matter;
            //name = "金星";
            //sex = "男";
            //num = "1人";
            //address = "中国建设银行北京数据中心";
            //vistor_building = "A座";
            //meet_person = "数据中心系统管理一部李晓峰";
            //luggage = "笔记本电脑";
            //person_id = "231024198409170015";
            //enter_time = "2014-11-31 8:50:15";
            //leave_time = "2014-11-31 8:50:15";
            //guard = "刘德华";
            matter = "";

            name = visitorinfo[4].ToString();
            sex = "";
            num = "1人";
            address = visitorinfo [5].ToString ();
            vistor_building = visitorinfo[6].ToString();
            //meet_person = visitorinfo[8].ToString() + "-" + visitorinfo[7].ToString ();// "数据中心系统管理一部李晓峰";
            meet_person = visitorinfo[7].ToString ();// "数据中心系统管理一部李晓峰";
            luggage = visitorinfo[9].ToString();
            person_id = visitorinfo[3].ToString();
            enter_time = datetime.ToString ();
            leave_time = "";
            guard = "";
            matter = visitorinfo[visitorinfo.Count - 1].ToString();

            //e.Graphics.DrawString(text, printFont, System.Drawing.Brushes.Black, e.MarginBounds.X, e.MarginBounds.Y);
            //e.Graphics.DrawLine(10, 150);
            e.Graphics.DrawString(text, header_font, System.Drawing.Brushes.Black, 28 * x_factor, 10 * y_factor);
            e.Graphics.DrawString(text1, header_font, System.Drawing.Brushes.Black, (105 + 30) * x_factor, 10 * y_factor); //抬头

            Pen pen = new Pen(Color.Black, 1);
            pen.Width = 0.06f;
            pen.DashStyle = DashStyle.Dot;

            Point from = new Point(Convert.ToInt32(105 * x_factor), 0);
            Point to = new Point(Convert.ToInt32(105 * x_factor), Convert.ToInt32(75 * y_factor));
            e.Graphics.DrawLine(pen, from, to); //描绘虚线 

            pen.Width = 0.08f;
            pen.DashStyle = DashStyle.Solid;
            int x_left = Convert.ToInt32(10 * x_factor); int x_right = Convert.ToInt32(95 * x_factor);
            int y_top = Convert.ToInt32(18 * y_factor); int y_bottom = Convert.ToInt32(71 * y_factor);
            //....................................................进楼证存根................................................
            //画框
            from.X = x_left; from.Y = y_top;
            to.X = x_right; to.Y = y_top;
            e.Graphics.DrawLine(pen, from, to);
            from.X = x_left; from.Y = y_top;
            to.X = x_left; to.Y = y_bottom;
            e.Graphics.DrawLine(pen, from, to);
            from.X = x_left; from.Y = y_bottom;
            to.X = x_right; to.Y = y_bottom;
            e.Graphics.DrawLine(pen, from, to);
            from.X = x_right; from.Y = y_bottom;
            to.X = x_right; to.Y = y_top;
            e.Graphics.DrawLine(pen, from, to);
            //end 画框

            //第一行

            // 第一列
            from.X = x_left; from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "来 客\n姓 名";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 2 * x_factor), y_top + Convert.ToInt32(2 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            e.Graphics.DrawString(name, time_font, System.Drawing.Brushes.Black, Convert.ToInt32(to.X + 9 * x_factor), y_top + Convert.ToInt32(4 * y_factor));

            //第二列
            from.X = Convert.ToInt32(x_left + 38 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 38 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            //第三列
            from.X = Convert.ToInt32(x_left + 42 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 42 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "性\n别";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 38 * x_factor), y_top + Convert.ToInt32(2 * y_factor));

            e.Graphics.DrawString(sex, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(7 * x_factor), y_top + Convert.ToInt32(4 * y_factor));
            //第四列
            from.X = Convert.ToInt32(x_left + 61 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 61 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            //第五列
            from.X = Convert.ToInt32(x_left + 68 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 68 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = " 人\n 数";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 61 * x_factor), y_top + Convert.ToInt32(2 * y_factor));

            e.Graphics.DrawString(num, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(6 * x_factor), y_top + Convert.ToInt32(4 * y_factor));

            //第二行 
            //第一列 
            from.X = x_left; from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "单位或\n 住址";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 1 * x_factor), y_top + Convert.ToInt32(12 * y_factor));

            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            e.Graphics.DrawString(address, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(7 * x_factor), y_top + Convert.ToInt32(14 * y_factor));
            //第二列
            from.X = Convert.ToInt32(x_left + 61 * x_factor); from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = Convert.ToInt32(x_left + 61 * x_factor); to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            //第三列
            from.X = Convert.ToInt32(x_left + 68 * x_factor); from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = Convert.ToInt32(x_left + 68 * x_factor); to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "访问\n楼层";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 61 * x_factor), y_top + Convert.ToInt32(12 * y_factor));

            e.Graphics.DrawString(vistor_building, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(6 * x_factor), y_top + Convert.ToInt32(14 * y_factor));
            //第三行
            //第一列
            from.X = x_left; from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "会见何\n单位何\n  人";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 1 * x_factor), y_top + Convert.ToInt32(21 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            e.Graphics.DrawString(meet_person, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(5 * x_factor), y_top + Convert.ToInt32(23 * y_factor));

            //第二列
            from.X = Convert.ToInt32(x_left + 61 * x_factor); from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = Convert.ToInt32(x_left + 61 * x_factor); to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            //第三列 
            from.X = Convert.ToInt32(x_left + 68 * x_factor); from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = Convert.ToInt32(x_left + 68 * x_factor); to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "携\n带\n物";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 62 * x_factor), y_top + Convert.ToInt32(21 * y_factor));
            e.Graphics.DrawString(luggage, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(1 * x_factor), y_top + Convert.ToInt32(24 * y_factor));
            //第四行
            //第一列 
            from.X = x_left; from.Y = y_top + Convert.ToInt32(44 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(44 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "证件号";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 1 * x_factor), y_top + Convert.ToInt32(37 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(43 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            e.Graphics.DrawString(person_id, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(7 * x_factor), y_top + Convert.ToInt32(37 * y_factor));
            //第二列
            from.X = Convert.ToInt32(x_left + 61 * x_factor); from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = Convert.ToInt32(x_left + 61 * x_factor); to.Y = y_top + Convert.ToInt32(44 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            //第三列 
            from.X = Convert.ToInt32(x_left + 68 * x_factor); from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = Convert.ToInt32(x_left + 68 * x_factor); to.Y = y_top + Convert.ToInt32(44 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "值\n班\n员";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 62 * x_factor), y_top + Convert.ToInt32(33 * y_factor));
            e.Graphics.DrawString(guard, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(3 * x_factor), y_top + Convert.ToInt32(36 * y_factor));
            //第五行 
            //第一列 
            text = "进楼\n时间";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 3 * x_factor), y_top + Convert.ToInt32(45 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(43 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(53 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            e.Graphics.DrawString(enter_time, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(1 * x_factor), y_top + Convert.ToInt32(47 * y_factor));
            //第二列 
           // from.X = Convert.ToInt32(x_left + 43 * x_factor); from.Y = y_top + Convert.ToInt32(44 * y_factor);
            //to.X = Convert.ToInt32(x_left + 43 * x_factor); to.Y = y_top + Convert.ToInt32(53 * y_factor);
            //e.Graphics.DrawLine(pen, from, to);
            //第三列
           // text = "离开\n时间";
           // e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 46 * x_factor), y_top + Convert.ToInt32(45 * y_factor));
           // from.X = Convert.ToInt32(x_left + 56 * x_factor); from.Y = y_top + Convert.ToInt32(44 * y_factor);
           // to.X = Convert.ToInt32(x_left + 56 * x_factor); to.Y = y_top + Convert.ToInt32(53 * y_factor);
           // e.Graphics.DrawLine(pen, from, to);
           // e.Graphics.DrawString(leave_time, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(1 * x_factor), y_top + Convert.ToInt32(47 * y_factor));
            //....................................................end进楼证存根........................................... 


            x_left = Convert.ToInt32((10 + 105) * x_factor); x_right = Convert.ToInt32((95 + 105) * x_factor);
            y_top = Convert.ToInt32(18 * y_factor); y_bottom = Convert.ToInt32(71 * y_factor);
            //........................................................进楼证..............................................
            //画框
            from.X = x_left; from.Y = y_top;
            to.X = x_right; to.Y = y_top;
            e.Graphics.DrawLine(pen, from, to);
            from.X = x_left; from.Y = y_top;
            to.X = x_left; to.Y = y_bottom;
            e.Graphics.DrawLine(pen, from, to);
            from.X = x_left; from.Y = y_bottom;
            to.X = x_right; to.Y = y_bottom;
            e.Graphics.DrawLine(pen, from, to);
            from.X = x_right; from.Y = y_bottom;
            to.X = x_right; to.Y = y_top;
            e.Graphics.DrawLine(pen, from, to);
            //end 画框

            //第一行

            // 第一列
            from.X = x_left; from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "来 客\n姓 名";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 2 * x_factor), y_top + Convert.ToInt32(2 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            e.Graphics.DrawString(name, time_font, System.Drawing.Brushes.Black, Convert.ToInt32(to.X + 9 * x_factor), y_top + Convert.ToInt32(4 * y_factor));
            //第二列
            from.X = Convert.ToInt32(x_left + 38 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 38 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            //第三列
            from.X = Convert.ToInt32(x_left + 42 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 42 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "性\n别";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 38 * x_factor), y_top + Convert.ToInt32(2 * y_factor));
            e.Graphics.DrawString(sex, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(7 * x_factor), y_top + Convert.ToInt32(4 * y_factor));
            //第四列
            from.X = Convert.ToInt32(x_left + 61 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 61 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            //第五列
            from.X = Convert.ToInt32(x_left + 68 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 68 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = " 人\n 数";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 61 * x_factor), y_top + Convert.ToInt32(2 * y_factor));
            e.Graphics.DrawString(num, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(6 * x_factor), y_top + Convert.ToInt32(4 * y_factor));
            //第二行 
            //第一列 
            from.X = x_left; from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "单位或\n 住址";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 1 * x_factor), y_top + Convert.ToInt32(12 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            e.Graphics.DrawString(address, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(7 * x_factor), y_top + Convert.ToInt32(14 * y_factor));
            //第二列
            from.X = Convert.ToInt32(x_left + 61 * x_factor); from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = Convert.ToInt32(x_left + 61 * x_factor); to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            //第三列
            from.X = Convert.ToInt32(x_left + 68 * x_factor); from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = Convert.ToInt32(x_left + 68 * x_factor); to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "访问\n楼层";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 61 * x_factor), y_top + Convert.ToInt32(12 * y_factor));
            e.Graphics.DrawString(vistor_building, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(6 * x_factor), y_top + Convert.ToInt32(14 * y_factor));
            //第三行
            //第一列
            from.X = x_left; from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "会见何\n单位何\n  人";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 1 * x_factor), y_top + Convert.ToInt32(21 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            e.Graphics.DrawString(meet_person, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(5 * x_factor), y_top + Convert.ToInt32(23 * y_factor));
            //第二列
            from.X = Convert.ToInt32(x_left + 61 * x_factor); from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = Convert.ToInt32(x_left + 61 * x_factor); to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            //第三列 
            from.X = Convert.ToInt32(x_left + 68 * x_factor); from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = Convert.ToInt32(x_left + 68 * x_factor); to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "携\n带\n物";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 62 * x_factor), y_top + Convert.ToInt32(21 * y_factor));
            e.Graphics.DrawString(luggage, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(1 * x_factor), y_top + Convert.ToInt32(24 * y_factor));
            //第四行
            //第一列 
            from.X = x_left; from.Y = y_top + Convert.ToInt32(44 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(44 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = " 进楼\n 时间";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 1 * x_factor), y_top + Convert.ToInt32(35 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(43 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            e.Graphics.DrawString(enter_time, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(7 * x_factor), y_top + Convert.ToInt32(37 * y_factor));
            //第二列
            from.X = Convert.ToInt32(x_left + 61 * x_factor); from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = Convert.ToInt32(x_left + 61 * x_factor); to.Y = y_top + Convert.ToInt32(44 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            //第三列 
            from.X = Convert.ToInt32(x_left + 68 * x_factor); from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = Convert.ToInt32(x_left + 68 * x_factor); to.Y = y_top + Convert.ToInt32(44 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "事\n由";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 62 * x_factor), y_top + Convert.ToInt32(34 * y_factor));
            //e.Graphics.DrawString(matter, time_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 62 * x_factor), y_top + Convert.ToInt32(34 * y_factor));
            e.Graphics.DrawString(matter, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(1 * x_factor), y_top + Convert.ToInt32(37 * y_factor));
            //第五行 
            //第一列 
            text = "注意";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 3 * x_factor), y_top + Convert.ToInt32(46 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(43 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(53 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "      凭此证进楼到指定楼层,不得到其他楼层\n            出门时请将此条交传达室。";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(0 * x_factor), y_top + Convert.ToInt32(45 * y_factor));
            //end...........................................end进楼证存根 ......................................................................
            /*
            

            //进楼证 
            //画框
            from.X = x_left; from.Y = y_top;
            to.X = x_right; to.Y = y_top;
            e.Graphics.DrawLine(pen, from, to);
            from.X = x_left; from.Y = y_top;
            to.X = x_left; to.Y = y_bottom;
            e.Graphics.DrawLine(pen, from, to);
            from.X = x_left; from.Y = y_bottom;
            to.X = x_right; to.Y = y_bottom;
            e.Graphics.DrawLine(pen, from, to);
            from.X = x_right; from.Y = y_bottom;
            to.X = x_right; to.Y = y_top;
            e.Graphics.DrawLine(pen, from, to);
            //end 画框

            //第一行

            // 第一列
            from.X = x_left; from.Y = y_top + 10;
            to.X = x_right; to.Y = y_top + 10;
            e.Graphics.DrawLine(pen, from, to);
            text = "来 客\n姓 名";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 3, y_top + 2);
            from.X = x_left + 13; from.Y = y_top;
            to.X = x_left + 13; to.Y = y_top + 10;
            e.Graphics.DrawLine(pen, from, to);

            //第二列
            from.X = x_left + 13 + 25; from.Y = y_top;
            to.X = x_left + 13 + 25; to.Y = y_top + 10;
            e.Graphics.DrawLine(pen, from, to);

            //第三列
            from.X = x_left + 13 + 25 + 5; from.Y = y_top;
            to.X = x_left + 13 + 25 + 5; to.Y = y_top + 10;
            e.Graphics.DrawLine(pen, from, to);
            text = "性\n别";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 13 + 25 + 1, y_top + 2);

            //第四列
            from.X = x_left + 13 + 25 + 5 + 18; from.Y = y_top;
            to.X = x_left + 13 + 25 + 5 + 18; to.Y = y_top + 10;
            e.Graphics.DrawLine(pen, from, to);

            //第五列
            from.X = x_left + 13 + 25 + 5 + 18 + 8; from.Y = y_top;
            to.X = x_left + 13 + 25 + 5 + 18 + 8; to.Y = y_top + 10;
            e.Graphics.DrawLine(pen, from, to);
            text = " 人\n 数";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 13 + 25 + 5 + 18 + 1, y_top + 2);

            //第二行 
            //第一列 
            from.X = x_left; from.Y = y_top + 20;
            to.X = x_right; to.Y = y_top + 20;
            e.Graphics.DrawLine(pen, from, to);
            text = "单位或\n 住址";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 2, y_top + 2 + 10);
            from.X = x_left + 13; from.Y = y_top + 10;
            to.X = x_left + 13; to.Y = y_top + 10 + 10;
            e.Graphics.DrawLine(pen, from, to);
            //第二列
            from.X = x_left + 13 + 25 + 5 + 18; from.Y = y_top + 10;
            to.X = x_left + 13 + 25 + 5 + 18; to.Y = y_top + 10 + 10;
            e.Graphics.DrawLine(pen, from, to);
            //第三列
            from.X = x_left + 13 + 25 + 5 + 18 + 8; from.Y = y_top + 10;
            to.X = x_left + 13 + 25 + 5 + 18 + 8; to.Y = y_top + 10 + 10;
            e.Graphics.DrawLine(pen, from, to);
            text = "访问\n楼层";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 13 + 25 + 5 + 18 + 1, y_top + 2 + 10);
            //第三行
            //第一列
            from.X = x_left; from.Y = y_top + 32;
            to.X = x_right; to.Y = y_top + 32;
            e.Graphics.DrawLine(pen, from, to);
            text = "会见何\n单位何\n 人";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 2, y_top + 10 + 12);
            from.X = x_left + 13; from.Y = y_top + 10 + 10;
            to.X = x_left + 13; to.Y = y_top + 10 + 10 + 12;
            e.Graphics.DrawLine(pen, from, to);
            //第二列
            from.X = x_left + 13 + 25 + 5 + 18; from.Y = y_top + 10 + 10;
            to.X = x_left + 13 + 25 + 5 + 18; to.Y = y_top + 10 + 10 + 12;
            e.Graphics.DrawLine(pen, from, to);
            //第三列 
            from.X = x_left + 13 + 25 + 5 + 18 + 8; from.Y = y_top + 10 + 10;
            to.X = x_left + 13 + 25 + 5 + 18 + 8; to.Y = y_top + 10 + 10 + 12;
            e.Graphics.DrawLine(pen, from, to);
            text = "携\n带\n物";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 13 + 25 + 5 + 18 + 2, y_top + 10 + 2 + 10);
            //第四行
            //第一列 
            from.X = x_left; from.Y = y_top + 43;
            to.X = x_right; to.Y = y_top + 43;
            e.Graphics.DrawLine(pen, from, to);
            text = "进楼\n时间";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 3, y_top + 3 + 10 + 10 + 12);
            from.X = x_left + 13; from.Y = y_top + 10 + 10 + 12;
            to.X = x_left + 13; to.Y = y_top + 10 + 10 + 10 + 12 + 1;
            e.Graphics.DrawLine(pen, from, to);
            //第二列
            from.X = x_left + 13 + 25 + 5 + 18; from.Y = y_top + 10 + 10 + 12;
            to.X = x_left + 13 + 25 + 5 + 18; to.Y = y_top + 10 + 10 + 12 + 10 + 1;
            e.Graphics.DrawLine(pen, from, to);
            //第三列 
            from.X = x_left + 13 + 25 + 5 + 18 + 8; from.Y = y_top + 10 + 10 + 12;
            to.X = x_left + 13 + 25 + 5 + 18 + 8; to.Y = y_top + 10 + 10 + 12 + 1 + 10;
            e.Graphics.DrawLine(pen, from, to);
            text = "事\n由";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 13 + 25 + 5 + 18 + 2, y_top + 10 + 1 + 10 + 12+1);
            //第五行 
            //第一列 
            text = "注意";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 3, y_top + 3 + 10 + 10 + 14 + 10);
            from.X = x_left + 13; from.Y = y_top + 43;
            to.X = x_left + 13; to.Y = y_top + 43 + 10;
            e.Graphics.DrawLine(pen, from, to);

            text = "      凭此证进楼到指定楼层,不得到其他楼层\n            出门时请将此条交传达室。";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, x_left + 2+13, y_top + 1 + 10 + 10 + 14 + 10);
          
          
            //end进楼证
           */
        }

        private void textBox_VistorBuilding_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
