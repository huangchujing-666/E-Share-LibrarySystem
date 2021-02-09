using DataUpdate.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpdate.Dal
{
    public class UniversityDal
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        private string Server { get; set; }
        /// <summary>
        /// 服务器数据库
        /// </summary>
        private string Database { get; set; }
        /// <summary>
        /// 服务器uid
        /// </summary>
        private string Uid { get; set; }
        /// <summary>
        /// 服务器pwd
        /// </summary>
        private string Pwd { get; set; }
        public UniversityDal() { }

        public UniversityDal(string server, string database, string uid, string pwd)
        {
            this.Server = server;
            this.Database = database;
            this.Uid = uid;
            this.Pwd = pwd;
        }

        #region 将服务器已经不存在,本地存在的数据Scode放入Removescode集合+void GetDeleteClientDataList()
        /// <summary>
        /// 将服务器已经不存在,本地存在的数据Scode放入Removescode集合
        /// </summary>
        public List<string> GetDeleteClientDataList(ViewConfig viewConfig)
        {
            Isbn Scode = GetServerClientISBN(viewConfig);
            List<string> RemoveClientScodeList = new List<string>();
            if (Scode.ServerISBN.Count > 0)
            {
                foreach (string scode in Scode.ClientISBN)
                {
                    if (!Scode.ServerISBN.Contains(scode))
                    {
                        RemoveClientScodeList.Add(scode);
                    }
                }
            }
            return RemoveClientScodeList;
        }

        private Isbn GetServerClientISBN(ViewConfig viewConfig)
        {
            Isbn scode = new Isbn();
            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(CommHelp.ConnStr))
                {
                    conn.Open();
                    cmd.CommandText = "SELECT Isbn from BookInfo where IsDelete=0 and Status=1 and UniversityId=" + CommHelp.SourceConfigId;
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<string> cli = new List<string>();
                    while (reader.Read())
                    {
                        cli.Add(reader["Isbn"].ToString());
                    }
                    scode.ClientISBN = cli;
                }
                scode.ServerISBN = GetServerISBN(viewConfig);
                return scode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 得到被实例化服务器和本地数据库的SCODE集合的SCODE对象+SCODE GetServerClientSCODE()
        /// <summary>
        /// 得到被实例化服务器和本地数据库的SCODE集合的SCODE对象
        /// </summary>
        /// <returns></returns>
        //private Isbn GetServerClientSCODE(ViewConfig viewConfig)
        //{

        //}
        #endregion

        #region 获取远程服务器上面的scode字段集合+List<string> GetServerScode()
        /// <summary>
        /// 获取远程服务器上面的ISBN字段集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetServerISBN(ViewConfig viewConfig)
        {
            SqlConnection serverCon = new SqlConnection(CommHelp.DataSourceConn);
            try
            {
                if (serverCon.State == ConnectionState.Closed)
                    serverCon.Open();
                List<string> list = new List<string>();
                SqlCommand com = new SqlCommand();
                com.Connection = serverCon;
                com.CommandText = "select " + viewConfig.Isbn + " from " + viewConfig.ViewName;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[viewConfig.Isbn].ToString().Trim());
                }
                com.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (serverCon.State == ConnectionState.Open)
                    serverCon.Close();
            }
        }

        #endregion


        #region 删除本地数据库数据+void DelteClientProductsourcestock()
        /// <summary>
        /// 删除本地数据库数据
        /// </summary>
        public void DelteClientBookInfo(List<string> list)
        {
            try
            {
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        using (SqlConnection conn = new SqlConnection(CommHelp.ConnStr))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;
                            StringBuilder sb = new StringBuilder(200);
                            foreach (string isbn in list)
                            {
                                sb.Append('\'');
                                sb.Append(isbn);
                                sb.Append('\'');
                                sb.Append(',');
                                //RemoveClientScodeList.Remove(scode);
                            }
                            string ss = sb.ToString();
                            cmd.CommandText = "update BookInfo set Status=" + (int)EnumHelp.EnabledEnum.无效 + " where Isbn in(" + ss.Substring(0, ss.Length - 1) + ")";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception)
            {
                DelteClientBookInfo(list);
                return;
            }
        }
        #endregion

        public void UpdateStatus()
        { 
        
        }
    }
}
