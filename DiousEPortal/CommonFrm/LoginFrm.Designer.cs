namespace DiousEPortal
{
    partial class LoginFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginFrm));
            this.Btn_Login = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_LoginOff = new DevExpress.XtraEditors.SimpleButton();
            this.Txt_UsrName = new System.Windows.Forms.TextBox();
            this.Txt_Pwd = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Btn_Login
            // 
            this.Btn_Login.Appearance.Font = new System.Drawing.Font("Tahoma", 16F);
            this.Btn_Login.Appearance.Options.UseFont = true;
            this.Btn_Login.Location = new System.Drawing.Point(267, 312);
            this.Btn_Login.Name = "Btn_Login";
            this.Btn_Login.Size = new System.Drawing.Size(118, 47);
            this.Btn_Login.TabIndex = 0;
            this.Btn_Login.Text = "login";
            this.Btn_Login.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // Btn_LoginOff
            // 
            this.Btn_LoginOff.Appearance.Font = new System.Drawing.Font("Tahoma", 16F);
            this.Btn_LoginOff.Appearance.Options.UseFont = true;
            this.Btn_LoginOff.Location = new System.Drawing.Point(402, 312);
            this.Btn_LoginOff.Name = "Btn_LoginOff";
            this.Btn_LoginOff.Size = new System.Drawing.Size(118, 47);
            this.Btn_LoginOff.TabIndex = 1;
            this.Btn_LoginOff.Text = "loginOff";
            this.Btn_LoginOff.Click += new System.EventHandler(this.Btn_LoginOff_Click);
            // 
            // Txt_UsrName
            // 
            this.Txt_UsrName.Location = new System.Drawing.Point(301, 238);
            this.Txt_UsrName.Name = "Txt_UsrName";
            this.Txt_UsrName.Size = new System.Drawing.Size(305, 22);
            this.Txt_UsrName.TabIndex = 2;
            this.Txt_UsrName.Text = "000000";
            // 
            // Txt_Pwd
            // 
            this.Txt_Pwd.Location = new System.Drawing.Point(301, 280);
            this.Txt_Pwd.Name = "Txt_Pwd";
            this.Txt_Pwd.PasswordChar = '*';
            this.Txt_Pwd.Size = new System.Drawing.Size(305, 22);
            this.Txt_Pwd.TabIndex = 3;
            this.Txt_Pwd.Text = "a123456";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(197, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 29);
            this.panel1.TabIndex = 4;
            // 
            // LoginFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(847, 472);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Txt_Pwd);
            this.Controls.Add(this.Txt_UsrName);
            this.Controls.Add(this.Btn_LoginOff);
            this.Controls.Add(this.Btn_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "LoginFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginFrm_FormClosed);
            this.Load += new System.EventHandler(this.LoginFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Btn_Login;
        private DevExpress.XtraEditors.SimpleButton Btn_LoginOff;
        private System.Windows.Forms.TextBox Txt_UsrName;
        private System.Windows.Forms.TextBox Txt_Pwd;
        private System.Windows.Forms.Panel panel1;
    }
}