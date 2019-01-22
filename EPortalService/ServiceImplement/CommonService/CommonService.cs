using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EPortalModel;
using EPortalBLL;
using EPortalCommom;
using System.Data;
using System.Data.SqlClient;
namespace EPortalService
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
    public partial class EPortalService : ICommon
    {
        public CommonBLL ComBLL { get; set; }
        public SqlConnection SqlCon { get; set; }
        public SqlCommand SqlCmd { get; set; }

        public EPortalService()
        {
            ComBLL = new CommonBLL();
            SqlCon = ComBLL.GetSqlCon();
            SqlCmd = SqlCon.CreateCommand();
        }

        /// <summary>
        /// 打开SQL连接.开始SQL事务
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
               SqlCmd.Connection.Open();           
               SqlCmd.Transaction = SqlCmd.Connection.BeginTransaction();
            }
            catch (Exception Ex)
            {
             Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 提交SQL事务,关闭SQL连接
        /// </summary>
       public void CommitTransaction()
        {
            try
            {
                SqlCmd.Transaction.Commit();
                SqlCmd.Connection.Close();
            }
            catch (Exception Ex)
            {
             Common.ShowMsg(Ex.Message);
            }
        }

        public void SetCurUsrInfo(string CurUsrID, string CurUsrName)
        {
            try
            {
                ComBLL.CurUsrID = CurUsrID;
                ComBLL.CurUsrName = CurUsrName;
                ComBLL.SetCurUsrInfo();
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
        public void SaveData(string StrXML, string SQL, OperType operType)
        {
            try
            {
                ComBLL.SaveData(StrXML, SQL, operType);
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
        public void SaveData1(string StrXML, string SQL)
        {
            try
            {
                ComBLL.SaveData1(StrXML, SQL, SqlCmd);
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
        /// <returns>1:表示无重复值,2:表示存在重复值</returns>
        public int RepeatCheck(string TabName, string PrimaryKey, string KeyValue)
        {
            try
            {
                return ComBLL.RepeatCheck(TabName, PrimaryKey, KeyValue);
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
                return ComBLL.GetChColByEnCol(TabNam, EnColNam);
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
                return ComBLL.GetColsByPanelNam(PanelNam, FrmNam, TabNam);
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
                return ComBLL.GetColsByPanelNam(PanelNam, FrmNam);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 获取表结构序列化数据(同时产生增加/修改/保存的SQL)
        /// </summary>
        /// <param name="TabName">表名</param>
        /// <param name="operType">数据操作类型</param>
        /// <param name="DataSource">表结构数据源</param>
        /// <param name="KeyWords">删除条件拼接脚本</param>
        /// <returns>表结构序列化数据(XML)</returns>
        public string GetTabStrc(string TabNam, string FrmNam, OperType operType, string KeyWords, string PanelNam, string CurUsrID = "", string CurUsrName = "")
        {
            try
            {
                ComBLL.CurUsrID = CurUsrID;
                ComBLL.CurUsrName = CurUsrName;
                ComBLL.SetCurUsrInfo();
                return ComBLL.GetTabStrc(TabNam, FrmNam, operType, KeyWords, PanelNam);
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
                return ComBLL.GetTabByFrmNam(FrmNam);
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

        public string GetFltSlt(string UsrID, string FrmNam)
        {
            try
            {
                return ComBLL.GetFltSlt(UsrID, FrmNam);
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
        public int AddPrtFltSlt(string CurUsrID, string SltName, string FrmName)
        {
            try
            {
                return ComBLL.AddPrtFltSlt(CurUsrID, SltName, FrmName);
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
        public int AddShFltSlt(string UsrID, string SltName, string FrmName)
        {
            try
            {
                return ComBLL.AddShFltSlt(UsrID, SltName, FrmName);
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
                return ComBLL.DelFltSlt(SltName, FrmName);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 检查方案名称是不是由当前用户建立的共享方案
        /// </summary>
        /// <param name="SltName">方案名称</param>
        /// <param name="UsrID">用户ID</param>
        /// <param name="FrmName">窗体名称</param>
        /// <returns>返回受影响的行数</returns>
        public int CheckIfUsrSlt(string SltName, string UsrID, string FrmName)
        {
            try
            {
                return ComBLL.CheckIfUsrSlt(SltName, UsrID, FrmName);
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
                return ComBLL.EditFltSlt(SltName, SltID);
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
        /// <param name="TabName">表名</param>
        /// <param name="KeyWords">Where查询关键字(删除方案数据用)</param>
        public void SaveFltSlt(OperType operType, string StrXML, string TabName, string KeyWords)
        {
            try
            {
                ComBLL.SaveFltSlt(operType, StrXML, TabName, KeyWords);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }


        public void SaveFltSlt1(OperType operType, string StrXML, string TabName, string KeyWords)
        {
            try
            {
                ComBLL.SaveFltSlt1(operType, StrXML, TabName,KeyWords,SqlCmd);
            }
            catch (Exception Ex)
            {
               Common.ShowMsg(Ex.Message);
            }
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
                return ComBLL.GetMaxSltID(FrmName, SltType, ParentID);
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
        /// <param name="SltID">过滤方案ID</param>
        /// <returns></returns>
        public string GetFltSltDtlBySltID(string SltID)
        {
            try
            {
                return ComBLL.GetFltSltDtlBySltID(SltID);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 关闭当前数据库连接
        /// </summary>
        public void CloseSqlCon()
        {
            try
            {
                SqlCon.Close();
            }
            catch (Exception Ex)
            {
              Common.ShowMsg(Ex.Message);
            }
        }
    }
}