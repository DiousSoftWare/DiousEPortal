namespace DiousEPortal
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.Nav_ADM = new DevExpress.XtraNavBar.NavBarControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Tab_Main = new DevExpress.XtraTab.XtraTabControl();
            this.Page_Main = new DevExpress.XtraTab.XtraTabPage();
            this.Noti_System = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Split_Main = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nav_ADM)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tab_Main)).BeginInit();
            this.Tab_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).BeginInit();
            this.Split_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "广东迪欧家具实业有限公司企业门户";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.EnableBonusSkins = true;
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Sharp Plus";
            this.defaultLookAndFeel1.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.False;
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // Nav_ADM
            // 
            this.Nav_ADM.ActiveGroup = null;
            this.Nav_ADM.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.Nav_ADM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Nav_ADM.ForeColor = System.Drawing.Color.Transparent;
            this.Nav_ADM.Location = new System.Drawing.Point(0, 0);
            this.Nav_ADM.Name = "Nav_ADM";
            this.Nav_ADM.OptionsNavPane.ExpandedWidth = 274;
            this.Nav_ADM.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.Nav_ADM.Size = new System.Drawing.Size(274, 717);
            this.Nav_ADM.TabIndex = 1;
            this.Nav_ADM.Text = "navBarControl1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1444, 37);
            this.panel1.TabIndex = 0;
            // 
            // Tab_Main
            // 
            this.Tab_Main.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageAndTabControlHeader;
            this.Tab_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Main.Location = new System.Drawing.Point(0, 0);
            this.Tab_Main.Name = "Tab_Main";
            this.Tab_Main.SelectedTabPage = this.Page_Main;
            this.Tab_Main.Size = new System.Drawing.Size(1159, 713);
            this.Tab_Main.TabIndex = 3;
            this.Tab_Main.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Page_Main});
            this.Tab_Main.CloseButtonClick += new System.EventHandler(this.Tab_Main_CloseButtonClick);
            // 
            // Page_Main
            // 
            this.Page_Main.Name = "Page_Main";
            this.Page_Main.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            this.Page_Main.Size = new System.Drawing.Size(1155, 687);
            this.Page_Main.Text = "主面板";
            // 
            // Noti_System
            // 
            this.Noti_System.ContextMenuStrip = this.contextMenuStrip1;
            this.Noti_System.Icon = ((System.Drawing.Icon)(resources.GetObject("Noti_System.Icon")));
            this.Noti_System.Text = "notifyIcon1";
            this.Noti_System.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Split_Main
            // 
            this.Split_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Split_Main.Location = new System.Drawing.Point(0, 37);
            this.Split_Main.Name = "Split_Main";
            this.Split_Main.Panel1.Controls.Add(this.Nav_ADM);
            this.Split_Main.Panel1.Text = "Panel1";
            this.Split_Main.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.Split_Main.Panel2.Controls.Add(this.Tab_Main);
            this.Split_Main.Panel2.Text = "Panel2";
            this.Split_Main.Size = new System.Drawing.Size(1444, 717);
            this.Split_Main.SplitterPosition = 274;
            this.Split_Main.TabIndex = 5;
            this.Split_Main.Text = "splitContainerControl1";
            // 
            // MainFrm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 754);
            this.Controls.Add(this.Split_Main);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainFrm";
            this.Text = "迪欧企业门户";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.SizeChanged += new System.EventHandler(this.MainFrm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nav_ADM)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tab_Main)).EndInit();
            this.Tab_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).EndInit();
            this.Split_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private System.Diagnostics.EventLog eventLog1;
        private DevExpress.XtraNavBar.NavBarControl Nav_ADM;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraTab.XtraTabControl Tab_Main;
        private DevExpress.XtraTab.XtraTabPage Page_Main;
        private System.Windows.Forms.NotifyIcon Noti_System;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private DevExpress.XtraEditors.SplitContainerControl Split_Main;
    }
}