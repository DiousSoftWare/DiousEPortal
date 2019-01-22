using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPortalModel;
using System.Data;
using EPortalCommom;
using System.Configuration;
//测试用
using System.Data.SqlClient;
using System.Dynamic;
namespace EPortalDAL
{
    public class UsrGroup
    {
        public UsrGroup()
        {
            //初始化数据库连接字符串
            SqlHelper.connectionString = ConfigurationManager.ConnectionStrings["Sql"].ToString();
        }

        //定义全局变量


        
        /// <summary>
        /// 获取所有用户组资料(中文字段名)
        /// </summary>
        /// <param name="TempTabNam">临时表名</param>
        /// <returns>用户组列表(中文字段名)</returns>
        public string GetUsrGrpsToChCol(string TempTabNam,string IfGetFstRow,string FltSQL="")
        {
            SqlParameter[] Parames = { new SqlParameter("@TempTabNam", TempTabNam), new SqlParameter("@IfGetFstRow", IfGetFstRow), new SqlParameter("@FltSQL", FltSQL) };
            string SpNam = "Sp_ADMM_GetUsrGrp";
            try
            {
              CommonDAL ComDAL = new CommonDAL();
                //将数据序列化为XML格式
              return Serializer.SerializeDTToXml(ComDAL.GetChColData(SpNam, Parames));
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }


        /// <summary>
        /// 获取用户组所属用户列表(中文字段名)
        /// </summary>
        /// <param name="TempTabNam">临时表名</param>
        /// /// <param name="GrpCode">用户组代号</param>
        /// <returns>用户组列表(中文字段名)</returns>
        public List<DTSerializer<string, object>> GetGrpUsrsByGrpCodeToChCol(string TempTabNam,string GrpCode,string IfGetFstRow,string PanelNam)
        {
            SqlParameter[] Parames = { new SqlParameter("@TempTabNam", TempTabNam), new SqlParameter("@GrpCode", GrpCode),new SqlParameter("@IfGetFstRow", IfGetFstRow),new SqlParameter ("@PanelNam", PanelNam) };
            string SpNam = "Sp_ADMM_GetGrpUsrsByGrpCode";
            try
            {
                CommonDAL ComDAL = new CommonDAL();
                return ComDAL.GetChColData(SpNam, Parames);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return new List<DTSerializer<string, object>>();
        }

        /// <summary>
        /// 获取用户组资料(英文字段名)
        /// </summary>
        /// <returns>用户组列表(英文字段名)</returns>
        public List<DTSerializer<string, object>> GetUsrGrpsToEnCol()
        {
            string SpNam = "SP_ADMM_CrtSQLByTblNmToEnCol";
            SqlParameter[] Parames = { new SqlParameter("@TableName", "t_ADMM_GrpMst"), new SqlParameter("@PanelNam", "Panel5"),new SqlParameter("@IfGetFstRow", "1") };
            try
            {
                CommonDAL ComDAL = new CommonDAL();
                return ComDAL.GetEnColData(CommandType.StoredProcedure, SpNam, Parames);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return new List<DTSerializer<string, object>>();
        }

        
    }
}
