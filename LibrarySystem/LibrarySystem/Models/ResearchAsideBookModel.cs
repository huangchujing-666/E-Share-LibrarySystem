using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Models
{
    public class ResearchAsideBookModel
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

        /// <summary>
        /// 移动电话
        /// </summary>
        public  string MobilePhone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public  string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public  string Address { get; set; }

        public string CustomerInfo { get; set; }

        public string SearchAccountName { get; set; }

        public int ShareSysAccountId { get; set; }

        public int ResearchStatus { get; set; }

        public BaseImage BaseImage { get; set; }

        public int BaseImageId { get; set; }

        /// <summary>
        /// 漂出图书运输类型
        /// </summary>
        public int TrafficType { get; set; }

        /// <summary>
        /// 出书人地址、联系方式
        /// </summary>
        public string ShareCustomerInfo { get; set; }

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