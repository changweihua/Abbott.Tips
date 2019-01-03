using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;

namespace Abbott.Tips.Framework.Util
{
    public class XmlUtil
    {
        /// <summary>
        /// XML文件
        /// </summary>
        private XElement root;

        /// <summary>
        /// 私有构造函数，必须传入参数
        /// </summary>
        private XmlUtil()
        {

        }

        /// <summary>
        /// 有参构造函数，初始化root对象
        /// </summary>
        /// <param name="file">XML文件路径或者XML字符串</param>
        /// <param name="xmlType"></param>
        public XmlUtil(string file, XmlType xmlType)
        {
            switch (xmlType)
            {
                case XmlType.File:
                    root = XElement.Load(file);
                    break;
                case XmlType.Text:
                    root = XElement.Parse(file);
                    break;
                default:
                    root = null;
                    break;
            }
        }


        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="IsChoosenAll">是否获取直接子节点,true表示获取直接子节点</param>
        /// <returns></returns>
        public IEnumerable<XElement> GetChildNodes(bool IsChoosenAll)
        {
            IEnumerable<XElement> childNodes = null;

            if (IsChoosenAll)
            {
                childNodes = root.Elements();
            }
            else
            {
                childNodes = root.Descendants();
            }

            return childNodes;

        }



        /// <summary>
        /// 返回父节点
        /// </summary>
        /// <param name="IsContainSelf">是否包含自身，true表示包含自身</param>
        /// <returns></returns>
        public IEnumerable<XElement> GetParentNodes(bool IsContainSelf)
        {
            IEnumerable<XElement> parentNodes = null;

            if (IsContainSelf)
            {
                parentNodes = root.AncestorsAndSelf();
            }
            else
            {
                parentNodes = root.Ancestors();
            }

            return parentNodes;

        }


        /// <summary>
        /// 获取元素自身之前的所有元素
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public IEnumerable<XElement> GetNextNodes(XElement element)
        {
            return element.ElementsAfterSelf();
        }

        /// <summary>
        /// 获取元素自身之后的所有元素
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public IEnumerable<XElement> GetPrevNodes(XElement element)
        {
            return element.ElementsBeforeSelf();
        }


        /// <summary>
        /// 获取节点元素
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public XElement GetNode(string nodeName)
        {
            XElement element = null;

            element = root.Element(nodeName);

            return element;
        }


        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="element">元素节点</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="content">节点内容</param>
        /// <param name="position">插入节点位置</param>
        public void AddNode(XElement element, string nodeName, string content, InsertNodePostion position)
        {
            XElement elem = null;
            if (string.IsNullOrEmpty(content))
            {
                elem = new XElement(nodeName);
            }
            else
            {
                elem = new XElement(nodeName, content);
            }

            switch (position)
            {
                case InsertNodePostion.Add:
                    element.Add(elem);
                    break;
                case InsertNodePostion.AddFirst:
                    element.AddFirst(elem);
                    break;
                case InsertNodePostion.AddAfterSelf:
                    element.AddAfterSelf(elem);
                    break;
                case InsertNodePostion.AddBeforeSelf:
                    element.AddBeforeSelf(elem);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="nodeName">节点名称，如果为null或empty，则删除所有元素</param>
        public void RemoveNode(string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName))
            {
                root.RemoveAll();
            }
            else
            {
                root.Element(nodeName).Remove();
            }
        }


        public void ReplaceNode(string nodeName, XName[] elems)
        {
            root.Element(nodeName).ReplaceWith(elems);
        }

    }

    /// <summary>
    /// 传入参数枚举值
    /// </summary>
    public enum XmlType
    {
        [Description("文件")]
        File,
        [Description("字符串")]
        Text
    }


    /// <summary>
    /// 插入节点位置
    /// </summary>
    public enum InsertNodePostion
    {
        [Description("在 XContainer 的子内容的末尾添加内容")]
        Add,
        [Description("在 XContainer 的子内容的末尾添加内容")]
        AddFirst,
        [Description("在 XNode 后面添加内容")]
        AddAfterSelf,
        [Description("在 XNode 前面添加内容")]
        AddBeforeSelf
    }
}
