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
using DevExpress.XtraEditors;
namespace DiousEPortal
{
    public partial class FrmSaveAs : XtraForm
    {
        public FrmSaveAs()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置当前登录用户ID
        /// </summary>
        public string CurUsrID { get; set; }

        /// <summary>
        /// 获取或设置当前登录用户名称
        /// </summary>
        public string CurUsrName { get; set; }

        public EPortalService.CommonClient ComClient { get; set; }

        /// <summary>
        /// 过滤窗口关联的窗体名称
        /// </summary>
        public string FrmNam { get; set; }

        /// <summary>
        /// 过滤方案明细
        /// </summary>
        public List<DTSerializer<string, object>> FltSltDtl { get; set; }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSaveAs_Load(object sender, EventArgs e)
        {
            try
            {
                //加载时默认选择私有
                RBtn_Private.Checked = true;
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 私有选项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtn_Private_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(RBtn_Share.Checked == false)
                {
                    Pal_AppRange.Enabled = false;

                }
                else if(RBtn_Private.Checked==false)
                {
                    Pal_AppRange.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }


        /// <summary>
        /// 保存过滤方案
        /// </summary>
        /// <param name="operType">操作类型</param>
        /// <param name="TabName">表名称</param>
        /// <param name="ParentID">父方案ID</param>
        /// <param name="SltType">过滤方案类型</param>
        /// <param name="KeyWords">查询关键字</param>
        private void AddFltSlt(OperType operType, string ParentID,string SltType,string KeyWords,List<DTSerializer<string, object>> List_DTObj)
        {
            try
            {
                //获取当前最大的方案ID
                string MaxSltID = ComClient.GetMaxSltID(FrmNam, SltType, ParentID);
                DTSerializer<string, object> DTObj1 = new DTSerializer<string, object>();
                List<DTSerializer<string, object>> List_DTObj1 = new List<DTSerializer<string, object>>();
                DTObj1.Add("FSltID", MaxSltID);
                DTObj1.Add("FSltName", Txt_SltName.Text.Trim());
                DTObj1.Add("FParentID", ParentID);
                DTObj1.Add("FUsrID", CurUsrID);
                DTObj1.Add("FFrmName", FrmNam);
                DTObj1.Add("FSltType", SltType);
                List_DTObj1.Add(DTObj1);

                //开始SQL事务
                ComClient.BeginTransaction();
                ComClient.SaveFltSlt1(operType, Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj1), "t_ADMM_FltSltList", KeyWords);               

                //向查询方案明细中追加方案ID和方案类型
                foreach (DTSerializer<string, object> DTObj2 in List_DTObj)
                {
                    DTObj2.Add("FSltID", MaxSltID);
                    DTObj2.Add("FSltType", SltType);
                }

                //增加/修改/删除过滤方案明细
                ComClient.SaveFltSlt1(operType, Serializer.SerializeDTToXml<List<DTSerializer<string, object>>>(List_DTObj), "t_ADMM_FltSltforDtl", KeyWords);
                //提交事务
                ComClient.CommitTransaction();
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txt_SltName.Text.Trim() != "" && RBtn_Private.Checked == true)
                {
                    AddFltSlt(OperType.AddSave, "P01", "0", "", FltSltDtl);
                    this.Close();
                }
                else if (Txt_SltName.Text.Trim() != "" && RBtn_Share.Checked == true)
                {
                    AddFltSlt(OperType.AddSave, "S01", "0", "", FltSltDtl);
                    this.Close();
                }
                else
                {
                    Common.ShowMsg("方案名称不能为空！");
                }
              
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
