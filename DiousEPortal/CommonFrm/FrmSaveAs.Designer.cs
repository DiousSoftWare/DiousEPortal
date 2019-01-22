namespace DiousEPortal
{
    partial class FrmSaveAs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSaveAs));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Txt_SltName = new DevExpress.XtraEditors.TextEdit();
            this.Pal_AppRange = new DevExpress.XtraEditors.PanelControl();
            this.Btn_Clear = new DevExpress.XtraEditors.SimpleButton();
            this.Txt_ChkCdt = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.ClBox_EftRange = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.RBtn_Private = new System.Windows.Forms.RadioButton();
            this.RBtn_Share = new System.Windows.Forms.RadioButton();
            this.Chk_StartUse = new DevExpress.XtraEditors.CheckEdit();
            this.Btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_SltName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pal_AppRange)).BeginInit();
            this.Pal_AppRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_ChkCdt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClBox_EftRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_StartUse.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "方案名称";
            // 
            // Txt_SltName
            // 
            this.Txt_SltName.Location = new System.Drawing.Point(92, 4);
            this.Txt_SltName.Name = "Txt_SltName";
            this.Txt_SltName.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.False;
            this.Txt_SltName.Size = new System.Drawing.Size(273, 20);
            this.Txt_SltName.TabIndex = 1;
            // 
            // Pal_AppRange
            // 
            this.Pal_AppRange.Controls.Add(this.Btn_Clear);
            this.Pal_AppRange.Controls.Add(this.Txt_ChkCdt);
            this.Pal_AppRange.Controls.Add(this.labelControl2);
            this.Pal_AppRange.Controls.Add(this.ClBox_EftRange);
            this.Pal_AppRange.Location = new System.Drawing.Point(0, 62);
            this.Pal_AppRange.Name = "Pal_AppRange";
            this.Pal_AppRange.Size = new System.Drawing.Size(393, 347);
            this.Pal_AppRange.TabIndex = 2;
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Location = new System.Drawing.Point(334, 5);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(48, 24);
            this.Btn_Clear.TabIndex = 4;
            this.Btn_Clear.Text = "清除";
            // 
            // Txt_ChkCdt
            // 
            this.Txt_ChkCdt.Location = new System.Drawing.Point(76, 6);
            this.Txt_ChkCdt.Name = "Txt_ChkCdt";
            this.Txt_ChkCdt.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.False;
            this.Txt_ChkCdt.Size = new System.Drawing.Size(252, 20);
            this.Txt_ChkCdt.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "应用范围";
            // 
            // ClBox_EftRange
            // 
            this.ClBox_EftRange.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ClBox_EftRange.Location = new System.Drawing.Point(2, 39);
            this.ClBox_EftRange.Name = "ClBox_EftRange";
            this.ClBox_EftRange.Size = new System.Drawing.Size(389, 306);
            this.ClBox_EftRange.TabIndex = 1;
            // 
            // RBtn_Private
            // 
            this.RBtn_Private.AutoSize = true;
            this.RBtn_Private.Location = new System.Drawing.Point(106, 30);
            this.RBtn_Private.Name = "RBtn_Private";
            this.RBtn_Private.Size = new System.Drawing.Size(49, 18);
            this.RBtn_Private.TabIndex = 3;
            this.RBtn_Private.TabStop = true;
            this.RBtn_Private.Text = "私有";
            this.RBtn_Private.UseVisualStyleBackColor = true;
            this.RBtn_Private.CheckedChanged += new System.EventHandler(this.RBtn_Private_CheckedChanged);
            // 
            // RBtn_Share
            // 
            this.RBtn_Share.AutoSize = true;
            this.RBtn_Share.Location = new System.Drawing.Point(161, 30);
            this.RBtn_Share.Name = "RBtn_Share";
            this.RBtn_Share.Size = new System.Drawing.Size(49, 18);
            this.RBtn_Share.TabIndex = 4;
            this.RBtn_Share.TabStop = true;
            this.RBtn_Share.Text = "共享";
            this.RBtn_Share.UseVisualStyleBackColor = true;
            // 
            // Chk_StartUse
            // 
            this.Chk_StartUse.Location = new System.Drawing.Point(251, 30);
            this.Chk_StartUse.Name = "Chk_StartUse";
            this.Chk_StartUse.Properties.Caption = "窗口打开时启用";
            this.Chk_StartUse.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.False;
            this.Chk_StartUse.Size = new System.Drawing.Size(104, 19);
            this.Chk_StartUse.TabIndex = 5;
            // 
            // Btn_OK
            // 
            this.Btn_OK.Location = new System.Drawing.Point(63, 413);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(75, 23);
            this.Btn_OK.TabIndex = 6;
            this.Btn_OK.Text = "确定";
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(229, 413);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 7;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // FrmSaveAs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 441);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.Chk_StartUse);
            this.Controls.Add(this.RBtn_Share);
            this.Controls.Add(this.RBtn_Private);
            this.Controls.Add(this.Pal_AppRange);
            this.Controls.Add(this.Txt_SltName);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSaveAs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "另存为";
            this.Load += new System.EventHandler(this.FrmSaveAs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Txt_SltName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pal_AppRange)).EndInit();
            this.Pal_AppRange.ResumeLayout(false);
            this.Pal_AppRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_ChkCdt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClBox_EftRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chk_StartUse.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit Txt_SltName;
        private DevExpress.XtraEditors.PanelControl Pal_AppRange;
        private DevExpress.XtraEditors.SimpleButton Btn_Clear;
        private DevExpress.XtraEditors.TextEdit Txt_ChkCdt;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckedListBoxControl ClBox_EftRange;
        private System.Windows.Forms.RadioButton RBtn_Private;
        private System.Windows.Forms.RadioButton RBtn_Share;
        private DevExpress.XtraEditors.CheckEdit Chk_StartUse;
        private DevExpress.XtraEditors.SimpleButton Btn_OK;
        private DevExpress.XtraEditors.SimpleButton Btn_Cancel;
    }
}