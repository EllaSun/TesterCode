using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections ;

namespace VMS
{
    public partial class GroupMember : Form
    {
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        public static GroupMember instance = null;
        internal static GroupMember GetGroupMember()
        {
            if (GroupMember.instance == null)
            {
                GroupMember.instance = new GroupMember();
            }
            return GroupMember.instance;
        }
        #endregion 获取主窗体

        ArrayList b = new ArrayList();
        GroupLogin grouplogin = GroupLogin.GetGroupLogin();
        public GroupMember()
        {
            InitializeComponent();
        }

        private void button_Pass_Click(object sender, EventArgs e)
        {


            b.Add(comboBox_VistorLuggage.Text.ToString());
            b.Add("");
            grouplogin.DBconn(b);


        }

        private void button_Reject_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void visitorinfo(ArrayList a)
        {
            
           // textBox_ID.Text = a[0].ToString().Trim();
            textBox_VistorID.Text = a[3].ToString().Trim();
            textBox_VistorName.Text = a[4].ToString().Trim();

            b.Add(a[0].ToString());
            b.Add(textBox_VistorID.Text.ToString());
            b.Add(textBox_VistorName.Text.ToString());
        }

        public void visitorinfo2(ArrayList a)
        {
            textBox_VistorID.Text = a[3].ToString().Trim();
            textBox_VistorName.Text = a[4].ToString().Trim();
 
            b.Add(a[0].ToString());
            b.Add(textBox_VistorID.Text.ToString());
            b.Add(textBox_VistorName.Text.ToString());
        }
    }
}
