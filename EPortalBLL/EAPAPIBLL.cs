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
    public class EAPAPIBLL
    {
        public EAPAPIDAL eAPAPIDAL { get; set; }


        /// <summary>
        /// 构造方法
        /// </summary>
        public EAPAPIBLL()
        {
            try
            {
                eAPAPIDAL = new EAPAPIDAL();
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
               return eAPAPIDAL.GetStateByUsrID(UsrID);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }
    }
}
