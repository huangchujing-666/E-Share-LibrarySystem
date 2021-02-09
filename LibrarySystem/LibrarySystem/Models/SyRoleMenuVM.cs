using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class SysRoleMenuVM
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int SysRoleId { get; set; } 
        /// <summary>
        /// 获取菜单
        /// </summary>
        public List<SysMenu> SysMenus { get; set; }
    }
}