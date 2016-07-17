using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VMS
{
    public partial class About : Form
    {
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static About instance = null;
        internal static About GetAbout()
        {
            if (About.instance == null)
            {
                About.instance = new About();
            }
            return About.instance;
        }
        #endregion 获取主窗体

        public About()
        {
            
            InitializeComponent();
            
            //richTextBox1.Text = "版权声明：中国建设银行北京数据中心门禁管理系统，系中国建设银行独立开发软件，中国建设银行依法独立享有该软件之所有权利。该软件使用者（含个人、法人或其它组织）：\r\n1、非经中国建设银行授权许可，不得将之用于盈利或非盈利性的任何用途。\r\n2、为适应实际的计算机应用环境，对其功能、性能、界面，可以进行必要的修改，但不得去除Powered By ：liangjing Version 2008的版本标示；\r\n3、未经中国建设银行书面授权许可，不得向任何第三方提供修改后的软件。使用该软件必须保留中国建设银行的版权声明，将该软件从原有自然语言文字转换成另一自然语言文字的，仍应注明出处，并不得向任何第三方提供修改后的软件。 \r\n4、不得有其他侵犯中国建设银行软件版权之行为。\r\n凡有上述侵权行为的个人、法人或其它组织，必须立即停止侵权并对其侵权造成的一切不良后果承担全部责任。对此前，尤其是此后侵犯中国建设银行版权的行为，中国建设银行将依据《著作权法》、《计算机软件保护条例》等相关法律、法规追究其经济责任和法律责任。";
        }

        private void About_Load(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
