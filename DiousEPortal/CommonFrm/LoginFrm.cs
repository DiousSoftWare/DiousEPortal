using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPortalBLL;
using EPortalCommom;
using EPortalModel;
using System.Configuration;
using EPortalService;
namespace DiousEPortal
{
    public partial class LoginFrm : DevExpress.XtraEditors.XtraForm
    {
        public LoginFrm()
        {
            InitializeComponent();
        }

        ///////////////////////////定义全局变量///////////////////////////////
        private MainFrm MainFrm1;
       public EPortalService.LoginServiceClient Client;
        /////////////////////////////////////////////////////////////////////


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            Client = new EPortalService.LoginServiceClient();
            //调用UsrVal()方法，0表示用户名或密码为空，1表示登录成功，2表示用户名或密码错误
            if (Client.UsrVal(Txt_UsrName.Text, Txt_Pwd.Text) ==1)
            {
                //隐藏当前窗体
                this.Hide();
              
                MainFrm1.IfSupper =Client.GetSupperUsr();
                //初始化主窗体WCF客户端
                MainFrm1.Client = Client;
                //初始化当前登录用户信息
                T_ADMM_UsrMst User = Client.GetUsrByUsrID(Txt_UsrName.Text);
                MainFrm1.CurUsrID = User.FUsrID;
                MainFrm1.CurUsrName = User.FUsrName;

                //显示主窗体
                MainFrm1.ShowDialog();
            }
            else if (Client.UsrVal(Txt_UsrName.Text, Txt_Pwd.Text) == 0)
            {
                Common.ShowMsg("用户名或密码为空！");
            }
            else if (Client.UsrVal(Txt_UsrName.Text, Txt_Pwd.Text) == 2)
            {
                Common.ShowMsg("用户名或密码错误！");
            }
            else
            {

                Common.ShowMsg("登录失败！");
            }

        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_LoginOff_Click(object sender, EventArgs e)
        {
            //关闭WCF客户端连接
            Client.Close();
            this.Close();
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            MainFrm1 = new DiousEPortal.MainFrm();
        }

        private void LoginFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}

