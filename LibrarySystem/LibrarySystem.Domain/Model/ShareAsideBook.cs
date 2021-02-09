using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class ShareAsideBook: IAggregateRoot
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int ShareAsideBookId { get; set; }

        /// <summary>
        /// 书号
        /// </summary>
        public virtual string Isbn { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        public virtual string PublicDate { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int Count { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// 图片Id
        /// </summary>
        public virtual int BaseImageId { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public virtual BaseImage BaseImage { get; set; }


        /// <summary>
        /// 尝还方式
        /// </summary>
        public virtual int PayType { get; set; }

        /// <summary>
        /// 补偿金额
        /// </summary>
        public virtual int PayMoney { get; set; }
        /// <summary>
        /// 出书人id
        /// </summary>
        public virtual int SysAccountId { get; set; }

        /// <summary>
        /// 出书人
        /// </summary>
        public virtual SysAccount SysAccount { get; set; }

        /// <summary>
        /// 运输类型
        /// </summary>
        public virtual int TrafficType { get; set; }

        /// <summary>
        /// 出书人地址 联系方式
        /// </summary>
        //public virtual string ShareCustomerInfo { get; set; }

        /// <summary>
        /// 操作人id
        /// </summary>
        public virtual int OperaAccountId { get; set; }
        /// <summary>
        /// 求书状态
        /// </summary>
        public virtual int ShareStatus { get; set; }

        /// <summary>
        /// 求书订单id
        /// </summary>
        public virtual int ResearchAsideBookId { get; set; }

        /// <summary>
        /// 求书订单
        /// </summary>
        public virtual ResearchAsideBook ResearchAsideBook { get; set; }
        /// <summary>
        /// 数据是否有效
        /// </summary>
        public virtual int IsDelete { get; set; }

        /// <summary>
        /// 数据状态（启用/禁用）
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }
    }
}
