namespace VMS
{
    partial class SearchVistorInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1_ID = new System.Windows.Forms.RadioButton();
            this.textBox1_ID = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button1_Search = new System.Windows.Forms.Button();
            this.button1_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入访客ID号或证件号：";
            // 
            // radioButton1_ID
            // 
            this.radioButton1_ID.AutoSize = true;
            this.radioButton1_ID.Checked = true;
            this.radioButton1_ID.Location = new System.Drawing.Point(24, 79);
            this.radioButton1_ID.Name = "radioButton1_ID";
            this.radioButton1_ID.Size = new System.Drawing.Size(83, 16);
            this.radioButton1_ID.TabIndex = 1;
            this.radioButton1_ID.TabStop = true;
            this.radioButton1_ID.Text = "访客ID号：";
            this.radioButton1_ID.UseVisualStyleBackColor = true;
            // 
            // textBox1_ID
            // 
            this.textBox1_ID.Location = new System.Drawing.Point(59, 101);
            this.textBox1_ID.Name = "textBox1_ID";
            this.textBox1_ID.Size = new System.Drawing.Size(142, 21);
            this.textBox1_ID.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(59, 150);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(142, 21);
            this.textBox1.TabIndex = 4;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(24, 128);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 16);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Text = "访客证件号：";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // button1_Search
            // 
            this.button1_Search.Location = new System.Drawing.Point(44, 206);
            this.button1_Search.Name = "button1_Search";
            this.button1_Search.Size = new System.Drawing.Size(75, 23);
            this.button1_Search.TabIndex = 5;
            this.button1_Search.Text = "搜索";
            this.button1_Search.UseVisualStyleBackColor = true;
            // 
            // button1_Cancel
            // 
            this.button1_Cancel.Location = new System.Drawing.Point(144, 206);
            this.button1_Cancel.Name = "button1_Cancel";
            this.button1_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button1_Cancel.TabIndex = 6;
            this.button1_Cancel.Text = "取消";
            this.button1_Cancel.UseVisualStyleBackColor = true;
            this.button1_Cancel.Click += new System.EventHandler(this.button1_Cancel_Click);
            // 
            // SearchVistorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1_Cancel);
            this.Controls.Add(this.button1_Search);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.textBox1_ID);
            this.Controls.Add(this.radioButton1_ID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchVistorInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "访客搜索";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchVistorInfo_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void SearchVistorInfo_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            SearchVistorInfo.instance = null;
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1_ID;
        private System.Windows.Forms.TextBox textBox1_ID;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button1_Search;
        private System.Windows.Forms.Button button1_Cancel;
    }
}