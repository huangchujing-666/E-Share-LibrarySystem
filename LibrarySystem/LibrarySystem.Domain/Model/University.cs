using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class University: IAggregateRoot
    {
        /// <summary>
        /// 大学id
        /// </summary>
        public virtual int UniversityId { get; set; }

        /// <summary>
        /// 大学名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 服务器
        /// </summary>
        public virtual string Service { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public virtual string UserId { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public virtual string UserPwd { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public virtual string  DataBase{get;set;}

        /// <summary>
        /// 时间间隔
        /// </summary>
        public virtual int TimeStart { get; set; }

        /// <summary>
        /// 是否启动更新
        /// </summary>
        public virtual int IsUpdate { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }
    }
}
