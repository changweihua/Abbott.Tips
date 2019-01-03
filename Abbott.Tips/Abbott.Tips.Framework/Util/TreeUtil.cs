using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    public sealed class TreeUtil
    {
        public static IList<TreeNode> GenTree(List<TreeNode> list)
        {
            var tree = new List<TreeNode>();
            //查找父节点
            List<TreeNode> rootType = list.Where(o => o.PId == 0).ToList();
            //递归主函数
            Action<TreeNode, int> addChildType = null;
            addChildType
             = (typeInfo, level) =>
             {
                 string display = GetPadding(level * 2) + typeInfo.Name;
                 typeInfo.RootLevel = level;
                 typeInfo.Depth = level;
                 typeInfo.DisplayName = display;
                 var childInfo = list.Where(o => o.PId == typeInfo.Id);
                 typeInfo.IsLeaf = !childInfo.Any(_ => _.PId == typeInfo.Id);
                 tree.Add(typeInfo);
                 if (typeInfo.IsLeaf)
                 {
                     return;
                 }
                 ++level;
                 childInfo.All(
                  o =>
                  {
                      addChildType(o, level);
                      return true;
                  });
             };
            //递归调用
            rootType.ForEach(
             o =>
             {
                 addChildType(o, 0);
             });

            return tree;
        }

        public static IList<TreeNode> GenTreeList(List<TreeNode> list, int rootParentId = 0, int startLevel = 0)
        {
            var tree = new List<TreeNode>();
            //查找父节点
            List<TreeNode> roots = list.Where(o => o.PId == rootParentId).ToList();
            //递归主函数
            Action<TreeNode, int> addNode = null;
            addNode
             = (node, level) =>
             {
                 var subNodes = list.Where(o => o.PId == node.Id);
                 node.IsLeaf = !subNodes.Any();
                 node.Nodes = subNodes.ToList();
                 string display = GetPadding(level * 2) + node.Name;
                 node.RootLevel = level;
                 node.Depth = level;
                 node.DisplayName = display;

                 var parentNode = tree.FirstOrDefault(n => n.Id == node.PId);
                 if (parentNode == null)
                 {
                     tree.Add(node);
                 }
                 else
                 {
                     parentNode.Nodes = parentNode.Nodes ?? new List<TreeNode>();
                     if (!parentNode.Nodes.Any(n => n.Id == node.Id))
                     {
                         parentNode.Nodes.Add(node);
                     }
                 }

                 if (node.IsLeaf)
                 {
                     return;
                 }
                 ++level;
                 subNodes.All(
                  o =>
                  {
                      addNode(o, level);
                      return true;
                  });
             };
            //递归调用
            roots.ForEach(
             o =>
             {
                 addNode(o, startLevel);
             });

            return tree;
        }

        private static string GetPadding(int count)
        {
            return ("" + GenSpace(count * 2)) + "|".PadRight(6, '-');
        }

        private static string GenSpace(int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append("&ensp;");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成符合LayUITree的数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rootParentId"></param>
        /// <param name="startLevel"></param>
        /// <returns></returns>
        public static IList<LayTreeNode> GenLayTreeList(List<LayTreeNode> list, int rootParentId = 0, int startLevel = 0)
        {
            var tree = new List<LayTreeNode>();
            //查找父节点
            List<LayTreeNode> roots = list.Where(o => o.PId == rootParentId).ToList();
            //递归主函数
            Action<LayTreeNode, int, LayTreeNode> addNode = null;
            addNode
             = (node, level, pNode) =>
             {
                 var subNodes = list.Where(o => o.PId == node.Id);
                 node.IsLeaf = !subNodes.Any();
                 node.Nodes = subNodes.ToList();

                 var parentNode = (pNode.Nodes ?? new List<LayTreeNode>()).FirstOrDefault(n => n.Id == node.PId);
                 if (parentNode == null)
                 {
                     tree.Add(node);
                     parentNode = node;
                 }
                 else
                 {
                     parentNode.Nodes = parentNode.Nodes ?? new List<LayTreeNode>();
                     if (!parentNode.Nodes.Any(n => n.Id == node.Id))
                     {
                         parentNode.Nodes.Add(node);
                     }
                 }

                 if (node.IsLeaf)
                 {
                     return;
                 }

                 subNodes.All(
                  o =>
                  {
                      addNode(o, level, parentNode);
                      return true;
                  });
             };
            //递归调用
            roots.ForEach(
             parentNode =>
             {
                 addNode(parentNode, startLevel, new LayTreeNode
                 {
                     Name = "根节点"
                 });
             });

            return tree;
        }

        #region 内部类

        public class LayXTreeNode
        {
            public string Title { get; set; }

            public int Id { get; set; }

            public string Value { get; set; }

            public List<LayXTreeNode> Nodes { get; set; }

            public int PId { get; set; }

            public bool IsDisabled { get; set; }

            public bool IsChecked { get; set; }
        }

        public class LayTreeNode
        {
            public bool Spread { get { return true; } }

            public string Name { get; set; }

            public int Id { get; set; }

            public List<LayTreeNode> Nodes { get; set; }

            public int PId { get; set; }

            public bool IsLeaf { get; set; }

            public string Href { get; set; }

            public bool Check { get; set; }
        }

        public class TreeNode
        {
            public int Id { get; set; }

            public int PId { get; set; }

            public string Name { get; set; }

            public string Text { get; set; }

            public string Value { get; set; }

            public int Depth { get; set; }

            public int RootLevel { get; set; }

            public bool IsLeaf { get; set; }

            public string DisplayName { get; set; }

            public IList<TreeNode> Nodes { get; set; }

        }

        #endregion

    }
}
