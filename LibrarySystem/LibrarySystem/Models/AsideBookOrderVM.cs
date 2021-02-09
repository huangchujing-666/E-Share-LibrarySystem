using LibrarySystem.Admin.Models;
using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class AsideBookOrderVM
    {
        public int AsideBookOrderId { get; set; }

        /// <summary>
        /// 下单人id
        /// </summary>
        public int SysAccountId { get; set; }

        /// <summary>
        /// 下单人
        /// </summary>
        public SysAccount SysAccount { get; set; }

        /// <summary>
        /// 运输人id （顺风送书）
        /// </summary>
        public int TrafficAccountId { get; set; }

        /// <summary>
        /// 运输人信息
        /// </summary>
        public string SenderInfo { get; set; }
        /// <summary>
        /// 图书运输类型
        /// </summary>
        public int TrafficType { get; set; }
        /// <summary>
        /// 图书运输费用
        /// </summary>
        public int TrafficFee { get; set; }

        /// <summary>
        /// 运输地址及联系方式
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 漂书订单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 图片id
        /// </summary>
        public int BaseImageId { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public BaseImage BaseImage { get; set; }
        /// <summary>
        /// 下单人地址信息
        /// </summary>
        public string CustomerInfo { get; set; }
        /// <summary>
        /// 闲置图书id
        /// </summary>
        public int AsideBookInfoId { get; set; }
        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Account { get; set; }

        public int UniversityId { get; set; }

        public string UniversityName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 接送人地址
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        /// 顺利送书的数量
        /// </summary>
        public int TrafficCount { get; set; }

        /// <summary>
        /// 顺利送书所赚费用
        /// </summary>
        public int TotalTrafficFee { get; set; }

        /// <summary>
        /// 平台上所漂入图书数量
        /// </summary>
        public int GetBookCount { get; set; }

        #region 查询条件
        /// <summary>
        /// 求书人账户
        /// </summary>
        public string QuerySysAccount { get; set; }
        /// <summary>
        /// 漂书运输类型
        /// </summary>
        public int QueryTrafType { get; set; }
        /// <summary>
        /// 尝还方式 有偿 无偿
        /// </summary>
        public int QueryPayType { get; set; }
        /// <summary>
        /// 否是接送
        /// </summary>
        public int QueryIsMyRecord { get; set; }
        /// <summary>
        /// 漂书订单状态
        /// </summary>
        public int QueryOrderStatus { get; set; }
        /// <summary>
        /// 图书所在学校
        /// </summary>
        public int QueryUId { get; set; }
        /// <summary>
        /// 图书类别
        /// </summary>
        public string QueryCategory { get; set; }
        /// <summary>
        /// 书名
        /// </summary>
        public string QueryName { get; set; }
        /// <summary>
        /// 图书isbn
        /// </summary>
        public string QueryIsbn { get; set; }
        #endregion

        #region 数据源
        public Paging<AsideBookOrder> Paging { get; set; }

        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }
        #endregion

        #region 订单邮编数据
        public int ExpressId { get; set; }

        public string ExpressName { get; set; }

        public string ExpressNo { get; set; }
        #endregion
    }
}