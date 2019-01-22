using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPortalCommom
{
    /// <summary>
    /// 窗体工具条按钮名称
    /// </summary>
    public enum BtnNam
    {
       Add, Copy, Edit, Delete, Unapprove, Approve, Filter, Enable,Disable, Exit, Save, Cancel ,Null  
    }

    /// <summary>
    /// 按钮审核或启用状态
    /// </summary>
    public enum State
    {
       //已审核
       Check,
       //未审核
       Uncheck,
       //已启用
       Enable,
       //未启用
       Disable
    }

    /// <summary>
    /// 数据操作类型
    /// </summary>
    public enum OperType
    {
        //增加保存
        AddSave,
        //复制保存
        CopySave,
        //修改保存
        EditSave,
        //删除
        Delete,
        //审核
        Approve,
        //反审核
        UnApprove,
        //禁用
        Disable,
        //启用
        Enable,
        //覆盖过滤方案
        CoverSlt,
        //初始化过滤器
        InitFilter,
        //显示过滤器
        ShowFilter
    }

    /// <summary>
    /// 控制控件生效或失效
    /// </summary>
    public enum EDabType
    {
        //使控件生效
        Enable,
        //使控件失效
        Disable
    }

    /// <summary>
    /// 过滤条件控件增加模式
    /// </summary>
    public enum AddType
    {
        //使控件生效
        Manual,
        //使控件失效
        Auto
    }


}
