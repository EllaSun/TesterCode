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
    public partial class AutoTest : Form
    {
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static AutoTest instance = null;
        internal static AutoTest GetAutoTest()
        {
            if (AutoTest.instance == null)
            {
                AutoTest.instance = new AutoTest();
            }
            return AutoTest.instance;
        }
        #endregion 获取主窗体

        public AutoTest()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Function.IDcheck(textBox_VistorID.Text.ToString());
            this.Close();
        }

       

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        //private void textBox_VistorID_TextChanged(object sender, EventArgs e)
        //{
        //    string txt = textBox_VistorID.Text;

        //    int i = txt.Length;

        //    if (i < 1) return;

        //    for (int m = 0; m < i; m++)
        //    {

        //        string str = txt.Substring(m, 1);

        //        if (!char.IsNumber(Convert.ToChar(str)))
        //        {
        //            textBox_VistorID.Text = textBox_VistorID.Text.Replace(str, ""); //将非数字文本过滤掉

        //            textBox_VistorID.SelectionStart = textBox_VistorID.Text.Length;//将光标定位到最后一位

        //        }
        //    }
        //}

        private void AutoTest_Load(object sender, EventArgs e)
        {

        }

        private void Close(object sender, EventArgs e)
        {
            AutoTest.instance = null;
        }
    }
}
