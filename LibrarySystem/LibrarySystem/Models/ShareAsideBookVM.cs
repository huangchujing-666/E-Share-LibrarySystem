using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class ShareAsideBookVM
    {
        public int ShareAsideBookId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }
        public string PublicDate { get; set; }

        public string Author { get; set; }
        /// <summary>
        /// 有偿金额
        /// </summary>
        public int PayMoney { get; set; }

        public int Count { get; set; }
        /// <summary>
        /// 寻书方式
        /// </summary>
        public int PayType { get; set; }

        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public int SysAccountId { get; set; }

        /// <summary>
        /// 求书id
        /// </summary>
        public int ResearchAsideBookId { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string Account { get; set; }

        public SysAccount SysAccount { get; set; }
        /// <summary>
        /// 漂出图书运输类型
        /// </summary>
        public int TrafficType { get; set; }
        /// <summary>
        /// 出书人地址、联系方式
        /// </summary>
        public string ShareCustomerInfo { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string MobilePhone { get; set; }

        public int ShareStatus { get; set; }


        public int BaseImageId { get; set; }

        public BaseImage BaseImage { get; set; }

        /// <summary>
        /// 平台中共享的图书数量
        /// </summary>
        public int ShareCount { get; set; }
        #region 搜索条件

        /// <summary>
        /// 共享方式（有偿 无偿）
        /// </summary>
        public int QueryPayType { get; set; }
        /// <summary>
        /// 共享者账号
        /// </summary>
        public string QuerySysAccount { get; set; }
        /// <summary>
        /// 书名
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 大学id
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
        /// 图书共享状态
        /// </summary>
        public int QueryShareStatus { get; set; }

        /// <summary>
        /// 图书共享运书方式
        /// </summary>
        public int QueryTrafficType { get; set; }

        /// <summary>
        /// 图书共享类型
        /// </summary>
        public int QueryShareType { get; set; }
        #endregion

        #region 数据源
        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }

        public Paging<ShareAsideBook> Paging { get; set; }
        #endregion
    }
}