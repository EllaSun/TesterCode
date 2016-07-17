namespace VMS
{
    partial class VisitorLogout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisitorLogout));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_VistorID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_VistorName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_HostDepart = new System.Windows.Forms.TextBox();
            this.textBox_VistorDepart = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_HostName = new System.Windows.Forms.TextBox();
            this.textBox_VistorBuilding = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_VistorLuggage = new System.Windows.Forms.TextBox();
            this.button_Reject = new System.Windows.Forms.Button();
            this.button_Pass = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.textBox_VistorLuggage);
            this.panel1.Controls.Add(this.button_Reject);
            this.panel1.Controls.Add(this.button_Pass);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 405);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::VMS.Properties.Resources.rightcheck2;
            this.pictureBox1.Location = new System.Drawing.Point(89, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(89, 94);
            this.pictureBox1.TabIndex = 75;
            this.pictureBox1.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label13.Location = new System.Drawing.Point(39, 183);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(200, 17);
            this.label13.TabIndex = 74;
            this.label13.Text = "（请确认访客实际携带物与此一致）";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_VistorID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_VistorName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_HostDepart);
            this.groupBox1.Controls.Add(this.textBox_VistorDepart);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_HostName);
            this.groupBox1.Controls.Add(this.textBox_VistorBuilding);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 164);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "详细信息";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(271, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "接待人姓名：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(23, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "访客证件号：";
            // 
            // textBox_VistorID
            // 
            this.textBox_VistorID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorID.Location = new System.Drawing.Point(106, 39);
            this.textBox_VistorID.Name = "textBox_VistorID";
            this.textBox_VistorID.ReadOnly = true;
            this.textBox_VistorID.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(23, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "访客姓名：";
            // 
            // textBox_VistorName
            // 
            this.textBox_VistorName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorName.Location = new System.Drawing.Point(106, 75);
            this.textBox_VistorName.Name = "textBox_VistorName";
            this.textBox_VistorName.ReadOnly = true;
            this.textBox_VistorName.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(23, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "访客单位：";
            // 
            // textBox_HostDepart
            // 
            this.textBox_HostDepart.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_HostDepart.Location = new System.Drawing.Point(354, 112);
            this.textBox_HostDepart.Name = "textBox_HostDepart";
            this.textBox_HostDepart.ReadOnly = true;
            this.textBox_HostDepart.Size = new System.Drawing.Size(133, 23);
            this.textBox_HostDepart.TabIndex = 11;
            this.textBox_HostDepart.TextChanged += new System.EventHandler(this.textBox_HostDepart_TextChanged);
            // 
            // textBox_VistorDepart
            // 
            this.textBox_VistorDepart.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorDepart.Location = new System.Drawing.Point(106, 116);
            this.textBox_VistorDepart.Name = "textBox_VistorDepart";
            this.textBox_VistorDepart.ReadOnly = true;
            this.textBox_VistorDepart.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorDepart.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(271, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "接待人部门：";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // textBox_HostName
            // 
            this.textBox_HostName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_HostName.Location = new System.Drawing.Point(354, 75);
            this.textBox_HostName.Name = "textBox_HostName";
            this.textBox_HostName.ReadOnly = true;
            this.textBox_HostName.Size = new System.Drawing.Size(133, 23);
            this.textBox_HostName.TabIndex = 9;
            this.textBox_HostName.TextChanged += new System.EventHandler(this.textBox_HostName_TextChanged);
            // 
            // textBox_VistorBuilding
            // 
            this.textBox_VistorBuilding.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorBuilding.Location = new System.Drawing.Point(354, 36);
            this.textBox_VistorBuilding.Name = "textBox_VistorBuilding";
            this.textBox_VistorBuilding.ReadOnly = true;
            this.textBox_VistorBuilding.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorBuilding.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(271, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "访客楼层权限：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(258, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(222, 84);
            this.label12.TabIndex = 71;
            this.label12.Text = "访 客 登 离！\r\n请收回访客卡！\r\n请确认携带物后允许。";
            // 
            // textBox_VistorLuggage
            // 
            this.textBox_VistorLuggage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorLuggage.Location = new System.Drawing.Point(119, 145);
            this.textBox_VistorLuggage.Name = "textBox_VistorLuggage";
            this.textBox_VistorLuggage.ReadOnly = true;
            this.textBox_VistorLuggage.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorLuggage.TabIndex = 22;
            this.textBox_VistorLuggage.TextChanged += new System.EventHandler(this.textBox_VistorLuggage_TextChanged);
            // 
            // button_Reject
            // 
            this.button_Reject.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Reject.Location = new System.Drawing.Point(302, 169);
            this.button_Reject.Name = "button_Reject";
            this.button_Reject.Size = new System.Drawing.Size(150, 45);
            this.button_Reject.TabIndex = 21;
            this.button_Reject.Text = "拒  绝";
            this.button_Reject.UseVisualStyleBackColor = true;
            this.button_Reject.Click += new System.EventHandler(this.button_Reject_Click);
            // 
            // button_Pass
            // 
            this.button_Pass.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Pass.Location = new System.Drawing.Point(302, 111);
            this.button_Pass.Name = "button_Pass";
            this.button_Pass.Size = new System.Drawing.Size(150, 45);
            this.button_Pass.TabIndex = 20;
            this.button_Pass.Text = "允  许";
            this.button_Pass.UseVisualStyleBackColor = true;
            this.button_Pass.Click += new System.EventHandler(this.button_Pass_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(48, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "携带物：";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // VisitorLogout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::VMS.Properties.Resources.backgroud1;
            this.ClientSize = new System.Drawing.Size(531, 405);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VisitorLogout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "个人访客登离";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VistorLogout_FormClosing);
            this.Load += new System.EventHandler(this.VistorLogout_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        void VistorLogout_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            VisitorLogout.instance = null;
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_VistorLuggage;
        private System.Windows.Forms.Button button_Reject;
        private System.Windows.Forms.Button button_Pass;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_HostDepart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_HostName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_VistorBuilding;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_VistorDepart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_VistorName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_VistorID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}