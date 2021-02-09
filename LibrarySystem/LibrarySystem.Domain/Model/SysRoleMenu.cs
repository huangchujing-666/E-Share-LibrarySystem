using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class SysRoleMenu : IAggregateRoot
    {
        /// <summary>
        /// 表的主键id
        /// </summary>
        public virtual int SysRoleMenuId { get; set; }
        /// <summary>
        ///  菜单id   
        /// </summary>
        public virtual int SysMenuId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public virtual int SysRoleId { get; set; }
        /// <summary>
        /// 数据创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 数据修改时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }
        /// <summary>
        /// 菜单实体
        /// </summary>
         public virtual SysMenu SysMenu { get; set; }
    }
}
