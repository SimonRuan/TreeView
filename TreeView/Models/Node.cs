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
        public int id { get; set; }
        /// <summary>
        /// 对应班组表的GroupName
        /// </summary>
        public string text { get; set; }
        public int parentId { get; set; }

        /// <summary>
        /// 层级，从0开始
        /// </summary>
        public int level { get; set; }
        public DateTime createTime { get; set; }
        public DateTime modifyTime { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<Node> nodes { get; set; }

    }
}