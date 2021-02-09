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
    public class ViewConfigDal
    {


        ErrorLogDal e;
        public ViewConfigDal()
        {
            e = new ErrorLogDal();
        }




        /// <summary>
        /// 获取视图对象
        /// </summary>
        /// <returns></returns>
        public ViewConfig GetViewConfig()
        {
            ViewConfig v = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(CommHelp.ConnStr))
                {
                    conn.Open();
                    string sqlstr = "select top 1 * from ViewConfig";
                    SqlDataAdapter da = new SqlDataAdapter(sqlstr, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        v = new ViewConfig();
                        v.ViewConfigId = int.Parse(dt.Rows[0]["ViewConfigId"].ToString());
                        v.ViewName = dt.Rows[0]["ViewName"] == null ? "" : dt.Rows[0]["ViewName"].ToString();
                        v.Title = dt.Rows[0]["Title"] == null ? "" : dt.Rows[0]["Title"].ToString();
                        v.PublicDate = dt.Rows[0]["PublicDate"] == null ? "" : dt.Rows[0]["PublicDate"].ToString();
                        v.Isbn = dt.Rows[0]["Isbn"] == null ? "" : dt.Rows[0]["Isbn"].ToString();
                        v.Count = dt.Rows[0]["Count"] == null ? "" : dt.Rows[0]["Count"].ToString();
                        v.Author = dt.Rows[0]["Author"] == null ? "" : dt.Rows[0]["Author"].ToString();
                        v.Available = dt.Rows[0]["Available"] == null ? "" : dt.Rows[0]["Available"].ToString();
                        v.Category = dt.Rows[0]["Category"] == null ? "" : dt.Rows[0]["Category"].ToString();
                        //CommHelp.SourceConfigId = int.Parse(dt.Rows[0]["UniversityId"].ToString());
                    }

                }
            }
            catch (Exception ex)//
            {
                SqlHelper.ExecuteNonQuery("Update University set IsUpdate=" + (int)EnumHelp.UpdateStartEnum.未启用 + " where UniversityId=" + CommHelp.SourceConfigId);
            }

            return v;
        }

        /// <summary>
        /// 获取视图查询语句
        /// </summary>
        /// <returns></returns>
        public string GetViewDataSqlStr()
        {
            string Sqlstr = string.Empty;
            var viewConfig = GetViewConfig();
            if (viewConfig != null)
            {
                string sqlstr = "SELECT " + viewConfig.Author + "," + viewConfig.Available + "," + viewConfig.Category + "," + viewConfig.Count + "," + viewConfig.Isbn + "," + viewConfig.PublicDate + "," + viewConfig.Title + " FROM " + viewConfig.ViewName;
            }
            return Sqlstr;
        }

        /// <summary>
        /// 获取查询视图中数据数量的语句
        /// </summary>
        /// <returns></returns>
        public string GetViewCountSqlStr()
        {
            string Sqlstr = string.Empty;
            //var viewConfig =CommHelp.viewConfig;
            if (CommHelp.viewConfig != null)
            {
                 Sqlstr = "SELECT count(*) FROM " + CommHelp.viewConfig.ViewName;
            }
            return Sqlstr;
        }

        /// <summary>
        /// 获取视图中数据的总数量
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public int GetViewTotalCount(string connStr, out string mess)
        {
            int count = 0;
            mess = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sqlstr = GetViewCountSqlStr();
                    SqlDataAdapter da = new SqlDataAdapter(sqlstr, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        count = int.Parse(dt.Rows[0][0].ToString());
                    }

                }
            }
            catch (Exception ex)//
            {
                DateTime date = System.DateTime.Now;
                e.UpdateErrorLog(new Model.ErrorLog()
                {
                    ErrorLogId = 0,
                    UniversityId = CommHelp.SourceConfigId,
                    ErrorData = "",
                    ErrorMsg = "第" + CommHelp.UpdateIndex + "次更新异常，获取数据源数据量失败：" + ex.Message,
                    ErrorSrc = "ViewConfigDal->GetViewTotalCount",
                    ErrorTime = date,
                    Status = (int)EnumHelp.UpdateStatus.更新失败,
                    UpdateCount = CommHelp.UpdateIndex,
                    SysAccountId = CommHelp.SysAccountId,
                    Type = (int)EnumHelp.ErrorLogType.获取数据源异常,
                    StartTime = date,
                    EndTime = date
                });


                //e.UpdateErrorLog(new Model.ErrorLog()
                //{
                //    ErrorLogId = CommHelp.ErrorLogId,
                //    UniversityId = CommHelp.SourceConfigId,
                //    UpdateCount = CommHelp.UpdateIndex,
                //    SysAccountId = CommHelp.SysAccountId,
                //    Type = (int)EnumHelp.ErrorLogType.更新记录,
                //    EndTime = date,
                //    ErrorData = "",
                //    ErrorMsg = "第" + CommHelp.UpdateIndex + "次更新异常，获取数据源数据量失败：" + ex.Message,
                //    ErrorSrc = "ViewConfigDal->GetViewTotalCount",
                //    ErrorTime = date,
                //    Status = (int)EnumHelp.UpdateStatus.更新失败,

                //    StartTime = date
                //});
                mess = ex.Message;
            }
            return count;
        }

    }
}
