using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPortalCommom;
using DevExpress.XtraGrid.Views.Grid;
using System.Dynamic;
using DevExpress.XtraEditors;
using System.Reflection;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Controls;

namespace DiousEPortal
{
    public partial class UsrContr_Filter : UserControl
    {

        ////////////////声明全局变量//////////////////
        
        //////////////////////////////////////////////

        /// <summary>
        /// 该控件的父容器
        /// </summary>
        public Control CtrContainer { get; set; }

        /// <summary>
        /// 过滤控件的增加模式;1:人为改变条件触发增加 0：系统自动触发增加
        /// </summary>
        public AddType addType { get; set; }

        /// <summary>
        /// 构造函数1
        /// </summary>
        public UsrContr_Filter(AddType Type)
        {
            addType = Type;
            InitializeComponent();
        }

        /// <summary>
        /// 条件选择值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LkUp_Cdt_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string DisplayText = SLkUp_ColNams.Text;
                if (DisplayText != null && DisplayText.ToString() !="")
                {
                    if (LkUp_Cdt.Text == "从...到..." || LkUp_Cdt.Text == "不在...之间")
                    {
                        if (DisplayText.Contains("日期"))
                        {
                            CBox_CdtValue1.Visible = false;
                            CBox_CdtValue2.Visible = false;
                            CBox_CdtValue3.Visible = false;

                            DEd_CdtValue5.Visible = true;
                            DEd_CdtValue6.Visible = true;
                            DEd_CdtValue3.Visible = false;
                        }
                        else
                        {
                            DEd_CdtValue5.Visible = false;
                            DEd_CdtValue6.Visible = false;
                            DEd_CdtValue3.Visible = false;

                            CBox_CdtValue3.Enabled = true;
                            CBox_CdtValue1.Visible = false;
                            CBox_CdtValue2.Visible = false;
                            CBox_CdtValue3.Visible = true;

                            Common.ShowMsg("只有包含日期的过滤条件才能选择'从...到...'或'不在...之间'");
                            LkUp_Cdt.EditValue = "";
                        }
                    }
                    else if (LkUp_Cdt.Text == "以...开头" || LkUp_Cdt.Text == "以...结尾" || LkUp_Cdt.Text == "在列表中" || LkUp_Cdt.Text == "不在列表中")
                    {
                        if (DisplayText.Contains("日期"))
                        {
                            DEd_CdtValue5.Visible = false;
                            DEd_CdtValue6.Visible = false;
                            DEd_CdtValue3.Visible = true;

                            CBox_CdtValue3.Enabled = false;
                            CBox_CdtValue1.Visible = false;
                            CBox_CdtValue2.Visible = false;
                            CBox_CdtValue3.Visible = false;

                            Common.ShowMsg("包含日期的过滤条件不能选择'以...开头'或'以...结尾'或 '在列表中' 或 '不在列表中'");
                            LkUp_Cdt.EditValue = "";
                            return;
                        }
                        else 
                        {
                            DEd_CdtValue5.Visible = false;
                            DEd_CdtValue6.Visible = false;
                            DEd_CdtValue3.Visible = false;

                            CBox_CdtValue3.Enabled = true;
                            CBox_CdtValue1.Visible = false;
                            CBox_CdtValue2.Visible = false;
                            CBox_CdtValue3.Visible = true;
                            return;
                        }
                    }
                    else if (LkUp_Cdt.Text == "为空" || LkUp_Cdt.Text == "不为空")
                    {
                        DEd_CdtValue5.Visible = false;
                        DEd_CdtValue6.Visible = false;
                        DEd_CdtValue3.Visible = false;

                        CBox_CdtValue1.Visible = false;
                        CBox_CdtValue2.Visible = false;
                        CBox_CdtValue3.Visible = true;
                        CBox_CdtValue3.EditValue = LkUp_Cdt.Text == "为空" ? "为空" : "不为空";
                        CBox_CdtValue3.Enabled = false;
                        return;
                    }
                    else
                    {
                        if (DisplayText.Contains("日期"))
                        {
                            CBox_CdtValue1.Visible = false;
                            CBox_CdtValue2.Visible = false;
                            CBox_CdtValue3.Visible = false;

                            DEd_CdtValue5.Visible = false;
                            DEd_CdtValue6.Visible = false;
                            DEd_CdtValue3.Visible = true;
                            return;
                        }
                        else
                        {
                            CBox_CdtValue3.EditValue = "";

                            CBox_CdtValue3.Enabled = true;
                            CBox_CdtValue1.Visible = false;
                            CBox_CdtValue2.Visible = false;
                            CBox_CdtValue3.Visible = true;

                            DEd_CdtValue5.Visible = false;
                            DEd_CdtValue6.Visible = false;
                            DEd_CdtValue3.Visible = false;
                            return;
                        }


                    }
                }
                else
                {
                    LkUp_Cdt.EditValue = null;
                    Common.ShowMsg("必须先选择字段名称");
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        ///删除条件选择控件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lab_Drop_Click(object sender, EventArgs e)
        {
            try
            {
                CtrContainer = this.Parent;
                //获取当前控件的索引
                int Index = int.Parse(this.Tag.ToString());
                
                if(Index != 2) //不能删除第一个条件选择控件
                {
                    //删除当前控件
                    CtrContainer.Controls.Remove(this);
                    for (int i = Index; i < 18; i++)
                    {
                        if (CtrContainer.Controls["UsrContr_Filter" + (i + 1)] != null)
                        {
                            //获取下个控件的当前位置
                            int PointY = CtrContainer.Controls["UsrContr_Filter" + (i + 1).ToString()].Location.Y;
                            CtrContainer.Controls["UsrContr_Filter" + (i + 1).ToString()].Location = new System.Drawing.Point(3, PointY - 23);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }


        /// <summary>
        /// 字段选择值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SLkUp_ColNams_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LkUp_Cdt.EditValue = " = '";
                string DisplayText = SLkUp_ColNams.Text;
                if (DisplayText.Contains("日期")  && LkUp_Cdt.EditValue !=null)
                {                       
                    if (LkUp_Cdt.Text == "从...到..." || LkUp_Cdt.Text == "不在...之间" )
                    {
                            CBox_CdtValue1.Visible = false;
                            CBox_CdtValue2.Visible = false;
                            CBox_CdtValue3.Visible = false;

                            DEd_CdtValue5.Visible = true;
                            DEd_CdtValue6.Visible = true;
                            DEd_CdtValue3.Visible = false;
                            return;                   
                    }
                    else if (LkUp_Cdt.Text == "以...开头" || LkUp_Cdt.Text == "以...结尾" || LkUp_Cdt.Text == "在列表中" || LkUp_Cdt.Text == "不在列表中")
                    {
                        Common.ShowMsg("包含日期的过滤条件不能选择'以...开头' 或 '以...结尾' 或 '在列表中' 或 '不在列表中'!");

                        CBox_CdtValue1.Visible = false;
                        CBox_CdtValue2.Visible = false;
                        CBox_CdtValue3.Visible = false;

                        DEd_CdtValue5.Visible = false;
                        DEd_CdtValue6.Visible = false;
                        DEd_CdtValue3.Visible = true;

                        //将条件选择控件重置为空
                        LkUp_Cdt.EditValue = "";
                        return;
                    }
                    else
                    {
                        if(LkUp_Cdt.Text == "为空" || LkUp_Cdt.Text == "不为空")
                        {
                            CBox_CdtValue3.Text = LkUp_Cdt.Text == "为空" ? "为空" : "不为空";

                            CBox_CdtValue1.Visible = false;
                            CBox_CdtValue2.Visible = false;
                            CBox_CdtValue3.Visible = true;

                            DEd_CdtValue5.Visible = false;
                            DEd_CdtValue6.Visible = false;
                            DEd_CdtValue3.Visible = false;
                            return;
                        }
                        else 
                        {
                            CBox_CdtValue1.Visible = false;
                            CBox_CdtValue2.Visible = false;
                            CBox_CdtValue3.Visible = false;

                            DEd_CdtValue5.Visible = false;
                            DEd_CdtValue6.Visible = false;
                            DEd_CdtValue3.Visible = true;
                            return;
                        }
                    }
                }
                else if(LkUp_Cdt.EditValue !=null)
                {
                    if (LkUp_Cdt.Text == "从...到..." || LkUp_Cdt.Text == "不在...之间" || LkUp_Cdt.Text == "在列表中" || LkUp_Cdt.Text == "不在列表中")
                    {
                        Common.ShowMsg("只有包含日期的过滤条件才能选择'从...到....' 或'不在....之间' 或 '在列表中' 或 '不在列表中'!");

                        //将条件选择控件重置为空
                        LkUp_Cdt.EditValue = "";

                        CBox_CdtValue3.Enabled = true;
                        CBox_CdtValue1.Visible = false;
                        CBox_CdtValue2.Visible = false;
                        CBox_CdtValue3.Visible = true;

                        DEd_CdtValue5.Visible = false;
                        DEd_CdtValue6.Visible = false;
                        DEd_CdtValue3.Visible = false;
                        return;
                    }
                    else
                    {
                            CBox_CdtValue3.Properties.Items.Clear();
                        if (DisplayText.Contains("审核状态"))
                        {                           
                            CBox_CdtValue3.Properties.Items.Add("未审核");
                            CBox_CdtValue3.Properties.Items.Add("已审核");
                            CBox_CdtValue3.Properties.Items.Add("多级审核中");
                            return;
                        }
                        else if(DisplayText.Contains("是否启用"))
                        {
                            CBox_CdtValue3.Properties.Items.Add("是");
                            CBox_CdtValue3.Properties.Items.Add("否");
                            return;
                        }

                        if(LkUp_Cdt.Text=="为空" || LkUp_Cdt.Text == "不为空")
                        {
                            CBox_CdtValue3.Enabled = false;
                            CBox_CdtValue3.Text = LkUp_Cdt.Text == "为空" ? "为空" : "不为空";
                            return;
                        }
                        else
                        {
                            CBox_CdtValue3.EditValue = "";
                            CBox_CdtValue3.Enabled = true;
                            return;
                        }

                        CBox_CdtValue1.Visible = false;
                        CBox_CdtValue2.Visible = false;
                        CBox_CdtValue3.Visible = true;

                        DEd_CdtValue5.Visible = false;
                        DEd_CdtValue6.Visible = false;
                        DEd_CdtValue3.Visible = false;
                        return;
                    }
                }
                else
                {
                    CBox_CdtValue3.Properties.Items.Clear();
                    if (DisplayText.Contains("审核状态"))
                    {
                        CBox_CdtValue3.Properties.Items.Add("未审核");
                        CBox_CdtValue3.Properties.Items.Add("已审核");
                        CBox_CdtValue3.Properties.Items.Add("多级审核中");

                        CBox_CdtValue1.Visible = false;
                        CBox_CdtValue2.Visible = false;
                        CBox_CdtValue3.Visible = true;

                        DEd_CdtValue5.Visible = false;
                        DEd_CdtValue6.Visible = false;
                        DEd_CdtValue3.Visible = false;
                        return;
                    }
                    else if(DisplayText.Contains("是否启用"))
                    {
                        CBox_CdtValue3.Properties.Items.Add("是");
                        CBox_CdtValue3.Properties.Items.Add("否");

                        CBox_CdtValue1.Visible = false;
                        CBox_CdtValue2.Visible = false;
                        CBox_CdtValue3.Visible = true;

                        DEd_CdtValue5.Visible = false;
                        DEd_CdtValue6.Visible = false;
                        DEd_CdtValue3.Visible = false;
                        return;
                    }
                }

            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsrContr_Filter_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message );
            }
        }

        /// <summary>
        /// 关系选择值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LkUp_Rlt_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (addType == AddType.Manual)
                {
                    //获取当前控件的父容器
                    CtrContainer = (Control)this.Parent;
                    //获取当前控件的父窗体
                    FrmFilter ParentForm = (FrmFilter)this.ParentForm;
                    if (CtrContainer != null && SLkUp_ColNams.EditValue != null)
                    {
                        UsrContr_Filter Filter = new UsrContr_Filter(AddType.Manual);
                        Filter.Size = new System.Drawing.Size(808, 25);
                        Filter.Tag = int.Parse(this.Tag.ToString()) + 1;
                        Filter.Name = "UsrContr_Filter" + (int.Parse(this.Tag.ToString()) + 1).ToString();
                        //先检查原面板内是否包含该控件，避免重复添加
                        var Qurey = from Control Contr in CtrContainer.Controls
                                    where Contr.Name == Filter.Name
                                    select Contr;
                        if (Qurey.Count<Control>() == 0)
                        {
                            ParentForm.InitLkUp(Filter);
                            CtrContainer.Controls.Add(Filter);
                        }

                        int Y = this.Location.Y + 23;
                        Filter.Location = new System.Drawing.Point(0, Y);
                    }
                    else
                    {
                        Common.ShowMsg("字段名称不能为空！");
                    }
                }              
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

       
    }
}
