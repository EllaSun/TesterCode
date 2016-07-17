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
using System.IO;


namespace VMS
{
    public partial class GroupLogin : Form
    {
        static string startpath = System.Environment.CurrentDirectory;
        public string error_path = startpath.Replace("bin\\Release", "") + "media\\error.wav";
        public string success_path = startpath.Replace("bin\\Release", "") + "media\\success.wav";
        
        DateTime datetime = DateTime.Now;
                
        Function checkfunction = new Function();
        DataTable groupmemberinfo = new DataTable(); //("序号", "姓名", "证件号");
        ArrayList Visitorinfo = new ArrayList();
       
        
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        public  static GroupLogin instance = null;
        internal static GroupLogin GetGroupLogin()
        {
            if (GroupLogin.instance == null)
            {
                GroupLogin.instance = new GroupLogin();
            }
            return GroupLogin.instance;
        }
        #endregion 获取主窗体
        public GroupLogin()
        {
           // groupmemberinfo.Columns.Add("序号");
            groupmemberinfo.Columns.Add("姓名");
            groupmemberinfo.Columns.Add("证件号");
            groupmemberinfo.Columns.Add("携带物");
            groupmemberinfo.Columns.Add("临时卡号");
            
            InitializeComponent();
        }

        private void button_Print_Click(object sender, EventArgs e)
        {
            
            //打印模块
            this.printPreviewDialog1.ShowDialog();
            //
            //checkfunction.print2();
            this.Close();
        }

        //数据库交互
        //界面记录更新
        public  void DBconn(ArrayList a)
        {
            string id = a[0].ToString();
            //string id = Visitorinfo[0].ToString().Trim();
            
            string Luggage = a[3].ToString();
            string linshika = a[4].ToString();
            //更新数据库
            int success_check = httprequest(id,Luggage,linshika);
            //更新界面记录表     
            if (success_check == 0)
            {
               // MessageBox.Show("数据库请求失败，请重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //写入临时文件，定时传输
                DataTable dt = new DataTable();
                
                dt.Columns.Add("id");
                dt.Columns.Add("luggage");
                dt.Columns.Add("linshika");
                DataRow dr1 = dt.NewRow();
                dt.Rows.Add(dr1);
                dt.Rows[0][0] = id;
                dt.Rows[0][1] = Luggage;
                dt.Rows[0][2] = linshika;
                string unsuccessful_request = System.Environment.CurrentDirectory.Replace("bin\\Release", "") + "temp\\grouplogout.txt"; 
                if (!File.Exists(unsuccessful_request))
                {
                    Function.DataTableToTXT(dt, unsuccessful_request);
                }
                else if (File.Exists(unsuccessful_request))
                {
                    DataTable dttemp = Function.TxtToDataTable(unsuccessful_request, 0);
                    DataRow dr = dttemp.NewRow();
                    dr[0]=dt.Rows[0][0].ToString().Trim();
                    dr[1]=dt.Rows[0][1].ToString().Trim();
                    dttemp.Rows.Add(dr);
                    File.Delete(unsuccessful_request);
                    Function.DataTableToTXT(dttemp, unsuccessful_request);
                }
            }
            else if (success_check == 1)
            {

                //MessageBox.Show("访客登录成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataRow dr = groupmemberinfo.NewRow();
                dr[0] = a[2].ToString().Trim();
                dr[1] = a[1].ToString().Trim();
                dr[2] = Luggage.ToString();
                dr[3] = linshika;
                //dr[3] = Luggage.ToString();
                groupmemberinfo.Rows.Add(dr);
                dataGridView_GroupMember.AutoSize = true;
                dataGridView_GroupMember.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridView_GroupMember.DataSource = groupmemberinfo;
               

           

                GroupMember groupmember = GroupMember.GetGroupMember();
                groupmember.Close();
            }
            
        }

        private void button_Pass_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_reject_Click(object sender, EventArgs e)
        {

        }

        public void GroupInfo(ArrayList a)
        {
            try
            {
                Visitorinfo = a;
                //textBox_GroupID.Text = a[1].ToString().Trim();
                //DataRow dr =  GroupMember.NewRow();            
                textBox_GroupName.Text = a[2].ToString().Trim();
                textBox_GroupDepart.Text = a[5].ToString().Trim();
                textBox_GroupVisitBuilding.Text = a[6].ToString().Trim();
                textBox_HostName.Text = a[7].ToString().Trim();
                textBox_HostDepart.Text = a[8].ToString().Trim();
                textBox_resaon.Text = a[17].ToString().Trim();

                button_Print.Visible = true;
                button1.Visible = false;


            
            }
            catch
            {
                MessageBox.Show("出错了，请重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



            //dr[0] = a[0].ToString().Trim();
            //dr[1] = a[3].ToString().Trim();
            //dr[2] =a[4].ToString().Trim();
            //GroupMember.Rows.Add(dr);
            //dataGridView_GroupMember.DataSource = GroupMember;

        }
        public void CallMemInfo()
        {

            GroupMember groupmember = GroupMember.GetGroupMember();
            if (Visitorinfo[6].ToString () != "A")
            {

                groupmember.visitorinfo(Visitorinfo);
            }
            else
            {
                groupmember.visitorinfo2(Visitorinfo);
            }
            groupmember.Show();
        }

        private int httprequest(string id,string luggage,string linshika)
        {
            //DataTable a_in = new DataTable();
            //a_in.Columns.Add("id");            
            //a_in.Columns.Add("enter_time");
            //a_in.Columns.Add("luggage");
            //访客ID号   
            
            //实际进入时间
            //datetime = DateTime.Now;
            ////携带物
            ////a_in.Rows.Add(groupmemberinfo.Rows[groupmemberinfo.Rows.Count - 1][0].ToString(), datetime.ToString(), luggage);
            //a_in.Rows.Add(Visitorinfo[0].ToString(), datetime.ToString(), luggage);
            

            //写入数据库，并通过返回值检查            
            int success_check = checkfunction.UpdateDBInfo(id,luggage);
            return success_check;
        }

        public int checkISGROUP(string acheckid)
        {
            if (acheckid == Visitorinfo[1].ToString().Trim()) 
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public void mainpanelchange()
        {
            
        }

        private void GroupLogin_Load(object sender, EventArgs e)
        {
            //PaperSize ps = new PaperSize("Your Paper Name", 826, 275);
            //ps.RawKind = 150;
            PaperSize ps = new PaperSize("Your Paper Name", 693, 950);
            ps.RawKind = 13;
            this.printDocument1.DefaultPageSettings.PaperSize = ps;
            //打印开始前           
             this.printDocument1.BeginPrint += new PrintEventHandler(printDocument_BeginPrint);
            ///打印输出（过程）           
            this.printDocument1.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);
            //打印结束            
            //this.printDocument1.EndPrint += new PrintEventHandler(printDocument_EndPrint);
            this.printPreviewDialog1.Document = this.printDocument1;
            //this.printPreviewDialog1.ShowDialog();

        }
        void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {            //也可以把一些打印的参数放在此处设置   

            this.printDocument1.DefaultPageSettings.Landscape = true;
            this.printDocument1.PrinterSettings.PrinterName = "Xerox Phaser 3124";
           // this.printDocument1.PrinterSettings.PrinterName = @"\\11.132.169.198\jinxingp";
        }
        private void docToPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)//设置打印机开始打印的事件处理函数  
        {
            print_group(e);
        }
        private void print_group(PrintPageEventArgs e)
        {
            //float x_factor = 826.0f / 210.0f;
            float x_factor = (693.0f / (210.0f)) * 1.38f;
            //float y_factor = 275.0f / 75.0f;
            float y_factor = (950.0f / (75.0f)) * 0.75f;
            string text = "进  楼  证  存  根 ";
            string text1 = "进      楼      证";
            System.Drawing.Font header_font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font content_font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular  );
            System.Drawing.Font time_font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold );
            string name, sex, num, address, vistor_building, meet_person, luggage, person_id, enter_time, leave_time, guard,matter;
            name = Visitorinfo[2].ToString();
            sex = "";            
            address = Visitorinfo[5].ToString();
            vistor_building = Visitorinfo[6].ToString();
            meet_person = Visitorinfo[7].ToString();
            luggage = "";
            person_id = Visitorinfo[3].ToString();
            enter_time = datetime.ToString();
            leave_time = Visitorinfo[13].ToString();
            guard = "";
            //mainpanel mainpanel1=mainpanel .Getmainpanel ();
           // DataTable dt=dataGridView_GroupMember .DataSource as DataTable ;

            num = groupmemberinfo.Rows.Count.ToString();
            //matter = Visitorinfo[Visitorinfo.Count - 1].ToString();
            string group_member = "";
            string reason = "";
            reason = Visitorinfo[17].ToString().Trim();
            for (int i = 0; i <= groupmemberinfo.Rows.Count - 1; i++)
            {
                if (i == 0)
                {
                    group_member = groupmemberinfo.Rows[i]["姓名"].ToString().Trim();
                    luggage = groupmemberinfo.Rows[i]["携带物"].ToString().Trim();                   
                }
                else
                {
                    if (groupmemberinfo.Rows[i]["姓名"].ToString().Trim()!="")
                    group_member = group_member.ToString().Trim() + "," + groupmemberinfo.Rows[i]["姓名"].ToString().Trim();
                    if (groupmemberinfo.Rows[i]["携带物"].ToString().Trim()!="")
                    luggage = luggage + "," + groupmemberinfo.Rows[i]["携带物"].ToString().Trim();
                }
            }

            

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
            text = "来 访\n单 位";
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
            from.X = Convert.ToInt32(x_left + 45 * x_factor); from.Y = y_top;
            to.X = Convert.ToInt32(x_left + 45 * x_factor); to.Y = y_top + Convert.ToInt32(10 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "访问\n楼层";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 38 * x_factor), y_top + Convert.ToInt32(2 * y_factor));

            e.Graphics.DrawString(vistor_building, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(7 * x_factor), y_top + Convert.ToInt32(4 * y_factor));
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
            text = "携带物";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 1 * x_factor), y_top + Convert.ToInt32(12 * y_factor));

            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            e.Graphics.DrawString(luggage, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(5 * x_factor), y_top + Convert.ToInt32(14 * y_factor));
            /*
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
            */
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
            text = "值\n班\n员";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 62 * x_factor), y_top + Convert.ToInt32(21 * y_factor));
            e.Graphics.DrawString(guard, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(1 * x_factor), y_top + Convert.ToInt32(24 * y_factor));

            //第四行
            //第一列 
            from.X = x_left; from.Y = y_top + Convert.ToInt32(44 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(44 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "来访\n人员";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 2 * x_factor), y_top + Convert.ToInt32(35 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(43 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            e.Graphics.DrawString(group_member, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(5 * x_factor), y_top + Convert.ToInt32(37 * y_factor));
            /*
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
            */
            //第五行 
            //第一列 
            text = "进楼\n时间";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 3 * x_factor), y_top + Convert.ToInt32(45 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(43 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(53 * y_factor);
            e.Graphics.DrawLine(pen, from, to);

            e.Graphics.DrawString(enter_time, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(1 * x_factor), y_top + Convert.ToInt32(47 * y_factor));
            //第二列 
            //from.X = Convert.ToInt32(x_left + 43 * x_factor); from.Y = y_top + Convert.ToInt32(44 * y_factor);
            //to.X = Convert.ToInt32(x_left + 43 * x_factor); to.Y = y_top + Convert.ToInt32(53 * y_factor);
            //e.Graphics.DrawLine(pen, from, to);
            //第三列
           // text = "离开\n时间";
            //e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 46 * x_factor), y_top + Convert.ToInt32(45 * y_factor));
            //from.X = Convert.ToInt32(x_left + 56 * x_factor); from.Y = y_top + Convert.ToInt32(44 * y_factor);
            //to.X = Convert.ToInt32(x_left + 56 * x_factor); to.Y = y_top + Convert.ToInt32(53 * y_factor);
            //e.Graphics.DrawLine(pen, from, to);
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
            text = "来 访\n单 位";
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
            text = "事\n由";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 38 * x_factor), y_top + Convert.ToInt32(2 * y_factor));
            e.Graphics.DrawString(reason, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(3 * x_factor), y_top + Convert.ToInt32(4 * y_factor));
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
            text = " 进楼\n 时间";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 1 * x_factor), y_top + Convert.ToInt32(12 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(10 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(20 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            e.Graphics.DrawString(enter_time, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(7 * x_factor), y_top + Convert.ToInt32(14 * y_factor));
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
            e.Graphics.DrawString(meet_person, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(3 * x_factor), y_top + Convert.ToInt32(23 * y_factor));
            //第二列
            from.X = Convert.ToInt32(x_left + 61 * x_factor); from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = Convert.ToInt32(x_left + 61 * x_factor); to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            //第三列 
            from.X = Convert.ToInt32(x_left + 68 * x_factor); from.Y = y_top + Convert.ToInt32(20 * y_factor);
            to.X = Convert.ToInt32(x_left + 68 * x_factor); to.Y = y_top + Convert.ToInt32(32 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = "值\n班\n员";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 62 * x_factor), y_top + Convert.ToInt32(21 * y_factor));
            e.Graphics.DrawString(guard, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(1 * x_factor), y_top + Convert.ToInt32(24 * y_factor));
            //第四行
            //第一列 
            from.X = x_left; from.Y = y_top + Convert.ToInt32(43 * y_factor);
            to.X = x_right; to.Y = y_top + Convert.ToInt32(43 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            text = " 来访\n 人员";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 1 * x_factor), y_top + Convert.ToInt32(35 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(32 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(43 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            e.Graphics.DrawString(group_member, time_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(4 * x_factor), y_top + Convert.ToInt32(37 * y_factor));
            /*
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
           */
            //第五行 
            //第一列 
            text = "携带物";
            e.Graphics.DrawString(text, content_font, System.Drawing.Brushes.Black, Convert.ToInt32(x_left + 2 * x_factor), y_top + Convert.ToInt32(46 * y_factor));
            from.X = Convert.ToInt32(x_left + 13 * x_factor); from.Y = y_top + Convert.ToInt32(43 * y_factor);
            to.X = Convert.ToInt32(x_left + 13 * x_factor); to.Y = y_top + Convert.ToInt32(53 * y_factor);
            e.Graphics.DrawLine(pen, from, to);
            //text = "      凭此证进楼到指定楼层,不得到其他楼层\n            出门时请将此条交传达室。";
            e.Graphics.DrawString(luggage, content_font, System.Drawing.Brushes.Black, to.X + Convert.ToInt32(3 * x_factor), y_top + Convert.ToInt32(45 * y_factor));
            //end...........................................end进楼证存根 ......................................................................


        }
    }
}
