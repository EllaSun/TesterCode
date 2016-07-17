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
    public partial class VistorReject : Form
    {
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static VistorReject instance = null;
        internal static VistorReject GetVistorReject()
        {
            if (VistorReject.instance == null)
            {
                VistorReject.instance = new VistorReject();
            }
            return VistorReject.instance;
        }
        #endregion 获取主窗体
        public VistorReject()
        {
            InitializeComponent();
        }

        private void VistorReject_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
