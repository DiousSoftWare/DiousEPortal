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
using DevExpress.XtraGrid.Controls;
using EPortalModel;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
namespace DiousEPortal
{
    public partial class FrmFilter : XtraForm
    {
        public FrmFilter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public string CurUsrID { set; get; }

        /// <summary>
        /// 关联的窗体全名
        /// </summary>
        public string CurUsrName { set; get; }

        /// <summary>
        /// 数据反序列化器
        /// </summary>
        public DataTransform DataTransformer { set; get; }

        /// <summary>
        /// WCF通用客户端
        /// </summary>
        public EPortalService.CommonClient ComClient { get; set; }

        /// <summary>
        /// 过滤方案树
        /// </summary>
        //public TreeList TrList_FltSlt { get; set; }

        /// <summary>
        /// 过滤窗口关联的窗体名称
        /// </summary>
        public string FrmNam { get; set; }

        /// <summary>
        /// 修改后的方案名称
        /// </summary>
        public string SltEditName { get; set; }

        /// <summary>
        /// 修改后的方案ID
        /// </summary>
        public TreeListNode SltEditNode { get; set; }

        /// <summary>
        /// 当前过滤方案明细
        /// </summary>
        public List<DTSerializer<string, object>> CurFltSltDtl { get; set; }

        //设置或返回过滤数据SQL语句
        public string FltSQL { get; set; }

        /// <summary>
        /// 设置或返回当前打开的过滤窗口
        /// </summary>
        public OperType CurOperType { get; set; }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmFilter_Load(object sender, EventArgs e)
        {
            try
            {
                if(CurOperType == OperType.InitFilter)
                {
                UsrContr_Filter2.CtrContainer = Pal_FlCtner;
                DataTransformer = new DataTransform();
                InitLkUp(this.UsrContr_Filter2);
                //新建树菜单列
                DevExpress.XtraTreeList.Columns.TreeListColumn Col_FSltID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                Col_FSltID.Name = "FSltID";
                Col_FSltID.FieldName = "FSltID";
                Col_FSltID.Caption = "FSltID";

                DevExpress.XtraTreeList.Columns.TreeListColumn Col_FParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                Col_FParentID.Name = "FParentID";
                Col_FParentID.Caption = "FParentID";
                Col_FParentID.FieldName = "FParentID";

                DevExpress.XtraTreeList.Columns.TreeListColumn Col_FSltName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                Col_FSltName.Name = "FSltName";
                Col_FSltName.Caption = "FSltName";
                Col_FSltName.FieldName = "FSltName";
                Col_FSltName.Visible = true;
                Col_FSltName.VisibleIndex = 2;

                DevExpress.XtraTreeList.Columns.TreeListColumn Col_FFrmName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                Col_FFrmName.Name = "FFrmName";
                Col_FFrmName.Caption = "FFrmName";
                Col_FFrmName.FieldName = "FFrmName";

                //将列添加到树菜单
                TrList_FltSlt.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
                        Col_FSltID,
                        Col_FParentID,
                        Col_FSltName,
                        Col_FFrmName}
                );
                
                TrList_FltSlt.KeyFieldName = "FSltID";
                TrList_FltSlt.ParentFieldName = "FParentID";
                TrList_FltSlt.OptionsView.ShowColumns = false;
                TrList_FltSlt.OptionsBehavior.Editable = false;

                //设置树菜单节点竖向边框不显示
                TrList_FltSlt.OptionsView.ShowVertLines = false;
                //设置树菜单节点横向边框不显示
                TrList_FltSlt.OptionsView.ShowHorzLines = false;
                //设置树菜单节点的字体大小
                TrList_FltSlt.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);

                Pal_FltSlt.Controls.Add(TrList_FltSlt);
                //将过滤方案反序列化成Datatable
                TrList_FltSlt.DataSource = Serializer.DeserializeXMLToDT(ComClient.GetFltSlt(CurUsrID, FrmNam));
                TrList_FltSlt.ExpandAll();
                }
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            

        }

        /// <summary>
        /// 清空条件选择控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                for(int i=1;i<=Pal_FlCtner.Controls.Count;i++)
                {
                    //清空全部条件选择控件
                    Pal_FlCtner.Controls.Clear(); 
                }
                //新建一个条件选择控件
                UsrContr_Filter Filter = new UsrContr_Filter(AddType.Manual);
                InitLkUp(Filter);
                Pal_FlCtner.Controls.Add(Filter);
                Filter.Name = "UsrContr_Filter2";
                Filter.Tag = "2";
                Filter.Location = new Point(3,4);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 另存为弹窗事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_SaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                //获取面板中最后一个用户控件
                int ContrNum = Pal_FlCtner.Controls.Count;
                UsrContr_Filter Filter = (UsrContr_Filter)Pal_FlCtner.Controls[ContrNum - 1];

                if (((SearchLookUpEdit)Filter.Controls["SLkUp_ColNams"]).EditValue != null)
                {
                    FrmSaveAs frmSaveAs = new DiousEPortal.FrmSaveAs();
                    frmSaveAs.ComClient = ComClient;
                    frmSaveAs.CurUsrID = CurUsrID;
                    frmSaveAs.CurUsrName = CurUsrName;
                    frmSaveAs.FrmNam = FrmNam;
                    CurFltSltDtl = GetFltSltDtl();
                    //判断左右括号是否匹配,如果不匹配则退出方法
                    if( BracketsMatch(CurFltSltDtl))
                    {
                        //传递过滤方案明细数据
                        frmSaveAs.FltSltDtl = CurFltSltDtl;
                        frmSaveAs.ShowDialog();
                        TrList_FltSlt.DataSource = Serializer.DeserializeXMLToDT(ComClient.GetFltSlt(CurUsrID, FrmNam));
                        TrList_FltSlt.ExpandAll();
                    }
                    else
                    {
                        return;
                    }                  
                }
                else
                {
                    Common.ShowMsg("字段名不能为空！");
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 检查左右括号是否匹配
        /// </summary>
        /// <param name="List_DTObj">过滤方案明细数据</param>
        /// <returns>返回true:左右括号匹配;返回false:左右括号不匹配</returns>
        private bool BracketsMatch(List<DTSerializer<string, object>> List_DTObj)
        {
            try
            {
                int Brackets1No1 = 0;
                int Brackets1No2 = 0;
                int Brackets1No3 = 0;
                int Brackets2No1 = 0;
                int Brackets2No2 = 0;
                int Brackets2No3 = 0;

                foreach (DTSerializer<string, object> DTObj in List_DTObj)
                {
                    foreach (string Key in DTObj.Keys)
                    {
                        if (Key == "FBrackets1Value" && DTObj[Key].ToString() == "(")
                        {
                            Brackets1No1 += 1;
                        }
                        else if (Key == "FBrackets1Value" && DTObj[Key].ToString() == "((")
                        {
                            Brackets1No2 += 1;
                        }
                        else if (Key == "FBrackets1Value" && DTObj[Key].ToString() == "(((")
                        {
                            Brackets1No3 += 1;
                        }
                        else if (Key == "FBrackets2" && DTObj[Key].ToString() == ")")
                        {
                            Brackets2No1 += 1;
                        }
                        else if (Key == "FBrackets2" && DTObj[Key].ToString() == "))")
                        {
                            Brackets2No2 += 1;
                        }
                        else if (Key == "FBrackets2" && DTObj[Key].ToString() == ")))")
                        {
                            Brackets2No3 += 1;
                        }
                    }
                }

                if (Brackets1No1 != Brackets2No1 || Brackets1No2 != Brackets2No2
                    || Brackets1No3 != Brackets2No3)
                {
                    Common.ShowMsg("过滤方案的左右括号不匹配，请修改！");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception Ex)
            {
              Common.ShowMsg(Ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 删除方案事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Drop_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选择的方案名称
                string FSltID = TrList_FltSlt.FocusedNode.GetValue("FSltID").ToString();
                ComClient.BeginTransaction();
                ComClient.SaveFltSlt1(OperType.Delete,"", "t_ADMM_FltSltList", " FSltID='" + FSltID + "'");
                ComClient.SaveFltSlt1(OperType.Delete, "", "t_ADMM_FltSltforDtl", " FSltID='" + FSltID + "'");
                ComClient.CommitTransaction();
                TrList_FltSlt.DataSource= Serializer.DeserializeXMLToDT(ComClient.GetFltSlt(CurUsrID, FrmNam));
                TrList_FltSlt.ExpandAll();
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 过滤方案单击选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrList_FltSlt_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选择的方案名称
                string SltName = TrList_FltSlt.FocusedNode.GetDisplayText("FSltName");
                //获取当前选择的树节点
                SltEditNode = TrList_FltSlt.FocusedNode;

                //检查当前选择的方案是否是该用户建立的方案，如果是，则可以删除，否则不能删除。
                if (ComClient.CheckIfUsrSlt(SltName, CurUsrID, FrmNam) >= 1)
                {
                    Btn_Drop.Enabled = true;
                }
                else
                {
                    Btn_Drop.Enabled = false;
                }

                }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 过滤方案双击选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrList_FltSlt_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //获取当前选择的方案名称
                string SltName = TrList_FltSlt.FocusedNode.GetDisplayText("FSltName");
                if (ComClient.CheckIfUsrSlt(SltName,CurUsrID,FrmNam) >=1)
                {
                    //将树节点切换成编辑模式
                    TrList_FltSlt.OptionsBehavior.Editable = true;
                }
                else
                {
                    //将树节点切换成不可编辑模式
                    TrList_FltSlt.OptionsBehavior.Editable = false;
                }
                
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 过滤方案节点焦点改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrList_FltSlt_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            try
            {
                //将树节点切换成不可编辑模式
                TrList_FltSlt.OptionsBehavior.Editable = false;
                SltEditNode = TrList_FltSlt.FocusedNode;

                System.Drawing.Point Point1 = new System.Drawing.Point(0, 4);

                string SltID = SltEditNode.GetValue("FSltID").ToString();
                //1.根据过滤方案ID获取方案明细数据
                List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(ComClient.GetFltSltDtlBySltID(SltID));
                if(SltID !="P01" && SltID != "S01")
                {
                    //2.清空面板中的所有过滤控件
                    Pal_FlCtner.Controls.Clear();
                }
               
                //3.依次读取过滤方案明细数据，根据读取到的数据设置过滤控件中每一个子控件的隐藏属性及控件的EditValue
                int i = 2;
                foreach (dynamic EObj in List_EObj)
                {
                    UsrContr_Filter Filter = new UsrContr_Filter(AddType.Auto);
                    Filter.Name = "UsrContr_Filter" + i.ToString();
                    Filter.Tag = i++;
                    //初始化过滤控件
                    InitLkUp(Filter);
                    string[] Names = { "LkUp_Brackets1", "SLkUp_ColNams", "LkUp_Cdt", "CBox_CdtValue1", "CBox_CdtValue2", "CBox_CdtValue3", "DEd_CdtValue3", "DEd_CdtValue5", "DEd_CdtValue6", "LkUp_Brackets2", "LkUp_Rlt" };
                    foreach (string name in Names)
                    {
                        //return;
                        //查询出指定名称的控件
                        var Query = from Contr1 in Filter.Controls.Cast<Control>()
                                    where Contr1.Name == name
                                    select Contr1;
                        Control Contr = Query.First<Control>();
                        if (Contr.Name == "LkUp_Brackets1")
                        {
                            //((LookUpEdit)Contr).Text = (string)EObj.FBrackets1Value;
                            List<ExpandoObject> ListEObj = (List<ExpandoObject>)(((LookUpEdit)Contr).Properties.DataSource);
                            var Qurey = from dynamic EObj1 in ListEObj
                                        where EObj1.FRemark == EObj.FBrackets1Value
                                        select EObj1;
                            if (Qurey.Count<dynamic>() != 0)
                            {
                                dynamic EObj2 = Qurey.First<dynamic>();
                                ((LookUpEdit)Contr).EditValue = EObj2.FColNm;
                            }

                        }
                        else if (Contr.Name == "SLkUp_ColNams")
                        {
                            List<ExpandoObject> ListEObj = (List<ExpandoObject>)(((SearchLookUpEdit)Contr).Properties.DataSource);
                            var Qurey = from dynamic EObj1 in ListEObj
                                        where EObj1.FRemark == EObj.FColNamsValue
                                        select EObj1;
                            if (Qurey.Count<dynamic>() != 0)
                            {
                                dynamic EObj2 = Qurey.First<dynamic>();
                                ((SearchLookUpEdit)Contr).EditValue = EObj2.FColNm;
                            }
                        }
                        else if (Contr.Name == "LkUp_Cdt")
                        {
                            //((LookUpEdit)Contr).Text = (string)EObj.FCdt;

                            List<ExpandoObject> ListEObj = (List<ExpandoObject>)(((LookUpEdit)Contr).Properties.DataSource);
                            var Qurey = from dynamic EObj1 in ListEObj
                                        where EObj1.FRemark == EObj.FCdt
                                        select EObj1;
                            if (Qurey.Count<dynamic>() != 0)
                            {
                                dynamic EObj2 = Qurey.First<dynamic>();
                                ((LookUpEdit)Contr).EditValue = EObj2.FColNm;
                            }
                        }
                        else if (Contr.Name == "CBox_CdtValue1")
                        {
                            ((ComboBoxEdit)Contr).Text = (string)EObj.FCBoxCdtValue1;
                            ((ComboBoxEdit)Contr).Visible = (string)EObj.FCBoxCdtValue1 == "null" ? false : true;
                        }
                        else if (Contr.Name == "CBox_CdtValue2")
                        {
                            ((ComboBoxEdit)Contr).Text = (string)EObj.FCBoxCdtValue2;
                            ((ComboBoxEdit)Contr).Visible = (string)EObj.FCBoxCdtValue2 == "null" ? false : true;
                        }
                        else if (Contr.Name == "CBox_CdtValue3")
                        {
                            ((ComboBoxEdit)Contr).Text = (string)EObj.FCBoxCdtValue3;
                            ((ComboBoxEdit)Contr).Visible = (string)EObj.FCBoxCdtValue3 == "null" ? false : true;
                        }
                        else if (Contr.Name == "DEd_CdtValue3")
                        {
                            ((DateEdit)Contr).Text = (string)EObj.FDedCdtValue3;
                            ((DateEdit)Contr).Visible = (string)EObj.FDedCdtValue3 == "null" ? false : true;
                        }
                        else if (Contr.Name == "DEd_CdtValue5")
                        {
                            ((DateEdit)Contr).Text = (string)EObj.FDedCdtValue5;
                            ((DateEdit)Contr).Visible = (string)EObj.FDedCdtValue5 == "null" ? false : true;
                        }
                        else if (Contr.Name == "DEd_CdtValue6")
                        {
                            ((DateEdit)Contr).Text = (string)EObj.FDedCdtValue6;
                            ((DateEdit)Contr).Visible = (string)EObj.FDedCdtValue6 == "null" ? false : true;
                        }
                        else if (Contr.Name == "LkUp_Brackets2")
                        {
                            //((LookUpEdit)Contr).Text = (string)EObj.FBrackets2;
                            List<ExpandoObject> ListEObj = (List<ExpandoObject>)(((LookUpEdit)Contr).Properties.DataSource);
                            var Qurey = from dynamic EObj1 in ListEObj
                                        where EObj1.FRemark == EObj.FBrackets2
                                        select EObj1;
                            if (Qurey.Count<dynamic>() != 0)
                            {
                                dynamic EObj2 = Qurey.First<dynamic>();
                                ((LookUpEdit)Contr).EditValue = EObj2.FColNm;
                            }
                        }
                        else if (Contr.Name == "LkUp_Rlt")
                        {
                            List<ExpandoObject> ListEObj = (List<ExpandoObject>)(((LookUpEdit)Contr).Properties.DataSource);
                            var Qurey = from dynamic EObj1 in ListEObj
                                        where EObj1.FRemark == EObj.FRlt
                                        select EObj1;
                            if(Qurey.Count<dynamic>() !=0)
                            {
                                dynamic EObj2 = Qurey.First<dynamic>();
                                ((LookUpEdit)Contr).EditValue = EObj2.FColNm;
                            }
                           

                            //((LookUpEdit)Contr).Text ="或者";
                            Filter.addType = AddType.Manual;                                             
                        }
                    }
                    Pal_FlCtner.Controls.Add(Filter);

                    if(Filter.Name != "UsrContr_Filter2")
                    {
                        Point1.Y = Point1.Y + 23;
                        Filter.Location = Point1;
                    }                   
                }

            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 过滤方案名称更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrList_FltSlt_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                EditFltSlt();
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 修改过滤方案
        /// </summary>
        private void EditFltSlt()
        {
            try
            {
                if (SltEditNode != null)
                {
                    string SltName = SltEditNode.GetValue("FSltName").ToString();
                    string SltID = SltEditNode.GetValue("FSltID").ToString();

                    if (ComClient.EditFltSlt(SltName, SltID) < 1)
                    {
                        Common.ShowMsg("方案名称保存失败！");
                    }
                    //将树节点切换成不可编辑模式
                    TrList_FltSlt.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 初始化下拉列表控件关键属性
        /// </summary>
        /// <param name="LkUpContr">下拉列表控件对象</param>
        public void InitLkUp(Control Contr)
        {
            try
            {
                string[] LkUp_Data;
                foreach (Control Control1 in Contr.Controls)
                {
                    if (Control1.Name == "LkUp_Brackets1")
                    {
                        //LkUp_Data = new string[] { "(", "((", "(((" };
                        List<ExpandoObject> ListEObj = new List<ExpandoObject>();
                        dynamic EObj1 = new ExpandoObject();
                        EObj1.FRemark = "(";
                        EObj1.FColNm = " ( ";
                        ListEObj.Add(EObj1);

                        dynamic EObj2 = new ExpandoObject();
                        EObj2.FRemark = "((";
                        EObj2.FColNm = " (( ";
                        ListEObj.Add(EObj2);

                        dynamic EObj3 = new ExpandoObject();
                        EObj3.FRemark = "(((";
                        EObj3.FColNm = " ((( ";
                        ListEObj.Add(EObj3);

                        ((LookUpEdit)Control1).Properties.DisplayMember = "FRemark";
                        ((LookUpEdit)Control1).Properties.ValueMember = "FColNm";
                        ((LookUpEdit)Control1).Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FRemark"));
                        ((LookUpEdit)Control1).Properties.DataSource = ListEObj;
                        //清空默认值
                        ((LookUpEdit)Control1).Properties.NullText = "";
                        ((LookUpEdit)Control1).Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    }
                    else if (Control1.Name == "LkUp_Brackets2")
                    {
                        //LkUp_Data = new string[] { ")", "))", ")))" };
                        List<ExpandoObject> ListEObj = new List<ExpandoObject>();
                        dynamic EObj1 = new ExpandoObject();
                        EObj1.FRemark = ")";
                        EObj1.FColNm = " ) ";
                        ListEObj.Add(EObj1);

                        dynamic EObj2 = new ExpandoObject();
                        EObj2.FRemark = "))";
                        EObj2.FColNm = " )) ";
                        ListEObj.Add(EObj2);

                        dynamic EObj3 = new ExpandoObject();
                        EObj3.FRemark = ")))";
                        EObj3.FColNm = " ))) ";
                        ListEObj.Add(EObj3);

                        ((LookUpEdit)Control1).Properties.DisplayMember = "FRemark";
                        ((LookUpEdit)Control1).Properties.ValueMember = "FColNm";
                        ((LookUpEdit)Control1).Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FRemark"));
                        ((LookUpEdit)Control1).Properties.DataSource = ListEObj;
                        //清空默认值
                        ((LookUpEdit)Control1).Properties.NullText = "";
                        ((LookUpEdit)Control1).Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    }
                    else if (Control1.Name == "LkUp_Rlt")
                    {
                        List<ExpandoObject> ListEObj = new List<ExpandoObject>();
                        dynamic EObj1 = new ExpandoObject();
                        EObj1.FRemark = "并且";
                        EObj1.FColNm = " and ";
                        ListEObj.Add(EObj1);

                        dynamic EObj2 = new ExpandoObject();
                        EObj2.FRemark = "或者";
                        EObj2.FColNm = " or ";
                        ListEObj.Add(EObj2);

                        //LkUp_Data = new string[] { "并且", "或者" };
                        ((LookUpEdit)Control1).Properties.DisplayMember = "FRemark";
                        ((LookUpEdit)Control1).Properties.ValueMember = "FColNm";
                        ((LookUpEdit)Control1).Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FRemark"));
                        ((LookUpEdit)Control1).Properties.DataSource = ListEObj;
                        //清空默认值
                        ((LookUpEdit)Control1).Properties.NullText = "";
                        ((LookUpEdit)Control1).Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    }
                    else if (Control1.Name == "SLkUp_ColNams")
                    {
                        LkUp_Data = new string[] { "开始日期", "结束日期", "订单号" };
                        ((SearchLookUpEdit)Control1).Properties.DisplayMember = "FRemark";
                        ((SearchLookUpEdit)Control1).Properties.ValueMember = "FColNm";
                        List <ExpandoObject> ListEObj = DataTransformer.LoadData(ComClient.GetFltColsByPanelNam("Panel5", FrmNam));
                        //增加一个可见的绑定列
                        ((SearchLookUpEdit)Control1).Properties.View.Columns.AddVisible("FRemark");
                        ((SearchLookUpEdit)Control1).Properties.DataSource = ListEObj;
                        ((SearchLookUpEdit)Control1).Properties.NullText = "";
                        ((SearchLookUpEdit)Control1).Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    }
                    else if (Control1.Name == "LkUp_Cdt")
                    {
                        List<ExpandoObject> ListEObj = new List<ExpandoObject>();

                        dynamic EObj1 = new ExpandoObject();                       
                        EObj1.FRemark = "以...开头";
                        EObj1.FColNm = " like";
                        ListEObj.Add(EObj1);

                        dynamic EObj2 = new ExpandoObject();
                        EObj2.FRemark = "等于";
                        EObj2.FColNm = " = ";
                        ListEObj.Add(EObj2);

                        dynamic EObj3 = new ExpandoObject();
                        EObj3.FRemark = "包含";
                        EObj3.FColNm = " like ";
                        ListEObj.Add(EObj3);

                        dynamic EObj4 = new ExpandoObject();
                        EObj4.FRemark = "不包含";
                        EObj4.FColNm = " not like ";
                        ListEObj.Add(EObj4);

                        dynamic EObj5 = new ExpandoObject();
                        EObj5.FRemark = "大于";
                        EObj5.FColNm = " > ";
                        ListEObj.Add(EObj5);

                        dynamic EObj6 = new ExpandoObject();
                        EObj6.FRemark = "大于等于";
                        EObj6.FColNm = " >= ";
                        ListEObj.Add(EObj6);

                        dynamic EObj7 = new ExpandoObject();
                        EObj7.FRemark = "小于";
                        EObj7.FColNm = " < ";
                        ListEObj.Add(EObj7);

                        dynamic EObj8 = new ExpandoObject();
                        EObj8.FRemark = "小于等于";
                        EObj8.FColNm = " <= ";
                        ListEObj.Add(EObj8);

                        dynamic EObj9 = new ExpandoObject();
                        EObj9.FRemark = "不等于";
                        EObj9.FColNm = " != ";
                        ListEObj.Add(EObj9);

                        dynamic EObj10 = new ExpandoObject();
                        EObj10.FRemark = "从...到...";
                        EObj10.FColNm = " between ";
                        ListEObj.Add(EObj10);

                        dynamic EObj12 = new ExpandoObject();
                        EObj12.FRemark = "为空";
                        EObj12.FColNm = " = ''";
                        ListEObj.Add(EObj12);

                        dynamic EObj13 = new ExpandoObject();
                        EObj13.FRemark = "不为空";
                        EObj13.FColNm = " != ''";
                        ListEObj.Add(EObj13);

                        dynamic EObj14 = new ExpandoObject();
                        EObj14.FRemark = "不在...之间";
                        EObj14.FColNm = " not between ";
                        ListEObj.Add(EObj14);

                        dynamic EObj15 = new ExpandoObject();
                        EObj15.FRemark = "以...结尾";
                        EObj15.FColNm = "like ";
                        ListEObj.Add(EObj15);

                        dynamic EObj16 = new ExpandoObject();
                        EObj16.FRemark = "在列表中";
                        EObj16.FColNm = " in ";
                        ListEObj.Add(EObj16);

                        dynamic EObj17 = new ExpandoObject();
                        EObj17.FRemark = "不在列表中";
                        EObj17.FColNm = " not in ";
                        ListEObj.Add(EObj17);
                        //LkUp_Data = new string[] { "以...开头", "等于", "包含", "不包含", "大于", "大于等于", "小于", "小于等于", "不等于", "从...到...", "为空", "不为空", "不在...之间", "以...结尾", "在列表中", "不在列表中" };

                        ((LookUpEdit)Control1).Properties.DisplayMember = "FRemark";
                        ((LookUpEdit)Control1).Properties.ValueMember = "FColNm";
                        ((LookUpEdit)Control1).Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FRemark"));
                       ((LookUpEdit)Control1).Properties.DataSource = ListEObj;

                        //清空默认值
                        ((LookUpEdit)Control1).Properties.NullText = "";
                        ((LookUpEdit)Control1).Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    }
                    else if (Control1.Name == "CBox_CdtValue1" || Control1.Name == "CBox_CdtValue2" || Control1.Name == "CBox_CdtValue3")
                    {
                        ((ComboBoxEdit)Control1).Properties.NullText = "";
                        ((ComboBoxEdit)Control1).Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    }
                    else if (Control1.GetType().ToString() == "DevExpress.XtraEditors.DateEdit")
                    {
                        //清空默认值
                        ((DateEdit)Control1).Properties.NullText = "";
                        ((DateEdit)Control1).Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    }
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 覆盖方案事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //获取面板中最后一个用户控件
              int ContrNum= Pal_FlCtner.Controls.Count;
              UsrContr_Filter Filter = (UsrContr_Filter)Pal_FlCtner.Controls[ContrNum - 1];

                if (((SearchLookUpEdit)Filter.Controls["SLkUp_ColNams"]).EditValue !=null)
                {
                    if(BracketsMatch(GetFltSltDtl()))
                    {
                    DialogResult Result = XtraMessageBox.Show("您是否需要覆盖本过滤方案！", "提醒", MessageBoxButtons.OKCancel);
                    if (Result == DialogResult.OK)
                    {
                        //EditFltSlt();
                        if (SltEditNode != null)
                        {
                            List<DTSerializer<string, object>> List_DTObj = GetFltSltDtl();
                            foreach (DTSerializer<string, object> DTObj2 in List_DTObj)
                            {
                                DTObj2.Add("FSltID", SltEditNode.GetValue("FSltID").ToString());
                                DTObj2.Add("FSltType", "0");
                            }
                            //开始事务
                            ComClient.BeginTransaction();
                            ComClient.SaveFltSlt1(OperType.Delete, Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), "t_ADMM_FltSltforDtl", " FSltID='" + SltEditNode.GetValue("FSltID").ToString() + "'");
                            ComClient.SaveFltSlt1(OperType.AddSave, Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), "t_ADMM_FltSltforDtl", "");
                            //提交事务
                            ComClient.CommitTransaction();
                            //将树节点切换成不可编辑模式
                            TrList_FltSlt.OptionsBehavior.Editable = false;
                        }
                    }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Common.ShowMsg("字段名称不能为空！");
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 查询用户控件中所有显示/非显示的子控件
        /// </summary>
        /// <param name="UsrContr">被查询的用户控件</param>
        /// <param name="IfVisible">是否显示</param>
        /// <returns></returns>
        private List<Control> GetVibContr(Control UsrContr,bool IfVisible)
        {
            try
            {
                UsrContr_Filter usrContr_Filter = (UsrContr_Filter)UsrContr;
                //查询出所有显示的控件
                var Qurey = from Control control in usrContr_Filter.Controls.Cast<Control>()
                            where control.Visible == IfVisible
                            select control;
               return Qurey.ToList<Control>();
            }
            catch (Exception Ex)
            {

             Common.ShowMsg(Ex.Message);
            }
            return new List<Control>();
        }

        /// <summary>
        /// 获取过滤方案明细
        /// </summary>
        /// <returns></returns>
        private List<DTSerializer<string, object>> GetFltSltDtl()
        {
            try
            {
                List<DTSerializer<string, object>> List_DTObj = new List<DTSerializer<string, object>>();
                //遍历用户控件中每个控件从而获取控件值
                foreach (Control UsrContr in Pal_FlCtner.Controls)
                {
                    //查询出所有显示的控件
                   List<Control> List_Contr1= GetVibContr(UsrContr, true);
                   DTSerializer<string, object> DTObj = new DTSerializer<string, object>();
                   DTObj.Add("FFilterID", UsrContr.Tag.ToString());
                    //遍历所有显示的控件
                    foreach (Control control in List_Contr1)
                    {
                                         
                        if (control.Name == "LkUp_Brackets1")
                        {
                            DTObj.Add("FBrackets1Value", ((LookUpEdit)control).EditValue==null ? "": ((LookUpEdit)control).Text);
                        }
                        else if (control.Name == "SLkUp_ColNams")
                        {
                            //dynamic EObj= (ExpandoObject)(((SearchLookUpEdit)control).EditValue);    
                            string DisplayText = ((SearchLookUpEdit)control).Text;
                            DTObj.Add("FColNamsValue", ((SearchLookUpEdit)control).EditValue==null ? "": DisplayText);
                        }
                        else if (control.Name == "LkUp_Cdt")
                        {
                            DTObj.Add("FCdt", ((LookUpEdit)control).EditValue==null ? "": ((LookUpEdit)control).Text);
                        }
                        else if (control.Name == "CBox_CdtValue1" || control.Name == "DEd_CdtValue5")
                        {
                            if(control.Name == "CBox_CdtValue1")
                            {
                                DTObj.Add("FCBoxCdtValue1", ((ComboBoxEdit)control).EditValue==null ? "": ((ComboBoxEdit)control).EditValue.ToString());
                            }
                            else
                            {
                                //((DateTime)(((DateEdit)control).EditValue)).ToString("yyyy-MM-dd")
                                DTObj.Add("FDedCdtValue5", ((DateEdit)control).EditValue==null ? "" : ((DateEdit)control).EditValue.ToString().Substring(0, 10));
                            }
                        }
                        else if (control.Name == "CBox_CdtValue2" || control.Name == "DEd_CdtValue6")
                        {
                            if (control.Name == "CBox_CdtValue2")
                            {
                                DTObj.Add("FCBoxCdtValue2", ((ComboBoxEdit)control).EditValue==null ? "" : ((ComboBoxEdit)control).EditValue.ToString());
                            }
                            else
                            {
                                DTObj.Add("FDedCdtValue6", ((DateEdit)control).EditValue==null ? "" : ((DateEdit)control).EditValue.ToString().Substring(0, 10));
                            }
                            
                        }
                        else if (control.Name == "CBox_CdtValue3" || control.Name == "DEd_CdtValue3")
                        {
                            if (control.Name == "CBox_CdtValue3")
                            {
                                DTObj.Add("FCBoxCdtValue3", ((ComboBoxEdit)control).EditValue==null ? "": ((ComboBoxEdit)control).EditValue.ToString());
                            }
                            else
                            {
                                //string Date = ((DateEdit)control).EditValue.ToString().Substring(0,10);
                                DTObj.Add("FDedCdtValue3", ((DateEdit)control).EditValue==null ? "": ((DateEdit)control).EditValue.ToString().Substring(0, 10));
                            }
                        }
                        else if (control.Name == "LkUp_Brackets2")
                        {
                                DTObj.Add("FBrackets2", ((LookUpEdit)control).EditValue==null ? "": ((LookUpEdit)control).Text);
                        }
                        else if(control.Name == "LkUp_Rlt")
                        {
                                DTObj.Add("FRlt", ((LookUpEdit)control).EditValue==null ? "": ((LookUpEdit)control).Text);
                        }
                    }

                    if (!DTObj.Keys.Contains<string>("FCBoxCdtValue1"))
                    {
                        DTObj.Add("FCBoxCdtValue1", "null");
                    }

                    if (!DTObj.Keys.Contains<string>("FCBoxCdtValue2"))
                    {
                        DTObj.Add("FCBoxCdtValue2", "null");
                    }

                    if (!DTObj.Keys.Contains<string>("FCBoxCdtValue3"))
                    {
                        DTObj.Add("FCBoxCdtValue3", "null");
                    }

                    if (!DTObj.Keys.Contains<string>("FDedCdtValue3"))
                    {
                        DTObj.Add("FDedCdtValue3", "null");
                    }

                    if (!DTObj.Keys.Contains<string>("FDedCdtValue5"))
                    {
                        DTObj.Add("FDedCdtValue5", "null");
                    }

                    if (!DTObj.Keys.Contains<string>("FDedCdtValue6"))
                    {
                        DTObj.Add("FDedCdtValue6", "null");
                    }

                    List_DTObj.Add(DTObj);
                }
                return List_DTObj;
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return new List<DTSerializer<string, object>>();
        }

        /// <summary>
        /// 获取过滤方案明细
        /// </summary>
        /// <returns></returns>
        private string GetFltSltDtlSQL()
        {
            try
            {
                List<DTSerializer<string, object>> List_DTObj = new List<DTSerializer<string, object>>();
                StringBuilder SQL = new StringBuilder ();
                int ContrNum = Pal_FlCtner.Controls.Count;
                int i = 0;
                string CdtName = "";
                    //遍历用户控件中每个控件从而获取控件值
                    foreach (Control UsrContr in Pal_FlCtner.Controls)
                    {
                        //查询出所有显示的控件
                        List<Control> List_Contr1 = GetVibContr(UsrContr, true);
                        string[] Names = { "LkUp_Brackets1", "SLkUp_ColNams", "LkUp_Cdt", "CBox_CdtValue1", "DEd_CdtValue5", "CBox_CdtValue2", "DEd_CdtValue6", "CBox_CdtValue3", "DEd_CdtValue3", "LkUp_Brackets2", "LkUp_Rlt" };
                        foreach (string Name in Names)
                        {
                            var Qurey = from Control Contr1 in List_Contr1
                                        where Contr1.Name == Name
                                        select Contr1;

                            if (Qurey.Count<Control>() != 0)
                            {
                                Control Contr2 = Qurey.First<Control>();
                                if (Contr2.Name == "LkUp_Brackets1")
                                {
                                    SQL.Append(((LookUpEdit)Contr2).EditValue == null ? "" : ((LookUpEdit)Contr2).EditValue.ToString());
                                }
                                else if (Contr2.Name == "SLkUp_ColNams")
                                {
                                    SQL.Append(((SearchLookUpEdit)Contr2).Properties.ValueMember == null ? "" : ((SearchLookUpEdit)Contr2).EditValue.ToString());
                                }
                                else if (Contr2.Name == "LkUp_Cdt")
                                {
                                if(((LookUpEdit)Contr2).EditValue !=null)
                                {
                                    CdtName = ((LookUpEdit)Contr2).EditValue.ToString();
                                }
                                    
                                    SQL.Append(((LookUpEdit)Contr2).Properties.ValueMember == null ? "" : " "+((LookUpEdit)Contr2).EditValue.ToString());
                                }
                                else if (Contr2.Name == "CBox_CdtValue1" || Contr2.Name == "DEd_CdtValue5")
                                {
                                    if (Contr2.Name == "CBox_CdtValue1")
                                    {
                                        SQL.Append(((ComboBoxEdit)Contr2).EditValue == null ? "" : ("'" + ((ComboBoxEdit)Contr2).EditValue.ToString()) + "'");
                                    }
                                    else
                                    {
                                        SQL.Append(((DateEdit)Contr2).EditValue == null ? "" : ("'" + ((DateEdit)Contr2).EditValue.ToString()) + "'");
                                    }
                                }
                                else if (Contr2.Name == "CBox_CdtValue2" || Contr2.Name == "DEd_CdtValue6")
                                {
                                    if (Contr2.Name == "CBox_CdtValue2")
                                    {
                                        SQL.Append(((ComboBoxEdit)Contr2).EditValue == null ? "" : ("'" + " and " + ((ComboBoxEdit)Contr2).EditValue.ToString()) + "'");
                                    }
                                    else
                                    {
                                        SQL.Append(((DateEdit)Contr2).EditValue == null ? "" : (" and " + "'" + ((DateEdit)Contr2).EditValue.ToString()) + "'");
                                    }

                                }
                                else if (Contr2.Name == "CBox_CdtValue3" || Contr2.Name == "DEd_CdtValue3")
                                {
                                    if (Contr2.Name == "CBox_CdtValue3")
                                {
                                    //条件等于“包含”或者“不包含”
                                    if (CdtName == " not like " || CdtName == " like ")
                                    {
                                        SQL.Append(((ComboBoxEdit)Contr2).EditValue == null ? "" : ("'%" + ((ComboBoxEdit)Contr2).EditValue.ToString()) + "%'");
                                    }
                                    //条件等于“以...开头”
                                    else if (CdtName==" like")
                                    {
                                        SQL.Append(((ComboBoxEdit)Contr2).EditValue == null ? "" : (" '" + ((ComboBoxEdit)Contr2).EditValue.ToString()) + "%'");
                                    }
                                    //条件等于“以...结尾”
                                    else if (CdtName=="like ")
                                    {
                                        SQL.Append(((ComboBoxEdit)Contr2).EditValue == null ? "" : (" '%" + ((ComboBoxEdit)Contr2).EditValue.ToString()) + "'");
                                    }
                                    //条件等于"为空"或者"不为空"
                                    else if (CdtName == " = ''" || CdtName == " != ''")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        SQL.Append(((ComboBoxEdit)Contr2).EditValue == null ? "" : "'"+((ComboBoxEdit)Contr2).EditValue.ToString()+"'");
                                    }
                                }
                                    else
                                    {
                                        SQL.Append(((DateEdit)Contr2).EditValue == null ? "" : ("'" + ((DateEdit)Contr2).EditValue.ToString()) + "'");
                                    }
                                }
                                else if (Contr2.Name == "LkUp_Brackets2")
                                {
                                    SQL.Append(((LookUpEdit)Contr2).EditValue == null ? "" : ((LookUpEdit)Contr2).EditValue.ToString());
                                }
                                else if (Contr2.Name == "LkUp_Rlt")
                                {
                                i = i + 1;
                                //如果过滤控件
                                if (ContrNum != i )
                                {
                                    SQL.Append(((LookUpEdit)Contr2).EditValue == null ? "" : (((LookUpEdit)Contr2).EditValue.ToString()) + " ");
                                }
                                }
                            }
                        }
                    }
                return SQL.ToString();
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception Ex)
            {
             Common.ShowMsg(Ex.Message);
            }
        }

        //确定过滤
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
               if(BracketsMatch(GetFltSltDtl()))
                {
                    FltSQL = GetFltSltDtlSQL();                 
                   this.Close();
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
    }
}
