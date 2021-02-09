using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class AsideBookOrderModel
    {

        /// <summary>
        /// 表的主键id
        /// </summary>
        public  int AsideBookOrderId { get; set; }
        /// <summary>
        /// 漂书用户id
        /// </summary>
        public  int SysAccountId { get; set; }
        /// <summary>
        /// 用户实体
        /// </summary>

        public  SysAccount SysAccount { get; set; }
        /// <summary>
        /// 漂流图书id
        /// </summary>

        public  int AsideBookInfoId { get; set; }
        /// <summary>
        /// 漂流图书
        /// </summary>

        public  AsideBookInfo AsideBookInfo { get; set; }
        /// <summary>
        /// 邮寄信息id
        /// </summary>

        public  int ExpressId { get; set; }
        /// <summary>
        /// 邮寄实体
        /// </summary>

        public  Express Express { get; set; }
        /// <summary>
        /// 运输对象id
        /// </summary>

        public  int TrafficAccountId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public  int Count { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public  int OrderStatus { get; set; }
        /// <summary>
        /// 配送人信息
        /// </summary>
        public  string SenderInfo { get; set; }
        /// <summary>
        /// 用户地址信息
        /// </summary>
        public  string CustomerInfo { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>

        public  string Remark { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public  int TrafficType { get; set; }
        /// <summary>
        /// 运费金额
        /// </summary>
        public  int TrafficFee { get; set; }
        /// <summary>
        /// 是否删除数据
        /// </summary>
        public  int IsDelete { get; set; }
        /// <summary>
        /// 图书是否启用状态
        /// </summary>

        public  int Status { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 接送人地址
        /// </summary>
        public string SenderAddress { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public  DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public  DateTime EditTime { get; set; }
    }
}