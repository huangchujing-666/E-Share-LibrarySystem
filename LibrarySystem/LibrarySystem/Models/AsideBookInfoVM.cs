using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class AsideBookInfoVM: BaseImgInfoVM
    {
        public int AsideBookInfoId { get; set; }

        public string Isbn { get; set; }

        public int BaseImageId { get; set; }
        public string Title { get; set; }

        public string PublicDate { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public int UniversityId { get; set; }

        public string UniversityName { get; set; }

        /// <summary>
        ///  可预借数量
        /// </summary>
        public int Available { get; set; }

        #region 页面搜索条件
        /// <summary>
        /// 书名
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 大学id--数据源
        /// </summary>
        public int QueryUId { get; set; }

        /// <summary>
        /// 图书isbn
        /// </summary>
        public string QueryIsbn { get; set; }

        /// <summary>
        /// 图书类别
        /// </summary>
        public string QueryCategory { get; set; }
        #endregion
        /// <summary>
        /// 页面列表数据
        /// </summary>
        public Paging<AsideBookInfo> Paging { get; set; }

        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }
    }
}