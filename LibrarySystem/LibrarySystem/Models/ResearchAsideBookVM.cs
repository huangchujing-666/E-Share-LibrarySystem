using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class ResearchAsideBookVM:BaseImgInfoVM
    {
        public int ResearchAsideBookId { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }
        public string PublicDate { get; set; }

        public string Author { get; set; }

        public string Remark { get; set; }

        public int UniversityId { get; set; }

        public string UniversityName { get; set; }

        public int SysAccountId { get; set; }

        public SysAccount SysAccount { get; set; }

        public string CustomerInfo { get; set; }

        public string SearchAccountName { get; set; }

        public int ShareSysAccountId { get; set; }

        public int ResearchStatus { get; set; }

        public BaseImage BaseImage { get; set; }

        /// <summary>
        /// 漂出图书运输类型
        /// </summary>
        public int TrafficType { get; set; }

        /// <summary>
        /// 出书人地址、联系方式
        /// </summary>
        public string ShareCustomerInfo { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 出书记录
        /// </summary>
        public int ShareAsideBookId { get; set; }

        /// <summary>
        /// 有偿金额
        /// </summary>
        public int PayMoney { get; set; }

        /// <summary>
        /// 寻书方式
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 求书成功数量
        /// </summary>
        public int ResearchSuccessfulCount { get; set; }
        #region 搜索条件
        /// <summary>
        /// 求书人账户
        /// </summary>
        public string QuerySysAccount { get; set; }
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
        /// 求书方式 有偿/无偿
        /// </summary>
        public int QueryPayType { get; set; }

        /// <summary>
        /// 求书状态 找到/未找到
        /// </summary>
        private int queryResearchStatus = -1;

        public int QueryResearchStatus
        {
            get { return queryResearchStatus; }
            set { queryResearchStatus = value; }
        }

        /// <summary>
        ///  求书人  我的求书=1  全部求书=2
        /// </summary>
        public int QueryIsMyResearch { get; set; }
        #endregion
        #region 数据源
        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }

        public Paging<ResearchAsideBook> Paging { get; set; }
        #endregion
        #region 订单邮编数据
        public int ExpressId { get; set; }

        public string ExpressName { get; set; }

        public string ExpressNo { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public int TrafficFee { get; set; }
        #endregion
    }
}