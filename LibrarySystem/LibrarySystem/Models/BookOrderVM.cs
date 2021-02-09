using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class BookOrderVM
    {
        public int BookOrderId { get; set; }

        /// <summary>
        /// 图书isbn
        /// </summary>
        public string Isbn { get; set; }

        /// <summary>
        /// 图书名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 大学名称
        /// </summary>
        public int UniversityId { get; set; }

        /// <summary>
        /// 大学名称
        /// </summary>
        public string UniversityName { get; set; }

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
        /// 学生所在大学名称
        /// </summary>
        public int QuerySUId { get; set; }
        /// <summary>
        /// 图书isbn
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 图书isbn
        /// </summary>
        public string QueryIsbn { get; set; }

        /// <summary>
        /// 图书id
        /// </summary>
        public int BookInfoId { get; set; }
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
        /// 快递id
        /// </summary>
        public int ExpressId { get; set; }

        /// <summary>
        /// 运输费
        /// </summary>
        public int TrafficFee { get; set; }
        /// <summary>
        /// 快递编号
        /// </summary>

        public string ExpressNo { get; set; }
        /// <summary>
        /// 快递名称
        /// </summary>

        public string ExpressName { get; set; }

        /// <summary>
        /// 是否收到图书
        /// </summary>
        public int IsReceived { get; set; }

        public Paging<BookOrder> Paging { get; set; }

        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }
    }
}