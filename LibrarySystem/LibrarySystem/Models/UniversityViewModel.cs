using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class UniversityViewModel
    {
        /// <summary>
        /// 大学id
        /// </summary>
        public int UniversityId { get; set; }


        public string Name { get; set; }

        public int IsUpdate { get; set; }
        /// <summary>
        /// 服务器
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBase { get; set; }

        /// <summary>
        /// 时间间隔
        /// </summary>
        public int TimeStart { get; set; }
        public string QueryName { get; set; }

        public Paging<University> Paging { get; set; }

        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }
    }
}