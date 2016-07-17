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
    public partial class GroupLogout : Form
    {
          DateTime datetime = DateTime.Now;
          Function checkfunction = new Function();
        ArrayList Visitorinfo = new ArrayList();
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static GroupLogout instance = null;
        internal static GroupLogout GetGroupLogout()
        {
            if (GroupLogout.instance == null)
            {
                GroupLogout.instance = new GroupLogout();
            }
            return GroupLogout.instance;
        }
        #endregion 获取主窗体
        public GroupLogout()
        {
            InitializeComponent();
        }
        public void GroupInfo(ArrayList a)
        {

            label12.Text = "访 客 登 离！\r\n请确认携带物后允许。";

            


            Visitorinfo = a;
            //textBox_ID.Text = a[0].ToString().Trim();
            //textBox_GroupID.Text = a[1].ToString().Trim();                    
            //textBox_GroupName.Text = a[2].ToString().Trim();
            textBox_VistorID.Text = a[3].ToString().Trim();
            textBox_VistorName.Text = a[4].ToString().Trim();
            textBox_GroupDepart.Text = a[5].ToString().Trim();
            textBox_GroupVisitBuilding.Text = a[6].ToString().Trim();
            textBox_HostName.Text = a[7].ToString().Trim();
            textBox_HostDepart.Text = a[8].ToString().Trim();
            textBox_VistorLuggage.Text = a[9].ToString().Trim();
            //textBox_ExpectTimeIn.Text = a[10].ToString().Trim();
            //textBox_ExpectTimeOut.Text = a[11].ToString().Trim();
            //textBox_EnterTime.Text = a[12].ToString().Trim();

          

        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            try
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
                  
                    dt.Rows[0][0] = Visitorinfo[14].ToString();

                    string unsuccessful_request = System.Environment.CurrentDirectory.Replace("bin\\Release", "") + "temp\\grouplogin.txt"; 
                    if (!File.Exists(unsuccessful_request))
                    {
                        Function.DataTableToTXT(dt, unsuccessful_request);
                    }
                    else if (File.Exists(unsuccessful_request))
                    {
                        DataTable dttemp = Function.TxtToDataTable(unsuccessful_request, 0);
                        dttemp.Rows.Add(dt.Rows[0][0].ToString().Trim());
                        File.Delete(unsuccessful_request);
                        Function.DataTableToTXT(dttemp, unsuccessful_request);
                    }
                }
                this.Close();
                //else if (success_check == 1)
                //{
                //    MessageBox.Show("访客登离成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

            }
            catch
            {
                MessageBox.Show("出错了，请重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }
        
        
        

        private void button_reject_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       private int httprequest()
        {

            int success_check = checkfunction.UpdateDBInfo2(Visitorinfo[14].ToString());
            return success_check;
        }

       private void label10_Click(object sender, EventArgs e)
       {

       }

       private void label13_Click(object sender, EventArgs e)
       {

       }

       private void GroupLogout_Load(object sender, EventArgs e)
       {

       }

       private void label3_Click(object sender, EventArgs e)
       {

       }

       private void label8_Click(object sender, EventArgs e)
       {

       }

       private void label12_Click(object sender, EventArgs e)
       {

       }
    }
}
