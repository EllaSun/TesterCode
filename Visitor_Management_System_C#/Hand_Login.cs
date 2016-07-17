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
    public partial class Hand_Login : Form
    {
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static Hand_Login instance = null;
        internal static Hand_Login GetHand_Login()
        {
            if (Hand_Login.instance == null)
            {
                Hand_Login.instance = new Hand_Login();
            }
            return Hand_Login.instance;
        }
        #endregion 获取主窗体
        public Hand_Login()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox_VistorID.ToString().Trim()) == "")
                {
                    MessageBox.Show("证件号不能为空值，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //连接服务器搜索
                    Function checkfunction = new Function();
                    ArrayList a = checkfunction.GetDBInfo(textBox_VistorID.ToString().Trim());
                    if (a.Count == 0)
                    {
                        MessageBox.Show("证件号不匹配，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //核对信息，正确
                        VisitorLogin vistorinfocheck = VisitorLogin.GetVistorLogin ();
                        vistorinfocheck.VistorInfo(a);
                        vistorinfocheck.Show();
                    }

                }
                //正确连接
            }
            catch (Exception ex)
            {

                MessageBox.Show("数据库连接错误，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
    
        }

        private void textBox_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Hand_Login_Load(object sender, EventArgs e)
        {
            
        }
    }
}
