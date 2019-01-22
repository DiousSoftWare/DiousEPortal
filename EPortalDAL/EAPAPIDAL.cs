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
    public class EAPAPIDAL
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        public EAPAPIDAL()
        {
            try
            {
                //初始化数据库连接字符串
                SqlHelper.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Sql"].ToString();
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 根据用户登录账号获取其启用状态
        /// </summary>
        /// <param name="UsrID">用户登录账号</param>
        /// <returns>1：已启用；0：未启用</returns>
        public string GetStateByUsrID(string UsrID)
        {
            try
            {
                string Sql = "select fIfUse from t_ADMM_UsrMst with(nolock) where fUsrID='" + UsrID.Trim()+"'";
                using (DataTable DT = SqlHelper.ExecuteDataSet(CommandType.Text, Sql, null).Tables[0])
                {
                   foreach(DataRow Row in DT.Rows)
                    {
                       return Row["fIfUse"].ToString();                      
                    }
                }
            }
            catch (Exception Ex)
            {
               Common.ShowMsg(Ex.Message) ;
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
                    return SerializeDataTableToXML(DT);
                }
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
        public string SerializeDataTableToXML(DataTable DT)
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
    }
}
