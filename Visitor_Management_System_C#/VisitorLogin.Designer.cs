namespace VMS
{
    partial class VisitorLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisitorLogin));
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button_Pass = new System.Windows.Forms.Button();
            this.button_Print = new System.Windows.Forms.Button();
            this.comboBox_VistorLuggage = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_resaon = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_VistorID = new System.Windows.Forms.TextBox();
            this.textBox_VistorName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_VistorDepart = new System.Windows.Forms.TextBox();
            this.textBox_HostDepart = new System.Windows.Forms.TextBox();
            this.textBox_VistorBuilding = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_HostName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::VMS.Properties.Resources.backgroud1;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.button_Pass);
            this.panel1.Controls.Add(this.button_Print);
            this.panel1.Controls.Add(this.comboBox_VistorLuggage);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 391);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::VMS.Properties.Resources.rightcheck2;
            this.pictureBox1.Location = new System.Drawing.Point(75, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(81, 85);
            this.pictureBox1.TabIndex = 78;
            this.pictureBox1.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(239, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(243, 56);
            this.label16.TabIndex = 77;
            this.label16.Text = "访客信息核对正确！\r\n请允许通过并发访客卡。";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label13.Location = new System.Drawing.Point(103, 170);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(236, 34);
            this.label13.TabIndex = 73;
            this.label13.Text = "（携带物选项，可以进行下拉菜单选择操作\r\n             也可以直接在文本框里输入。）";
            // 
            // button_Pass
            // 
            this.button_Pass.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Pass.Location = new System.Drawing.Point(315, 113);
            this.button_Pass.Name = "button_Pass";
            this.button_Pass.Size = new System.Drawing.Size(130, 45);
            this.button_Pass.TabIndex = 20;
            this.button_Pass.Text = "允许";
            this.button_Pass.UseVisualStyleBackColor = true;
            this.button_Pass.Click += new System.EventHandler(this.button_Pass_Click);
            // 
            // button_Print
            // 
            this.button_Print.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Print.Location = new System.Drawing.Point(315, 113);
            this.button_Print.Name = "button_Print";
            this.button_Print.Size = new System.Drawing.Size(130, 45);
            this.button_Print.TabIndex = 19;
            this.button_Print.Text = "允许并打印";
            this.button_Print.UseVisualStyleBackColor = true;
            this.button_Print.Click += new System.EventHandler(this.button_Print_Click);
            // 
            // comboBox_VistorLuggage
            // 
            this.comboBox_VistorLuggage.DisplayMember = "包";
            this.comboBox_VistorLuggage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_VistorLuggage.FormattingEnabled = true;
            this.comboBox_VistorLuggage.Items.AddRange(new object[] {
            "背包",
            "肩包",
            "拉杆箱",
            "纸箱"});
            this.comboBox_VistorLuggage.Location = new System.Drawing.Point(106, 113);
            this.comboBox_VistorLuggage.Name = "comboBox_VistorLuggage";
            this.comboBox_VistorLuggage.Size = new System.Drawing.Size(132, 25);
            this.comboBox_VistorLuggage.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(26, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "携带物：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_resaon);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_VistorID);
            this.groupBox1.Controls.Add(this.textBox_VistorName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_VistorDepart);
            this.groupBox1.Controls.Add(this.textBox_HostDepart);
            this.groupBox1.Controls.Add(this.textBox_VistorBuilding);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_HostName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(3, 207);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 172);
            this.groupBox1.TabIndex = 79;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "详细信息";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // textBox_resaon
            // 
            this.textBox_resaon.Location = new System.Drawing.Point(104, 136);
            this.textBox_resaon.Name = "textBox_resaon";
            this.textBox_resaon.ReadOnly = true;
            this.textBox_resaon.Size = new System.Drawing.Size(133, 21);
            this.textBox_resaon.TabIndex = 79;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(21, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 17);
            this.label8.TabIndex = 78;
            this.label8.Text = "事  由：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(21, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "访客姓名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "访客证件号：";
            // 
            // textBox_VistorID
            // 
            this.textBox_VistorID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorID.Location = new System.Drawing.Point(104, 33);
            this.textBox_VistorID.Name = "textBox_VistorID";
            this.textBox_VistorID.ReadOnly = true;
            this.textBox_VistorID.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorID.TabIndex = 1;
            // 
            // textBox_VistorName
            // 
            this.textBox_VistorName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorName.Location = new System.Drawing.Point(104, 68);
            this.textBox_VistorName.Name = "textBox_VistorName";
            this.textBox_VistorName.ReadOnly = true;
            this.textBox_VistorName.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(21, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "访客单位：";
            // 
            // textBox_VistorDepart
            // 
            this.textBox_VistorDepart.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorDepart.Location = new System.Drawing.Point(104, 104);
            this.textBox_VistorDepart.Name = "textBox_VistorDepart";
            this.textBox_VistorDepart.ReadOnly = true;
            this.textBox_VistorDepart.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorDepart.TabIndex = 5;
            // 
            // textBox_HostDepart
            // 
            this.textBox_HostDepart.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_HostDepart.Location = new System.Drawing.Point(365, 104);
            this.textBox_HostDepart.Name = "textBox_HostDepart";
            this.textBox_HostDepart.ReadOnly = true;
            this.textBox_HostDepart.Size = new System.Drawing.Size(133, 23);
            this.textBox_HostDepart.TabIndex = 11;
            // 
            // textBox_VistorBuilding
            // 
            this.textBox_VistorBuilding.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorBuilding.Location = new System.Drawing.Point(365, 30);
            this.textBox_VistorBuilding.Name = "textBox_VistorBuilding";
            this.textBox_VistorBuilding.ReadOnly = true;
            this.textBox_VistorBuilding.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorBuilding.TabIndex = 7;
            this.textBox_VistorBuilding.TextChanged += new System.EventHandler(this.textBox_VistorBuilding_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(280, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "接待人部门：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(279, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "接待人姓名：";
            // 
            // textBox_HostName
            // 
            this.textBox_HostName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_HostName.Location = new System.Drawing.Point(365, 65);
            this.textBox_HostName.Name = "textBox_HostName";
            this.textBox_HostName.ReadOnly = true;
            this.textBox_HostName.Size = new System.Drawing.Size(133, 23);
            this.textBox_HostName.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(280, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "访客楼层权限：";
            // 
            // VisitorLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 391);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VisitorLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "个人访客登陆";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VistorInfoCheck_FormClosing);
            this.Load += new System.EventHandler(this.VistorInfoCheck_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        void VistorInfoCheck_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            VisitorLogin.instance = null;
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_VistorID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_VistorName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_VistorDepart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_VistorBuilding;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_HostName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_HostDepart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Print;
        private System.Windows.Forms.ComboBox comboBox_VistorLuggage;
        private System.Windows.Forms.Button button_Pass;
        private System.Windows.Forms.Label label13;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_resaon;
        private System.Windows.Forms.Label label8;

    }
}