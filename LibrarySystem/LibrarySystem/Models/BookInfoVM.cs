using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;

namespace LibrarySystem.Admin.Models
{
    public class BookInfoVM
    {
        public int IsBorrow { get; set; }
        public int BookInfoId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string PublicDate { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public int UniversityId { get; set; }

        public string UniversityName { get; set; }

        /// <summary>
        ///  可预借数量
        /// </summary>
        public int Available { get; set; }

        public int Count { get; set; }

        public int BorrowCount { get; set; }

        public int AvaliableCount { get; set; }
        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 大学id--数据源
        /// </summary>
        public int QueryUId { get; set; }

        /// <summary>
        /// 图书isbn
        /// </summary>
        public string QueryIsbn { get; set; }

        /// <summary>
        /// 图书类别
        /// </summary>
        public string QueryCategory { get; set; }

        /// <summary>
        /// 借书状态
        /// </summary>
        public int QueryBorrowStatus { get; set; }
        public Paging<BookInfo> Paging { get;  set; }

        public Paging<UserBookInfoVM> UserPaging { get; set; }


        //用户id
        public int SysAccountId { get; set; }

        /// <summary>
        /// 个人电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 个人移动电话
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 运书类型  自取或邮寄
        /// </summary>
        public int TrafficType { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 借书订单Id
        /// </summary>
        public int BookOrderId { get; set; }
    }
}