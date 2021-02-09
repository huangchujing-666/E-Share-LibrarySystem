using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class RegisterVM
    {
        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }
    }
}