using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace VMS
{
    public partial class VisitorLogout : Form
    {
        DateTime datetime = DateTime.Now;
        Function checkfunction = new Function();
        ArrayList visitorinfo = new ArrayList();
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static VisitorLogout instance = null;
        internal static VisitorLogout GetVistorLogout()
        {
            if (VisitorLogout.instance == null)
            {
                VisitorLogout.instance = new VisitorLogout();
            }
            return VisitorLogout.instance;
        }
        #endregion 获取主窗体
        public VisitorLogout()
        {
            InitializeComponent();
        }

        private void VistorLogout_Load(object sender, EventArgs e)
        {
            Hand_Logout hand_logout = Hand_Logout.GetHand_Logout();
            hand_logout.Close();
            

        }

        public void VistorInfo(ArrayList a)
        {


            label12.Text = "访 客 登 离！\r\n请确认携带物后允许。";



            visitorinfo = a;

            textBox_VistorID.Text = a[3].ToString().Trim();
            textBox_VistorName.Text = a[4].ToString().Trim();
            textBox_VistorDepart.Text = a[5].ToString().Trim();
            textBox_VistorBuilding.Text = a[6].ToString().Trim();

            textBox_HostName.Text = a[7].ToString().Trim();
            textBox_HostDepart.Text = a[8].ToString().Trim();
            textBox_VistorLuggage.Text = a[9].ToString().Trim();
          
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox_HostDepart_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Pass_Click(object sender, EventArgs e)
        {
            mainpanel mainpanel1 = mainpanel.Getmainpanel();
            //http请求，更改访客状态为“已出园”
            int success_check = httprequest();
            if (success_check == 0)
            {
                //MessageBox.Show("数据库请求失败，请重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //数据库请求失败，将请求写入临时文件，准备下一次请求。
                DataTable dt = new DataTable();
                dt.Columns.Add("record_id");
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                dt.Rows[0][0] = visitorinfo[14].ToString();

                string unsuccessful_request = System.Environment.CurrentDirectory.Replace("bin\\Release", "") + "temp\\visitorlogout.txt"; 
                if (!File.Exists(unsuccessful_request))
                {
                    Function.DataTableToTXT(dt, unsuccessful_request);
                }
                else if (File.Exists(unsuccessful_request))
                {
                    DataTable dttemp = Function.TxtToDataTable(unsuccessful_request, 0);
                    //dttemp.Rows.Add(dt.Rows[0][0].ToString().Trim());
                    DataRow drr = dttemp.NewRow();
                    drr[0] = dt.Rows[0][0].ToString().Trim();
                    //dr[1] = dt.Rows[0][1].ToString().Trim();
                    dttemp.Rows.Add(drr);
                    File.Delete(unsuccessful_request);
                    Function.DataTableToTXT(dttemp, unsuccessful_request);
                }
            }
            else if (success_check == 1)
            {
                MessageBox.Show("访客登离成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
         
                this.Close();                
            }
        }

        private void button_Reject_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int httprequest()
        {
            DataTable a_out = new DataTable();
            a_out.Columns.Add("record_id");
            a_out.Columns.Add("leave_time");
           
            //当日ID号
            //实际离开时间
            datetime = DateTime.Now;
            a_out.Rows .Add(visitorinfo [0].ToString (),datetime .ToString ());   
           
            //写入数据库，并通过返回值检查
            //Function checkfunction = new Function();
            int success_check = checkfunction.UpdateDBInfo2(visitorinfo [14] .ToString ());
            return success_check;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_Status_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_Status_Click(object sender, EventArgs e)
        {

        }

        private void textBox_VistorLuggage_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox_HostName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
