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
namespace EPortalBLL
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class Login
    {
        //定义全局变量
        public string IfSupper="";

        /// <summary>
        /// 用户名密码验证
        /// </summary>
        /// <param name="UsrID">用户名</param>
        /// <param name="PassWord">密码</param>
        public int UsrVal(string UsrID, string PassWord)
        {
            try
            {
                //判断用户名或密码是否为空
                if (UsrID == "" || PassWord == "")
                {
                    return 0;
                }
                else
                {
                    EPortalDAL.Login Login = new EPortalDAL.Login();
                    T_ADMM_UsrMst Usr = GetUsrByUsrID(UsrID);
                    if (UsrID == Usr.FUsrID && PassWord == Usr._x_f009)
                    {
                        IfSupper = Usr.FIfSupper;
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }                  
                }              
            }
            catch(Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return 2;
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
                EPortalDAL.Login Login = new EPortalDAL.Login();
                return Login.GetUser(UsrID);
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            return new T_ADMM_UsrMst();
        }

        //测试
        public DataSet ExecuteDataSet()
        {
            EPortalDAL.Login Login = new EPortalDAL.Login();
           return  Login.ExecuteDataSet();
        }

        /// <summary>
        /// 获取功能菜单
        /// </summary>
        /// <returns></returns>
        public List<T_ADMM_FuncList> GetMenus()
        {
            EPortalDAL.Login Login = new EPortalDAL.Login();
            List<T_ADMM_FuncList> FuncList = new List<T_ADMM_FuncList>();
            try
            {
                FuncList= Login.GetMenus();
            }
            catch(Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            return FuncList;
        }
    }
}
