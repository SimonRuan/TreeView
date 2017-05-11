using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeView.Models
{
    /// <summary>
    /// 节点
    /// </summary>
    public class Node
    {
        public int Id { get; set; }
        /// <summary>
        /// 对应班组表的GroupName
        /// </summary>
        public string Text { get; set; }
        public int ParentId { get; set; }

        /// <summary>
        /// 层级，从0开始
        /// </summary>
        public int Level { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<Node> Nodes { get; set; }

    }
}