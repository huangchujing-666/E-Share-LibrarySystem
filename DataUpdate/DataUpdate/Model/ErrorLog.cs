using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpdate.Model
{
    public class ErrorLog
    {
        private int errorLogId;
        public int ErrorLogId
        {
            get { return this.errorLogId; }
            set { this.errorLogId = value; }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get { return this.errorMsg; }
            set { this.errorMsg = value; }
        }

        private string errorSrc;
        public string ErrorSrc
        {
            get { return this.errorSrc; }
            set { this.errorSrc = value; }
        }

        private DateTime errorTime;
        public DateTime ErrorTime
        {
            get { return this.errorTime; }
            set { this.errorTime = value; }
        }

        private DateTime startTime;
        public DateTime StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }
        private DateTime endTime;
        public DateTime EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }

        private int type;
        public int Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        private string errorData;
        public string ErrorData
        {
            get { return this.errorData; }
            set { this.errorData = value; }
        }
        private int sysAccountId;
        public int SysAccountId
        {
            get { return this.sysAccountId; }
            set { this.sysAccountId = value; }
        }
        private int universityId;
        public int UniversityId
        {
            get { return this.universityId; }
            set { this.universityId = value; }
        }
        private int updateCount;
        public int UpdateCount
        {
            get { return this.updateCount; }
            set { this.updateCount = value; }
        }
        private int status;
        public int Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
