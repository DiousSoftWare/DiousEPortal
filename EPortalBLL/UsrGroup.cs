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
    public class UsrGroup
    {
        /// <summary>
        /// 获取所有用户组资料(中文字段名)
        /// </summary>
        /// <returns>用户组资料列表清单(中文字段名)</returns>
        public string GetUsrGrpsToChCol(string TempTabNam,string IfGetFstRow,string FltSQL="")
        {
            try
            {
                EPortalDAL.UsrGroup UsrGr = new EPortalDAL.UsrGroup();
                return UsrGr.GetUsrGrpsToChCol(TempTabNam, IfGetFstRow, FltSQL);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 获取所有用户组资料(英文字段名)
        /// </summary>
        /// <returns>用户组资料列表清单(英文字段名)</returns>
        public List<DTSerializer<string, object>> GetUsrGrpsToEnCol()
        {
            try
            {
                EPortalDAL.UsrGroup UsrGr = new EPortalDAL.UsrGroup();
                return UsrGr.GetUsrGrpsToEnCol(); 
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new List<DTSerializer<string, object>>();
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
                EPortalDAL.UsrGroup UsrGr = new EPortalDAL.UsrGroup();
                //将数据序列化为XML格式
                string StrXml = Serializer.SerializeDTToXml(UsrGr.GetGrpUsrsByGrpCodeToChCol(TempTabNam, GrpCode, IfGetFstRow, PanelNam));
                return StrXml;
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return "";
        }
     }
}
