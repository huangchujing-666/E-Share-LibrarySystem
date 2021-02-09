using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpdate.Model
{
    public class University
    {
        private int universityId;
        public int UniversityId
        {
            get { return universityId; }
            set { this.universityId = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        private string service;
        public string Service
        {
            get { return service; }
            set { this.service = value; }
        }

        private string userId;
        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }
        private string userPwd;
        public string UserPwd
        {
            get { return this.userPwd; }
            set { this.userPwd = value; }
        }


        private string dataBase;
        public string DataBase
        {
            get { return this.dataBase; }
            set { this.dataBase = value; }
        }

        private int timeStart;
        public int TimeStart
        {
            get { return this.timeStart; }
            set { this.timeStart = value; }
        }

        private int isUpdate;
        public int IsUpdate {
            get { return this.isUpdate; }
            set { this.isUpdate = value; }
        }
        private int isDelete;
        public int IsDelete
        {
            get { return this.IsDelete; }
            set { this.IsDelete = value; }
        }

        private int status;
        public int Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        private DateTime editTime;

        public DateTime EditTime
        {
            get { return this.editTime; }
            set { this.editTime = value; }
        }

        private DateTime createTime;

        public DateTime CreateTime
        {
            get { return this.createTime; }
            set { this.createTime = value; }
        }
    }
}
