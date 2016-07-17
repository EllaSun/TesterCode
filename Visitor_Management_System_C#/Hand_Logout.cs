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
    public partial class Hand_Logout : Form
    { 
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static Hand_Logout instance = null;
        internal static Hand_Logout GetHand_Logout()
        {
            if (Hand_Logout.instance == null)
            {
                Hand_Logout.instance = new Hand_Logout();
            }
            return Hand_Logout.instance;
        }
        #endregion 获取主窗体
        public Hand_Logout()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox_ID.ToString().Trim()) == "")
                {
                    MessageBox.Show("证件号不能为空值，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //连接服务器搜索
                    Function checkfunction = new Function();
                    ArrayList a = checkfunction.GetDBInfo(textBox_ID.ToString().Trim());
                    if (a.Count == 0)
                    {
                        MessageBox.Show("证件号不匹配，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //核对信息，正确
                        VisitorLogout vistorlogout = VisitorLogout.GetVistorLogout();
                        vistorlogout.VistorInfo(a);
                        vistorlogout.Show();
                    }

                }
                //正确连接
            }
            catch (Exception ex)
            {

                MessageBox.Show("数据库连接错误，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
