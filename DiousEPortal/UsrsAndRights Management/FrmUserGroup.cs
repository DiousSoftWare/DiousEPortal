using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPortalModel;
using EPortalCommom;
using System.Dynamic;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using System.Reflection;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace DiousEPortal
{
    //用户组窗体继承自通用窗体
    public partial class FrmUserGroup : FrmCommon
    {
        

        ///////////////////////////////定义全局变量/////////////////////////////////
        Dictionary<string, object> DTObj = new Dictionary<string, object>();
        ///////////////////////////////////////////////////////////////////////////

        public EPortalService.UsrGroupServiceClient UsrGroupClient { get; set; }
       

        public FrmUserGroup()
        {
            InitializeComponent();

            //初始化WCF客户端
            UsrGroupClient = new EPortalService.UsrGroupServiceClient();
        }

        /// <summary>
        /// 窗体加载事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmUserGroup_Load(object sender, EventArgs e)
        {
            try
            {
                //初始化主键控件名称
                KeyContrNam =new string[1] { "Txt_Grpcode" };
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        
        private void Split_Detail_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Tab_Common1_Click(object sender, EventArgs e)
        {

        }

       

        /// <summary>
        /// 保存用户组资料数据
        /// </summary>
        /// <returns>1:保存成功;0:保存失败</returns>
        public override int SaveData()
        {
            string TabNam = "t_ADMM_GrpMst";
            string PriKey = "fGrpCode";
            FocColNam = "分组代号";
            if(BtnName==BtnNam.Add || BtnName==BtnNam.Copy)
            {
                FocColVal = Txt_Grpcode.Text;
            }
            else
            {
                //初始化FocColVal,方便后续新增数据定位行用
                FocColVal = (string)((IDictionary<string, object>)((ExpandoObject)View_Common2.GetFocusedRow()))[FocColNam];
            }
           
            //初始化主键控件名称
            string ContrNam = KeyContrNam[0];
            try
            {
                string SQL = "";
                List<DTSerializer<string, object>> List_DTObj = new List<DTSerializer<string, object>>();
                List<DTSerializer<string, object>> List_DTObj1 = new List<DTSerializer<string, object>>();
                List<ExpandoObject> List_EObj;
                //开始SQL事务
                ComClient.BeginTransaction();
                //只有不重复才往下执行
                if (BtnName == BtnNam.Add && ComClient.RepeatCheck(TabNam, PriKey, Txt_Grpcode.Text) == 0)
                {
                    //1.查询哪些表需要更新(输出表名、面板名、窗体实例名)
                    List<ExpandoObject> List_UpTab = LoadData(ComClient.GetTabByFrmNam(FrmNam));

                    foreach (dynamic UpTab in List_UpTab)
                    {
                        //2.根据表名和窗体实例名查询出更新表需要的关键信息
                        List_EObj = LoadData(ComClient.GetTabStrc(UpTab.FTblNm, FrmNam, OperType.AddSave, "", UpTab.FPanelNam,"",""));
                        //获取执行SQL
                        foreach (ExpandoObject EObj in List_EObj)
                        {
                            var IDObj = (IDictionary<string, object>)EObj;
                            SQL = (string)IDObj["SQL"];
                        }

                        List<List<ExpandoObject>> List_PanelObjs = new List<List<ExpandoObject>>();
                        //根据面板名称读取该面板内所有相关字段信息
                        List<ExpandoObject> List_Panel4EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel4", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel4EObj);
                        List<ExpandoObject> List_Panel3EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel3", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel3EObj);
                        List<ExpandoObject> List_Panel7EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel7", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel7EObj);
                        List<ExpandoObject> List_Panel8EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel8", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel8EObj);
                        List<ExpandoObject> List_Panel9EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel9", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel9EObj);
                        List<ExpandoObject> List_Panel10EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel10", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel10EObj);
                        List<ExpandoObject> List_Panel11EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel11", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel11EObj);
                        List<ExpandoObject> List_Panel12EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel12", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel12EObj);
                        //用于存储该表在所有面板内的字段值
                        DTSerializer<string, object> DTObj = new DTSerializer<string, object>();
                        foreach (List<ExpandoObject> List_PanelObj in List_PanelObjs)
                        {
                            if (List_PanelObj.Count != 0)
                            {
                                var Query = from Obj in List_PanelObj
                                            where EObjToIDObj(Obj)["FPanelNam"].ToString() == "Panel3"
                                            select Obj;
                                //Panel3面板中是网格控件,则按照以下逻辑单独处理
                                if (Query.Count<ExpandoObject>() > 0)
                                {
                                    //遍历View_Common1数据源获取每个字段的值
                                    foreach (ExpandoObject EObj3 in (BindingList<ExpandoObject>)View_Common1.DataSource)
                                    {
                                        IDictionary<string, object> IDObj = (IDictionary<string, object>)EObj3;
                                        DTSerializer<string, object> DTObj1 = new DTSerializer<string, object>();
                                        foreach (string Key in IDObj.Keys)
                                        {
                                            //利用LinQ将中文字段名转换成英文字段名
                                            var Query1 = from Obj in List_PanelObj
                                                         where EObjToIDObj(Obj)["FRemark"].ToString() == Key
                                                         select EObjToIDObj(Obj)["FColNm"].ToString();
                                            string Key1 = "";
                                            foreach (var key in Query1)
                                            {
                                                Key1 = key.ToString();
                                            }
                                            if (Key1 != "")
                                            {
                                                if (Key1 == PriKey)
                                                {
                                                    DTObj1.Add(Key1, Panel4.Controls[ContrNam].Text);
                                                }
                                                else
                                                {
                                                    DTObj1.Add(Key1, IDObj[Key].ToString());
                                                }
                                            }
                                        }
                                        //3.循环完Panel3面板的每一行字段后，生成更新对象列表
                                        List_DTObj1.Add(DTObj1);
                                    }
                                    //4.服务端根据接收到的对象列表更新数据库
                                    //ComClient.SaveData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj1), SQL, OperType.AddSave);
                                    ComClient.SaveData1(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj1), SQL);
                                }
                                //非Panel3面板，则按照以下逻辑单独处理
                                else
                                {
                                    //根据面板内的字段信息自动生成更新对象列表
                                    foreach (dynamic EObj in List_PanelObj)
                                    {
                                        //如果该字段绑定了指定控件,则按照控件指定值更新
                                        if (EObj.FContrNam != "")
                                        {
                                            if (EObj.FColNm == "fCFlag")
                                            {
                                                DTObj.Add(EObj.FColNm, Panel4.Controls[EObj.FContrNam].Text == "已审核" ? 1 : 0);
                                            }
                                            else if (EObj.FColNm == "fIfUse")
                                            {
                                                DTObj.Add(EObj.FColNm, Panel4.Controls[EObj.FContrNam].Text == "已启用" ? 1 : 0);
                                            }
                                            else
                                            {
                                                if (EObj.FPanelNam == "Panel4")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel4.Controls[EObj.FContrNam].Text);
                                                }

                                                else if (EObj.FPanelNam == "Panel7")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel7.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel8")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel8.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel9")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel9.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel10")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel10.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel11")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel11.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel12")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel12.Controls[EObj.FContrNam].Text);
                                                }
                                            }

                                        }
                                        //如果该字段没有绑定任何控件，则按照默认值更新
                                        else
                                        {
                                            //如果该字段的默认数据类型是DateTime,则默认值是当天
                                            if (EObj.FColNm == "fCDate")
                                            {
                                                DTObj.Add(EObj.FColNm, DateTime.Now.ToString());
                                            }
                                            else if (EObj.FColNm == "fCreatorID")
                                            {
                                                DTObj.Add(EObj.FColNm, CurUsrID);
                                            }
                                            else if (EObj.FColNm == "fCreator")
                                            {
                                                DTObj.Add(EObj.FColNm, CurUsrName);
                                            }
                                            else
                                            {
                                                DTObj.Add(EObj.FColNm, EObj.FDefValue);
                                            }
                                        }
                                    }
                                    //3.循环完所有面板的字段后，生成更新对象列表
                                    List_DTObj.Add(DTObj);
                                    //4.服务端根据接收到的对象列表更新数据库
                                    //ComClient.SaveData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), SQL, OperType.AddSave);
                                    ComClient.SaveData1(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), SQL);
                                }
                            }
                        }
                    }
                    //提交事务
                    ComClient.CommitTransaction();
                    BtnName = BtnNam.Null;
                    //重新刷新网格数据
                    Grip_Common2.DataSource = LoadData(UsrGroupClient.GetUsrGrpsToChCol("#Tmp_" + CurUsrID + "_" + DateTime.Now.Second.ToString(), "0",""));                   
                    return 1;
                }
                else if (BtnName == BtnNam.Copy && ComClient.RepeatCheck(TabNam, PriKey, Txt_Grpcode.Text) == 0)
                {
                    //1.查询哪些表需要更新(输出表名、面板名、窗体实例名)
                    List<ExpandoObject> List_UpTab = LoadData(ComClient.GetTabByFrmNam(FrmNam));

                    foreach (dynamic UpTab in List_UpTab)
                    {
                        //2.根据表名和窗体实例名查询出更新表需要的关键信息
                        List_EObj = LoadData(ComClient.GetTabStrc(UpTab.FTblNm, FrmNam, OperType.AddSave, "", UpTab.FPanelNam,"",""));
                        //获取执行SQL
                        foreach (ExpandoObject EObj in List_EObj)
                        {
                            var IDObj = (IDictionary<string, object>)EObj;
                            SQL = (string)IDObj["SQL"];
                        }

                        List<List<ExpandoObject>> List_PanelObjs = new List<List<ExpandoObject>>();
                        //根据面板名称读取该面板内所有相关字段信息
                        List<ExpandoObject> List_Panel4EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel4", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel4EObj);
                        List<ExpandoObject> List_Panel3EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel3", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel3EObj);
                        List<ExpandoObject> List_Panel7EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel7", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel7EObj);
                        List<ExpandoObject> List_Panel8EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel8", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel8EObj);
                        List<ExpandoObject> List_Panel9EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel9", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel9EObj);
                        List<ExpandoObject> List_Panel10EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel10", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel10EObj);
                        List<ExpandoObject> List_Panel11EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel11", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel11EObj);
                        List<ExpandoObject> List_Panel12EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel12", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel12EObj);
                        //用于存储该表在所有面板内的字段值
                        DTSerializer<string, object> DTObj = new DTSerializer<string, object>();
                        foreach (List<ExpandoObject> List_PanelObj in List_PanelObjs)
                        {
                            if (List_PanelObj.Count != 0)
                            {
                                var Query = from Obj in List_PanelObj
                                            where EObjToIDObj(Obj)["FPanelNam"].ToString() == "Panel3"
                                            select Obj;
                                //Panel3面板中是网格控件,则按照以下逻辑单独处理
                                if (Query.Count<ExpandoObject>() > 0)
                                {
                                    //遍历View_Common1数据源获取每个字段的值
                                    foreach (ExpandoObject EObj3 in (BindingList<ExpandoObject>)View_Common1.DataSource)
                                    {
                                        IDictionary<string, object> IDObj = (IDictionary<string, object>)EObj3;
                                        DTSerializer<string, object> DTObj1 = new DTSerializer<string, object>();
                                        foreach (string Key in IDObj.Keys)
                                        {
                                            //利用LinQ将中文字段名转换成英文字段名
                                            var Query1 = from Obj in List_PanelObj
                                                         where EObjToIDObj(Obj)["FRemark"].ToString() == Key
                                                         select EObjToIDObj(Obj)["FColNm"].ToString();
                                            string Key1 = "";
                                            foreach (var key in Query1)
                                            {
                                                Key1 = key.ToString();
                                            }
                                            if (Key1 != "")
                                            {
                                                if (Key1 == PriKey)
                                                {
                                                    DTObj1.Add(Key1, Panel4.Controls[ContrNam].Text);
                                                }
                                                else
                                                {
                                                    DTObj1.Add(Key1, IDObj[Key].ToString());
                                                }
                                            }
                                        }
                                        //3.循环完Panel3面板的每一行字段后，生成更新对象列表
                                        List_DTObj1.Add(DTObj1);
                                    }
                                    //4.服务端根据接收到的对象列表更新数据库
                                    //ComClient.SaveData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj1), SQL, OperType.AddSave);
                                    ComClient.SaveData1(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj1), SQL);
                                }
                                //非Panel3面板，则按照以下逻辑单独处理
                                else
                                {
                                    //根据面板内的字段信息自动生成更新对象列表
                                    foreach (dynamic EObj in List_PanelObj)
                                    {
                                        //如果该字段绑定了指定控件,则按照控件指定值更新
                                        if (EObj.FContrNam != "")
                                        {
                                            if (EObj.FColNm == "fCFlag")
                                            {
                                                DTObj.Add(EObj.FColNm, Panel4.Controls[EObj.FContrNam].Text == "已审核" ? 1 : 0);
                                            }
                                            else if (EObj.FColNm == "fIfUse")
                                            {
                                                DTObj.Add(EObj.FColNm, Panel4.Controls[EObj.FContrNam].Text == "已启用" ? 1 : 0);
                                            }
                                            else
                                            {
                                                if (EObj.FPanelNam == "Panel4")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel4.Controls[EObj.FContrNam].Text);
                                                }

                                                else if (EObj.FPanelNam == "Panel7")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel7.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel8")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel8.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel9")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel9.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel10")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel10.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel11")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel11.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel12")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel12.Controls[EObj.FContrNam].Text);
                                                }
                                            }

                                        }
                                        //如果该字段没有绑定任何控件，则按照默认值更新
                                        else
                                        {
                                            //如果该字段的默认数据类型是DateTime,则默认值是当天
                                            if (EObj.FColNm == "fCDate")
                                            {
                                                DTObj.Add(EObj.FColNm, DateTime.Now.ToString());
                                            }
                                            //if (EObj.FColNm == "fAppDate")
                                            //{
                                            //    DTObj.Add(EObj.FColNm, DateTime.Now.ToString());
                                            //}
                                            else if (EObj.FColNm == "fCreatorID")
                                            {
                                                DTObj.Add(EObj.FColNm, CurUsrID);
                                            }
                                            else if (EObj.FColNm == "fCreator")
                                            {
                                                DTObj.Add(EObj.FColNm, CurUsrName);
                                            }
                                            else
                                            {
                                                DTObj.Add(EObj.FColNm, EObj.FDefValue);
                                            }
                                        }
                                    }
                                    //3.循环完所有面板的字段后，生成更新对象列表
                                    List_DTObj.Add(DTObj);
                                    //4.服务端根据接收到的对象列表更新数据库
                                    //ComClient.SaveData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), SQL, OperType.AddSave);
                                    ComClient.SaveData1(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), SQL);
                                    //提交事务
                                    ComClient.CommitTransaction();
                                }
                            }
                        }
                        BtnName = BtnNam.Null;
                        //重新刷新网格数据
                        Grip_Common2.DataSource = LoadData(UsrGroupClient.GetUsrGrpsToChCol("#Tmp_" + CurUsrID + "_" + DateTime.Now.Second.ToString(), "0",""));
                    }
                    return 1;
                }
                else if (BtnName == BtnNam.Delete)
                {
                    //1.查询哪些表需要更新(输出表名、面板名、窗体实例名)
                    List<ExpandoObject> List_UpTab = LoadData(ComClient.GetTabByFrmNam(FrmNam));
                    //获取当前选择行的数据
                    IDictionary<string, object> IDObj = (IDictionary<string, object>)View_Common2.GetFocusedRow();
                    foreach (dynamic UpTab in List_UpTab)
                    {
                        List_EObj = LoadData(ComClient.GetTabStrc(UpTab.FTblNm, FrmNam, OperType.Delete, PriKey + " ='" + IDObj[ComClient.GetChColByEnCol(TabNam, PriKey)].ToString() + "'", UpTab.FPanelNam,"",""));
                        //获取执行SQL
                        foreach (ExpandoObject EObj in List_EObj)
                        {
                            var IDObj1 = (IDictionary<string, object>)EObj;
                            SQL = (string)IDObj1["SQL"];
                        }

                        //ComClient.SaveData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), SQL, OperType.Delete);
                        ComClient.SaveData1("", SQL);
                    }
                    //提交事务
                    ComClient.CommitTransaction();

                    BtnName = BtnNam.Null;
                    //重新刷新网格数据
                    Grip_Common2.DataSource = LoadData(UsrGroupClient.GetUsrGrpsToChCol("#Tmp_" + CurUsrID + "_" + DateTime.Now.Second.ToString(), "0",""));
                    return 1;
                }
                else if (BtnName == BtnNam.Edit)
                {
                    //1.查询哪些表需要更新(输出表名、面板名、窗体实例名)
                    List<ExpandoObject> List_UpTab = LoadData(ComClient.GetTabByFrmNam(FrmNam));
                    //获取当前选择行的数据
                    IDictionary<string, object> IDObj1 = (IDictionary<string, object>)View_Common2.GetFocusedRow();
                    foreach (dynamic UpTab in List_UpTab)
                    {
                        //判断更新表归属于哪个面板
                        if (UpTab.FPanelNam != "Panel3")
                        {
                            //2.根据表名和窗体实例名查询出更新表需要的关键信息
                            List_EObj = LoadData(ComClient.GetTabStrc(UpTab.FTblNm, FrmNam, OperType.EditSave, PriKey + " ='" + IDObj1[ComClient.GetChColByEnCol(TabNam, PriKey)].ToString() + "'", UpTab.FPanelNam,"",""));
                            //获取执行SQL
                            foreach (ExpandoObject EObj in List_EObj)
                            {
                                var IDObj = (IDictionary<string, object>)EObj;
                                SQL = (string)IDObj["SQL"];
                            }
                        }
                        else
                        {
                            //2.根据表名和窗体实例名查询出更新表需要的关键信息
                            List_EObj = LoadData(ComClient.GetTabStrc(UpTab.FTblNm, FrmNam, OperType.Delete, PriKey + " ='" + IDObj1[ComClient.GetChColByEnCol(TabNam, PriKey)].ToString() + "'", UpTab.FPanelNam,"",""));
                            //获取执行SQL
                            foreach (ExpandoObject EObj in List_EObj)
                            {
                                var IDObj = (IDictionary<string, object>)EObj;
                                SQL = (string)IDObj["SQL"];
                            }
                            //先执行删除数据操作
                            //ComClient.SaveData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj1), SQL, OperType.Delete);
                            ComClient.SaveData1("", SQL);
                            //2.根据表名和窗体实例名查询出更新表需要的关键信息
                            List_EObj = LoadData(ComClient.GetTabStrc(UpTab.FTblNm, FrmNam, OperType.AddSave, "", UpTab.FPanelNam,"",""));
                            //获取执行SQL
                            foreach (ExpandoObject EObj in List_EObj)
                            {
                                var IDObj = (IDictionary<string, object>)EObj;
                                SQL = (string)IDObj["SQL"];
                            }
                        }

                        List<List<ExpandoObject>> List_PanelObjs = new List<List<ExpandoObject>>();
                        //根据面板名称读取该面板内所有相关字段信息
                        List<ExpandoObject> List_Panel4EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel4", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel4EObj);
                        List<ExpandoObject> List_Panel3EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel3", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel3EObj);
                        List<ExpandoObject> List_Panel7EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel7", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel7EObj);
                        List<ExpandoObject> List_Panel8EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel8", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel8EObj);
                        List<ExpandoObject> List_Panel9EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel9", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel9EObj);
                        List<ExpandoObject> List_Panel10EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel10", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel10EObj);
                        List<ExpandoObject> List_Panel11EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel11", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel11EObj);
                        List<ExpandoObject> List_Panel12EObj = Serializer.DeserializeXMLToEObject(ComClient.GetColsByPanelNam("Panel12", FrmNam, UpTab.FTblNm));
                        List_PanelObjs.Add(List_Panel12EObj);
                        //用于存储该表在所有面板内的字段值
                        DTSerializer<string, object> DTObj = new DTSerializer<string, object>();
                        foreach (List<ExpandoObject> List_PanelObj in List_PanelObjs)
                        {
                            if (List_PanelObj.Count != 0)
                            {
                                var Query = from Obj in List_PanelObj
                                            where EObjToIDObj(Obj)["FPanelNam"].ToString() == "Panel3"
                                            select Obj;
                                //Panel3面板中是网格控件,则按照以下逻辑单独处理
                                if (Query.Count<ExpandoObject>() > 0)
                                {
                                    //遍历View_Common1数据源获取每个字段的值
                                    foreach (ExpandoObject EObj3 in (BindingList<ExpandoObject>)View_Common1.DataSource)
                                    {
                                        IDictionary<string, object> IDObj = (IDictionary<string, object>)EObj3;
                                        DTSerializer<string, object> DTObj1 = new DTSerializer<string, object>();
                                        foreach (string Key in IDObj.Keys)
                                        {
                                            //利用LinQ将中文字段名转换成英文字段名
                                            var Query1 = from Obj in List_PanelObj
                                                         where EObjToIDObj(Obj)["FRemark"].ToString() == Key
                                                         select EObjToIDObj(Obj)["FColNm"].ToString();
                                            string Key1 = "";
                                            foreach (var key in Query1)
                                            {
                                                Key1 = key.ToString();
                                            }
                                            if (Key1 != "")
                                            {
                                                if (Key1 == PriKey)
                                                {
                                                    DTObj1.Add(Key1, Panel4.Controls[ContrNam].Text);
                                                }
                                                else
                                                {
                                                    DTObj1.Add(Key1, IDObj[Key].ToString());
                                                }
                                            }
                                        }
                                        //3.循环完Panel3面板的每一行字段后，生成更新对象列表
                                        List_DTObj1.Add(DTObj1);
                                    }
                                    //4.服务端根据接收到的对象列表更新数据库
                                    //ComClient.SaveData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj1), SQL, OperType.AddSave);
                                    ComClient.SaveData1(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj1), SQL);
                                }
                                //非Panel3面板，则按照以下逻辑单独处理
                                else
                                {
                                    //根据面板内的字段信息自动生成更新对象列表
                                    foreach (dynamic EObj in List_PanelObj)
                                    {
                                        //如果该字段绑定了指定控件,则按照控件指定值更新
                                        if (EObj.FContrNam != "")
                                        {
                                            if (EObj.FColNm == "fCFlag")
                                            {
                                                DTObj.Add(EObj.FColNm, Panel4.Controls[EObj.FContrNam].Text == "已审核" ? 1 : 0);
                                            }
                                            else if (EObj.FColNm == "fIfUse")
                                            {
                                                DTObj.Add(EObj.FColNm, Panel4.Controls[EObj.FContrNam].Text == "已启用" ? 1 : 0);
                                            }
                                            else
                                            {
                                                if (EObj.FPanelNam == "Panel4")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel4.Controls[EObj.FContrNam].Text);
                                                }

                                                else if (EObj.FPanelNam == "Panel7")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel7.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel8")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel8.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel9")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel9.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel10")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel10.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel11")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel11.Controls[EObj.FContrNam].Text);
                                                }
                                                else if (EObj.FPanelNam == "Panel12")
                                                {
                                                    DTObj.Add(EObj.FColNm, Panel12.Controls[EObj.FContrNam].Text);
                                                }
                                            }

                                        }
                                        //如果该字段没有绑定任何控件，则按照默认值更新
                                        else
                                        {
                                            //如果该字段的默认数据类型是DateTime,则默认值是当天
                                            if (EObj.FColNm == "fCDate")
                                            {
                                                DTObj.Add(EObj.FColNm, IDObj1["建立日期"].ToString());
                                            }
                                            else if (EObj.FColNm == "fModiDate")
                                            {
                                                DTObj.Add(EObj.FColNm, DateTime.Now.ToString());
                                            }
                                            else if (EObj.FColNm == "fModifier")
                                            {
                                                DTObj.Add(EObj.FColNm, CurUsrName);
                                            }
                                            else if (EObj.FColNm == "fModifierID")
                                            {
                                                DTObj.Add(EObj.FColNm, CurUsrID);
                                            }
                                            else if (EObj.FColNm == "fCreatorID")
                                            {
                                                DTObj.Add(EObj.FColNm, IDObj1["建立人代号"].ToString());
                                            }
                                            else if (EObj.FColNm == "fCreator")
                                            {
                                                DTObj.Add(EObj.FColNm, IDObj1["建立人姓名"].ToString());
                                            }
                                            else
                                            {
                                                DTObj.Add(EObj.FColNm, EObj.FDefValue);
                                            }
                                        }
                                    }
                                    //3.循环完所有面板的字段后，生成更新对象列表
                                    List_DTObj.Add(DTObj);
                                    //4.服务端根据接收到的对象列表更新数据库
                                    //ComClient.SaveData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), SQL, OperType.AddSave);
                                    ComClient.SaveData1(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), SQL);
                                    ComClient.CommitTransaction();
                                }
                            }
                        }
                        BtnName = BtnNam.Null;
                        //重新刷新网格数据
                        Grip_Common2.DataSource = LoadData(UsrGroupClient.GetUsrGrpsToChCol("#Tmp_" + CurUsrID + "_" + DateTime.Now.Second.ToString(), "0",""));
                    }
                    return 1;
                   
                }
                else if (BtnName == BtnNam.Unapprove || BtnName == BtnNam.Approve || BtnName==BtnNam.Disable || BtnName==BtnNam.Enable)
                {
                    //获取当前选择行的数据
                    IDictionary<string, object> IDObj1 = (IDictionary<string, object>)View_Common2.GetFocusedRow(); 
                    if (BtnName == BtnNam.Unapprove)
                    {
                        //根据表名和窗体实例名查询出更新表需要的关键信息
                        List_EObj = LoadData(ComClient.GetTabStrc(TabNam, FrmNam, OperType.UnApprove, PriKey + " ='" + IDObj1[ComClient.GetChColByEnCol(TabNam, PriKey)].ToString() + "'", "Panel4",CurUsrID,CurUsrName));
                        //获取执行SQL
                        foreach (ExpandoObject EObj in List_EObj)
                        {
                            var IDObj = (IDictionary<string, object>)EObj;
                            SQL = (string)IDObj["SQL"];
                        }
                    } 
                    else if (BtnName ==BtnNam.Approve)
                    {
                        //根据表名和窗体实例名查询出更新表需要的关键信息
                        List_EObj = LoadData(ComClient.GetTabStrc(TabNam, FrmNam, OperType.Approve, PriKey + " ='" + IDObj1[ComClient.GetChColByEnCol(TabNam, PriKey)].ToString() + "'", "Panel4",CurUsrID,CurUsrName));
                        //获取执行SQL
                        foreach (ExpandoObject EObj in List_EObj)
                        {
                            var IDObj = (IDictionary<string, object>)EObj;
                            SQL = (string)IDObj["SQL"];
                        }
                    }
                    else if (BtnName == BtnNam.Disable)
                    {
                        //根据表名和窗体实例名查询出更新表需要的关键信息
                        List_EObj = LoadData(ComClient.GetTabStrc(TabNam, FrmNam, OperType.Disable, PriKey + " ='" + IDObj1[ComClient.GetChColByEnCol(TabNam, PriKey)].ToString() + "'", "Panel4","",""));
                        //获取执行SQL
                        foreach (ExpandoObject EObj in List_EObj)
                        {
                            var IDObj = (IDictionary<string, object>)EObj;
                            SQL = (string)IDObj["SQL"];
                        }
                    }
                    else if (BtnName == BtnNam.Enable)
                    {
                        //根据表名和窗体实例名查询出更新表需要的关键信息
                        List_EObj = LoadData(ComClient.GetTabStrc(TabNam, FrmNam, OperType.Enable, PriKey + " ='" + IDObj1[ComClient.GetChColByEnCol(TabNam, PriKey)].ToString() + "'", "Panel4","",""));
                        //获取执行SQL
                        foreach (ExpandoObject EObj in List_EObj)
                        {
                            var IDObj = (IDictionary<string, object>)EObj;
                            SQL = (string)IDObj["SQL"];
                        }
                    }
                  
                    //ComClient.SaveData(Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), SQL, OperType.UnApprove);
                    ComClient.SaveData1("", SQL);
                    //提交事务
                    ComClient.CommitTransaction();
                    BtnName = BtnNam.Null;
                    //重新刷新网格数据
                    Grip_Common2.DataSource = LoadData(UsrGroupClient.GetUsrGrpsToChCol("#Tmp_" + CurUsrID + "_" + DateTime.Now.Second.ToString(), "0",""));
                }
                else
                {
                    Common.ShowMsg("用户组代号不允许重复！");
                    return 0;
                }
            }
            catch (Exception Ex)
            {
                //关闭当前SQL连接
                ComClient.CloseSqlCon();
                Common.ShowMsg(Ex.Message);
            }
            return 0;
        }


        /// <summary>
        /// 根据主表定位行获取细表数据源
        /// </summary>
        /// <param name="EObject">主表定位行数据</param>
        /// <returns>细表数据源</returns>
        public override List<ExpandoObject> GetDataByFocRow(ExpandoObject EObject,string PanelNam)
        {
            try
            {
             dynamic Obj = EObject;
             return   Serializer.DeserializeXMLToEObject(UsrGroupClient.GetGrpUsrsByGrpCodeToChCol("#Tmp_" + CurUsrID + "_" + DateTime.Now.Second.ToString(), Obj.分组代号,"0",PanelNam));
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new List<ExpandoObject>();
        }

        /// <summary>
        /// 初始化主表网格数据
        /// </summary>
        /// <returns>主表网格控件数据源</returns>
        public override List<ExpandoObject> InitGridData(string IfGetFstRow,string FltSQL="")
        {
            try
            {
               return Serializer.DeserializeXMLToEObject(UsrGroupClient.GetUsrGrpsToChCol("#Tmp_" + CurUsrID + "_" + DateTime.Now.Second.ToString(), IfGetFstRow, FltSQL));
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new List<ExpandoObject>();
        }

        public override string  GetColsByPanelNam(string PanelNam, string FrmNam, string TabNam)
        {
            try
            {
               return ComClient.GetColsByPanelNam(PanelNam, FrmNam, TabNam);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 根据主键字段获取中文字段的数据(重写基类方法)
        /// </summary>
        /// <returns></returns>
        public override string GetDataByPKToChCol(string PanelNam)
        {
            try
            {
                return UsrGroupClient.GetGrpUsrsByGrpCodeToChCol("#Tmp_" + CurUsrID + "_" + DateTime.Now.Second.ToString(), "", "1", PanelNam);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return "";
        }


        private void Txt_AppSts_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Memo_Remark_EditValueChanged(object sender, EventArgs e)
        {

        }



        private void FrmUserGroup_Load_1(object sender, EventArgs e)
        {

        }

        private void FrmUserGroup_Load_2(object sender, EventArgs e)
        {

        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Lab_Grpcode_Click(object sender, EventArgs e)
        {

        }

        private void Txt_Grpname_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Chk_IfUse_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MailMessage msg = new MailMessage();
            //增加收件人
            msg.To.Add("1834619488@qq.com");
            //增加抄送人
            //msg.CC.Add("抄送人地址@qq.com");
            //增加发件人 
            msg.From = new MailAddress("569338665@qq.com", "QQ");
            //邮件标题
            msg.Subject = "收款通知";
            //标题格式为UTF8  
            msg.SubjectEncoding = Encoding.UTF8;
            //邮件内容
            msg.Body = "尊敬的李丽，您的账户有新的款项收取，你查收，谢谢！";
            //内容格式为UTF8 
            msg.BodyEncoding = Encoding.UTF8;

            SmtpClient client = new SmtpClient();
            //SMTP服务器地址 
            client.Host = "smtp.qq.com";
            //SMTP端口，QQ邮箱填写587  
            client.Port = 587;
            //启用SSL加密  
            client.EnableSsl = true;

            //569338665@qq.com邮箱授权码:hhxdfpyczithbdbi
            //1834619488@qq.com邮箱授权码:oddqlnangcdrdiji
            client.Credentials = new NetworkCredential("569338665@qq.com", "hhxdfpyczithbdbi");
            //发送邮件  
            try
            {
                client.Send(msg);
            }
            catch (SmtpException ex)
            {
                Common.ShowMsg(ex.Message);
            }
            finally
            {
                client.Dispose();
                msg.Dispose();
            }
        }

        //定义一个list集合
        List<String> list = new List<String>();
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            director("C:\\Documents and Settings\\Administrator\\Local Settings\\Apps\\2.0\\YOAWYWT1.WL9\\3KZ58WXB.P1P\\diou..tion_8e4e3e9cb04b2cc2_0001.0001_1f77bb72cf0e2778");
            //list = null;
        }

        public void director(string dirs)
        {
            //绑定到指定的文件夹目录
            DirectoryInfo dir = new DirectoryInfo(dirs);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                //判断是否为空文件夹　　
                if (fsinfo is DirectoryInfo)
                {
                    //递归调用
                    director(fsinfo.FullName);
                }
                else
                {
                    Console.WriteLine(fsinfo.Name);
                    //将得到的文件全路径放入到集合中
                    list.Add(fsinfo.Name+"--"+fsinfo.LastWriteTime);
                }
            }
        }

    }
}
