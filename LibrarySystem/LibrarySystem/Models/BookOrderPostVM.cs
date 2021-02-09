using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class BookOrderPostVM
    {
        public int BookOrderId { get; set; }

        /// <summary>
        /// 图书isbn
        /// </summary>
        public string Isbn { get; set; }

        /// <summary>
        /// 大学名称
        /// </summary>
        public int UniversityId { get; set; }

        public int BorrowStatus { get; set; }

        public string Account { get; set; }

        public int Count { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string QueryAccountName { get; set; }


        /// <summary>
        /// 借书状态
        /// </summary>
        public int QueryBorrowStatus { get; set; }
        /// <summary>
        /// 大学id--数据源
        /// </summary>
        public int QueryUId { get; set; }

        /// <summary>
        /// 图书isbn
        /// </summary>
        public string QueryName { get; set; }

        public Paging<BookOrder> Paging { get; set; }

        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }
    }
}