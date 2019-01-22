using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors;
using EPortalCommom;
using EPortalModel;
using EPortalBLL;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTab;
using System.Reflection;
using EPortalService;
namespace DiousEPortal
{

    public partial class MainFrm : DevExpress.XtraEditors.XtraForm
    {
       
        public MainFrm()
        {
            InitializeComponent();
        }

        ///////////////////////////定义全局变量///////////////////////////////
        public string IfSupper;
        public TreeList TrList_Menus;
        public EPortalService.LoginServiceClient Client;
        //新建容器列表
        List<NavBarGroupControlContainer> List_Container = new List<NavBarGroupControlContainer>();
        public string CurUsrID; //当前登录的用户ID
        public string CurUsrName;//当前登录的用户名
        ///////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 主窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void MainFrm_Load(object sender, EventArgs e)
        {
            try
            {
                //加载菜单
                LoadMenus(Client.GetMenus().ToList<T_ADMM_FuncList>());
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }
        #endregion

        #region
        /// <summary>
        /// 根据权限列表加载菜单
        /// </summary>
        /// <param name="FuncList">功能权限列表</param>
        private void LoadMenus(List<T_ADMM_FuncList> FuncList)
        {
            try
            {
                if (FuncList.Count > 0 && FuncList != null)
                {
                    //将所有一级菜单统计出来
                    var query = from Func in FuncList
                                where Func.Levels == 0
                                select Func;

                    int i = 0;                                
                    //逐一将一级菜单加入分组
                    foreach (var Func1 in query)
                    {
                        NavBarGroup NBG1 = new NavBarGroup();
                        NBG1.Caption = Func1.FuncNam;

                        //新建树菜单
                        TrList_Menus = new DevExpress.XtraTreeList.TreeList();
                        TrList_Menus.Dock = System.Windows.Forms.DockStyle.Fill;
                        TrList_Menus.Name = "TrList_"+Func1.GroupID;
                        //新建树菜单列
                        DevExpress.XtraTreeList.Columns.TreeListColumn Col_FuncID=new DevExpress.XtraTreeList.Columns.TreeListColumn();
                        Col_FuncID.Name = "FuncID";
                        Col_FuncID.FieldName = "FuncID";
                        Col_FuncID.Caption = "FuncID";
                        
                        DevExpress.XtraTreeList.Columns.TreeListColumn Col_ParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                        Col_ParentID.Name = "ParentID";
                        Col_ParentID.Caption = "ParentID";
                        Col_ParentID.FieldName = "ParentID";
                     
                        DevExpress.XtraTreeList.Columns.TreeListColumn Col_FuncNam = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                        Col_FuncNam.Name = "FuncNam";
                        Col_FuncNam.Caption = "FuncNam";
                        Col_FuncNam.FieldName = "FuncNam";
                        Col_FuncNam.Visible = true;
                        Col_FuncNam.VisibleIndex = 2;

                        DevExpress.XtraTreeList.Columns.TreeListColumn Col_FrmNam = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                        Col_FrmNam.Name = "FrmNam";
                        Col_FrmNam.Caption = "FrmNam";
                        Col_FrmNam.FieldName = "FrmNam";

                        //将列添加到树菜单
                        TrList_Menus.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
                        Col_FuncID,
                        Col_ParentID,
                        Col_FuncNam,
                        Col_FrmNam}
                        );
                        TrList_Menus.KeyFieldName = "FuncID";
                        TrList_Menus.ParentFieldName = "ParentID";
                        TrList_Menus.OptionsView.ShowColumns = false;
                        TrList_Menus.OptionsBehavior.Editable = false;
                        ////设置树菜单背景色
                        //TrList_Menus.BackColor = Color.White;
                        ////设置树菜单空白区域的背景色
                        //TrList_Menus.Appearance.Empty.BackColor = Color.Transparent;
                        //设置树菜单节点竖向边框不显示
                        TrList_Menus.OptionsView.ShowVertLines = false;
                        //设置树菜单节点横向边框不显示
                        TrList_Menus.OptionsView.ShowHorzLines = false;
                        //设置树菜单节点的字体大小
                        TrList_Menus.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
                        //TrList_Menus.OptionsView.ShowIndentAsRowStyle = true;

                        ////设置获得焦点单元格背景色
                        //TrList_Menus.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSteelBlue;
                        //TrList_Menus.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.SteelBlue;

                        //TrList_Menus.Appearance.FocusedCell.Options.UseBackColor = true;
                        //TrList_Menus.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
                        //TrList_Menus.LookAndFeel.UseDefaultLookAndFeel = false;

                        //注册鼠标双击事件
                        if (Func1.GroupID =="ADM")
                        {
                            TrList_Menus.DoubleClick += new System.EventHandler(TrList_ADM_DoubleClick);
                        }
                        else if (Func1.GroupID == "Test")
                        {
                            TrList_Menus.DoubleClick += new System.EventHandler(TrList_Test_DoubleClick);
                        }
                        else if(Func1.GroupID == "test1")
                        {
                            TrList_Menus.DoubleClick += new System.EventHandler(TrList_Test1_DoubleClick);
                        }

                        List_Container.Add(new NavBarGroupControlContainer());
                        List_Container[i].Name = "Contain_" + Func1.GroupID;

                        //将容器加入导航控件
                        Nav_ADM.Controls.Add(List_Container[i]);

                        //将树菜单加入容器
                        List_Container[i].Controls.Add(TrList_Menus);

                        //将容器加入菜单组
                        NBG1.ControlContainer = List_Container[i];

                        //将菜单组加入导航控件
                        Nav_ADM.Groups.Add(NBG1);

                        //将该菜单组所有的子菜单全部统计出来
                        var query1 = from Func in FuncList
                                     where Func.GroupID == Func1.GroupID
                                     select new { FuncID=Func.FuncID,ParentID=Func.ParentID,FuncNam=Func.FuncNam,FrmNam=Func.FrmNam} into  query2
                                     select query2;

                        //将查询结果转换为集合
                        List<MainMenus> ListTreeData = new List<MainMenus>();
                        foreach (var TreeData1 in query1)
                        {
                            MainMenus TreeData = new MainMenus();
                            TreeData.FuncID = TreeData1.FuncID;
                            TreeData.ParentID = TreeData1.ParentID;
                            TreeData.FuncNam = TreeData1.FuncNam;
                            TreeData.FrmNam = TreeData1.FrmNam;

                            ListTreeData.Add(TreeData);
                        }
                        //树控件绑定数据源 
                        TrList_Menus.DataSource = ListTreeData;
                        i += 1;
                    }
                }
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }
        #endregion


        #region
        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region
        /// <summary>
        /// 系统管理模块双击节点事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrList_ADM_DoubleClick(object sender,EventArgs e)
        {
            //加载功能菜单对应的窗体
            LoadFrm("Contain_ADM", "TrList_ADM");
        }
        #endregion
       
        #region
        /// <summary>
        /// 测试管理模块双击节点事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrList_Test_DoubleClick(object sender, EventArgs e)
        {
            //加载功能菜单对应的窗体
            LoadFrm("Contain_Test", "TrList_Test");
        }
        #endregion

        #region
        /// <summary>
        /// 测试管理1模块双击节点事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrList_Test1_DoubleClick(object sender, EventArgs e)
        {
            //加载功能菜单对应的窗体
            LoadFrm("Contain_test1", "TrList_test1");
        }
        #endregion

        #region
        /// <summary>
        /// 加载功能菜单对应的窗体
        /// </summary>
        /// <param name="CtnerNam">树菜单对应的容器名称</param>
        /// <param name="TrName">树菜单名称</param>
        /// <param name="Frm">XtraTabPage中装载的窗体</param>
        private void LoadFrm(string CtnerNam,string TrName)
        {
            try
            { 
            //获取测试管理模块容器
            var query = from Container1 in List_Container
                        where Container1.Name == CtnerNam
                        select Container1;
            NavBarGroupControlContainer NavContainer = null;
            foreach (var Container2 in query)
            {
                NavContainer = Container2 as NavBarGroupControlContainer;
            }
            //获取容器中的菜单树
            TreeList TrList_ADM = NavContainer.Controls[TrName] as TreeList;
            DevExpress.XtraTab.XtraTabPage Page = new DevExpress.XtraTab.XtraTabPage();
            
            //页签标题设置
            Page.Text = TrList_ADM.FocusedNode.GetValue("FuncNam").ToString();

                //判断将要打开的页签是否已经存在
                if (!PageIsExist(Page))
                {
                    //利用反射实例化窗体
                    Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
                    FrmCommon Frm = assembly.CreateInstance(TrList_ADM.FocusedNode.GetValue("FrmNam").ToString()) as FrmCommon;
                    Page.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;               
                    //指定该窗体是有父窗体的窗体(非顶级窗体)。
                    Frm.TopLevel = false;
                    //初始化窗体实例名称
                    Frm.FrmNam = TrList_ADM.FocusedNode.GetValue("FrmNam").ToString();
                    //初始化当前登录用户信息
                    Frm.CurUsrID = CurUsrID;
                    Frm.CurUsrName = CurUsrName;
                    Frm.Dock = DockStyle.Fill;

                    //去边框处理
                    Frm.FormBorderStyle = FormBorderStyle.None;
                    //将窗体装载入页面中
                    Page.Controls.Add(Frm);
                    //增加新页面
                    Tab_Main.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { Page });
                    //实例化关闭页面委托
                    Frm.PgClose = new FrmCommon.PageClose(ClosePage);
                    //将窗体显示出来。
                    Frm.Show();
                    //新页面增加后自动定位到该页面
                    Tab_Main.SelectedTabPage = Page;
                }
            }
            catch(Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
        }

        /// <summary>
        /// 判断将要打开页签是否已经存在
        /// </summary>
        /// <param name="Page">将要打开的页签</param>
        /// <returns>存在返回true,不存在返回false</returns>
        private bool PageIsExist(XtraTabPage Page)
        {
            try
            {
                foreach (XtraTabPage Page1 in Tab_Main.TabPages)
                {
                    if (Page.Text == Page1.Text)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 关闭指定的页签
        /// </summary>
        /// <param name="PageText">页签的Text属性</param>
        public void ClosePage()
        {
            try
            {
                string PageText = Tab_Main.SelectedTabPage.Text;
                var query =
                    from Page in Tab_Main.TabPages
                    where Page.Text == PageText
                    select Page;

                for (int i= query.Count<XtraTabPage>()-1; i >-1; i--)
                {
                    XtraTabPage Page1 = query.ElementAt<XtraTabPage>(i);
                    //不能关闭首页
                    if (Page1.Text != "主面板")
                    {
                        Tab_Main.TabPages.Remove(Page1);
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }
            
        }
        #endregion

        private void MainFrm_SizeChanged(object sender, EventArgs e)
        {
            if(WindowState==FormWindowState.Minimized)
            {
                //this.ShowInTaskbar = false;
                Noti_System.Visible = true;
            }
        }

        /// <summary>
        /// 关闭当前选择的页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_Main_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                ClosePage();
            }
            catch (Exception Ex)
            {
                Common.ShowMsg(Ex.Message);
            }
            
        }
    }
}