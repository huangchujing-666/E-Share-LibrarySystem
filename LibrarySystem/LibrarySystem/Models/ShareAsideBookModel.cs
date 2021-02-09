using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class ShareAsideBookModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public  int ShareAsideBookId { get; set; }

        /// <summary>
        /// 书号
        /// </summary>
        public  string Isbn { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public  string Title { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public  string Category { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        public  string PublicDate { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public  int Count { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public  string Author { get; set; }

        /// <summary>
        /// 图片Id
        /// </summary>
        public  int BaseImageId { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public  BaseImage BaseImage { get; set; }


        /// <summary>
        /// 尝还方式
        /// </summary>
        public  int PayType { get; set; }

        /// <summary>
        /// 补偿金额
        /// </summary>
        public  int PayMoney { get; set; }
        /// <summary>
        /// 出书人id
        /// </summary>
        public  int SysAccountId { get; set; }

        /// <summary>
        /// 出书人
        /// </summary>
        public  SysAccount SysAccount { get; set; }

        /// <summary>
        /// 运输类型
        /// </summary>
        public  int TrafficType { get; set; }

        /// <summary>
        /// 出书人地址 联系方式
        /// </summary>
        public  string ShareCustomerInfo { get; set; }

        /// <summary>
        /// 操作人id
        /// </summary>
        public  int OperaAccountId { get; set; }
        /// <summary>
        /// 求书状态
        /// </summary>
        public  int ShareStatus { get; set; }

        /// <summary>
        /// 求书订单id
        /// </summary>
        public  int ResearchAsideBookId { get; set; }

        /// <summary>
        /// 求书订单
        /// </summary>
        public  ResearchAsideBook ResearchAsideBook { get; set; }
        /// <summary>
        /// 数据是否有效
        /// </summary>
        public  int IsDelete { get; set; }

        /// <summary>
        /// 数据状态（启用/禁用）
        /// </summary>
        public  int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public  DateTime CreateTime { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public  DateTime EditTime { get; set; }

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
    }
}