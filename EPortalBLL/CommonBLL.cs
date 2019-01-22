using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPortalDAL;
using EPortalModel;
using EPortalCommom;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
namespace EPortalBLL
{
    public class CommonBLL
    {
        /// <summary>
        /// 获取或设置当前登录用户ID
        /// </summary>
        public string CurUsrID { get; set; }

        /// <summary>
        /// 获取或设置当前登录用户名称
        /// </summary>
        public string CurUsrName { get; set; }

        /// <summary>
        /// 当前窗体名称
        /// </summary>
        public string CurFrmName { get; set; }

        public CommonDAL ComDAL { get; set; }

        public CommonBLL()
        {
            ComDAL = new CommonDAL();
        }

        public void SetCurUsrInfo()
        {
            try
            {
                ComDAL.CurUsrID = CurUsrID;
                ComDAL.CurUsrName = CurUsrName;
            }
            catch (Exception Ex)
            {
             Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 获取表结构序列化数据(同时产生增加/修改/保存的SQL)
        /// </summary>
        /// <param name="TabName">表名</param>
        /// <param name="operType">数据操作类型</param>
        /// <param name="DataSource">表结构数据源</param>
        /// <param name="KeyWords">删除条件拼接脚本</param>
        /// <returns>表结构序列化数据(XML)</returns>
        public string GetStructure(string TabName, OperType operType, List<ExpandoObject> DataSource, string KeyWords)
        {
            //DataSource反序列化成List<ExpandObject>列表
            //List<ExpandoObject> ListEObj = Serializer.DeserializeXMLToEObject(DataSource);
            //将数据序列化为XML格式
            return Serializer.SerializeDTToXml(ComDAL.GetStructure(TabName, operType, DataSource, KeyWords));   
        }

        /// <summary>
        /// 保存数据 
        /// </summary>
        /// <param name="StrXML">实体对象的序列化数据</param>
        /// <param name="operType">操作类型</param>
        public void SaveData(string StrXML, string SQL, OperType operType)
        {
            try
            {
                ComDAL.SaveData(StrXML, SQL, operType);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 保存数据 
        /// </summary>
        /// <param name="StrXML">实体对象的序列化数据</param>
        /// <param name="operType">操作类型</param>
        public void SaveData1(string StrXML, string SQL,SqlCommand Cmd)
        {
            try
            {
                ComDAL.SaveData1(StrXML, SQL,Cmd);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }


        /// <summary>
        /// 检查重复值
        /// </summary>
        /// <param name="TabName">表名</param>
        /// <param name="PrimaryKey">主键名</param>
        /// <param name="KeyValue">键值</param>
        /// <returns>0:表示无重复值,1:表示存在重复值</returns>
        public int RepeatCheck(string TabName, string PrimaryKey, string KeyValue)
        {
            try
            {
              return  ComDAL.RepeatCheck(TabName, PrimaryKey, KeyValue);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 根据英文字段名获取中文字段名
        /// </summary>
        /// <param name="TabNam">表名</param>
        /// <param name="EnColNam">英文字段名</param>
        /// <returns></returns>
        public string GetChColByEnCol(string TabNam, string EnColNam)
        {
            try
            {
              return  ComDAL.GetChColByEnCol(TabNam, EnColNam);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 根据控件面板名称返回该面板关联的所有字段信息
        /// </summary>
        /// <param name="PanelNam">面板名称</param>
        /// <param name="FrmNam">窗体实例全名</param>
        /// <param name="TabNam">表名</param>
        /// <returns>面板内所有相关联的字段信息</returns>
        public string GetColsByPanelNam(string PanelNam, string FrmNam, string TabNam)
        {
            try
            {
                return ComDAL.GetColsByPanelNam(PanelNam,FrmNam, TabNam);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return "";
        }


        /// <summary>
        /// 根据控件面板名称返回该面板关联的所有字段信息
        /// </summary>
        /// <param name="PanelNam">面板名称</param>
        /// <param name="FrmNam">窗体实例全名</param>
        /// <returns>面板内所有相关联的字段信息</returns>
        public string GetColsByPanelNam(string PanelNam, string FrmNam)
        {
            try
            {
                return ComDAL.GetColsByPanelNam(PanelNam, FrmNam);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return "";
        }
        /// <summary>
        /// 获取英文字段名的表结构数据
        /// </summary>
        /// <param name="cmdType">数据操作类型</param>
        /// <param name="cmdText">sql命令文本</param>
        /// <param name="Parameters">sql参数</param>
        /// <returns></returns>
        public List<DTSerializer<string, object>> GetEnColData(CommandType cmdType, string cmdText, params SqlParameter[] Parameters)
        {
            try
            {
                return ComDAL.GetEnColData(cmdType, cmdText, Parameters);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new List<DTSerializer<string, object>> ();
        }

        /// <summary>
        /// 获取表结构序列化数据(同时产生增加/修改/保存的SQL)
        /// </summary>
        /// <param name="TabName">表名</param>
        /// <param name="operType">数据操作类型</param>
        /// <param name="DataSource">表结构数据源</param>
        /// <param name="KeyWords">删除条件拼接脚本</param>
        /// <returns>表结构序列化数据(XML)</returns>
        public string GetTabStrc(string TabNam, string FrmNam, OperType operType, string KeyWords,string PanelNam)
        {
            try
            {
                List<ExpandoObject> List_EObj = new List<ExpandoObject>();
                SqlParameter[] Parameters = { new SqlParameter("@TableName", TabNam), new SqlParameter("@PanelNam", PanelNam),new SqlParameter("@IfGetFstRow", "1"),new SqlParameter("@FFrmNam", FrmNam) };
                //获取表架构数据源 
                List<DTSerializer<string, object>> ListDTObj =GetEnColData(CommandType.StoredProcedure, "Sp_ADMM_CrtSQLByTblNmToEnCol", Parameters);
                //ListDTObj没有数据
                foreach (var DTObj in ListDTObj)
                {
                    dynamic EObj = new ExpandoObject();
                    var IDObj = (IDictionary<string, object>)EObj;
                    foreach (var a in DTObj)
                    {
                        IDObj[a.Key] = a.Value;
                    }

                    List_EObj.Add(EObj);
                }

                return GetStructure(TabNam, operType, List_EObj, KeyWords);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }

            return "";
        }

        /// <summary>
        /// 根据窗体实例全名获取需要更新数据的表
        /// </summary>
        /// <param name="FrmNam">窗体实例全名</param>
        /// <returns>返回结果的序列化数据</returns>
        public string GetTabByFrmNam(string FrmNam)
        {
            try
            {
                return  ComDAL.GetTabByFrmNam(FrmNam);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }


        /// <summary>
        /// 获取当前用户的过滤方案
        /// </summary>
        /// <returns></returns>
        public string GetFltSlt(string UsrID, string FrmNam)
        {
            try
            {
                return ComDAL.GetFltSlt(UsrID,FrmNam);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 增加私有过滤方案
        /// </summary>
        /// <param name="FSltName">过滤方案名称</param>
        /// <param name="FFrmName"></param>
        /// <returns>返回受影响的行数</returns>
        public int AddPrtFltSlt(string CurUsrID, string SltName,string FrmName)
        {
            try
            {
                return ComDAL.AddPrtFltSlt(CurUsrID, SltName, FrmName);
            }
            catch (Exception Ex)
            {
               Common.ShowMsg(Ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 保存过滤方案数据
        /// </summary>
        /// <param name="operType">操作类型</param>
        /// <param name="StrXML">序列化后的List<DTSerializer<string, object>>对象字符串</param>
        /// <param name="KeyWords">Where查询关键字(删除方案数据用)</param>
        public void SaveFltSlt(OperType operType,string StrXML,string TabName,string KeyWords)
        {
            try
            {
                DataTransform DataTransformer = new DataTransform();
                //1.获取增加方案SQL语句
                string SQL = "";
                List<ExpandoObject> List_EObj= DataTransformer.LoadData(GetTabStrc(TabName, "DiousEPortal.FrmFilter", operType, KeyWords, "Pal_FlCtner"));

                    foreach (ExpandoObject EObj in List_EObj)
                    {
                        var IDObj = (IDictionary<string, object>)EObj;
                        SQL = (string)IDObj["SQL"];
                    }


                //2.保存数据
                SaveData(StrXML, SQL, operType);
            }
            catch (Exception Ex)
            {
                //如果增加方案明细时报错，则回滚删除方案名称
               if((operType==OperType.AddSave || operType==OperType.Delete) && TabName== "t_ADMM_FltSltforDtl")
                {
                    List<ExpandoObject> List_EObj1 = Serializer.DeserializeXMLToEObject(StrXML);
                    string FSltID = "";
                    foreach (dynamic EObj in List_EObj1)
                    {
                        FSltID = EObj.FSltID;
                        break;
                    }

                    DataTransform DataTransformer = new DataTransform();
            
                    string SQL = "";
                    List<ExpandoObject> List_EObj2;
                    if (operType!= OperType.Delete)
                    {
                     List_EObj2 = DataTransformer.LoadData(GetTabStrc("t_ADMM_FltSltList", "DiousEPortal.FrmFilter", OperType.Delete, " FSltID='" + FSltID + "'", "Pal_FlCtner"));
                        foreach (ExpandoObject EObj in List_EObj2)
                        {
                            var IDObj = (IDictionary<string, object>)EObj;
                            SQL = (string)IDObj["SQL"];
                        }
                    }
                 //如果删除方案明细时报错，则回滚删除方案名称
                    else
                    {
                     List_EObj2 = DataTransformer.LoadData(GetTabStrc("t_ADMM_FltSltList", "DiousEPortal.FrmFilter", OperType.Delete, KeyWords, "Pal_FlCtner"));
                        foreach (ExpandoObject EObj in List_EObj2)
                        {
                            var IDObj = (IDictionary<string, object>)EObj;
                            SQL = (string)IDObj["SQL"];
                        }
                    }

                    SaveData("", SQL, OperType.Delete);
                }

                Common.ShowMsg(Ex.Message);
            }
        }

        public void SaveFltSlt1(OperType operType, string StrXML, string TabName, string KeyWords,SqlCommand Cmd)
        {
            try
            {
                DataTransform DataTransformer = new DataTransform();
                //1.获取增加方案SQL语句
                string SQL = "";
                List<ExpandoObject> List_EObj = DataTransformer.LoadData(GetTabStrc(TabName, "DiousEPortal.FrmFilter", operType, KeyWords, "Pal_FlCtner"));

                foreach (ExpandoObject EObj in List_EObj)
                {
                    var IDObj = (IDictionary<string, object>)EObj;
                    SQL = (string)IDObj["SQL"];
                }

                //2.保存数据
                SaveData1(StrXML, SQL, Cmd);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }


        /// <summary>
        /// 返回一个新的SqlConnection对象
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetSqlCon()
        {
            try
            {
                return ComDAL.GetSqlCon();
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return new SqlConnection();
        }

        /// <summary>
        /// 增加共享过滤方案
        /// </summary>
        /// <param name="UsrID">用户ID</param>
        /// <param name="SltName">过滤方案名称</param>
        /// <param name="FrmName">窗体名称</param>
        /// <returns>返回受影响的行数</returns>
        public int AddShFltSlt(string UsrID,string SltName, string FrmName)
        {
            try
            {
                return ComDAL.AddShFltSlt(UsrID,SltName, FrmName);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 删除共享过滤方案
        /// </summary>
        /// <param name="SltName">过滤方案名称</param>
        /// <param name="FrmName">窗体名称</param>
        /// <returns>返回受影响的行数</returns>
        public int DelFltSlt(string SltName, string FrmName)
        {
            try
            {
                return ComDAL.DelFltSlt(SltName, FrmName);
            }
            catch (Exception Ex)
            {
             Common.ShowMsg(Ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 根据过滤方案ID获取过滤方案明细数据
        /// </summary>
        /// <param name="SltID"></param>
        /// <returns></returns>
        public string GetFltSltDtlBySltID(string SltID)
        {
            try
            {
                return ComDAL.GetFltSltDtlBySltID(SltID);
            }
            catch (Exception Ex)
            {
              Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 检查方案名称是不是由当前用户建立的共享方案
        /// </summary>
        /// <param name="SltName">方案名称</param>
        /// <param name="UsrID">用户ID</param>
        /// <param name="FrmName">窗体名称</param>
        /// <returns></returns>
        public int CheckIfUsrSlt(string SltName, string UsrID, string FrmName)
        {
            try
            {
                return ComDAL.CheckIfUsrSlt(SltName, UsrID, FrmName);
            }
            catch (Exception Ex)
            {
              Common.ShowMsg(Ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 编辑方案名称
        /// </summary>
        /// <param name="SltName">方案名称</param>
        /// <param name="SltID">方案</param>
        /// <returns></returns>
        public int EditFltSlt(string SltName, string SltID)
        {
            try
            {
                return ComDAL.EditFltSlt(SltName, SltID);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
                return 0;
        }

        /// <summary>
        /// 获取最大的过滤/查询方案序号
        /// </summary>
        /// <param name="FrmName">当前窗体全名</param>
        /// <param name="SltType">方案类型;1:查询方案 2:过滤方案</param>
        /// <param name="UsrID">当前登录的用户ID</param>
        /// <returns>最大的过滤/查询方案序号</returns>
        public string GetMaxSltID(string FrmName, string SltType, string ParentID)
        {
            try
            {
                return ComDAL.GetMaxSltID(FrmName, SltType, ParentID);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }
       }
}
