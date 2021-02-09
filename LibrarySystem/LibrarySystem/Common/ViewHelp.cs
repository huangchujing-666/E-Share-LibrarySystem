using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibrarySystem.Common
{
    public static class ViewHelp
    {
        /// <summary>
        /// 获取视图查询语句
        /// </summary>
        /// <returns></returns>
        public static string GetViewDataSqlStr(ViewConfig v,string isbn)
        {
            string Sqlstr = string.Empty;
            if (v != null)
            {
                //select Available from BookView where Isbn='9580-9308-9041'
                Sqlstr = "SELECT " + v.Available + " FROM " + v.ViewName + " WHERE " + v.Isbn+"='"+isbn+"'";
            }
            return Sqlstr;
        }

        /// <summary>
        /// 获取数据源连接字符串
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static string GetSourceConnectionStr(University u)
        {
            if (u == null)
                return string.Empty;
            return"server=" + u.Service + ";database=" + u.DataBase + ";uid=" + u.UserId + ";pwd=" + u.UserPwd;
        }

        public static int GetCountByUinversityId(ViewConfig v,University u,string Isbn)
        {
            //1.根据视图拼接查询字符串
            string sqlstr = GetViewDataSqlStr(v, Isbn);
            //2.连接数据源数据库  查询获得库存
            string connStr = GetSourceConnectionStr(u);
            int result = 0;
            if (!string.IsNullOrWhiteSpace(sqlstr) && !string.IsNullOrWhiteSpace(connStr))
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.AddRange(new SqlParameter[] { });
                        result=(int)cmd.ExecuteScalar();
                    }
                }
            }
            //3.返回结果
            return result;
        }
    }
}