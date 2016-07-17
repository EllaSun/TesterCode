using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace VMS
{
    public partial class GuardLogin : Form
    {
        public int acheck = 0;
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static GuardLogin instance = null;
        internal static GuardLogin GetGuardLogin()
        {
            if (GuardLogin.instance == null)
            {
                GuardLogin.instance = new GuardLogin();
            }
            return GuardLogin.instance;
        }
        #endregion 获取主窗体


        public GuardLogin()
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label3.Text = DateTime.Now.ToLongDateString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
           
            label4.Text ="正在与服务器连接中，请稍候……";//.Visible = true;
            label4.Show();

           
            //读取用户名，密码
            //get username,password
            string  username = textBox_UserName.Text.ToString ();
            string pw = textBox_PW.Text.ToString();
            try
            {
                if (username.ToString() != "" && pw != "")
                {
                    //哈希
                    //与数据库匹配,返回值
                    //hash
                    //match the database
                    Function checkfunction = new Function();
                    int a=checkfunction.CheckGuardINFO(username ,pw);
                   
                    //正确返回值
                   if(a==1)
                   {
                    acheck = 1;
                    label4.Text = "登陆成功";
                    this.Close();
                   }
                    //错误返回值
                   else if (a == 0)
                   {
                       label4.Visible = false;
                       MessageBox.Show("用户名或密码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
                   else
                   {
                       label4.Visible = false;
                       MessageBox.Show("网络连接失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
                }
                else
                {
                    label4.Visible = false;
                    MessageBox.Show("用户名和密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                //acheck = 4;
                label4.Visible = false;
                MessageBox.Show(ex.ToString (), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
           // this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void GuardLogin_Load(object sender, EventArgs e)
        {
           
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
