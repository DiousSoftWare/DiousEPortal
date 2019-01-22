using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Data.SqlClient;
using EPortalCommom;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace EPortalDAL
{
    public class CommonDAL
    {
        //////////////////////////////声明全局变量///////////////////////////////////
        
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 获取或设置当前登录用户ID
        /// </summary>
        public string CurUsrID { get; set; }

        /// <summary>
        /// 获取或设置当前登录用户名称
        /// </summary>
        public string CurUsrName { get; set; }

        /// <summary>
        /// 获取中文字段名数据
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public List<DTSerializer<string, object>> GetChColData(string SpNam, params SqlParameter[] commandParameters)
        {
            try
            {
                    using (DataTable DT2 = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, SpNam, commandParameters).Tables[0])
                    {
                    //获取DataTable所有的字段名
                    List<string> list_ColNam = new List<string>();
                    foreach (DataColumn Column in DT2.Columns)
                    {
                        list_ColNam.Add(Column.ColumnName);
                    }

                    List<DTSerializer<string, object>> ListDictionary = new List<DTSerializer<string, object>>();
                        foreach (DataRow Row in DT2.Rows)
                        {
                            DTSerializer<string, object> DTObj = new DTSerializer<string, object>();
                            foreach (string ColNam in list_ColNam)
                            {
                                DTObj.Add(ColNam, Row[ColNam].ToString());
                            }
                            ListDictionary.Add(DTObj);
                        }
                        return ListDictionary;
                   }
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new List<DTSerializer<string, object>>();
        }

        /// <summary>
        /// 获取英文字段名数据(适合所有存储过程和SQL语句)
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public List<DTSerializer<string, object>> GetEnColData(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                if (cmdText== "Sp_ADMM_CrtSQLByTblNmToEnCol")
                {
                    using (DataTable DT1 = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, cmdText, commandParameters).Tables[0])
                    {
                        string Sql = "";
                        foreach (DataRow Row in DT1.Rows)
                        {
                            Sql = Row["Sql"].ToString();
                        }

                        using (DataTable DT2 = SqlHelper.ExecuteDataSet(CommandType.Text, Sql, null).Tables[0])
                        {
                            //获取DataTable所有的字段名
                            List<string> list_ColNam = new List<string>();
                            foreach (DataColumn Column in DT2.Columns)
                            {
                                list_ColNam.Add(Column.ColumnName);
                            }

                            List<DTSerializer<string, object>> ListDictionary = new List<DTSerializer<string, object>>();
                            foreach (DataRow Row in DT2.Rows)
                            {
                                DTSerializer<string, object> DObj = new DTSerializer<string, object>();
                                foreach (string ColNam in list_ColNam)
                                {
                                    DObj.Add(ColNam, Row[ColNam].ToString());
                                }
                                ListDictionary.Add(DObj);
                            }
                            return ListDictionary;
                        }
                    }
                }
                else 
                {
                    using (DataTable DT2 = SqlHelper.ExecuteDataSet(cmdType, cmdText, commandParameters).Tables[0])
                    {
                        //获取DataTable所有的字段名
                        List<string> list_ColNam = new List<string>();
                        foreach (DataColumn Column in DT2.Columns)
                        {
                            list_ColNam.Add(Column.ColumnName);
                        }

                        List<DTSerializer<string, object>> ListDictionary = new List<DTSerializer<string, object>>();
                        foreach (DataRow Row in DT2.Rows)
                        {
                            DTSerializer<string, object> DObj = new DTSerializer<string, object>();
                            foreach (string ColNam in list_ColNam)
                            {
                                DObj.Add(ColNam, Row[ColNam].ToString());
                            }
                            ListDictionary.Add(DObj);
                        }
                        return ListDictionary;
                    }
                }
                    
                
                }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new List<DTSerializer<string, object>>();
        }

        /// <summary>
        /// 检查重复值
        /// </summary>
        /// <param name="TabName">表名</param>
        /// <param name="PrimaryKey">主键名</param>
        /// <param name="KeyValue">键值</param>
        /// <returns>0:表示无重复值,等于1:表示存在重复值</returns>
        public int RepeatCheck (string TabName,string PrimaryKey,string KeyValue)
        {
            try
            {
                string Sql = "";
                SqlParameter[] Parameters = { new SqlParameter("@TableName", TabName),new SqlParameter ("@PrimaryKey", PrimaryKey),new SqlParameter ("@KeyValue", KeyValue) };
                using (DataTable DT1 = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "SP_ADMM_CrtRptCkSQLByTblNm", Parameters).Tables[0])
                {
                    Sql=DT1.Rows[0]["Sql"].ToString();

                    using (DataTable DT2 = SqlHelper.ExecuteDataSet(CommandType.Text, Sql, null).Tables[0])
                    {
                      return int.Parse(DT2.Rows[0]["Result"].ToString());
                    }
                }
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return 0;
        }


        /// <summary>
        /// 自动生成增/删/改SQL语句
        /// </summary>
        /// <param name="TabName">表名</param>
        /// <param name="operType">数据操作类型</param>
        /// <param name="DataSource">表结构数据源</param>
        /// <param name="KeyWords">删除条件拼接脚本</param>
        /// <returns>拼接后的SQL和键值为空的动态对象</returns>
        public List<DTSerializer<string, object>> CreateSql(string TabName, OperType operType, List<ExpandoObject> DataSource, string KeyWords)
        {
            try
            {
                List<DTSerializer<string, object>> List_DTObj = new List<DTSerializer<string, object>>();
                DTSerializer<string, object> DTObj = new DTSerializer<string, object>();
                //string DlrSql = "declare ";
                string InsertSql = "Insert into " + TabName + "\r\n" + "(" + "\r\n";
                string ValueSql = "values " + "\r\n" + "(" + "\r\n";
                string DeleteSql = "Delete from " + TabName + " where " + KeyWords + "\r\n";
                string UpdateSql = "Update " + TabName + "\r\n"+" set ";
                //0：未审核 1:已审核 2：多级审核中 3：反审核
                string UnApproveSql = "Update " + TabName + "\r\n"+ " set fCFlag='3',fAppDate=getdate(),fApprover='"+CurUsrName+ "',fApproverID='"+CurUsrID+"' where " + KeyWords + "\r\n";
                string ApproveSql= "Update " + TabName + "\r\n" + " set fCFlag='1',fAppDate=getdate(),fApprover='"+ CurUsrName+ "', fApproverID='"+CurUsrID+"' where " + KeyWords + "\r\n";
                string DisableSql= "Update " + TabName + "\r\n" + " set fIfUse='0' where " + KeyWords + "\r\n";
                string EnableSql = "Update " + TabName + "\r\n" + " set fIfUse='1' where " + KeyWords + "\r\n";
                //遍历集合，将动态对象的键值清除
                foreach (ExpandoObject EObj in DataSource)
                {
                    int i = 0;
                   
                    var IDObj = (IDictionary<string, object>)EObj;
                    int KeysNo = IDObj.Keys.Count;
                    foreach (var key in IDObj.Keys)
                    {
                        i += 1;
                        if (i == KeysNo)
                        {
                            //根据键名自动生插入SQL语句
                            //DlrSql += "@" + key + " varchar(300)" + "\r\n";
                            InsertSql += key + "\r\n" + ")" + "\r\n";
                            ValueSql += "@" + key + "\r\n" + ")" + "\r\n";
                            UpdateSql += key + "=" + "@" + key  + "\r\n"+"where "+KeyWords+ "\r\n";
                        }
                        else
                        {
                            //根据键名自动生插入SQL语句
                            //DlrSql += "@" + key +" varchar(300)"+ "," + "\r\n";
                            InsertSql += key + "," + "\r\n";
                            ValueSql += "@" + key + "," + "\r\n";
                            UpdateSql += key + "=" + "@" + key  +","+ "\r\n";

                        }
                        DTObj.Add(key, IDObj[key]);                  
                    }
                    //List_DTObj.Add(DTObj);
                }

                if (operType == OperType.AddSave || operType == OperType.CopySave)
                {
                    //三段SQL语句拼接起来
                    DTObj.Add("SQL", InsertSql + ValueSql);
                    List_DTObj.Add(DTObj);
                    return List_DTObj;
                    //return new Tuple<string, List<DTSerializer<string, object>>>(DlrSql + InsertSql + ValueSql, List_DTObj);
                }
                else if (operType == OperType.Delete)
                {
                    //三段SQL语句拼接起来
                    DTObj.Add("SQL", DeleteSql);
                    List_DTObj.Add(DTObj);
                    return List_DTObj;
                    //return new Tuple<string, List<DTSerializer<string, object>>>(DeleteSql, List_DTObj);                 
                }
                else if (operType == OperType.EditSave || operType==OperType.CoverSlt)
                {
                    //三段SQL语句拼接起来
                    DTObj.Add("SQL", UpdateSql);
                    List_DTObj.Add(DTObj);
                    return List_DTObj;

                    //return new Tuple<string, List<DTSerializer<string, object>>>(DeleteSql + DlrSql + InsertSql + ValueSql, List_DTObj);                     
                }
                else if (operType == OperType.Approve)
                {
                    //三段SQL语句拼接起来
                    DTObj.Add("SQL", ApproveSql);
                    List_DTObj.Add(DTObj);
                    return List_DTObj;
                }
                else if (operType==OperType.UnApprove)
                {
                    //三段SQL语句拼接起来
                    DTObj.Add("SQL", UnApproveSql);
                    List_DTObj.Add(DTObj);
                    return List_DTObj;
                }
                else if (operType == OperType.Disable)
                {
                    //三段SQL语句拼接起来
                    DTObj.Add("SQL", DisableSql);
                    List_DTObj.Add(DTObj);
                    return List_DTObj;
                }
                else if (operType == OperType.Enable)
                {
                    //三段SQL语句拼接起来
                    DTObj.Add("SQL", EnableSql);
                    List_DTObj.Add(DTObj);
                    return List_DTObj;
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }

            return new List<DTSerializer<string, object>>();
            //return new Tuple<string, List<DTSerializer<string, object>>>("", new List<DTSerializer<string, object>> ());
        }

        /// <summary>
        /// 获取表结构(同时产生增加/修改/保存的SQL)
        /// </summary>
        /// <param name="TabName">表名</param>
        /// <param name="operType">数据操作类型</param>
        /// <param name="DataSource">表结构数据源</param>
        /// <param name="KeyWords">删除条件拼接脚本</param>
        /// <returns></returns>
        public List<DTSerializer<string, object>> GetStructure(string TabName, OperType operType, List<ExpandoObject> DataSource, string KeyWords)
        {
            try
            {
                List<DTSerializer<string, object>> List_DTObj = CreateSql(TabName, operType, DataSource, KeyWords);
               
                return List_DTObj;
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new List<DTSerializer<string, object>>();
        }

       
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="StrXML">包含参数名和参数值的序列化数据</param>
        /// <param name="operType">操作类型</param>
        public void SaveData(string StrXML,string SQL,OperType operType)
        {
            try
            {                            
                //根据操作类型执行不同的操作
                if ((operType != OperType.Delete && operType != OperType.EditSave && operType != OperType.UnApprove && operType != OperType.UnApprove) || (operType == OperType.CoverSlt) )
                {
                    List<ExpandoObject> List_EObj = Serializer.DeserializeXMLToEObject(StrXML);
                    foreach (ExpandoObject EObj in List_EObj)
                    {
                        //自动生成SQL参数列表
                        List<SqlParameter> Parameters = new List<SqlParameter>();
                        IDictionary<string, object> IDObj = (IDictionary<string, object>)EObj;

                        if (IDObj.Keys.Count > 0)
                        {
                            foreach (string Key in IDObj.Keys)
                            {
                                SqlParameter Parameter;
                                if (Key == "fAppDate" || Key == "fModiDate")
                                {
                                    if (IDObj[Key].ToString() != "")
                                    {
                                        Parameter = new SqlParameter(Key, IDObj[Key]);
                                    }
                                    else
                                    {
                                        Parameter = new SqlParameter(Key, DBNull.Value);
                                    }
                                }
                                else
                                {
                                    Parameter = new SqlParameter(Key, IDObj[Key]);
                                }

                                Parameters.Add(Parameter);
                            }
                            //执行自动生成的SQL语句
                            SqlHelper.ExecteNonQuery(CommandType.Text, SQL, Parameters.ToArray());
                        }                       
                    }                   
                }
                else
                {
                    //执行自动生成的SQL语句
                    SqlHelper.ExecteNonQuery(CommandType.Text, SQL, null);
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
        /// <param name="StrXML">包含参数名和参数值的序列化数据</param>
        /// <param name="operType">操作类型</param>
        public void SaveData1(string StrXML, string SQL,SqlCommand Cmd)
        {
            try
            {
                List<ExpandoObject> List_EObj=new List<ExpandoObject>();
                if (StrXML != "")
                {
                    List_EObj = Serializer.DeserializeXMLToEObject(StrXML);
                    foreach (ExpandoObject EObj in List_EObj)
                    {
                        //自动生成SQL参数列表
                        List<SqlParameter> Parameters = new List<SqlParameter>();
                        IDictionary<string, object> IDObj = (IDictionary<string, object>)EObj;

                        if (IDObj.Keys.Count > 0)
                        {
                            foreach (string Key in IDObj.Keys)
                            {
                                SqlParameter Parameter;
                                if (Key == "fAppDate" || Key == "fModiDate")
                                {
                                    if (IDObj[Key].ToString() != "")
                                    {
                                        Parameter = new SqlParameter(Key, IDObj[Key]);
                                    }
                                    else
                                    {
                                        Parameter = new SqlParameter(Key, DBNull.Value);
                                    }
                                }
                                else
                                {
                                    Parameter = new SqlParameter(Key, IDObj[Key]);
                                }

                                Parameters.Add(Parameter);
                            }

                            SqlHelper.ExecteNonQuery1(Cmd, CommandType.Text, SQL, Parameters.ToArray());
                        }
                    }
                }
                else
                {
                            SqlHelper.ExecteNonQuery1(Cmd, CommandType.Text, SQL, null);
                }
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
                return new SqlConnection(SqlHelper.connectionString);

            }
            catch (Exception Ex)
            {
             Common.ShowMsg(Ex.Message);
            }
            return new SqlConnection();
        }

        /// <summary>
        /// 根据英文字段名获取中文字段名
        /// </summary>
        /// <param name="TabNam">表名</param>
        /// <param name="EnColNam">英文字段名</param>
        /// <returns></returns>
        public string GetChColByEnCol(string TabNam,string EnColNam)
        {
            try
            {
                string Sql = "select ColNm,Remark from t_ADMM_TblInforDtl with(nolock) where TblNm='"+ TabNam + "' and ColNm='" + EnColNam+"'";
                string ChCol = "";
                using (DataTable DT1 = SqlHelper.ExecuteDataSet(CommandType.Text, Sql, null).Tables[0])
                {
                    foreach (DataRow Row in DT1.Rows)
                    {
                        ChCol = Row["Remark"].ToString();
                    }
                }
                return ChCol;
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
        public string GetColsByPanelNam(string PanelNam,string FrmNam,string TabNam)
        {
            try
            {
                string Sql = "select [FTblNm],[FColNm],[FRemark],[FRemark1],[FDataType],[FDefValue],[FContrNam],[FIfReadOnly],[FIfHide],[FFrmNam],[FColEdit],[FPanelNam],[FPanelType] from t_ADMM_TblInforDtlToFunc with(nolock) where FPanelNam='" + PanelNam + "'" + " and FFrmNam='" + FrmNam + "'";
                if (TabNam !="")
                {
                    Sql += " and FTblNm='"+ TabNam+"'";
                }
                return GetSerDataToStr(CommandType.Text,Sql,null);
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
                string Sql = "select [FRemark] ,[FColNm] from t_ADMM_TblInforDtlToFunc with(nolock) where FPanelNam='" + PanelNam + "'" + " and FFrmNam='" + FrmNam + "'";        
                return GetSerDataToStr(CommandType.Text, Sql, null);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return "";
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
                string SQL = "select FBrackets1Value,FColNamsValue,FCdt,FCBoxCdtValue1,FCBoxCdtValue2,FCBoxCdtValue3,FDedCdtValue3,FDedCdtValue5,FDedCdtValue6,FBrackets2,FRlt from t_ADMM_FltSltforDtl with(nolock) where FSltID='"+SltID+"'";
                return GetSerDataToStr(CommandType.Text, SQL, null); ;
            }
            catch (Exception Ex)
            {
             Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 获取序列化数据(字符串格式)
        /// </summary>
        /// <param name="commadType">操作类型</param>
        /// <param name="CommandText">操作命令文本</param>
        /// <param name="Parameters">参数列表</param>
        /// <returns></returns>
        public string GetSerDataToStr(CommandType commadType,string CommandText,SqlParameter[] Parameters)
        {
            try
            {
                using (DataTable DT = SqlHelper.ExecuteDataSet(commadType, CommandText, Parameters).Tables[0])
                {
                    //获取DataTable所有的字段名
                    List<string> list_ColNam = new List<string>();
                    foreach (DataColumn Column in DT.Columns)
                    {
                        list_ColNam.Add(Column.ColumnName);
                    }

                    List<DTSerializer<string, object>> ListDictionary = new List<DTSerializer<string, object>>();
                    foreach (DataRow Row in DT.Rows)
                    {
                        DTSerializer<string, object> DObj = new DTSerializer<string, object>();
                        foreach (string ColNam in list_ColNam)
                        {
                            DObj.Add(ColNam, Row[ColNam].ToString());
                        }
                        ListDictionary.Add(DObj);
                    }                   
                    //将数据序列化为XML格式
                    return Serializer.SerializeDTToXml(ListDictionary);
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 获取序列化数据(二进制格式)
        /// </summary>
        /// <param name="commadType">操作类型</param>
        /// <param name="CommandText">操作命令文本</param>
        /// <param name="Parameters">参数列表</param>
        /// <returns></returns>
        public string GetSerDataToXML(CommandType commadType, string CommandText, SqlParameter[] Parameters)
        {
            try
            {
                using (DataTable DT = SqlHelper.ExecuteDataSet(commadType, CommandText, Parameters).Tables[0])
                {
                  return  SerializeDataTableToXML(DT);
                }
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
                string Sql = "select FTblNm,FPanelNam,FFrmNam from t_ADMM_UpTabInfo with(nolock)";
                return GetSerDataToStr(CommandType.Text,Sql,null);
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
        /// <param name="UsrID">当前登录用户ID</param>
        /// <param name="FrmNam">过滤方案关联的窗体全名</param>
        /// <returns>过滤方案序列化数据</returns>
        public string GetFltSlt(string UsrID,string FrmNam)
        {
            try
            {
                StringBuilder Sql=new StringBuilder () ;
                Sql.Append("select FSltID,FSltName,FParentID from t_ADMM_FltSltList where FSltID ='P01' ");
                Sql.Append(" union all ");
                Sql.Append(" select FSltID,FSltName,FParentID from t_ADMM_FltSltList where FSltID ='S01' ");
                Sql.Append(" union all ");
                Sql.Append(" select FSltID,FSltName,FParentID from t_ADMM_FltSltList where FFrmName='" + FrmNam + "' and FSltType='0' and ((FUsrID='" + UsrID + "' and FSltID like 'P%' ) or FSltID like 'S%')");

                return GetSerDataToXML(CommandType.Text,Sql.ToString(),null);             
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 将数据表序列化为XML。
        /// </summary>
        /// <param name="DT">需要序列化的数据表</param>
        /// <returns>XML序列化数据</returns>
        public string  SerializeDataTableToXML(DataTable DT)
        {
            try
            {
               return Serializer.SerializeDTToXml<DataTable>(DT);
            }
            catch (Exception EX)
            {
                Common.ShowMsg(EX.Message);
            }
            return "";
        }

        /// <summary>
        /// 获取最大的过滤/查询方案序号
        /// </summary>
        /// <param name="FrmName">当前窗体全名</param>
        /// <param name="SltType">方案类型;1:查询方案 2:过滤方案</param>
        /// <param name="UsrID">当前登录的用户ID</param>
        /// <returns>最大的过滤/查询方案序号</returns>
        public string GetMaxSltID(string FrmName,string SltType,string ParentID)
        {
            try
            {
                string SQL = "select * from Fn_ADMM_GetMaxSltID(@FrmName,@SltType,@ParentID)";
                SqlParameter[] Parameters = { new SqlParameter("@FrmName", FrmName) , new SqlParameter("@SltType", SltType) , new SqlParameter("@ParentID", ParentID) };

                using (DataTable DT = SqlHelper.ExecuteDataSet(CommandType.Text, SQL, Parameters).Tables[0])
                {
                    foreach(DataRow Row in DT.Rows)
                    {
                      return  Row["SltID"].ToString();
                    }
                }
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
        /// <param name="SltName">过滤方案名称</param>
        /// <param name="FrmName">窗体名称</param>
        /// <returns>返回受影响的行数</returns>
        public int AddPrtFltSlt( string CurUsrID,string SltName,string FrmName)
        {
            try
            {
                string FSltID = GetMaxSltID(FrmName, "0", "P01");
                string SQL = "insert into t_ADMM_FltSltList values(@FSltID,@FSltName,@FParentID,@FUsrID,@FFrmName,@FSltType)";
                SqlParameter[] Parameters = { new SqlParameter("@FSltName", SltName),new SqlParameter("@FSltID", FSltID), new SqlParameter("@FParentID", "P01"), new SqlParameter("@FUsrID", CurUsrID), new SqlParameter("@FFrmName", FrmName), new SqlParameter("@FSltType", "0")};
                return SqlHelper.ExecteNonQuery(CommandType.Text, SQL, Parameters);
            }
            catch (Exception Ex)
            {
              Common.ShowMsg(Ex.Message);
            }
            return 0;
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
                string FSltID = GetMaxSltID(FrmName, "0", "S01");
                string SQL = "insert into t_ADMM_FltSltList values(@FSltID,@FSltName,@FParentID,@FUsrID,@FFrmName,@FSltType)";
                SqlParameter[] Parameters = { new SqlParameter("@FSltName", SltName), new SqlParameter("@FSltID", FSltID), new SqlParameter("@FParentID", "S01"), new SqlParameter("@FUsrID", UsrID), new SqlParameter("@FFrmName", FrmName), new SqlParameter("@FSltType", "0") };
                return SqlHelper.ExecteNonQuery(CommandType.Text, SQL, Parameters);
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
        public int EditFltSlt(string SltName,string SltID)
        {
            try
            {
                string SQL = "Update t_ADMM_FltSltList set FSltName=@FSltName where FSltID=@FSltID";
                SqlParameter[] Parameters = { new SqlParameter("@FSltName", SltName), new SqlParameter("@FSltID", SltID) };
                return SqlHelper.ExecteNonQuery(CommandType.Text, SQL, Parameters);
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
                string SQL = "delete from t_ADMM_FltSltList where FFrmName=@FFrmName and FSltName=@FSltName";
                SqlParameter[] Parameters = { new SqlParameter("@FSltName", SltName),new SqlParameter("@FFrmName", FrmName)};
                return SqlHelper.ExecteNonQuery(CommandType.Text, SQL, Parameters);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 检查方案名称是不是由当前用户建立的方案
        /// </summary>
        /// <param name="SltName">方案名称</param>
        /// <param name="UsrID">用户ID</param>
        /// <param name="FrmName">窗体名称</param>
        /// <returns>返回受影响的行数</returns>
        public int CheckIfUsrSlt(string SltName,string UsrID,string FrmName)
        {
            try
            {
                string SQL = "select FSltName from t_ADMM_FltSltList where FSltName=@FSltName and FUsrID=@FUsrID and FFrmName=@FFrmName";
                SqlParameter[] Parameters = { new SqlParameter("@FSltName", SltName),new SqlParameter ("@FUsrID", UsrID),new SqlParameter ("@FFrmName",FrmName) };
                using (DataTable DT = SqlHelper.ExecuteDataSet(CommandType.Text, SQL, Parameters).Tables[0])
                {
                    return DT.Rows.Count;
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return 0;
        }


    }
}
