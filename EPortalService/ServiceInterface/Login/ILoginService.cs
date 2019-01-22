using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EPortalModel;
namespace EPortalService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface ILoginService
    {
        /// <summary>
        /// 获取系统功能菜单
        /// </summary>
        /// <returns>功能菜单信息列表</returns>
        [OperationContract]
        List<T_ADMM_FuncList> GetMenus();
        // TODO: 在此添加您的服务操作

        /// <summary>
        /// 登录用户验证
        /// </summary>
        /// <param name="UsrID">用户ID</param>
        /// <param name="PassWord">用户密码</param>
        /// <returns>true:登录成功;false:登录失败</returns>
        [OperationContract]
        int UsrVal(string UsrID, string PassWord);

        /// <summary>
        /// 获取超级管理员信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetSupperUsr();

        /// <summary>
        /// 根据用户ID获取用户对象
        /// </summary>
        /// <param name="UsrID">用户ID</param>
        /// <returns>用户对象</returns>
        [OperationContract]
        T_ADMM_UsrMst GetUsrByUsrID(string UsrID);
    }
}
