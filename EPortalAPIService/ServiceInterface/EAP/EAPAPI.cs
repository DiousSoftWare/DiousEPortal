using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace EPortalAPIService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IEAPAPI
    {

        /// <summary>
        /// 根据用户登录账号获取其启用状态
        /// </summary>
        /// <param name="UsrID">用户登录账号</param>
        /// <returns>1：已启用；0：未启用</returns>
        [OperationContract]
        string GetStateByUsrID(string UsrID);
        
    }


    
}
