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

namespace EPortalAPIService
{
    public partial class EAPAPIService: IEAPAPI
    {
        public EAPAPIBLL eAPAPIBLL { get; set; }
        public EAPAPIService()
        {
            try
            {
                eAPAPIBLL = new EAPAPIBLL();
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
              return  eAPAPIBLL.GetStateByUsrID(UsrID);
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return "";
        }
    }
}