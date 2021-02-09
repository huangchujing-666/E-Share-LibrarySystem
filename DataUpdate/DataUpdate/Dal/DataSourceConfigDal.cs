using DataUpdate.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace DataUpdate.Dal
{
    public class DataSourceConfigDal
    {
        /// <summary>
        /// 连接超时次数 (更新本地数据)
        /// </summary>
        private static int SqlExceptionCount = 0;
        /// <summary>
        /// 连接异常次数 (更新本地数据)
        /// </summary>
        private static int InvalidOperationExceptionCount = 0;
        /// <summary>
        /// 连接超时次数  (获取服务器数据集合)
        /// </summary>
        private static int SqlExceptionServerCount = 0;
        /// <summary>
        /// 连接异常次数 (获取服务器数据集合)
        /// </summary>
        private static int InvalidOperationExceptionServerCount = 0;
        ErrorLogDal e;
        public DataSourceConfigDal()
        {
            e = new ErrorLogDal();
        }

        #region 根据供应商编号获取数据源配置信息+DataSourceConfig GetDataSourceConfig(string Id)
        /// <summary>
        /// 根据供应商编号获取数据源配置信息
        /// </summary>
        /// <param name="SourceCode"></param>
        /// <returns></returns>
        public University GetDataSourceConfig(string Id)
        {
            University d = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(CommHelp.ConnStr))
                {
                    conn.Open();
                    string sqlstr = "select * from University where UniversityId='" + Id + "'";
                    SqlDataAdapter da = new SqlDataAdapter(sqlstr, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        d = new University();
                        d.UniversityId = int.Parse(dt.Rows[0]["UniversityId"].ToString());
                        d.DataBase = dt.Rows[0]["DataBase"] == null ? "" : dt.Rows[0]["DataBase"].ToString();
                        d.Service = dt.Rows[0]["Service"] == null ? "" : dt.Rows[0]["Service"].ToString();
                        d.UserId = dt.Rows[0]["UserId"] == null ? "" : dt.Rows[0]["UserId"].ToString();
                        d.UserPwd = dt.Rows[0]["UserPwd"] == null ? "" : dt.Rows[0]["UserPwd"].ToString();
                        d.TimeStart = dt.Rows[0]["TimeStart"] == null ? 0 : int.Parse(dt.Rows[0]["TimeStart"].ToString());
                        CommHelp.SourceConfigId = int.Parse(dt.Rows[0]["UniversityId"].ToString());
                        CommHelp.SleepTime = int.Parse(dt.Rows[0]["TimeStart"].ToString());
                    }

                }
            }
            catch (Exception ex)//
            {
                SqlHelper.ExecuteNonQuery("Update University set IsUpdate=" + (int)EnumHelp.UpdateStartEnum.未启用 + " where UniversityId=" + CommHelp.SourceConfigId);
            }

            return d;
        }
        #endregion


        #region 更新本地数据+bool UpdateClientDataTables(int skip, int take, out string message)
        /// <summary>
        /// 更新本地数据
        /// </summary>
        /// <param name="skip">跳过skip条数据</param>
        /// <param name="take">更新到第take条数据</param>
        /// <param name="message">错误信息</param>
        /// <returns>false则更新出错，true更新成功</returns>
        public bool UpdateClientDataTables(int skip, int take, out string message)
        {
            try
            {
                //创建数据表，存储要新增的行数据
                DataTable dt = CreateDataTable();
                ServerTables tables = new ServerTables();
                List<string> list = new List<string>();
                SqlConnection conn = new SqlConnection(CommHelp.ConnStr);
                //获取本地数据库数据
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string sqlstr = "select Isbn from BookInfo where UniversityId='" + CommHelp.SourceConfigId + "' and Status=1 and IsDelete=0";
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["Isbn"].ToString());
                }
                reader.Close();
                //获取服务器数据集合
                tables.ServiceViewModel = GetServerScodeList(skip, take, out message); 
                if (tables.ServiceViewModel == null)
                {
                    message = "DataSourceConfigDal->UpdateClientDataTables->ServerTables ServiceViewModel=null";
                    UpdateClientDataTables(skip, take, out message);
                    if (tables.ServiceViewModel == null)
                    {
                        return false;
                    }
                }
                foreach (BookInfo st in tables.ServiceViewModel)
                {
                    if (!list.Contains(st.Isbn))//判断本地数据中是否已经存在服务器数据，是的话更改 否则新增
                    {
                        DataRow dr = dt.NewRow();
                        DateTime date = System.DateTime.Now;
                        dr["Isbn"] = st.Isbn != null ? st.Isbn : "";
                        dr["Title"] = st.Title != null ? st.Title : "";
                        dr["PublicDate"] = st.PublicDate != null ? st.PublicDate : "";
                        dr["Author"] = st.Author != null ? st.Author : "";
                        dr["Category"] = st.Category != null ? st.Category : "";
                        dr["Available"] = st.Available;// st.Available != null ? st.Available : 0;
                        dr["Count"] = st.Count;// st.Count != null ? st.Count : 0;
                        dr["IsDelete"] = (int)EnumHelp.IsDeleteEnum.有效;
                        dr["Status"] = (int)EnumHelp.EnabledEnum.有效;
                        dr["EditTime"] = date;
                        dr["CreateTime"] = date;
                        dr["UniversityId"] = CommHelp.SourceConfigId;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        string sql = "update BookInfo set Count=" + st.Count + ",EditTime='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")+ "' where Isbn='" + st.Isbn + "' and UniversityId=" + CommHelp.SourceConfigId;
                        cmd.CommandText = sql;
                        cmd.CommandTimeout = 240; 
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            // AddErrorlog("SCODE为:" + st.SCODE.ToString() + ",Priceb=" + st.PRICEB + ",Pricec=" + st.PRICEC + ",Priced=" + st.PRICED + ",Pricee=" + st.PRICEE + ",Disca=" + st.DISCA + ",Discb=" + st.DISCB + ",Discc=" + st.DISCC + ",Discd=" + st.DISCD + ",Disce=" + st.DISCE + ",PrevStock=Balance, Balance=" + st.BAL +";当SCODE=" + st.SCODE.ToString() , SourceRenewIndex, true);
                            //  AddErrorlog("SCODE为:" + st.SCODE.ToString() + ",Priceb=" + st.PRICEB + ",Pricec=" + st.PRICEC + ",Priced=" + st.PRICED + ",Pricee=" + st.PRICEE + ",Disca=" + st.DISCA + ",Discb=" + st.DISCB + ",Discc=" + st.DISCC + ",Discd=" + st.DISCD + ",Disce=" + st.DISCE + ",PrevStock=Balance, Balance=" + st.BAL + ";当SCODE=" + st.SCODE.ToString(), SourceRenewIndex, SourceCode, 699,st.SCODE);
                        }
                        cmd.Dispose();
                    }
                    Thread.Sleep(60);
                }
                //批量更新
                if (!ExecuteTransactionScopeInsert(dt, out message))
                {
                    Exception ex = new Exception(message);
                    ex.Source = "ExecuteTransactionScopeInsert()批量插入";
                    DateTime date = System.DateTime.Now;
                    e.UpdateErrorLog(new ErrorLog()
                    {
                        ErrorLogId = 0,
                        Status = (int)EnumHelp.UpdateStatus.更新中,
                        SysAccountId = CommHelp.SysAccountId,
                        UpdateCount = CommHelp.UpdateIndex,
                        UniversityId = CommHelp.SourceConfigId,
                        ErrorSrc = "",
                        Type = (int)EnumHelp.ErrorLogType.插入异常,
                        ErrorMsg = ex.Message,
                        ErrorTime = date,
                        EndTime = date,
                        StartTime = date,
                        ErrorData = (skip + 1) + "-" + take
                    });
                    return false;
                }
                message = "";
                e.DeleteErrorlog((skip + 1) + "-" + take, CommHelp.UpdateIndex, CommHelp.SourceConfigId);//将上次更新数据产生的错误全部删除
                conn.Dispose();
                conn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                if (SqlExceptionCount < 3)
                {
                    SqlExceptionCount++;
                    DateTime date = System.DateTime.Now;
                    //连接超时 递归重新连接
                    e.UpdateErrorLog(new ErrorLog()
                    {
                        ErrorLogId = 0,
                        Status = (int)EnumHelp.UpdateStatus.更新中,
                        SysAccountId = CommHelp.SysAccountId,
                        UpdateCount = CommHelp.UpdateIndex,
                        UniversityId = CommHelp.SourceConfigId,
                        ErrorSrc = "",
                        Type = (int)EnumHelp.ErrorLogType.插入异常,
                        ErrorMsg = "DataSourceConfigDal->UpdateClientDataTables()->SqlException" + ex.Message,
                        ErrorTime = date,
                        StartTime = date,
                        EndTime = date,
                        ErrorData = (skip + 1) + "-" + take
                    });
                    //AddErrorlog(ex, (skip + 1) + "-" + take, SourceRenewIndex, SourceCode, 692);
                    UpdateClientDataTables(skip, take, out message);
                    return true;
                }
                message = ex.Message;
                SqlHelper.ExecuteNonQuery("Update University set IsUpdate=" + (int)EnumHelp.UpdateStartEnum.未启用 + " where UniversityId=" + CommHelp.SourceConfigId);
                return false;
            }
            catch (InvalidOperationException ex)
            {
                if (InvalidOperationExceptionCount < 3)
                {
                    InvalidOperationExceptionCount++;
                    DateTime date = System.DateTime.Now;
                    //连接异常  递归重新连接
                    e.UpdateErrorLog(new ErrorLog()
                    {
                        ErrorLogId = 0,
                        Status = (int)EnumHelp.UpdateStatus.更新中,
                        SysAccountId = CommHelp.SysAccountId,
                        UpdateCount = CommHelp.UpdateIndex,
                        UniversityId = CommHelp.SourceConfigId,
                        ErrorSrc = "DataSourceConfigDal->UpdateClientDataTables()->InvalidOperationException",
                        Type = (int)EnumHelp.ErrorLogType.插入异常,
                        ErrorMsg = ex.Message,
                        ErrorTime = date,
                        StartTime = date,
                        EndTime = date,
                        ErrorData = (skip + 1) + "-" + take
                    });
                    //AddErrorlog(ex, (skip + 1) + "-" + take, SourceRenewIndex, SourceCode, 692);
                    UpdateClientDataTables(skip, take, out message);
                    return true;
                }
                message = ex.Message;
                SqlHelper.ExecuteNonQuery("Update University set IsUpdate=" + (int)EnumHelp.UpdateStartEnum.未启用 + " where UniversityId=" + CommHelp.SourceConfigId);
                return false;
            }
            catch (Exception ex)
            {
                DateTime date = System.DateTime.Now;
                e.UpdateErrorLog(new ErrorLog()
                {
                    ErrorLogId = 0,
                    Status = (int)EnumHelp.UpdateStatus.更新中,
                    SysAccountId = CommHelp.SysAccountId,
                    UpdateCount = CommHelp.UpdateIndex,
                    UniversityId = CommHelp.SourceConfigId,
                    ErrorSrc = "",
                    Type = (int)EnumHelp.ErrorLogType.插入异常,
                    ErrorMsg = "更新数据异常:"+ex.Message,
                    ErrorTime = date,
                    StartTime = date,
                    EndTime = date,
                    ErrorData = (skip + 1) + "-" + take
                });
                //AddErrorlog(ex, (skip + 1) + "-" + take, SourceRenewIndex, SourceCode, 692);
                UpdateClientDataTables(skip, take, out message);
                SqlHelper.ExecuteNonQuery("Update University set IsUpdate=" + (int)EnumHelp.UpdateStartEnum.未启用 + " where UniversityId=" + CommHelp.SourceConfigId);
                message = ex.Message;
                return false;
            }
        }
        #endregion


        #region 获取源数据图书数据集合+List<BookInfo> GetServerScodeList(int skip, int take, out string message)
        /// <summary>
        /// 获取源数据图书数据集合
        /// </summary>
        /// <param name="skip">从第skip条开始取数据</param>
        /// <param name="take">取到第take条</param>
        /// <param name="message">错误信息</param>
        /// <returns></returns>
        private List<BookInfo> GetServerScodeList(int skip, int take, out string message)
        {
            message = "";
            List<BookInfo> list = new List<BookInfo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(CommHelp.DataSourceConn))
                {
                    conn.Open();
                    string sqlstr = "select * from (select " + CommHelp.viewConfig.Author + "," + CommHelp.viewConfig.Available + "," + CommHelp.viewConfig.Category + "," + CommHelp.viewConfig.Count + "," + CommHelp.viewConfig.PublicDate + "," + CommHelp.viewConfig.Title + "," + CommHelp.viewConfig.Isbn + ",row_number() over(order by " + CommHelp.viewConfig.Isbn + ") as rowid from " + CommHelp.viewConfig.ViewName + ") b where b.rowid between " + (skip + 1) + " and " + take; ;
                    //SELECT row_number() over(order by ISBN) row_num from ViewName where row_num between +(skip + 1) + " and " + take
                    //string sqlstr = "select a.*,b.* from ( select row_number() over(order by SCODE) row_num," +
                    //            "SCODE,BCODE,BCODE2,DESCRIPT,CDESCRIPT,UNIT,CURRENCY,CAT,CAT1,CAT2,COLOR,SIZE,STYLE,PRICEA," +
                    //            "PRICEB,PRICEC,PRICED,PRICEE,DISCA,DISCB,DISCC,DISCD,DISCE,MODEL,RO_LEVEL,RO_AMT,STOPSALES," +
                    //            "LAST_GRN_D from STOCK_VIEW WHERE STYLE is not null and STYLE<>'') a left join " +
                    //            "(select StockBal_View.SCODE as SCODE1,SUM(StockBal_View.BALANCE) as BAL from StockBal_View WHERE LOC !='head' and LOC !='shop2' and LOC !='shop4' and LOC !='shop5' and LOC !='shop6' group by StockBal_View.SCODE) b on a.SCODE=b.SCODE1 " +
                    //            "where a.row_num between " + (skip + 1) + " and " + take;
                    SqlCommand cmd = new SqlCommand(sqlstr, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BookInfo stock = new BookInfo();
                        stock.Author = reader[CommHelp.viewConfig.Author] != null ? reader[CommHelp.viewConfig.Author].ToString() : "";
                        stock.Available = reader[CommHelp.viewConfig.Available] != null ? int.Parse(reader[CommHelp.viewConfig.Available].ToString()) : 0;
                        stock.Category = reader[CommHelp.viewConfig.Category] != null ? reader[CommHelp.viewConfig.Category].ToString() : "";
                        stock.Isbn = reader[CommHelp.viewConfig.Isbn] != null ? reader[CommHelp.viewConfig.Isbn].ToString() : "";
                        stock.PublicDate = reader[CommHelp.viewConfig.PublicDate] != null ? reader[CommHelp.viewConfig.PublicDate].ToString() : "";
                        stock.Title = reader[CommHelp.viewConfig.Title] != null ? reader[CommHelp.viewConfig.Title].ToString() : "";
                        stock.Count = reader[CommHelp.viewConfig.Count] != null ? int.Parse(reader[CommHelp.viewConfig.Count].ToString()) : 0;
                        list.Add(stock);
                        //Thread.Sleep(10);
                    }
                    if (list == null || list.Count <= 0)
                    {
                        GetServerScodeList(skip, take, out message);
                    }
                    e.DeleteErrorlog((skip + 1) + "-" + take, CommHelp.UpdateIndex, CommHelp.SysAccountId);
                    return list;
                }
            }
            catch (SqlException ex)
            {
                if (SqlExceptionServerCount < 3)
                {
                    SqlExceptionServerCount++;
                    GetServerScodeList(skip, take, out message);
                    return list;
                }
                message = ex.Message;
                return null;
            }
            catch (InvalidOperationException ex)
            {
                if (InvalidOperationExceptionServerCount < 3)
                {
                    InvalidOperationExceptionServerCount++;
                    GetServerScodeList(skip, take, out message);
                    return list;
                }
                message = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                DateTime date = System.DateTime.Now;
                e.UpdateErrorLog(new ErrorLog()
                {
                    ErrorLogId = 0,
                    EndTime = date,
                    StartTime = date,
                    ErrorData = (skip + 1) + "-" + take,
                    ErrorMsg = "获取数据源数据异常",
                    ErrorSrc = "DataSourceConfigDal->GetServerScodeList",
                    ErrorTime = date,
                    Type = (int)EnumHelp.ErrorLogType.获取数据源异常,
                    UniversityId = CommHelp.SourceConfigId,
                    UpdateCount = CommHelp.UpdateIndex,
                    SysAccountId = CommHelp.SysAccountId,
                    Status = (int)EnumHelp.UpdateStatus.更新中

                });// AddErrorlog(ex, (skip + 1) + "-" + take, SourceRenewIndex, SourceCode, 642);
                return null;
            }
        }
        #endregion



        #region 创建本地数据表+DataTable CreateDataTable()
        /// <summary>
        /// 创建本地数据表
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("BookInfoId", typeof(int));
            dt.Columns.Add(dc);
            DataColumn dc1 = new DataColumn("Isbn", typeof(string));
            dt.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn("Title", typeof(string));
            dt.Columns.Add(dc2);
            DataColumn dc3 = new DataColumn("PublicDate", typeof(string));
            dt.Columns.Add(dc3);
            DataColumn dc4 = new DataColumn("Author", typeof(string));
            dt.Columns.Add(dc4);
            DataColumn dc5 = new DataColumn("Category", typeof(string));
            dt.Columns.Add(dc5);
            DataColumn dc6 = new DataColumn("UniversityId", typeof(int));
            dt.Columns.Add(dc6);
            DataColumn dc7 = new DataColumn("Available", typeof(int));
            dt.Columns.Add(dc7);
            DataColumn dc8 = new DataColumn("Count", typeof(int));
            dt.Columns.Add(dc8);
            DataColumn dc9 = new DataColumn("IsDelete", typeof(int));
            dt.Columns.Add(dc9);
            DataColumn dc10 = new DataColumn("Status", typeof(int));
            dt.Columns.Add(dc10);
            DataColumn dc11 = new DataColumn("EditTime", typeof(DateTime));
            dt.Columns.Add(dc11);
            DataColumn dc12 = new DataColumn("CreateTime", typeof(DateTime));
            dt.Columns.Add(dc12);
            return dt;
        }
        #endregion

        private object locker = new object();
        #region 批量更新数据+bool ExecuteTransactionScopeInsert(DataTable dt, out string message)
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="message"></param>
        /// <returns>false为更新失败，true为更新成功</returns>
        private bool ExecuteTransactionScopeInsert(DataTable dt, out string message)
        {
            message = "";
            bool flag = false;
            int count = dt.Rows.Count;
            if (count == 0)
            {
                return true;
            }
            int copyTimeout = 200;

            try
            {
                lock (locker)
                {
                    using (SqlConnection conn = new SqlConnection(CommHelp.ConnStr))
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            conn.Open();
                            using (SqlBulkCopy sbc = new SqlBulkCopy(conn))
                            {
                                sbc.DestinationTableName = "BookInfo";
                                sbc.BatchSize = count;
                                sbc.BulkCopyTimeout = copyTimeout;
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    //列映射定义数据源中的列和目标表中的列之间的关系 
                                    sbc.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                                }
                                sbc.WriteToServer(dt);
                                flag = true;
                                //有效的事务 
                                scope.Complete();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return flag;
        }
        #endregion


        #region 再次获取本次更新失败的数据List<int[]> AgainRenew(string SourceCode, int SourceRenewIndex, out string mess)
        /// <summary>
        /// 再次获取本次更新失败的数据
        /// </summary>
        /// <param name="SourceCode">供应商编号</param>
        /// <param name="SourceRenewIndex">更新次数</param>
        /// <param name="mess">错误信息</param>
        /// <returns>返回一个存有</returns>
        public List<int[]> AgainRenew(int UinversityId, int UpdateIndex, out string mess)
        {
            mess = "";
            List<int[]> list = new List<int[]>();
            try
            {
                using (SqlConnection conn = new SqlConnection(CommHelp.ConnStr))
                {
                    conn.Open();
                    string sqlstr = "select ErrorData from ErrorLog where Type=" + (int)EnumHelp.ErrorLogType.插入异常 + " and UniversityId=" + UinversityId + " and UpdateCount="+UpdateIndex;
                    SqlCommand cmd = new SqlCommand(sqlstr, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string str = reader["ErrorData"].ToString();
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            string[] strs = str.Split('-');
                            int[] index;
                            if (IsRowIndex(strs, out index))
                            {
                                list.Add(index);
                            }
                        }
                     
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                mess = ex.Message;
                return null;
            }
        }
        #endregion

        #region 判断数组是否为int类型,是的话转化为int数组bool IsRowIndex(string[] strs, out int[] index)
        /// <summary>
        /// 判断数组是否为int类型,是的话转化为int数组
        /// </summary>
        /// <param name="strs">传入的数组</param>
        /// <param name="index">返回的int数组</param>
        /// <returns>返回是否能转换为数组集合，int数组集合</returns>
        public bool IsRowIndex(string[] strs, out int[] index)
        {
            index = new int[2];
            try
            {
                if (strs.Length == 2 && !string.IsNullOrWhiteSpace(strs[0].ToString()) && !string.IsNullOrWhiteSpace(strs[1].ToString()))
                {
                    index[0] = int.Parse(strs[0].ToString()) - 1;
                    index[1] = int.Parse(strs[1].ToString());
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
