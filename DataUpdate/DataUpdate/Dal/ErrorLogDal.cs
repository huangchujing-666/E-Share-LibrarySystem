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
    public class ErrorLogDal
    {
        /// <summary>
        /// 获取指定数据源为第几次更新
        /// </summary>
        /// <param name="SourceId"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public bool GetUpdateIndex(int SourceId, int SysAccountId, out int Index)
        {
            Index = 0;
            bool result = true;
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable("select top 1 * from ErrorLog where [Type]=" + (int)EnumHelp.ErrorLogType.更新记录 + " and UniversityId=" + SourceId + " order by ErrorLogId desc", new SqlParameter[] { });
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (int.Parse(dt.Rows[0]["Status"].ToString()) == (int)EnumHelp.UpdateStatus.更新成功)
                        {
                            Index = (string.IsNullOrEmpty(dt.Rows[0]["UpdateCount"] == null ? "" : dt.Rows[0]["UpdateCount"].ToString())) ? 1 : int.Parse(dt.Rows[0]["UpdateCount"].ToString()) + 1;
                            DateTime date = System.DateTime.Now;
                            UpdateErrorLog(new ErrorLog()
                            {
                                ErrorLogId = 0,
                                UpdateCount = Index,
                                ErrorTime = date,
                                Type = (int)EnumHelp.ErrorLogType.更新记录,
                                UniversityId = SourceId,
                                ErrorData = "",
                                ErrorSrc = "",
                                Status = (int)EnumHelp.UpdateStatus.更新中,
                                ErrorMsg = "第" + Index + "次更新",
                                SysAccountId = SysAccountId,
                                EndTime = date,
                                StartTime = date
                            });
                            CommHelp.ErrorLogId = GetErrorLogId(Index, (int)EnumHelp.ErrorLogType.更新记录, SourceId);
                        }
                        else if (int.Parse(dt.Rows[0]["Status"].ToString()) == (int)EnumHelp.UpdateStatus.更新失败 || int.Parse(dt.Rows[0]["Status"].ToString()) == (int)EnumHelp.UpdateStatus.更新中)
                        {
                            Index = (string.IsNullOrEmpty(dt.Rows[0]["UpdateCount"] == null ? "" : dt.Rows[0]["UpdateCount"].ToString())) ? 1 : int.Parse(dt.Rows[0]["UpdateCount"].ToString());
                            DateTime date = System.DateTime.Now;
                            CommHelp.ErrorLogId = int.Parse(dt.Rows[0]["ErrorLogId"].ToString());
                            UpdateErrorLog(new ErrorLog()
                            {
                                ErrorLogId = int.Parse(dt.Rows[0]["ErrorLogId"].ToString()),
                                UpdateCount = int.Parse(dt.Rows[0]["UpdateCount"].ToString()),
                                ErrorTime = date,
                                Type = (int)EnumHelp.ErrorLogType.更新记录,
                                UniversityId = SourceId,
                                SysAccountId = SysAccountId,
                                Status = (int)EnumHelp.UpdateStatus.更新中,
                                StartTime = date,
                                EndTime = date
                            });
                        }
                    }
                    else//初次更新
                    {
                        Index += 1;
                        DateTime date = System.DateTime.Now;
                        UpdateErrorLog(new ErrorLog()
                        {
                            ErrorLogId = 0,
                            UpdateCount = Index,
                            ErrorTime = date,
                            Type = (int)EnumHelp.ErrorLogType.更新记录,
                            UniversityId = SourceId,
                            ErrorData = "",
                            ErrorSrc = "",
                            Status = (int)EnumHelp.UpdateStatus.更新中,
                            ErrorMsg = "第" + Index + "次更新",
                            SysAccountId = SysAccountId,
                            EndTime = date,
                            StartTime = date

                        });
                        CommHelp.ErrorLogId = GetErrorLogId(Index, (int)EnumHelp.ErrorLogType.更新记录, SourceId);
                    }

                }
            }
            catch (Exception e)
            {
                DateTime date = System.DateTime.Now;
                UpdateErrorLog(new ErrorLog()
                {
                    ErrorLogId = 0,
                    UpdateCount = Index,
                    ErrorTime = date,
                    StartTime = date,
                    EndTime = date,
                    Type = (int)EnumHelp.ErrorLogType.获取更新次数异常,
                    UniversityId = SourceId,
                    ErrorData = "",
                    ErrorSrc = "ErrorLogDal->GetUpdateIndex",
                    Status = (int)EnumHelp.UpdateStatus.更新失败,
                    ErrorMsg = e.Message,
                    SysAccountId = SysAccountId
                });

                UpdateErrorLog(new ErrorLog()
                {
                    ErrorLogId = CommHelp.ErrorLogId,
                    UpdateCount = Index,
                    ErrorTime = date,
                    StartTime = date,
                    EndTime = date,
                    Type = (int)EnumHelp.ErrorLogType.更新记录,
                    UniversityId = SourceId,
                    ErrorData = "",
                    ErrorSrc = "ErrorLogDal->GetUpdateIndex()",
                    Status = (int)EnumHelp.UpdateStatus.更新失败,
                    ErrorMsg = e.Message,
                    SysAccountId = SysAccountId
                });
                SqlHelper.ExecuteNonQuery("Update University set IsUpdate=" + (int)EnumHelp.UpdateStartEnum.未启用 + " where UniversityId=" + CommHelp.SourceConfigId);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 更新/新增 错误日志状态
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Status"></param>
        public bool UpdateErrorLog(ErrorLog e)
        {
            bool result = false;
            StringBuilder sb = new StringBuilder(200);
            if (e.ErrorLogId > 0)//update
            {
                if (e.Status > 0)
                {
                    switch (e.Status)
                    {
                        case (int)EnumHelp.UpdateStatus.更新成功:
                            sb.Append("update ErrorLog set Status=");
                            sb.Append(e.Status);
                            sb.Append(",EndTime='");
                            sb.Append(e.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            sb.Append("' where UpdateCount=");
                            sb.Append(e.UpdateCount);
                            sb.Append(" and Type=");
                            sb.Append(e.Type);
                            sb.Append(" and UniversityId=");
                            sb.Append(e.UniversityId);
                            sb.Append(" and SysAccountId=");
                            sb.Append(e.SysAccountId);
                            break;
                        case (int)EnumHelp.UpdateStatus.更新中:
                            sb.Append("update ErrorLog set Status=");
                            sb.Append(e.Status);
                            sb.Append(",StartTime='");
                            sb.Append(e.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            sb.Append("',EndTime='");
                            sb.Append(e.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            sb.Append("' where UpdateCount=");
                            sb.Append(e.UpdateCount);
                            sb.Append(" and Type=");
                            sb.Append(e.Type);
                            sb.Append(" and UniversityId=");
                            sb.Append(e.UniversityId);
                            sb.Append(" and SysAccountId=");
                            sb.Append(e.SysAccountId);
                            break;
                        case (int)EnumHelp.UpdateStatus.更新失败:
                            sb.Append("update ErrorLog set Status=");
                            sb.Append(e.Status);
                            sb.Append(",EndTime='");
                            sb.Append(e.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            sb.Append("' where UpdateCount=");
                            sb.Append(e.UpdateCount);
                            sb.Append(" and Type=");
                            sb.Append(e.Type);
                            sb.Append(" and UniversityId=");
                            sb.Append(e.UniversityId);
                            sb.Append(" and SysAccountId=");
                            sb.Append(e.SysAccountId);
                            break;
                        default:
                            break;
                    }
                }
                //sb.Append("update ErrorLog set Status=");
                //sb.Append(e.ErrorTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //sb.Append("' ,StartTime=");
                //sb.Append(e.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //sb.Append("' ,EndTime=");
                //sb.Append(e.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //sb.Append("' ,Status=");
                //sb.Append(e.Status);
                //sb.Append(" where UpdateCount=");
                //sb.Append(e.UpdateCount);
                //sb.Append(" and Type=");
                //sb.Append(e.Type);
                //sb.Append(" and UniversityId=");
                //sb.Append(e.UniversityId);
                //sb.Append(" and SysAccountId=");
                //sb.Append(e.SysAccountId);
            }
            else//insert
            {
                sb.Append("insert into ErrorLog(ErrorMsg,ErrorSrc,StartTime,EndTime,ErrorTime,[Type],ErrorData,SysAccountId,UniversityId,UpdateCount,[Status]) ");
                sb.Append(" values('");
                sb.Append(e.ErrorMsg);
                sb.Append("','");
                sb.Append(e.ErrorSrc);
                sb.Append("','");
                sb.Append(e.StartTime.ToString("yyyy-MM-dd hh:mm:ss"));
                sb.Append("','");
                sb.Append(e.EndTime.ToString("yyyy-MM-dd hh:mm:ss"));
                sb.Append("','");
                sb.Append(e.ErrorTime.ToString("yyyy-MM-dd hh:mm:ss"));
                sb.Append("','");
                sb.Append(e.Type);
                sb.Append("','");
                sb.Append(e.ErrorData);
                sb.Append("',");
                sb.Append(e.SysAccountId);
                sb.Append(",");
                sb.Append(e.UniversityId);
                sb.Append(",");
                sb.Append(e.UpdateCount);
                sb.Append(",");
                sb.Append(e.Status);
                sb.Append(")");

            }
            if (!string.IsNullOrWhiteSpace(sb.ToString()))
                result = SqlHelper.ExecuteNonQuery(sb.ToString()) > 0 ? true : false;

            return result;
        }

        public void DeleteErrorDal(int SourceId, int Index)
        {
            string sqlstr = "delete ErrorLog where Type!=" + EnumHelp.ErrorLogType.更新记录 + " and UniversityId=" + SourceId + " and UpdateCount=" + Index;
            SqlHelper.ExecuteNonQuery(sqlstr);
        }

        public void DeleteErrorlog(string errorData, int undateIndex, int sysAccount)
        {
            string sqlstr = "delete ErrorLog where ErrorData='" + errorData + "' and UniversityId=" + sysAccount + " and UpdateCount=" + undateIndex;
            SqlHelper.ExecuteNonQuery(sqlstr);
        }

        public int GetErrorLogId(int UpdateIndex, int Type, int University)
        {
            int id = 0;
            DataTable dt = SqlHelper.ExecuteDataTable("select top 1 * from ErrorLog where [Type]=" + Type + " and UniversityId=" + University + " and UpdateCount=" + UpdateIndex, new SqlParameter[] { });
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    id= int.Parse(dt.Rows[0]["ErrorLogId"].ToString());
                }
            }
            return id;
        }
    }
}
