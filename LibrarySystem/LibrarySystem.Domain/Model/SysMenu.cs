using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class SysMenu : IAggregateRoot
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public virtual int SysMenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public virtual string Icon { get; set; }
        /// <summary>
        /// 父菜单Id
        /// </summary>
        public virtual int Fid { get; set; }
        /// <summary>
        /// 菜单排序
        /// </summary>
        public virtual int SortNo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 数据创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 数据修改时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }

         
    }
}
