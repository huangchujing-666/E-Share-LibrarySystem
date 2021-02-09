using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class BookOrder:IAggregateRoot
    {
        /// <summary>
        /// 表的主键id
        /// </summary>
        public virtual int BookOrderId { get; set; }
        /// <summary>
        /// 借阅用户id
        /// </summary>
        public virtual int SysAccountId { get; set; }
        /// <summary>
        /// 用户实体
        /// </summary>
        public virtual SysAccount SysAccount { get; set; }

        /// <summary>
        /// 图书id
        /// </summary>
        public virtual int BookInfoId { get; set; }

        /// <summary>
        /// 运书方式
        /// </summary>
        public virtual int TrafficType { get; set; }

        /// <summary>
        /// 图书实体
        /// </summary>
        public virtual BookInfo BookInfo { get; set; }
        /// <summary>
        /// 快递id
        /// </summary>
        public virtual int ExpressId { get; set; }

        /// <summary>
        /// 大学id
        /// </summary>
        public virtual int UniversityId { get; set; }

        /// <summary>
        /// 大学
        /// </summary>
        public virtual University University { get; set; }
        /// <summary>
        /// 快递实体
        /// </summary>
        public virtual Express Express { get; set; }
        /// <summary>
        /// 借书数量
        /// </summary>
        public virtual int Count { get; set;}
        /// <summary>
        /// 逾期费用
        /// </summary>
        public virtual decimal Ticket { get; set; }
        /// <summary>
        /// 借书状态
        /// </summary>
        public virtual int BorrowStatus { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 是否已经收到图书
        /// </summary>
        public virtual int IsReceived { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 操作人id
        /// </summary>
        public virtual int EditPersonId { get; set; }
        /// <summary>
        /// 数据修改时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }
        /// <summary>
        /// 数据创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 归还时间
        /// </summary>
        public virtual DateTime BackTime { get; set; }
    }
}
