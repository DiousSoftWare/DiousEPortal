using EPortalCommom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace EPortalCommom
{
   public static class Serializer
    {
        /// <summary>
        /// 将数据序列化成为XML格式 
        /// </summary>
        /// <typeparam name="T">序列化数据源类型</typeparam>
        /// <param name="t">序列化数据源</param>
        /// <returns></returns>
        public static string SerializeDTToXml<T>(T t)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, t);
            writer.Close();
            return sb.ToString();
        }


        /// <summary>
        /// 将XML数据反序列化
        /// </summary>
        /// <param name="StrXml">反序列化数据源(XML字符串)</param>
        /// <returns>反序列化后的动态对象列表</returns>
        public static List<ExpandoObject> DeserializeXMLToEObject(string StrXml)
        {
            try
            {
                StringReader strReader = new StringReader(StrXml);
                
                XmlReader xmlReader = XmlReader.Create(strReader);
                XmlSerializer serializer = new XmlSerializer(typeof(List<DTSerializer<string, Object>>));
                List<DTSerializer<string, Object>> List_Dictionary = (List<DTSerializer<string, Object>>)serializer.Deserialize(xmlReader);
                List<ExpandoObject> List_EObj = new List<ExpandoObject>();

                foreach (var DTObj in List_Dictionary)
                {
                    dynamic EObj = new ExpandoObject();
                    var IDObj = (IDictionary<string, object>)EObj;
                    foreach (var a in DTObj)
                    {
                        IDObj[a.Key] = a.Value;
                    }

                    List_EObj.Add(EObj);
                }
                return List_EObj;
            }
            catch (Exception Ex)
            {

               Common.ShowMsg(Ex.Message);
            }
            

            return new List<ExpandoObject>();
        }

        /// <summary>
        /// 将XML数据反序列化成DataTable对象
        /// </summary>
        /// <param name="StrXml">反序列化数据源(XML字符串)</param>
        /// <returns>DataTable对象</returns>
        public static DataTable DeserializeXMLToDT(string StrXml)
        {
            try
            {
                StringReader strReader = new StringReader(StrXml);
                XmlReader xmlReader = XmlReader.Create(strReader);
                XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                DataTable DT = (DataTable)serializer.Deserialize(xmlReader);          
                return DT;
            }
            catch (Exception Ex)
            {

                Common.ShowMsg(Ex.Message);
            }


            return new DataTable();
        }
    }
}
