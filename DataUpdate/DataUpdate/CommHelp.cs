using DataUpdate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpdate
{
    public static class CommHelp
    {
        public static int UpdateIndex=0;

        public static University University = null;
        /// <summary>
        /// 是否初次更新
        /// </summary>
        public static bool IsFirstUpdate = true;

        public static ViewConfig viewConfig=null;

        /// <summary>
        /// 数据源连接字符串
        /// </summary>
        public static string DataSourceConn = "";

        public static int SysAccountId = 0;

        public static int ErrorLogId=0;
        /// <summary>
        /// 数据源ID
        /// </summary>
        public static int SourceConfigId = 0;
        /// <summary>
        /// 是否多次更新
        /// </summary>
        public static bool IsRepetitionUpdate = false;
        /// <summary>
        /// 本地数据库连接字符串
        /// </summary>
        public static string ConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString.ToString();

        /// <summary>
        /// 线程数量
        /// </summary>
        public static int ThreadCount = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ThreadCount"].ToString());

        /// <summary>
        /// 每次更新的数量
        /// </summary>
        public static int UpdateCount = int.Parse(System.Configuration.ConfigurationManager.AppSettings["UpdCount"].ToString());

        /// <summary>
        /// 线程睡眠时间 毫秒
        /// </summary>
        public static int SleepTime = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SleepTime"].ToString());


    }
}
