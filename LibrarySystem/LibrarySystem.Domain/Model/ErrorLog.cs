using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class ErrorLog:IAggregateRoot
    {
        /// <summary>
        /// 表的主键id
        /// </summary>
        public virtual int ErrorLogId { get; set; }
        /// <summary>
        /// 错误路径
        /// </summary>
        public virtual string ErrorSrc { get; set; }
        /// <summary>
        /// 更新信息或错误信息
        /// </summary>
        public virtual string ErrorMsg { get; set; }
        /// <summary>
        /// 发生错误时间
        /// </summary>
        public virtual DateTime ErrorTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndTime { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public virtual int Type { get; set; }
        /// <summary>
        /// 发生错误的数据
        /// </summary>
        public virtual string ErrorData { get; set; }
        /// <summary>
        /// 更新次数
        /// </summary>
        public virtual int UpdateCount { get; set; }
        /// <summary>
        /// 更新状态
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 更新数据源Id
        /// </summary>
        public virtual int UniversityId { get; set; }
        /// <summary>
        /// 数据源实体
        /// </summary>
        public virtual University University { get; set; }
        /// <summary>
        /// 操作人Id
        /// </summary>
        public virtual int SysAccountId { get; set; }
        /// <summary>
        /// 操作人实体
        /// </summary>
        public virtual SysAccount SysAccount { get; set; }
    }
}
