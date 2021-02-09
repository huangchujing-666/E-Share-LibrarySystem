using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class SysRole : IAggregateRoot
    {
        /// <summary>
        /// 表的主键id
        /// </summary>
        public virtual int SysRoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public virtual string Remarks { get; set; }
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
        /// 数据状态
        /// </summary>
        public virtual DateTime EditTime { get; set; }

         
    }
}
