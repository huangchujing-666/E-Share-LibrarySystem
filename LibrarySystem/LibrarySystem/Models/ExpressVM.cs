using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Models
{
    public class ExpressVM
    {
        /// <summary>
        /// 漂流订单
        /// </summary>
        public int AsideBookOrderId { get; set; }

        /// <summary>
        /// 借书订单
        /// </summary>
        public int BookOrderId { get; set; }

        public int ResearchAsideBookId { get; set; }
        public int ExpressId { get; set; }

        public string ExpressName { get; set; }

        public string ExpressNo { get; set; }

        public int TrafficFee { get; set; }
    }
}