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
    public partial class SearchVistorInfo : Form
    {
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static SearchVistorInfo instance = null;
        internal static SearchVistorInfo GetSearchVistorInfo()
        {
            if (SearchVistorInfo.instance == null)
            {
                SearchVistorInfo.instance = new SearchVistorInfo();
            }
            return SearchVistorInfo.instance;
        }
        #endregion 获取主窗体
        public SearchVistorInfo()
        {
            InitializeComponent();
        }

        private void button1_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
