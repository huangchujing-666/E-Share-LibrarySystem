using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class ViewConfigPostVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int ViewConfigId { get; set; }

        /// <summary>
        /// 是否多次更新
        /// </summary>
        public virtual int UpdateCount { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public virtual string Isbn { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public virtual string Title { get; set; }

        public virtual string PublicDate { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// 图书类别
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public virtual string Count { get; set; }

        /// <summary>
        /// 可借数量
        /// </summary>
        public virtual string Available { get; set; }
        /// <summary>
        /// 视图名称
        /// </summary>
        public virtual string ViewName { get; set; }
        /// <summary>
        /// 大学id
        /// </summary>
        public int UniversityId { get; set; }
    }
}