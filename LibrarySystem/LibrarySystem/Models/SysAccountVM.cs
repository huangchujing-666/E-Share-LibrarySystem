using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibrarySystem.Admin.Models;
using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;

namespace LibrarySystem.Admin.Models
{
    public class SysAccountVM: BaseImgInfoVM
    {
        public int V { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 实体信息
        /// </summary>
        public SysAccount SysAccount { get; set; }
        /// <summary>
        /// 实体集合
        /// </summary>
        public List<SysAccount> SysAccounts { get; set; }

        public List<SysRole> SysRoles { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<SysAccount> Paging { get; set; }

        /// <summary>
        /// 下拉菜单
        /// </summary>
        public List<University> UinversityList { get; set; }


        //查询条件
        public string QueryName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string QueryPhoneNo { get; set; }
        public int QueryType { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public int QuerySysRoleId { get; set; }

        /// <summary>
        /// 大学id--数据源
        /// </summary>
        public int QueryUId { get; set; }

        /// <summary>
        /// 大学id
        /// </summary>
        public int UniversityId { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}