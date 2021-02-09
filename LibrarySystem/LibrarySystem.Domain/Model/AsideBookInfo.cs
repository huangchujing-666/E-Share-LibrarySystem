using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class AsideBookInfo:IAggregateRoot
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int AsideBookInfoId { get; set; }

        /// <summary>
        /// 捐出人
        /// </summary>
        public virtual int SysAccountId { get; set; }
        /// <summary>
        /// 图像实体
        /// </summary>
        public virtual BaseImage BaseImage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int BaseImageId { get; set; }

        /// <summary>
        /// 捐出人id
        /// </summary>
        public virtual SysAccount SysAccount { get; set; }
        /// <summary>
        /// 图书书号
        /// </summary>
        public virtual string Isbn { get; set; }

        /// <summary>
        /// 图书书名
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 出版时间
        /// </summary>
        public virtual string PublicDate { get; set; }

        /// <summary>
        /// 图书作者
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// 图书类别
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// 图书所在院校编号
        /// </summary>
        public virtual int UniversityId { get; set; }

        /// <summary>
        /// 在漂数量
        /// </summary>
        public virtual University University { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 状态
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

        /// <summary>
        /// 可借数量
        /// </summary>
        public virtual int Available { get; set; }

        /// <summary>
        /// 平台图书数量
        /// </summary>
        public virtual int Count { get; set; }

        /// <summary>
        /// 漂出数据
        /// </summary>
        public virtual List<AsideBookOrder> AsideBookOrderList { get; set; }
    }
}
