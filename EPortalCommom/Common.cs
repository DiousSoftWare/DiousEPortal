using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System.Dynamic;

namespace EPortalCommom
{
    public static class Common
    {

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="Msg"></param>
        public static void ShowMsg(string Msg)
        {
            XtraMessageBox.Show(Msg);
        }

    }

    /// <summary>
    /// 数据转换类
    /// </summary>
    public class DataTransform
    {
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="StrXml">需要反序列化的Xml格式数据</param>
        /// <returns></returns>
        public List<ExpandoObject> LoadData(string StrXml = "")
        {
            return Serializer.DeserializeXMLToEObject(StrXml);
        }
    }
}
