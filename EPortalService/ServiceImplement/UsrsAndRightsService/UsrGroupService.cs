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
namespace EPortalService
{
    public partial class EPortalService: IUsrGroupService
    {
        /// <summary>
        /// 获取所有用户组资料序列化数据(中文字段名)
        /// </summary>
        /// <returns>用户组资料序列化数据(中文字段名)</returns>
        public string GetUsrGrpsToChCol(string TempTabNam,string IfGetFstRow,string FltSQL="")
        {
            try
            {
                UsrGroup UsrGrp = new UsrGroup();
                return UsrGrp.GetUsrGrpsToChCol(TempTabNam,IfGetFstRow, FltSQL);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.ToString());
            }
            return "";
            
        }

        /// <summary>
        /// 获取用户组所属用户列表(中文字段名)
        /// </summary>
        /// <param name="TempTabNam">临时表名</param>
        /// /// <param name="GrpCode">用户组代号</param>
        /// <returns>用户组列表(中文字段名)</returns>
        public string GetGrpUsrsByGrpCodeToChCol(string TempTabNam, string GrpCode,string IfGetFstRow,string PanelNam)
        {
            try
            {
                UsrGroup UsrGrp = new UsrGroup();
                return UsrGrp.GetGrpUsrsByGrpCodeToChCol(TempTabNam, GrpCode, IfGetFstRow, PanelNam);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }


    }
}