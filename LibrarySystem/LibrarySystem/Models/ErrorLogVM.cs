using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class ErrorLogVM
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public int QueryType { get; set; }
        /// <summary>
        /// 更新状态
        /// </summary>
        public int QueryStatus { get; set; }
        /// <summary>
        /// 更新数据源
        /// </summary>
        public int QueryUniversityId { get; set; }

        /// <summary>
        /// 第几次更新
        /// </summary>
        public int QueryCount { get; set; }
        /// <summary>
        /// 数据列表
        /// </summary>
        public Paging<ErrorLog> Paging { get; set; }

        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }
    }
}