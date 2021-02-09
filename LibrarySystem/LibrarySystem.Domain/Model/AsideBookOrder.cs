using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class AsideBookOrder: IAggregateRoot
    {
        /// <summary>
        /// 表的主键id
        /// </summary>
        public virtual int AsideBookOrderId { get; set; }
        /// <summary>
        /// 漂书用户id
        /// </summary>
        public virtual int SysAccountId { get; set; }
        /// <summary>
        /// 用户实体
        /// </summary>

        public virtual SysAccount SysAccount { get; set; }
        /// <summary>
        /// 漂流图书id
        /// </summary>

        public virtual int AsideBookInfoId { get; set; }
        /// <summary>
        /// 漂流图书
        /// </summary>

        public virtual AsideBookInfo AsideBookInfo { get; set; }
        /// <summary>
        /// 邮寄信息id
        /// </summary>

        public virtual int ExpressId { get; set; }
        /// <summary>
        /// 邮寄实体
        /// </summary>

        public virtual Express Express { get; set; }
        /// <summary>
        /// 运输对象id
        /// </summary>

        public virtual int TrafficAccountId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public virtual int Count { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public virtual int OrderStatus { get; set; }
        /// <summary>
        /// 配送人信息
        /// </summary>
        public virtual string SenderInfo { get; set; }
        /// <summary>
        /// 用户地址信息
        /// </summary>
        //public virtual string CustomerInfo { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>

        public virtual string Remark { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public virtual int TrafficType { get; set; }
        /// <summary>
        /// 运费金额
        /// </summary>
        public virtual int TrafficFee { get; set; }
        /// <summary>
        /// 是否删除数据
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 图书是否启用状态
        /// </summary>

        public virtual int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }
    }
}
