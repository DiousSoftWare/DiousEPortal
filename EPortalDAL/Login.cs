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

namespace EPortalDAL
{
   public class Login
    {
        public Login()
        {
            //初始化数据库连接字符串
            SqlHelper.connectionString = ConfigurationManager.ConnectionStrings["Sql"].ToString();
        }
       

        /// <summary>
        /// 根据传入的用户名返回用户对象
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns>用户对象</returns>
        public T_ADMM_UsrMst GetUser(string UsrID)
        {
            T_ADMM_UsrMst Usr = new T_ADMM_UsrMst();          
            string Sql = "select fUsrID,FUsrName,_x_f009 from [t_ADMM_UsrMst] with(nolock) where fUsrID='" + UsrID+"'";
            try
            {   
              if (SqlHelper.ExecuteDataSet(CommandType.Text, Sql, null).Tables[0].Rows.Count>0)
                {
                    using (DataTable D1= SqlHelper.ExecuteDataSet(CommandType.Text, Sql, null).Tables[0])
                    {
                        //初始化用户ID、用户名、密码
                        Usr.FUsrID= D1.Rows[0]["fUsrID"].ToString();
                        Usr.FUsrName = D1.Rows[0]["FUsrName"].ToString();
                        Usr._x_f009=D1.Rows[0]["_x_f009"].ToString();
                    }                       
                }
               else
                {
                    return Usr;
                }
              
            }
            catch(Exception Ex)
            {
                //将异常信息显示出来
                 Common.ShowMsg(Ex.Message);
            }
            return Usr;       
        }

        //测试
        public DataSet ExecuteDataSet()
        {
            string Sql = "Sp_Do_StkQuery";
            SqlParameter[] GoodsNamLike = { new SqlParameter("@fGoodsNameLike", SqlDbType.VarChar) };
            GoodsNamLike[0].Value = "S106D16H";

            DataSet DS = new DataSet();
            DS = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, Sql, GoodsNamLike);
            return DS;
        }

        /// <summary>
        /// 加载功能菜单
        /// </summary>
        /// <returns></returns>
        public List<T_ADMM_FuncList> GetMenus()
        {
            string Sql = "select * from [t_ADMM_FuncList] with(nolock)";
            List<T_ADMM_FuncList> FuncList = new List<T_ADMM_FuncList>();
            try
            {          
                if (SqlHelper.ExecuteDataSet(CommandType.Text, Sql, null).Tables[0].Rows.Count > 0)
                {
                    using (DataTable DT= SqlHelper.ExecuteDataSet(CommandType.Text, Sql, null).Tables[0])
                    {
                        foreach(DataRow Row in DT.Rows  )
                        {
                            T_ADMM_FuncList Func = new T_ADMM_FuncList();
                            Func.FrmNam = Row["FrmNam"].ToString();
                            Func.FuncID = Row["FuncID"].ToString();
                            Func.FuncNam = Row["FuncNam"].ToString();
                            Func.Levels = int.Parse(Row["Levels"].ToString());
                            Func.ParentID = Row["ParentID"].ToString();
                            Func.ParentNam = Row["ParentNam"].ToString();
                            Func.GroupID= Row["GroupID"].ToString();
                            FuncList.Add(Func);                         
                        }
                    }
                }
                else
                {
                    Common.ShowMsg("该用户未配置权限！");
                }
                return FuncList;
            }
            catch (Exception Ex)
            {
                //将异常信息显示出来
                Common.ShowMsg(Ex.Message);
            }
            return FuncList;
        }
    }
}
