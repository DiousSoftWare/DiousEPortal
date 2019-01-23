using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPortalCommom;
using DevExpress.XtraGrid.Views.Grid;
using System.Dynamic;
using DevExpress.XtraEditors;
using System.Reflection;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;

//01
//这是分支上的更改
namespace DiousEPortal
{

    public partial class FrmCommon : XtraForm
    {
 
        ////////////////////////////定义全局变量//////////////////////////////
        public BtnNam BtnName;
        //定义页签关闭委托
        public delegate void PageClose();
        public PageClose PgClose;
        //当前登录的用户ID
        public string CurUsrID;
        //当前登录的用户名
        public string CurUsrName;
        //定位行依据列名
        public string FocColNam;
        //行定位唯一数据标识
        public string FocColVal;
        //继承本窗体的子窗体全名
        //public string FrmNam;
        //定义主键控件名
        public string[] KeyContrNam;
        /////////////////////////////////////////////////////////////////////

        public EPortalService.CommonClient ComClient { get; set; }
        /// <summary>
        /// 当前打开的过滤窗口实例
        /// </summary>
        public FrmFilter CurFilter { get; set; }
        //96
        /// <summary>
        /// 继承本窗体的子窗体全名
        /// </summary>
        public string FrmNam { get; set; }
        
        public DataTransform DataTransformer { get; set; }

        //设置或返回过滤数据SQL语句
        public string FltSQL { get; set; }

       
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCommon()
        {
            InitializeComponent();

        }
        //111111
        //11111111
        /// <summary>
        /// 修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Edit;
                Tab_Common1.SelectedTabPage = Page_Preview;
                Tab_Common1.SelectedTabPage.PageEnabled = false;
                EnabledByBtnNam(BtnNam.Edit);
                EDabDtControls(EDabType.Enable, KeyContrNam);
                //AssiValue();
                View_Common1.OptionsBehavior.Editable = true;
                View_Common1.OptionsBehavior.ReadOnly = false;

                List<ExpandoObject> EObj = AddNewRow(View_Common1);
                ((BindingList<ExpandoObject>)View_Common1.DataSource).Add(EObj[0]);
                View_Common1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                SetColumnEdit(View_Common1, "Panel3");
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Delete;
                DialogResult Result= XtraMessageBox.Show("您是否确定删除!", "提醒", MessageBoxButtons.OKCancel);
                if (Result==DialogResult.OK)
                {
                    SaveData();
                    //只有当前选择的是详细信息页签才会触发控件赋值
                    if(Tab_Common1.SelectedTabPage.Name== "Page_Detail")
                    {
                        AssiValue();
                    }                 
                }            
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }           
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                View_Common1.OptionsBehavior.Editable = false;
                View_Common1.OptionsBehavior.ReadOnly = true;

                if (SaveData() == 1)
                {
                    View_Common2.FocusedRowHandle = LocRowByVal(View_Common2, FocColNam, FocColVal);
                    BtnName = BtnNam.Save;
                    BtnEnabledByFocusRow();       
                    Page_Preview.PageEnabled = true;
                    EDabDtControls(EDabType.Disable);                  
                }
                else
                {
                    return;
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Cancel;
                BtnEnabledByFocusRow();
                Page_Detail.PageEnabled=true;
                EDabDtControls(EDabType.Disable);
                AssiValue();
                Page_Preview.PageEnabled = true;
                View_Common1.DeleteSelectedRows();
                View_Common1.CloseEditor();
                View_Common1.OptionsBehavior.Editable = false;
                View_Common1.OptionsBehavior.ReadOnly = true;
                //重新刷新数据
                ExpandoObject EObject = (ExpandoObject)View_Common2.GetFocusedRow();
                ShowData(Grip_Common1, GetDataByFocRow(EObject, "Panel3"), GetDataByPKToChCol("Panel3"), View_Common1, "Panel3");
            }
            catch(Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 定位行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Common2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (Grip_Common2.DataSource !=null)
                {
                    if (BtnName != BtnNam.Null)
                    {
                        dynamic EObject = (ExpandoObject)View_Common2.GetFocusedRow();
                        FocColVal = EObject.分组代号;
                        //LocRowByVal(View_Common2, FocColNam, EObject.分组代号);
                        //View_Common2.FocusedRowHandle;
                    }

                    BtnEnabledByFocusRow();
                }               
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
           
        }

        /// <summary>
        /// 反审核事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Unapprove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Unapprove;
                UpdateAction("反审核");
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 审核事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Approve_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Approve;
                UpdateAction("审核");
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }


        /// <summary>
        /// 过滤事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Filter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BtnName = BtnNam.Filter;
            if(CurFilter == null)
            {
                CurFilter = new DiousEPortal.FrmFilter();
                CurFilter.CurUsrID = CurUsrID;
                CurFilter.CurUsrName = CurUsrName;
                CurFilter.ComClient = ComClient;
                CurFilter.FrmNam = FrmNam;
                CurFilter.CurOperType = OperType.InitFilter;
            }
            else
            { 
                CurFilter.CurOperType = OperType.ShowFilter;
            }  
            CurFilter.ShowDialog();
           //11
            //获取到过滤SQL
            FltSQL = CurFilter.FltSQL;
            if (FltSQL != null)
            {
                List<ExpandoObject> DataSource= InitGridData("0", FltSQL);
                if(DataSource.Count >0)
                {
                    Grip_Common2.DataSource = DataSource;
                    AssiValue();
                    ExpandoObject EObject = (ExpandoObject)View_Common2.GetFocusedRow();
                    ShowData(Grip_Common3, GetDataByFocRow(EObject, "Panel6"), GetDataByPKToChCol("Panel6"), View_Common3, "Panel6");
                    ShowData(Grip_Common1, GetDataByFocRow(EObject, "Panel3"), GetDataByPKToChCol("Panel3"), View_Common1, "Panel3");
                }
                else
                {
                    Grip_Common1.DataSource = null;
                    Grip_Common2.DataSource = null;
                    Grip_Common3.DataSource = null;
                }
               
            }
            
        }

        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Exit;
                this.Close();
                PgClose();
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
                 
        }

        /// <summary>
        /// 复制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Copy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Copy;
                Tab_Common1.SelectedTabPage = Page_Preview;
                Tab_Common1.SelectedTabPage.PageEnabled = false;
                EnabledByBtnNam(BtnNam.Copy);
                EDabDtControls(EDabType.Enable, KeyContrNam);

                View_Common1.OptionsBehavior.Editable = true;
                View_Common1.OptionsBehavior.ReadOnly = false;
                SetColumnEdit(View_Common1, "Panel3");
                

            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
        }

        private void Page_Detail_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCommon_Load(object sender, EventArgs e)
        {
            try
            {
                ComClient = new EPortalService.CommonClient();
                DataTransformer = new DataTransform();
                ComClient.SetCurUsrInfo(CurUsrID, CurUsrName);
                //设置行序号列的宽度
                this.View_Common1.IndicatorWidth = 36;
                this.View_Common2.IndicatorWidth = 36;
                this.View_Common3.IndicatorWidth = 36;
                //注册自动计算行号事件
                this.View_Common1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.View_Common1_CustomDrawRowIndicator);
                this.View_Common2.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.View_Common2_CustomDrawRowIndicator);
                this.View_Common3.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.View_Common3_CustomDrawRowIndicator);
                //设置当前选择页签
                //Tab_Common1.SelectedTabPage = Page_Preview;
                //失效详细信息页签上的控件
                EDabDtControls(EDabType.Disable);

                Grip_Common2.DataSource = InitGridData("0");
                AssiValue();
                ExpandoObject EObject = (ExpandoObject)View_Common2.GetFocusedRow();
                ShowData(Grip_Common3, GetDataByFocRow(EObject,"Panel6"), GetDataByPKToChCol("Panel6"), View_Common3, "Panel6");
                ShowData(Grip_Common1, GetDataByFocRow(EObject,"Panel3"), GetDataByPKToChCol("Panel3"), View_Common1, "Panel3");
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
           
        }

        /// <summary>
        /// 自动显示网格序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Common1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 增加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Add;
                Tab_Common1.SelectedTabPage = Page_Preview;
                Tab_Common1.SelectedTabPage.PageEnabled = false;
                EnabledByBtnNam(BtnNam.Add);
                EDabDtControls(EDabType.Enable);
                View_Common1.OptionsBehavior.Editable = true;
                View_Common1.OptionsBehavior.ReadOnly = false;

                //删除所有数据
                View_Common1.OptionsSelection.MultiSelect = true;
                View_Common1.SelectAll();
                View_Common1.DeleteSelectedRows();
                View_Common1.OptionsSelection.MultiSelect = false;

                List<ExpandoObject> EObj = AddNewRow(View_Common1);
                ((BindingList<ExpandoObject>)View_Common1.DataSource).Add(EObj[0]);
                View_Common1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                SetColumnEdit(View_Common1, "Panel3");
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            
        }

        /// <summary>
        /// 新增一行
        /// </summary>
        /// <param name="view">需要增加新行的GridView实例</param>
        /// <returns></returns>
        public List<ExpandoObject> AddNewRow(GridView view)
        {
            try
            {
                DTSerializer<string, object> DTObj = new DTSerializer<string, object>();
                List<DTSerializer<string, object>> List_DTObj = new List<DTSerializer<string, object>>(); 
                foreach (GridColumn Column in view.Columns)
                {
                    DTObj.Add(Column.Name.Replace("col",""), "");
                }
                List_DTObj.Add(DTObj);
                return LoadData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj));
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new List<ExpandoObject>();
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grip_Comon2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Tab_Common1.SelectedTabPage = Page_Detail;
                AssiValue();
                ExpandoObject EObject = (ExpandoObject)View_Common2.GetFocusedRow();
                ShowData(Grip_Common1, GetDataByFocRow(EObject, "Panel3"), GetDataByPKToChCol("Panel3"), View_Common1, "Panel3");
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 页签切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_Common1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                if(e.Page.Name== "Page_Detail")
                {                   
                AssiValue();
                ExpandoObject EObject = (ExpandoObject)View_Common2.GetFocusedRow();
                ShowData(Grip_Common1, GetDataByFocRow(EObject, "Panel3"), GetDataByPKToChCol("Panel3"), View_Common1, "Panel3");
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 禁用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Disable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Disable;
                UpdateAction("禁用");
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 启用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Enable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BtnName = BtnNam.Enable;
                UpdateAction("启用");
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }

        }

        /// <summary>
        /// 自动显示网格序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Common2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 自动显示网格序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_Common3_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }

        }


        /// <summary>
        /// 根据点击的按钮名称更改按钮状态
        /// </summary>
        /// <param name="BtnName">按钮名称</param>
        private void EnabledByBtnNam(BtnNam BtnName)
        {
            try
            {
                if (BtnName == BtnNam.Add || BtnName == BtnNam.Edit || BtnName == BtnNam.Copy)
                {
                    //点击新增或修改，保存和取消亮起，其他失效
                    Btn_Add.Enabled = false;
                    Btn_Copy.Enabled = false;
                    Btn_Edit.Enabled = false;
                    Btn_Delete.Enabled = false;
                    Btn_Unapprove.Enabled = false;
                    Btn_Approve.Enabled = false;
                    Btn_Filter.Enabled = false;
                    Btn_Disable.Enabled = false;
                    Btn_Exit.Enabled = false;
                    Btn_Enable.Enabled = false;

                    Btn_Save.Enabled = true;
                    Btn_Cancel.Enabled = true;
                }
                else if (BtnName == BtnNam.Cancel)
                {
                    //点击取消，根据当前定位记录的审核状态和启用状态设置按钮的Enable状态

                    //
                    dynamic EObject = (ExpandoObject)View_Common2.GetFocusedRow();
                }
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 根据状态更改按钮编辑状态
        /// </summary>
        /// <param name="AppState">审核状态</param>
        /// <param name="EnState">启用状态</param>
        private void BtnEnabledByState(State AppState, State EnState)
        {
            try
            {

                if (AppState == State.Check && EnState == State.Enable)
                {
                    //新增、复制、反审核、过滤、查询、禁用按钮亮起，其他失效
                    Btn_Add.Enabled = true;
                    Btn_Copy.Enabled = true;
                    Btn_Edit.Enabled = false;
                    Btn_Delete.Enabled = false;
                    Btn_Unapprove.Enabled = true;
                    Btn_Approve.Enabled = false;
                    Btn_Filter.Enabled = true;
                    Btn_Disable.Enabled = true;
                    Btn_Exit.Enabled = true;
                    Btn_Save.Enabled = false;
                    Btn_Cancel.Enabled = false;
                    Btn_Enable.Enabled = false;
                }
                else if (AppState == State.Check && EnState == State.Disable)
                {
                    //新增、复制、反审核、过滤、查询、启用按钮亮起，其他失效
                    Btn_Add.Enabled = true;
                    Btn_Copy.Enabled = true;
                    Btn_Edit.Enabled = false;
                    Btn_Delete.Enabled = false;
                    Btn_Unapprove.Enabled = true;
                    Btn_Approve.Enabled = false;
                    Btn_Filter.Enabled = true;
                    Btn_Disable.Enabled = false;
                    Btn_Exit.Enabled = true;
                    Btn_Save.Enabled = false;
                    Btn_Cancel.Enabled = false;
                    Btn_Enable.Enabled = true;
                }
                else if (AppState == State.Uncheck && EnState == State.Enable)
                {
                    //新增、复制、修改、删除、审核、过滤、禁用、退出按钮亮起，其他失效
                    Btn_Add.Enabled = true;
                    Btn_Copy.Enabled = true;
                    Btn_Edit.Enabled = true;
                    Btn_Delete.Enabled = true;
                    Btn_Approve.Enabled = true;
                    Btn_Filter.Enabled = true;
                    Btn_Disable.Enabled = true;
                    Btn_Exit.Enabled = true;

                    Btn_Cancel.Enabled = false;
                    Btn_Unapprove.Enabled = false;
                    Btn_Save.Enabled = false;
                    Btn_Enable.Enabled = false;
                }
                else if (AppState == State.Uncheck && EnState == State.Disable)
                {
                    //新增、复制、修改、删除、审核、过滤、启用、退出按钮亮起，其他失效
                    Btn_Add.Enabled = true;
                    Btn_Copy.Enabled = true;
                    Btn_Edit.Enabled = true;
                    Btn_Delete.Enabled = true;
                    Btn_Approve.Enabled = true;
                    Btn_Filter.Enabled = true;
                    Btn_Enable.Enabled = true;
                    Btn_Exit.Enabled = true;

                    Btn_Cancel.Enabled = false;
                    Btn_Unapprove.Enabled = false;
                    Btn_Save.Enabled = false;
                    Btn_Disable.Enabled = false;
                }
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 数据更新动作
        /// </summary>
        /// <param name="Msg"></param>
        private void UpdateAction(string Msg)
        {
            try
            {
                DialogResult Result = XtraMessageBox.Show("您是否确定" + Msg + "!", "提醒", MessageBoxButtons.OKCancel);
                if (Result == DialogResult.OK)
                {
                    SaveData();
                    BtnEnabledByFocusRow();
                    //将焦点移动到原定位行
                    View_Common2.FocusedRowHandle = LocRowByVal(View_Common2, FocColNam, FocColVal);
                    //假如当前选择的页签是"详细信息"页，则需要对页签内控件进行重新赋值
                    if (Tab_Common1.SelectedTabPage.Name== "Page_Detail")
                    {
                        AssiValue();
                    }
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 根据当前选择行改变按钮Enable状态
        /// </summary>
        public void BtnEnabledByFocusRow()
        {
            dynamic EObject = (ExpandoObject)View_Common2.GetFocusedRow();
            if (EObject.审核状态 == "已审核" && EObject.是否启用 == "已启用")
            {
                BtnEnabledByState(State.Check, State.Enable);
            }
            else if (EObject.审核状态 == "已审核" && EObject.是否启用 == "未启用")
            {
                BtnEnabledByState(State.Check, State.Disable);
            }
            else if (EObject.审核状态 == "未审核" && EObject.是否启用 == "已启用")
            {
                BtnEnabledByState(State.Uncheck, State.Enable);
            }
            else if (EObject.审核状态 == "未审核" && EObject.是否启用 == "未启用")
            {
                BtnEnabledByState(State.Uncheck, State.Disable);
            }
        }

        /// <summary>
        /// 根据面板名称获取面板内相关字段信息
        /// </summary>
        /// <param name="PanelNam">面板名称</param>
        /// <param name="FrmNam">窗体名称</param>
        /// <param name="TabNam"></param>
        /// <returns></returns>
        public virtual string GetColsByPanelNam(string PanelNam,string FrmNam,string TabNam)
        {
            return "";
        }

        /// <summary>
        /// 生效或失效控件
        /// </summary>
        /// <param name="type">生效/失效类型</param>
        private void EDabDtControls(EDabType type,params string[] KeyContrNam)
        {
            try
            {
                //失效所有控件
                foreach(Control control in Split_Detail.Panel1.Controls[0].Controls)
                {
                    //如果循环到的控件是Tab_Common6,则继续循环Tab_Common6里面每个页签
                    if (control.Name=="Tab_Common6")
                    {
                        if(Panel7.Controls.Count > 0)
                        {
                            //根据面板名称读取该面板内所有相关字段信息
                            List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(GetColsByPanelNam("Panel7", FrmNam,""));
                           
                            foreach (dynamic EObj in List_EObj)
                            {
                                if(EObj.FContrNam !="" && type==EDabType.Disable)
                                {
                                    Panel7.Controls[EObj.FContrNam].Enabled = false;
                                }
                                else if(EObj.FContrNam != "" && type == EDabType.Enable)
                                {
                                    //如果点击复制按钮，则保留非主键控件的值
                                    if (BtnName != BtnNam.Copy)
                                    {
                                        Panel7.Controls[EObj.FContrNam].Text = EObj.FDefValue;
                                    }
                                    Panel7.Controls[EObj.FContrNam].Enabled = EObj.FIfReadOnLy;
                                }
                            }
                        }
                        
                        if(Panel8.Controls.Count > 0)
                        {
                            //根据面板名称读取该面板内所有相关字段信息
                            List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(GetColsByPanelNam("Panel8", FrmNam,""));

                            foreach (dynamic EObj in List_EObj)
                            {
                                if (EObj.FContrNam != "" && type == EDabType.Disable)
                                {
                                    Panel8.Controls[EObj.FContrNam].Enabled = false;
                                }
                                else if (EObj.FContrNam != "" && type == EDabType.Enable)
                                {
                                    //如果点击复制按钮，则保留非主键控件的值
                                    if (BtnName != BtnNam.Copy)
                                    {
                                        Panel8.Controls[EObj.FContrNam].Text = EObj.FDefValue;
                                    }
                                    Panel8.Controls[EObj.FContrNam].Enabled = EObj.FIfReadOnLy;
                                }
                            }
                        }
                        
                        if(Panel9.Controls.Count > 0)
                        {
                            //根据面板名称读取该面板内所有相关字段信息
                            List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(GetColsByPanelNam("Panel9", FrmNam,""));

                            foreach (dynamic EObj in List_EObj)
                            {
                                if (EObj.FContrNam != "" && type == EDabType.Disable)
                                {
                                    Panel9.Controls[EObj.FContrNam].Enabled = false;
                                }
                                else if (EObj.FContrNam != "" && type == EDabType.Enable)
                                {
                                    //如果点击复制按钮，则保留非主键控件的值
                                    if (BtnName != BtnNam.Copy)
                                    {
                                        Panel9.Controls[EObj.FContrNam].Text = EObj.FDefValue;
                                    }
                                    Panel9.Controls[EObj.FContrNam].Enabled = EObj.FIfReadOnLy;
                                }
                            }
                        }
                        
                        if (Panel10.Controls.Count > 0)
                        {
                            //根据面板名称读取该面板内所有相关字段信息
                            List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(GetColsByPanelNam("Panel10", FrmNam,""));

                            foreach (dynamic EObj in List_EObj)
                            {
                                if (EObj.FContrNam != "" && type == EDabType.Disable)
                                {
                                    Panel10.Controls[EObj.FContrNam].Enabled = false;
                                }
                                else if (EObj.FContrNam != "" && type == EDabType.Enable)
                                {
                                    //如果点击复制按钮，则保留非主键控件的值
                                    if (BtnName != BtnNam.Copy)
                                    {
                                        Panel10.Controls[EObj.FContrNam].Text = EObj.FDefValue;
                                    }
                                    Panel10.Controls[EObj.FContrNam].Enabled = EObj.FIfReadOnLy;
                                }
                            }
                        }
                       
                        if(Panel11.Controls.Count > 0)
                        {
                            //根据面板名称读取该面板内所有相关字段信息
                            List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(GetColsByPanelNam("Panel11", FrmNam,""));

                            foreach (dynamic EObj in List_EObj)
                            {
                                if (EObj.FContrNam != "" && type == EDabType.Disable)
                                {
                                    Panel11.Controls[EObj.FContrNam].Enabled = false;
                                }
                                else if (EObj.FContrNam != "" && type == EDabType.Enable)
                                {
                                    //如果点击复制按钮，则保留非主键控件的值
                                    if (BtnName != BtnNam.Copy)
                                    {
                                        Panel11.Controls[EObj.FContrNam].Text = EObj.FDefValue;
                                    }
                                    Panel11.Controls[EObj.FContrNam].Enabled = EObj.FIfReadOnLy;
                                }
                            }
                        }
                        
                        if(Panel12.Controls.Count > 0)
                        {
                            //根据面板名称读取该面板内所有相关字段信息
                            List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(GetColsByPanelNam("Panel12", FrmNam,""));

                            foreach (dynamic EObj in List_EObj)
                            {
                                if (EObj.FContrNam != "" && type == EDabType.Disable)
                                {
                                    Panel12.Controls[EObj.FContrNam].Enabled = false;
                                }
                                else if (EObj.FContrNam != "" && type == EDabType.Enable)
                                {
                                    //如果点击复制按钮，则保留非主键控件的值
                                    if (BtnName != BtnNam.Copy)
                                    {
                                        Panel12.Controls[EObj.FContrNam].Text = EObj.FDefValue;
                                    }
                                    Panel12.Controls[EObj.FContrNam].Enabled = EObj.FIfReadOnLy;
                                }
                            }
                        }
                    }
                    else 
                    {
                        //根据面板名称读取该面板内所有相关字段信息
                        List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(GetColsByPanelNam("Panel4", FrmNam,""));

                        foreach (dynamic EObj in List_EObj)
                        {
                            if (EObj.FContrNam != "" && type == EDabType.Disable)
                            {
                                Panel4.Controls[EObj.FContrNam].Enabled = false;
                            }
                            else if (EObj.FContrNam != "" && type == EDabType.Enable)
                            {
                                //如果点击复制按钮，则清空主键控件的值，保留其他非主键控件值
                                if (BtnName==BtnNam.Copy)
                                {
                                    Panel4.Controls[KeyContrNam[0]].Text = "";
                                    if(EObj.FContrNam== "Txt_AppSts")
                                    {
                                        Panel4.Controls[EObj.FContrNam].Text = EObj.FDefValue;
                                    }
                                    else if (EObj.FContrNam == "Chk_IfUse")
                                    {
                                        Panel4.Controls[EObj.FContrNam].Text = EObj.FDefValue;
                                    }
                                }
                                //如果点击增加按钮，则控件值按绑定字段的默认值取
                                else if (BtnName==BtnNam.Add)
                                {                                   
                                    Panel4.Controls[EObj.FContrNam].Text = EObj.FDefValue;
                                }

                                //如果点击修改按钮，则使主键控件的值不可编辑，同时保留其他非主键控件的值             
                                if (BtnName !=BtnNam.Edit)
                                {
                                    Panel4.Controls[EObj.FContrNam].Enabled = EObj.FIfReadOnly == "1" ? false : true;
                                }
                                else
                                {
                                    if(EObj.FContrNam == KeyContrNam[0])
                                    {
                                        Panel4.Controls[KeyContrNam[0]].Enabled = false;
                                    }
                                    else
                                    {
                                        Panel4.Controls[EObj.FContrNam].Enabled = EObj.FIfReadOnly == "1" ? false : true;
                                    }
                                }                                             
                            }
                        }
                    }              
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            
        }

        /// <summary>
        /// 根据当前定位的网格行数据给Detail页签上的控件Text属性动态赋值
        /// </summary>
        public void AssiValue()
        {
            try
            {
                dynamic EObject = (ExpandoObject)View_Common2.GetFocusedRow();
                IDictionary<string, object> IDObj = EObject;
                //根据面板名称读取该面板内所有相关字段信息
                List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(GetColsByPanelNam("Panel4", FrmNam,""));

                //遍历字段列表设置网格控件单元格字段属性
                foreach (dynamic EObj in List_EObj)
                {
                    if(EObj.FContrNam !="")
                    {
                        Panel4.Controls[EObj.FContrNam].Text = IDObj[EObj.FRemark];
                    }                   
                }
                ShowData(Grip_Common1, GetDataByFocRow(EObject, "Panel3"), GetDataByPKToChCol("Panel3"),View_Common1, "Panel3");
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        
        private void Tab_Common1_SelectedPageChanged_1(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

        }

        /// <summary>
        /// 鼠标点击事件(点击主表自动关联显示细表)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grip_Comon2_Click(object sender, EventArgs e)
        {
            try
            {
                ExpandoObject EObject = (ExpandoObject)View_Common2.GetFocusedRow();
                ShowData(Grip_Common3, GetDataByFocRow(EObject,"Panel6"), GetDataByPKToChCol("Panel6"), View_Common3, "Panel6");
            }
            catch (Exception Ex)
            {
               Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 设置网格的单元格编辑控件属性
        /// </summary>
        /// <param name="Grid">GridControl 实例名称</param>
        /// <param name="PanelNam">窗体面板名称</param>
        public void SetColumnEdit(GridView Grid,string PanelNam)
        {
            try
            {
                //根据面板名称读取该面板内所有相关字段信息
                List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(GetColsByPanelNam(PanelNam, FrmNam,""));
                //遍历字段列表设置网格控件单元格字段属性
                foreach (dynamic EObj in List_EObj)
                {
                    if (EObj.FColEdit != "")
                    {
                        //如果FColEdit属性值不为空，则根据FColEdit属性值反射单元格编辑控件
                        Grid.Columns[EObj.FRemark].ColumnEdit = Assembly.Load("DevExpress.XtraEditors.v14.1").CreateInstance(EObj.FColEdit) as RepositoryItem;
                    }
                    else if (EObj.FIfHide == "1")
                    {
                        Grid.Columns[EObj.FRemark].Visible = false;
                    }

                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        public virtual int SaveData()
        {
            return 0;
        }

        public virtual string GetDataByPKToChCol(string PanelNam)
        {
            return "";
        }

        /// <summary>
        /// 显示网格数据
        /// </summary>
        /// <param name="GridCrl">网格控件实例</param>
        /// <param name="DataSource">网格控件数据源</param>
        /// <param name="XMLDS">网格空数据源XML格式</param>
        /// <param name="view">该网格控件包含的View实例</param>
        /// <param name="PanelNam">网格控件归属窗体的Panel名称</param>
        public void ShowData(DevExpress.XtraGrid.GridControl GridCrl, List<ExpandoObject> DataSource,string XMLDS, GridView view, string PanelNam)
        {
            try
            {
                if (DataSource.Count == 0)
                {
                    //如果面板名称是"Panel3",则需要转换成BindingList<T>对象
                    if (PanelNam == "Panel3")
                    {
                        BindingList<ExpandoObject> BList_EObj = new BindingList<ExpandoObject>();
                        foreach (ExpandoObject EObj in Serializer.DeserializeXMLToEObject(XMLDS))
                        {
                            BList_EObj.Add(EObj);

                        }
                        GridCrl.DataSource = BList_EObj;
                        view.DeleteSelectedRows();
                    }
                    else if (PanelNam == "Panel6")
                    {
                        GridCrl.DataSource = Serializer.DeserializeXMLToEObject(XMLDS);
                        view.DeleteSelectedRows();
                    }

                    SetColumnEdit(view, PanelNam);
                }

                else
                {
                    //如果面板名称是"Panel3",则需要转换成BindingList<T>对象
                    if (PanelNam == "Panel3")
                    {
                        BindingList<ExpandoObject> BList_EObj = new BindingList<ExpandoObject>();
                        foreach (ExpandoObject EObj in DataSource)
                        {
                            BList_EObj.Add(EObj);

                        }
                        GridCrl.DataSource = BList_EObj;
                    }
                    else
                    {
                        GridCrl.DataSource = DataSource;
                    }

                    SetColumnEdit(view, PanelNam);
                }

            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 根据主表定位行获取细表数据源
        /// </summary>
        /// <param name="EObject">主表定位行数据</param>
        /// <returns>细表数据源</returns>
        public virtual List<ExpandoObject> GetDataByFocRow(ExpandoObject EObject,string PanelNam)
        {
            dynamic Obj = EObject;
            return new List<ExpandoObject>();
        }

        /// <summary>
        /// 初始化主表网格数据
        /// </summary>
        /// <returns>主表网格控件数据源</returns>
        public virtual List<ExpandoObject> InitGridData(string IfGetFstRow="",string FltSQL="")
        {
            return new List<ExpandoObject>();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="StrXml">需要反序列化的Xml格式数据</param>
        /// <returns></returns>
        public List<ExpandoObject> LoadData(string StrXml = "")
        {
            return Serializer.DeserializeXMLToEObject(StrXml);
        }

        /// <summary>
        /// 根据指定列和字段值定位行
        /// </summary>
        /// <param name="view">执行定位行的网格实例</param>
        /// <param name="ColNam">定位行依据列名</param>
        /// <param name="Value">行定位唯一数据标识</param>
        /// <returns></returns>
        public int LocRowByVal(GridView view,string ColNam,string Value)
        {
            try
            {
            return view.LocateByValue(ColNam, Value);
            }
            catch (Exception Ex) 
            {
                Common.ShowMsg(Ex.Message);
            }

            return 0;
        }

        /// <summary>
        /// 将ExpandObject对象转换成IDictionary<string, object>对象
        /// </summary>
        /// <param name="EObj"></param>
        /// <returns></returns>
        public IDictionary<string, object> EObjToIDObj(ExpandoObject EObj)
        {
            try
            {
                return (IDictionary<string, object>)EObj;
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new Dictionary<string, object>();
        }
    }
}
