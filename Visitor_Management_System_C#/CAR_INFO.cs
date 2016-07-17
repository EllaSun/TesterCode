using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace VMS
{
    public partial class CAR_INFO : Form
    {
        #region 获取主窗体
        /// <summary>
        /// 获取主窗体
        /// </summary>
        private static CAR_INFO instance = null;
        internal static CAR_INFO GetCAR_INFO()
        {
            if (CAR_INFO.instance == null)
            {
                CAR_INFO.instance = new CAR_INFO();
            }
            return CAR_INFO.instance;
        }
        #endregion 获取主窗体


        public CAR_INFO()
        {
            
            InitializeComponent();
                       
        }


        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void dataGridView_car_detail_data(DataTable tb)
        {
           
        }

        private void CAR_INFO_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void car_detail_show(ArrayList car_detail)
        {
            label7.Text = car_detail[0].ToString().Trim();
            label8.Text = car_detail[1].ToString().Trim();
            label9.Text = car_detail[2].ToString().Trim();
            label10.Text = car_detail[3].ToString().Trim();
            if(label10 .Text.ToString () =="")
            {
                label10.Text = "空          ";
            }

            label11.Text = car_detail[4].ToString().Trim();
            textBox1.Text = car_detail[5].ToString().Trim();
            if (textBox1.Text.ToString() == "")
            {
                textBox1.Text = "空          ";
            }
        }
    }
}
