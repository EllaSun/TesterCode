using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YinanSoft.IDCardReader;
using System.IO;
using System.Collections;
using System.Net;




namespace VMS
{
    public partial class mainpanel : Form
    {
 
        static string startpath = System.Environment.CurrentDirectory;
        public string CarFile = startpath.Replace("bin\\Release", "") + "temp\\CarFile.txt";

        public string flagcar = "";

        public DataTable CarInfo = new DataTable();
        
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        public  static mainpanel instance = null;
        internal static mainpanel Getmainpanel()
        {
            if (mainpanel.instance == null)
            {
                mainpanel.instance = new mainpanel();
            }
            return mainpanel.instance;
        }
        #endregion 获取主窗体


        public mainpanel()
        {
            InitializeComponent();
       
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_handlogin_Click(object sender, EventArgs e)
        {
            try
            {
                Hand_Login hand_login = Hand_Login.GetHand_Login();
                hand_login.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show("系统错误，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about1 = About.GetAbout();
            about1.Show();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_handlogout_Click(object sender, EventArgs e)
        {
            try
            {
                AutoTest autotest = AutoTest.GetAutoTest();
                autotest.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show("系统错误，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_autotest_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            try
            {
                //手动登记
                AutoTest autotest = AutoTest.GetAutoTest();
                autotest.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show("系统错误，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mainpanel_Load(object sender, EventArgs e)

        {

            panel2.Visible = false;
            try
            {
               //读取当天车辆信息
                CarInfo = ReadCarInfo();
            }
            catch (Exception ex)
            {

                MessageBox.Show("登录错误，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //从数据库读取车辆信息
        private DataTable ReadCarInfo()
        {
            //读取当天车辆信息
            DataTable dtcar = new DataTable();
            dtcar.Columns.Add("CarID");

            try
            {
                string[] carId = { "1111", "2222", "3333", "4444" };
                for(int i = 0; i < 4; i++)
                {
                    dtcar.Rows.Add(carId[i]);
                }
                //连接数据库读取车辆信息 
                /*Function get_car = new Function();
                int flag = 0;
                DataTable temp_tb = get_car.GetCardDBInfo(out flag);*/
                /* if (flag==1)
                 {
                     for (int i = 0; i <temp_tb.Rows.Count; i++)
                     {
                         dtcar.Rows.Add(temp_tb.Rows[i][2].ToString());
                     }
                 }
                 else if (flag == 0)
                 {
                     MessageBox.Show("本日无车辆记录，请稍后再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
                 else
                 {
                     MessageBox.Show("数据库连接失败，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }*/

                return dtcar;
            }
            catch
            {
                return dtcar;
            }

           
        }

        private void 访客登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 手动登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 手动登离ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       

        private void 注销用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            GuardLogin guardlogin1 = GuardLogin.GetGuardLogin();
            guardlogin1.ShowDialog();
            //this.Close();
        }

        private void 注销用户ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        int iRetCOM = IDCardReader.InitComm(1001); //设备串口连接
        private bool flag = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!flag)
            {
                flag = true;
                string person_id = null;
                bool is_success = AutoReadCard(out person_id);
                
                if (is_success)
                {

                    person_id = Function.process_person_id_str(person_id);

                    Function.IDcheck(person_id);
                    flag = false;

                }
                else
                {
                  
                    flag = false;
                }
              

            }

        }
        public bool FillData(out string person_id)
        {
            person_id = null;
            string tmpStr;
            if (!System.IO.File.Exists(Application.StartupPath + "\\WZ.TXT")) return false;
                try
                {



                    using (System.IO.FileStream stream = System.IO.File.Open("WZ.TXT", System.IO.FileMode.Open))
                    {
                        byte[] bytes = new byte[512];
                        //姓名
                        stream.Read(bytes, 0, 30);
                        for (int i = 0; i < 30; i++)
                        {
                            if (bytes[i] == 0x20 && bytes[i + 1] == 0)
                                bytes[i] = 0;
                            i++;
                        }
                        string name = IDCardReader.ByteArrayToStr(bytes);

                        //性别      
                        for (int i = 0; i < bytes.Length; i++) bytes[i] = 0;
                        stream.Read(bytes, 0, 2);
                        string sex = null;
                        if (Convert.ToInt32(IDCardReader.ByteArrayToStr(bytes)) == 1)
                            sex = "男";
                        else if (Convert.ToInt32(IDCardReader.ByteArrayToStr(bytes)) == 2)
                            sex = "女";
                        else sex = "未知";

                        //民族
                        stream.Read(bytes, 0, 4);
                        string people = IDCardReader.Nation[Convert.ToInt32(IDCardReader.ByteArrayToStr(bytes))]; ;

                        //出生                
                        stream.Read(bytes, 0, 16);
                        tmpStr = IDCardReader.ByteArrayToStr(bytes);
                        string birthday = tmpStr.Substring(0, 4) + "年" +
                            tmpStr.Substring(4, 2) + "月" +
                            tmpStr.Substring(6, 2) + "日";

                        //住址
                        stream.Read(bytes, 0, 70);
                        string address = IDCardReader.ByteArrayToStr(bytes);

                        //证件号
                        for (int i = 0; i < bytes.Length; i++) bytes[i] = 0;
                        stream.Read(bytes, 0, 36);
                        string number = IDCardReader.ByteArrayToStr(bytes);
                        this.toolStripStatusLabel2.Text = "读取成功！";
                        person_id = number;

                        for (int i = 0; i < bytes.Length; i++) bytes[i] = 0;
                        stream.Read(bytes, 0, 30);
                        string signdate = IDCardReader.ByteArrayToStr(bytes);

                        //
                        for (int i = 0; i < bytes.Length; i++) bytes[i] = 0;
                        stream.Read(bytes, 0, 16);
                        tmpStr = IDCardReader.ByteArrayToStr(bytes);
                        string validtermOfStart = tmpStr.Substring(0, 4) + "年" +
                            tmpStr.Substring(4, 2) + "月" +
                            tmpStr.Substring(6, 2) + "日";

                        //
                        for (int i = 0; i < bytes.Length; i++) bytes[i] = 0;
                        stream.Read(bytes, 0, 16);
                        tmpStr = IDCardReader.ByteArrayToStr(bytes);
                        string validtermOfEnd = tmpStr.Substring(0, 4) + "年" +
                            tmpStr.Substring(4, 2) + "月" +
                            tmpStr.Substring(6, 2) + "日";
                        return true;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
        
        }
        private bool AutoReadCard(out string p_id)
        {
            p_id = null;
            if (iRetCOM == 1)
            {
                int authenticate = IDCardReader.Authenticate();
                if (authenticate == 1)
                {
                    int readContent = IDCardReader.Read_Content(1);
                    if (readContent == 1)
                    {

                        this.toolStripStatusLabel2.Text = "读取中......";
                        bool flag = false;
                        flag = FillData(out p_id);
                        if (flag)
                            return true;
                        else
                            return false;


                    }
                    else
                    {

                        this.toolStripStatusLabel2.Text = "读取失败！";
                        return false;
                    }
                }
                else
                {
                    this.toolStripStatusLabel2.Text = "请放身份证！";
                    return false;

                }
            }
            else
            {
                this.toolStripStatusLabel2.Text = "身份证扫描器初始化失败！";
                return false;
            }
        }

       

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_Car_Click(object sender, EventArgs e)
        {
            //初始化
            panel2.Visible = true;
            splitContainer4.Visible = true;
            Car_Init();
           
            int i=1;
            while (i == 1)
            {
                refresh_car_info();
                Function function=new Function();
                function.Delay(60);

            }
                

        }

        //车辆管理初始化
        public  void Car_Init()
        {
            panel3.Controls.Clear();
            //读取当天车辆信息
            DataTable dtcar = CarInfo.Clone();
            dtcar = ReadCarInfo();

            //显示
            //增加动态控件
            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";

            panel3.Controls.Add(flowLayoutPanel1);
            flagcar = "grid";
            //
            int buttonlen = 0;
            for (int i = 0; i < dtcar.Rows.Count; i++)
            {
                if (buttonlen <( dtcar.Rows[i][0].ToString().Trim().Length))
                {
                    buttonlen = dtcar.Rows[i][0].ToString().Trim().Length;
                }

            }
            for (int i = 0; i < dtcar.Rows.Count; i++)
            {
                Button button = new Button();
                button.BackColor = System.Drawing.Color.Lime;
                button.Text = dtcar.Rows[i][0].ToString().Trim();
                button.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
             
                button.Name = "button" + i.ToString();
                button.Size = new System.Drawing.Size(24*buttonlen , 52);

                button.UseVisualStyleBackColor = false;
                flowLayoutPanel1.Controls.Add(button);
             
                button.MouseHover += button_MouseHover;
                button.Click += button_Click;                
            }



        }
        
        void button_MouseHover(object sender, EventArgs e)
        {
            
        }

        void button_Click(object sender, EventArgs e)
        {
            //获取当前按钮的文本
            string name = (sender as Button).Text.ToString().Trim();

            try
            {
                //读取当天车辆信息
                ArrayList ALcar = new ArrayList();


                //连接数据库读取车辆信息 
                Function get_car = new Function();
                int flag = 0;
                DataTable temp_tb = get_car.GetCardDBInfo(out flag);

                if (flag == 1)
                {
                    for (int i = 0; i < temp_tb.Rows.Count; i++)
                    {
                        if (temp_tb.Rows[i][2].ToString() == name)
                        {
                            ALcar.Add(temp_tb.Rows[i][2].ToString());
                            ALcar.Add(temp_tb.Rows[i][1].ToString());
                            ALcar.Add(temp_tb.Rows[i][8].ToString());
                            ALcar.Add(temp_tb.Rows[i][6].ToString());
                            ALcar.Add(temp_tb.Rows[i][5].ToString());
                            ALcar.Add(temp_tb.Rows[i][7].ToString());
                        }
                        //dtcar.Rows.Add(temp_tb.Rows[i][2].ToString(),temp_tb.Rows[i][1].ToString(),temp_tb.Rows[i][8].ToString(),temp_tb.Rows[i][6].ToString(),temp_tb.Rows[i][4].ToString(),temp_tb.Rows[i][7].ToString());
                    }
                }
                else if (flag == 0)
                {
                    MessageBox.Show("本日无车辆记录，请稍后再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("数据库连接失败，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                if (ALcar.Count == 6)
                {
                    CAR_INFO carinfo = CAR_INFO.GetCAR_INFO();
                    carinfo.car_detail_show(ALcar);
                    carinfo.Show();
                }
                else
                {
                    MessageBox.Show("查询该车辆信息出错，请稍后再试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        




        private void 系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            refresh_car_info();
        }
        private void refresh_car_info()
        {

            try
            {
                if (flagcar == "grid")
                {
                    Car_Init();
                }
                else if (flagcar == "list")
                {
                    panel3.Controls.Clear();
                    flagcar = "list";
                    //读取当天车辆信息
                    DataTable dtcar = CarInfo.Clone();
                    dtcar = ReadCarInfo();
                    //如果读取失败，则使用上次车辆管理操作时使用的dtcar
                    if (dtcar.Rows.Count == 0)
                    {
                        //dtcar.Dispose();
                        //CarInfo.TableName = "dtcar";
                    }
                    //增加动态控件
                    DataGridView dataGridView1 = new DataGridView();
                    dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
                    //dataGridView1.Location = new System.Drawing.Point(0, 0);
                    dataGridView1.Name = "dataGridView1";
                    dataGridView1.RowTemplate.Height = 30;
                    dataGridView1.Size = new System.Drawing.Size(801, 573);
                    //dataGridView1.TabIndex = 0;

                    panel3.Controls.Add(dataGridView1);

                    //
                    dataGridView1.DataSource = dtcar;
                }
            }
            catch
            {
                MessageBox.Show("刷新失败，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_grid_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_list_Click(object sender, EventArgs e)
        {

            panel3.Controls.Clear();
            flagcar = "list";
            //读取当天车辆信息
            DataTable dtcar = CarInfo.Clone();
            dtcar = ReadCarInfo();
            //如果读取失败，则使用上次车辆管理操作时使用的dtcar
            if (dtcar.Rows.Count == 0)
            {
                //dtcar.Dispose();
                //CarInfo.TableName = "dtcar";
            }
            //增加动态控件
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            //dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.Size = new System.Drawing.Size(801, 573);
            //dataGridView1.TabIndex = 0;

            panel3.Controls.Add(dataGridView1);

            //
            dataGridView1.DataSource = dtcar;

        }

        private void button1_Grid_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            Car_Init();
            #region
            //读取当天车辆信息
            DataTable dtcar = CarInfo.Clone();
            dtcar = ReadCarInfo();
            //如果读取失败，则使用上次车辆管理操作时使用的dtcar
            if (dtcar.Rows.Count == 0)
            {
                dtcar.Dispose();
                CarInfo.TableName = "dtcar";
            }
            //增加动态控件
            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";          
            
            panel3.Controls.Add(flowLayoutPanel1);
            //
            for (int i = 0; i < dtcar.Rows.Count; i++)
            {
                Button button = new Button();
                button.BackColor = System.Drawing.Color.Lime;
                button.Text = dtcar.Rows[i][0].ToString().Trim();
                button.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //button.Location = new System.Drawing.Point(3, 3);
                button.Name = "button"+i.ToString ();
                button.Size = new System.Drawing.Size(123, 52);
                button.Text = dtcar .Rows [i][0].ToString ().Trim ();
                button.UseVisualStyleBackColor = false;
                flowLayoutPanel1.Controls.Add(button);
            }
            #endregion

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        //定时调用批量写入
        private void timer2_Tick(object sender, EventArgs e)
        {
            Function function =new Function ();
             function.BATToWrite ();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            refresh_car_info();
        }


    }
}
