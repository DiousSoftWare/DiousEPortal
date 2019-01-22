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
    public partial class EPortalService : ILoginService
    {
        /////////////////////////声明全局变量////////////////////////////
        public string IfSupper;
        ////////////////////////////////////////////////////////////////


        /// <summary>
        /// 获取系统功能菜单
        /// </summary>
        /// <returns>功能菜单信息列表</returns>
        public List<T_ADMM_FuncList> GetMenus()
        {
            Login Login1 = new Login();
            return Login1.GetMenus();
        }

        /// <summary>
        /// 登录用户验证
        /// </summary>
        /// <param name="UsrID">用户ID</param>
        /// <param name="PassWord">用户密码</param>
        /// <returns>true:登录成功;false:登录失败</returns>
        public int UsrVal(string UsrID, string PassWord)
        {
            try
            {
                Login Login1 = new Login();
                IfSupper = Login1.IfSupper;
                return Login1.UsrVal(UsrID, PassWord);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 获取超级管理员信息
        /// </summary>
        /// <returns></returns>
        public string GetSupperUsr()
        {
            return IfSupper;
        }

        /// <summary>
        /// 根据用户ID获取用户对象
        /// </summary>
        /// <param name="UsrID">用户ID</param>
        /// <returns>用户对象</returns>
        public T_ADMM_UsrMst GetUsrByUsrID(string UsrID)
        {
            try
            {
                Login Login1 = new Login();
               return Login1.GetUsrByUsrID(UsrID);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new T_ADMM_UsrMst();
        }
    }
}