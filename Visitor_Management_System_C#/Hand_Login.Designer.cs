namespace VMS
{
    partial class Hand_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hand_Login));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_VistorID = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入访客证件号：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox_VistorID
            // 
            this.textBox_VistorID.Location = new System.Drawing.Point(81, 45);
            this.textBox_VistorID.Name = "textBox_VistorID";
            this.textBox_VistorID.Size = new System.Drawing.Size(215, 21);
            this.textBox_VistorID.TabIndex = 1;
            this.textBox_VistorID.TextChanged += new System.EventHandler(this.textBox_ID_TextChanged);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(123, 82);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(111, 23);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "确认";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // Hand_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::VMS.Properties.Resources.backgroud1;
            this.ClientSize = new System.Drawing.Size(370, 117);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_VistorID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Hand_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Hand_Login_FormClosing);
            this.Load += new System.EventHandler(this.Hand_Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void Hand_Login_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Hand_Login.instance = null;
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_VistorID;
        private System.Windows.Forms.Button button_OK;
    }
}