using EPortalCommom;
namespace DiousEPortal
{
    partial class FrmFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFilter));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.Tab_Slts = new DevExpress.XtraTab.XtraTabControl();
            this.Pg_SltFilter = new DevExpress.XtraTab.XtraTabPage();
            this.Pal_FltSlt = new DevExpress.XtraEditors.PanelControl();
            this.TrList_FltSlt = new DevExpress.XtraTreeList.TreeList();
            this.Pg_Range = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.Btn_Drop = new DevExpress.XtraEditors.SimpleButton();
            this.Lab_Edit = new DevExpress.XtraEditors.LabelControl();
            this.Pal_FlCtner = new DevExpress.XtraEditors.PanelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Btn_SaveAs = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Clear = new DevExpress.XtraEditors.SimpleButton();
            this.UsrContr_Filter2 = new DiousEPortal.UsrContr_Filter(AddType.Auto) ;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tab_Slts)).BeginInit();
            this.Tab_Slts.SuspendLayout();
            this.Pg_SltFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pal_FltSlt)).BeginInit();
            this.Pal_FltSlt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrList_FltSlt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pal_FlCtner)).BeginInit();
            this.Pal_FlCtner.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.splitContainerControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1473, 800);
            this.panelControl1.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 2);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl3);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl1.Panel2.Controls.Add(this.Pal_FlCtner);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl7);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl6);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl5);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl4);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl3);
            this.splitContainerControl1.Panel2.Controls.Add(this.Btn_SaveAs);
            this.splitContainerControl1.Panel2.Controls.Add(this.Btn_Save);
            this.splitContainerControl1.Panel2.Controls.Add(this.Btn_Cancel);
            this.splitContainerControl1.Panel2.Controls.Add(this.Btn_Ok);
            this.splitContainerControl1.Panel2.Controls.Add(this.Btn_Clear);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1469, 796);
            this.splitContainerControl1.SplitterPosition = 216;
            this.splitContainerControl1.TabIndex = 9;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(216, 35);
            this.panelControl3.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl2.Location = new System.Drawing.Point(6, 6);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 19);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "过滤方案";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControl5);
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(216, 796);
            this.panelControl2.TabIndex = 0;
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.Tab_Slts);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl5.Location = new System.Drawing.Point(2, 27);
            this.panelControl5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(212, 721);
            this.panelControl5.TabIndex = 4;
            // 
            // Tab_Slts
            // 
            this.Tab_Slts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Slts.Location = new System.Drawing.Point(2, 2);
            this.Tab_Slts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tab_Slts.Name = "Tab_Slts";
            this.Tab_Slts.SelectedTabPage = this.Pg_SltFilter;
            this.Tab_Slts.Size = new System.Drawing.Size(208, 717);
            this.Tab_Slts.TabIndex = 0;
            this.Tab_Slts.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Pg_SltFilter,
            this.Pg_Range});
            // 
            // Pg_SltFilter
            // 
            this.Pg_SltFilter.Controls.Add(this.Pal_FltSlt);
            this.Pg_SltFilter.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Pg_SltFilter.Name = "Pg_SltFilter";
            this.Pg_SltFilter.Size = new System.Drawing.Size(202, 680);
            this.Pg_SltFilter.Text = "过滤方案";
            // 
            // Pal_FltSlt
            // 
            this.Pal_FltSlt.Controls.Add(this.TrList_FltSlt);
            this.Pal_FltSlt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pal_FltSlt.Location = new System.Drawing.Point(0, 0);
            this.Pal_FltSlt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Pal_FltSlt.Name = "Pal_FltSlt";
            this.Pal_FltSlt.Size = new System.Drawing.Size(202, 680);
            this.Pal_FltSlt.TabIndex = 0;
            // 
            // TrList_FltSlt
            // 
            this.TrList_FltSlt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrList_FltSlt.Location = new System.Drawing.Point(2, 2);
            this.TrList_FltSlt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TrList_FltSlt.Name = "TrList_FltSlt";
            this.TrList_FltSlt.Size = new System.Drawing.Size(198, 676);
            this.TrList_FltSlt.TabIndex = 0;
            this.TrList_FltSlt.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.TrList_FltSlt_FocusedNodeChanged);
            this.TrList_FltSlt.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.TrList_FltSlt_CellValueChanged);
            this.TrList_FltSlt.Click += new System.EventHandler(this.TrList_FltSlt_Click);
            this.TrList_FltSlt.DoubleClick += new System.EventHandler(this.TrList_FltSlt_DoubleClick);
            // 
            // Pg_Range
            // 
            this.Pg_Range.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Pg_Range.Name = "Pg_Range";
            this.Pg_Range.Size = new System.Drawing.Size(202, 680);
            this.Pg_Range.Text = "应用范围";
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.Btn_Drop);
            this.panelControl4.Controls.Add(this.Lab_Edit);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(2, 748);
            this.panelControl4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(212, 46);
            this.panelControl4.TabIndex = 3;
            // 
            // Btn_Drop
            // 
            this.Btn_Drop.Location = new System.Drawing.Point(3, 6);
            this.Btn_Drop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Drop.Name = "Btn_Drop";
            this.Btn_Drop.Size = new System.Drawing.Size(70, 36);
            this.Btn_Drop.TabIndex = 1;
            this.Btn_Drop.Text = "删除";
            this.Btn_Drop.Click += new System.EventHandler(this.Btn_Drop_Click);
            // 
            // Lab_Edit
            // 
            this.Lab_Edit.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.Lab_Edit.Location = new System.Drawing.Point(133, 13);
            this.Lab_Edit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Lab_Edit.Name = "Lab_Edit";
            this.Lab_Edit.Size = new System.Drawing.Size(128, 19);
            this.Lab_Edit.TabIndex = 2;
            this.Lab_Edit.Text = "双击修改方案名称";
            // 
            // Pal_FlCtner
            // 
            this.Pal_FlCtner.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Pal_FlCtner.Controls.Add(this.UsrContr_Filter2);
            this.Pal_FlCtner.Location = new System.Drawing.Point(3, 46);
            this.Pal_FlCtner.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Pal_FlCtner.Name = "Pal_FlCtner";
            this.Pal_FlCtner.Size = new System.Drawing.Size(1226, 682);
            this.Pal_FlCtner.TabIndex = 43;
            // 
            // UsrContr_Filter2
            // 
            this.UsrContr_Filter2.addType = EPortalCommom.AddType.Manual;
            this.UsrContr_Filter2.CtrContainer = null;
            this.UsrContr_Filter2.Location = new System.Drawing.Point(6, 6);
            this.UsrContr_Filter2.Margin = new System.Windows.Forms.Padding(6);
            this.UsrContr_Filter2.Name = "UsrContr_Filter2";
            this.UsrContr_Filter2.Size = new System.Drawing.Size(1154, 37);
            this.UsrContr_Filter2.TabIndex = 0;
            this.UsrContr_Filter2.Tag = "2";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl7.Location = new System.Drawing.Point(7, 19);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(32, 19);
            this.labelControl7.TabIndex = 23;
            this.labelControl7.Text = "括号";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl1.Location = new System.Drawing.Point(864, 19);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 19);
            this.labelControl1.TabIndex = 22;
            this.labelControl1.Text = "括号";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl6.Location = new System.Drawing.Point(600, 19);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(78, 19);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "  条  件  值";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl5.Location = new System.Drawing.Point(994, 19);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(32, 19);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "关系";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl4.Location = new System.Drawing.Point(349, 19);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(64, 19);
            this.labelControl4.TabIndex = 15;
            this.labelControl4.Text = "条件名称";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelControl3.Location = new System.Drawing.Point(146, 19);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(64, 19);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "字段名称";
            // 
            // Btn_SaveAs
            // 
            this.Btn_SaveAs.Location = new System.Drawing.Point(770, 746);
            this.Btn_SaveAs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_SaveAs.Name = "Btn_SaveAs";
            this.Btn_SaveAs.Size = new System.Drawing.Size(106, 36);
            this.Btn_SaveAs.TabIndex = 13;
            this.Btn_SaveAs.Text = "另存为";
            this.Btn_SaveAs.Click += new System.EventHandler(this.Btn_SaveAs_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(620, 746);
            this.Btn_Save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(106, 36);
            this.Btn_Save.TabIndex = 12;
            this.Btn_Save.Text = "覆盖方案";
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(470, 746);
            this.Btn_Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(106, 36);
            this.Btn_Cancel.TabIndex = 11;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Location = new System.Drawing.Point(329, 746);
            this.Btn_Ok.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(106, 36);
            this.Btn_Ok.TabIndex = 10;
            this.Btn_Ok.Text = "确定";
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Location = new System.Drawing.Point(187, 746);
            this.Btn_Clear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(106, 36);
            this.Btn_Clear.TabIndex = 9;
            this.Btn_Clear.Text = "清空";
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // FrmFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1473, 800);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFilter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "过滤";
            this.Load += new System.EventHandler(this.FrmFilter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tab_Slts)).EndInit();
            this.Tab_Slts.ResumeLayout(false);
            this.Pg_SltFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pal_FltSlt)).EndInit();
            this.Pal_FltSlt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TrList_FltSlt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pal_FlCtner)).EndInit();
            this.Pal_FlCtner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl Lab_Edit;
        private DevExpress.XtraTab.XtraTabControl Tab_Slts;
        private DevExpress.XtraTab.XtraTabPage Pg_Range;
        private DevExpress.XtraEditors.SimpleButton Btn_Drop;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton Btn_SaveAs;
        private DevExpress.XtraEditors.SimpleButton Btn_Save;
        private DevExpress.XtraEditors.SimpleButton Btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton Btn_Ok;
        private DevExpress.XtraEditors.SimpleButton Btn_Clear;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl Pal_FlCtner;
        private DevExpress.XtraTab.XtraTabPage Pg_SltFilter;
        private DevExpress.XtraEditors.PanelControl Pal_FltSlt;
        private DevExpress.XtraTreeList.TreeList TrList_FltSlt;
        public DiousEPortal.UsrContr_Filter UsrContr_Filter2;
    }
}