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
namespace EPortalService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUsrGroupService”。
    [ServiceContract]
    public interface IUsrGroupService
    {
        /// <summary>
        /// 获取用户组数据(中文字段名)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetUsrGrpsToChCol(string TempTabNam,string IfGetFstRow,string FltSQL="");

        ///// <summary>
        ///// 获取表结构序列化数据(同时产生增加/修改/保存的SQL)
        ///// </summary>
        ///// <param name="TabName">表名</param>
        ///// <param name="operType">数据操作类型</param>
        ///// <param name="DataSource">表结构数据源</param>
        ///// <param name="KeyWords">删除条件拼接脚本</param>
        ///// <returns>表结构序列化数据(XML)</returns>
        //[OperationContract]
        //string GetUsrGroupStrc(OperType operType, string KeyWords);

        /// <summary>
        /// 获取用户组所属用户列表(中文字段名)
        /// </summary>
        /// <param name="TempTabNam">临时表名</param>
        /// /// <param name="GrpCode">用户组代号</param>
        /// <returns>用户组列表(中文字段名)</returns>
        [OperationContract]
        string GetGrpUsrsByGrpCodeToChCol(string TempTabNam, string GrpCode,string IfGetFstRow,string PanelNam);
    }


}
