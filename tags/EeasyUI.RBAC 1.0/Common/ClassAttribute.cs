using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EF.Common
{
    /// <summary>
    /// 类属性
    /// Modeule:
    /// ClassName:类名称
    /// MethodName:类的方法名
    /// 通过调用类的构造函数实例化，来获取类的Model（命名空间名）,ClassName（类名），MethodName（方法名）
    /// </summary>
    public class ClassAttribute
    {
        public string Module { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }

        /// <summary>
        /// 方法名的编号id
        /// </summary>
        /// <param name="id"></param>
        public ClassAttribute(string id)
        {
            //初始化方法
            string xmlpath = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Config.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlpath);

            string xpath = string.Concat("//Method[@id='", id, "']");
            XmlNode node = xml.SelectSingleNode(xpath);
            if (node != null)
            {
                XmlNode nodeBR = node.ParentNode;
                if (nodeBR != null)
                {
                    XmlNode nodeModule = nodeBR.ParentNode;
                    if (nodeModule != null)
                    {
                        this.Module = nodeModule.Attributes["name"].Value;
                        this.ClassName = nodeBR.Attributes["name"].Value;
                        this.MethodName = node.Attributes["method"].Value;
                    }
                }
            }
        }
    }
}
