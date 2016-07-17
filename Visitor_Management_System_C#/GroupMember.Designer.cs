namespace VMS
{
    partial class GroupMember
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupMember));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.button_Reject = new System.Windows.Forms.Button();
            this.button_Pass = new System.Windows.Forms.Button();
            this.comboBox_VistorLuggage = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_VistorName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_VistorID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::VMS.Properties.Resources.backgroud1;
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.button_Reject);
            this.panel1.Controls.Add(this.button_Pass);
            this.panel1.Controls.Add(this.comboBox_VistorLuggage);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox_VistorName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_VistorID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 303);
            this.panel1.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label13.Location = new System.Drawing.Point(24, 142);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(236, 34);
            this.label13.TabIndex = 74;
            this.label13.Text = "（携带物选项，可以进行下拉菜单选择操作\r\n             也可以直接在文本框里输入。）";
            // 
            // button_Reject
            // 
            this.button_Reject.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Reject.Location = new System.Drawing.Point(163, 246);
            this.button_Reject.Name = "button_Reject";
            this.button_Reject.Size = new System.Drawing.Size(97, 45);
            this.button_Reject.TabIndex = 39;
            this.button_Reject.Text = "拒  绝";
            this.button_Reject.UseVisualStyleBackColor = true;
            this.button_Reject.Click += new System.EventHandler(this.button_Reject_Click);
            // 
            // button_Pass
            // 
            this.button_Pass.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Pass.Location = new System.Drawing.Point(28, 246);
            this.button_Pass.Name = "button_Pass";
            this.button_Pass.Size = new System.Drawing.Size(97, 45);
            this.button_Pass.TabIndex = 38;
            this.button_Pass.Text = "允  许";
            this.button_Pass.UseVisualStyleBackColor = true;
            this.button_Pass.Click += new System.EventHandler(this.button_Pass_Click);
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
            this.comboBox_VistorLuggage.Location = new System.Drawing.Point(116, 107);
            this.comboBox_VistorLuggage.Name = "comboBox_VistorLuggage";
            this.comboBox_VistorLuggage.Size = new System.Drawing.Size(133, 25);
            this.comboBox_VistorLuggage.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(33, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 36;
            this.label7.Text = "携带物：";
            // 
            // textBox_VistorName
            // 
            this.textBox_VistorName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorName.Location = new System.Drawing.Point(116, 67);
            this.textBox_VistorName.Name = "textBox_VistorName";
            this.textBox_VistorName.ReadOnly = true;
            this.textBox_VistorName.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorName.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(33, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 32;
            this.label2.Text = "访客姓名：";
            // 
            // textBox_VistorID
            // 
            this.textBox_VistorID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_VistorID.Location = new System.Drawing.Point(116, 31);
            this.textBox_VistorID.Name = "textBox_VistorID";
            this.textBox_VistorID.ReadOnly = true;
            this.textBox_VistorID.Size = new System.Drawing.Size(133, 23);
            this.textBox_VistorID.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 30;
            this.label1.Text = "访客证件号：";
            // 
            // GroupMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 303);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GroupMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "团体访客成员";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GroupMember_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        void GroupMember_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            GroupMember.instance = null;
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_VistorName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_VistorID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_VistorLuggage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_Reject;
        private System.Windows.Forms.Button button_Pass;
        private System.Windows.Forms.Label label13;
    }
}