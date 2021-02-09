using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class UserBookInfoVM
    {
        public int BookOrderId { get; set; }
        public int BookInfoId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string PublicDate { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public int UniversityId { get; set; }

        public int AccountUniversityId { get; set; }

        public string  UniversityName { get; set; }

        public int TrafficType { get; set; }
        /// <summary>
        /// 借书状态
        /// </summary>
        public string BorrowStatus { get; set; }

        /// <summary>
        /// 借书状态枚举
        /// </summary>
        public int BookStatus { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }

        /// <summary>
        /// 可借数量
        /// </summary>
        public int Available { get; set; }

        /// <summary>
        /// 是否已借
        /// </summary>
        public bool IsBorrow { get; set; }

        /// <summary>
        /// 运输费
        /// </summary>
        public int TrafficFee { get; set; }

        public string ExpressNo { get; set; }

        public string ExpressName { get; set; }

        /// <summary>
        /// 运书id
        /// </summary>
        public int ExpressId { get; set; }

        /// <summary>
        /// 是否已收到
        /// </summary>
        public int IsReceived { get; set; }
    }
}