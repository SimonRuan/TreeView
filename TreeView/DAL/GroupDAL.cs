using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using TreeView.Models;

namespace TreeView.DAL
{
    public class GroupDAL
    {
        private static string connStr = "Data Source=.;Initial Catalog=TreeView;Integrated Security=True";

        /// <summary>
        /// 获取最大层级
        /// </summary>
        /// <returns></returns>
        public int GetMaxLevel()
        {
            var res = 0;
            string cmdStr = " SELECT ISNULL(MAX([Level]),0) AS 'MaxLevel'  FROM [Group](NOLOCK) ";
            var conn = new SqlConnection(connStr);
            var cmd = new SqlCommand(cmdStr, conn);
            using (conn)
            {
                conn.Open();
                res = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            return res;
        }

        /// <summary>
        /// 根据层级获取班组
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<Node> GetNodesByLevel(int level)
        {
            var res = new List<Node>();
            string cmdStr = " SELECT *  FROM [Group](NOLOCK) WHERE [Level]=@Level ";
            var conn = new SqlConnection(connStr);
            var cmd = new SqlCommand(cmdStr, conn);
            cmd.Parameters.AddWithValue("@Level", level);
            using (conn)
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new Node
                    {
                        Id = Int32.Parse(reader["Id"].ToString()),
                        Text = reader["GroupName"].ToString()
                    };
                    res.Add(item);
                }
            }
            return res;
        }


        /// <summary>
        /// 获取所有班组
        /// </summary>
        /// <returns></returns>
        public List<Node> GetAllNodes()
        {
            var res = new List<Node>();
            string cmdStr = " SELECT *  FROM [Group](NOLOCK) ";
            var conn = new SqlConnection(connStr);
            var cmd = new SqlCommand(cmdStr, conn);
            using (conn)
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new Node
                    {
                        Id = Int32.Parse(reader["Id"].ToString()),
                        Text = reader["GroupName"].ToString(),
                        Level = Int32.Parse(reader["Level"].ToString()),
                        ParentId = Int32.Parse(reader["ParentId"].ToString()),
                        CreateTime = DateTime.Parse(reader["CreateTime"].ToString()),
                        ModifyTime = DateTime.Parse(reader["ModifyTime"].ToString())
                    };
                    res.Add(item);
                }
            }
            return res;
        }


        /// <summary>
        /// 新增node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int AddGroup(Node node)
        {
            int res;
            var sb = new StringBuilder();
            sb.AppendLine(" INSERT INTO [Group] ");
            sb.AppendLine(" ( GroupName ,[Level] ,ParentId ,CreateTime ,ModifyTime ) ");
            sb.AppendLine(" VALUES ");
            sb.AppendLine(" ( @GroupName ,@[Level] ,@ParentId ,@CreateTime ,@ModifyTime ) ");

            var conn = new SqlConnection(connStr);
            var cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@GroupName", node.Text);
            cmd.Parameters.AddWithValue("@[Level]", node.Level);
            cmd.Parameters.AddWithValue("@ParentId", node.ParentId);
            cmd.Parameters.AddWithValue("@CreateTime", node.CreateTime);
            cmd.Parameters.AddWithValue("@ModifyTime", node.ModifyTime);
            using (conn)
            {
                conn.Open();
                res = cmd.ExecuteNonQuery();
            }
            return res;
        }

    }
}