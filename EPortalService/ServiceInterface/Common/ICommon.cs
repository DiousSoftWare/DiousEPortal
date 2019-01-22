using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EPortalModel;
using EPortalCommom;
using System.Dynamic;
using System.Data;
using System.Data.SqlClient;
namespace EPortalService
{
    [ServiceContract(SessionMode=SessionMode.Allowed)]
    public interface ICommon
    {
        /// <summary>
        /// 设置当前登录用户名
        /// </summary>
        [OperationContract]
        void SetCurUsrInfo(string CurUsrID,string CurUsrName);

        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="StrXML">实体对象的序列化数据</param>
        /// <param name="operType">操作类型</param>
        [OperationContract]
        void SaveData(string StrXML, string SQL, OperType operType);

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="StrXML">实体对象的序列化数据</param>
        ///<param name="SQL">保存数据需要执行的SQL命令</param>
        [OperationContract]
        void SaveData1(string StrXML, string SQL );

        /// <summary>
        /// 检查重复值
        /// </summary>
        /// <param name="TabName">表名</param>
        /// <param name="PrimaryKey">主键名</param>
        /// <param name="KeyValue">键值</param>
        /// <returns>0:表示无重复值,1:表示存在重复值</returns>
        [OperationContract]
        int RepeatCheck(string TabName, string PrimaryKey, string KeyValue);

        /// <summary>
        /// 根据英文字段名获取中文字段名
        /// </summary>
        /// <param name="TabNam">表名</param>
        /// <param name="EnColNam">英文字段名</param>
        /// <returns></returns>
        [OperationContract]
        string GetChColByEnCol(string TabNam, string EnColNam);

        /// <summary>
        /// 根据控件面板名称返回该面板关联的所有字段信息
        /// </summary>
        /// <param name="PanelNam">面板名称</param>
        /// <param name="FrmNam">窗体实例全名</param>
        /// <param name="TabNam">表名</param>
        /// <returns>面板内所有相关联的字段信息</returns>
        [OperationContract]
        string GetColsByPanelNam(string PanelNam, string FrmNam, string TabNam);

        /// <summary>
        /// 根据控件面板名称返回该面板关联的所有字段信息
        /// </summary>
        /// <param name="PanelNam">面板名称</param>
        /// <param name="FrmNam">窗体实例全名</param>
        /// <returns>面板内所有相关联的字段信息</returns>
        [OperationContract(Name = "GetFltColsByPanelNam")]
        string GetColsByPanelNam(string PanelNam, string FrmNam);

        /// <summary>
        /// 获取表结构序列化数据(同时产生增加/修改/保存的SQL)
        /// </summary>
        /// <param name="TabName">表名</param>
        /// <param name="operType">数据操作类型</param>
        /// <param name="DataSource">表结构数据源</param>
        /// <param name="KeyWords">删除条件拼接脚本</param>
        /// <returns>表结构序列化数据(XML)</returns>
        [OperationContract]
        string GetTabStrc(string TabNam, string FrmNam, OperType operType, string KeyWords,string PanelNam,string CurUsrID="",string CurUsrName="");

        /// <summary>
        /// 根据窗体实例全名获取需要更新数据的表
        /// </summary>
        /// <param name="FrmNam">窗体实例全名</param>
        /// <returns>返回结果的序列化数据</returns>
        [OperationContract]
        string GetTabByFrmNam(string FrmNam);

        /// <summary>
        /// 获取当前用户的过滤方案
        /// </summary>
        /// <param name="UsrID">当前登录用户ID</param>
        /// <param name="FrmNam">过滤方案关联的窗体全名</param>
        /// <returns>过滤方案序列化数据</returns>
        [OperationContract]
        string GetFltSlt(string UsrID, string FrmNam);

        /// <summary>
        /// 增加私有过滤方案
        /// </summary>
        /// <param name="FSltName">过滤方案名称</param>
        /// <param name="FFrmName"></param>
        /// <returns>返回受影响的行数</returns>
        [OperationContract]
        int AddPrtFltSlt(string CurUsrID,string SltName, string FrmName);

        /// <summary>
        /// 增加共享过滤方案
        /// </summary>
        /// <param name="UsrID">用户ID</param>
        /// <param name="SltName">过滤方案名称</param>
        /// <param name="FrmName">窗体名称</param>
        /// <returns>返回受影响的行数</returns>
        [OperationContract]
        int AddShFltSlt(string UsrID,string SltName, string FrmName);

        /// <summary>
        /// 删除共享过滤方案
        /// </summary>
        /// <param name="SltName">过滤方案名称</param>
        /// <param name="FrmName">窗体名称</param>
        /// <returns>返回受影响的行数</returns>
        [OperationContract]
        int DelFltSlt(string SltName, string FrmName);

        /// <summary>
        /// 检查方案名称是不是由当前用户建立的共享方案
        /// </summary>
        /// <param name="SltName">方案名称</param>
        /// <param name="UsrID">用户ID</param>
        /// <param name="FrmName">窗体名称</param>
        /// <returns>返回受影响的行数</returns>
        [OperationContract]
        int CheckIfUsrSlt(string SltName, string UsrID, string FrmName);

        /// <summary>
        /// 编辑方案名称
        /// </summary>
        /// <param name="SltName">方案名称</param>
        /// <param name="SltID">方案</param>
        /// <returns></returns>
        [OperationContract]
        int EditFltSlt(string SltName, string SltID);

        /// <summary>
        /// 保存过滤方案数据
        /// </summary>
        /// <param name="operType">操作类型</param>
        /// <param name="StrXML">序列化后的List<DTSerializer<string, object>>对象字符串</param>
        /// <param name="KeyWords">Where查询关键字(删除方案数据用)</param>
        [OperationContract]
        void SaveFltSlt(OperType operType, string StrXML, string TabName, string KeyWords);

        /// <summary>
        /// 获取最大的过滤/查询方案序号
        /// </summary>
        /// <param name="FrmName">当前窗体全名</param>
        /// <param name="SltType">方案类型;1:查询方案 2:过滤方案</param>
        /// <param name="UsrID">当前登录的用户ID</param>
        /// <returns>最大的过滤/查询方案序号</returns>
        [OperationContract]
        string GetMaxSltID(string FrmName, string SltType, string ParentID);

        /// <summary>
        /// 根据过滤方案ID获取过滤方案明细数据
        /// </summary>
        /// <param name="SltID"></param>
        /// <returns></returns>
        [OperationContract]
        string GetFltSltDtlBySltID(string SltID);

        /// <summary>
        /// 打开SQL连接，开始SQL事务
        /// </summary>
        [OperationContract]
        void BeginTransaction();

        /// <summary>
        /// 提交SQL事务，关闭SQL连接
        /// </summary>
        [OperationContract]
        void CommitTransaction();

        /// <summary>
        /// 保存过滤方案数据
        /// </summary>
        /// <param name="operType">操作类型</param>
        /// <param name="StrXML">序列化后的List<DTSerializer<string, object>>对象字符串</param>
        /// <param name="TabName">表名称</param>
        /// <param name="KeyWords">Where查询关键字(删除方案数据用)</param>
        [OperationContract]
        void SaveFltSlt1(OperType operType, string StrXML, string TabName, string KeyWords);

        /// <summary>
        /// 关闭当前数据库连接
        /// </summary>
        [OperationContract]
        void CloseSqlCon();
        
        }
}