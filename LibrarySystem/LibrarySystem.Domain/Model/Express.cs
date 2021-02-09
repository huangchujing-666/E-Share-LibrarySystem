using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public  class Express: IAggregateRoot
    {
        /// <summary>
        /// 主键id    
        /// </summary>
        public virtual int ExpressId { get; set; }
        /// <summary>
        /// 快递名称
        /// </summary>
        public virtual string ExpressName { get; set; }
        /// <summary>
        /// 快递编号
        /// </summary>
        public virtual string ExpressNo { get; set; }
        /// <summary>
        /// 快递费用
        /// </summary>
        public virtual int TrafficFee { get; set; }
        
    }
}
