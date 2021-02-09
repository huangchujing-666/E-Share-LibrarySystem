using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Model
{
    public class SysAccount : IAggregateRoot
    {
        /// <summary>
        /// 表的主键id
        /// </summary>
        public virtual int SysAccountId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public virtual int SysRoleId { get; set; }
        /// <summary>
        /// 用户所属大学
        /// </summary>

        public virtual int UniversityId { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>
        public virtual string Account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public virtual string PassWord { get; set; }
        /// <summary>
        /// 账户头像id
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public virtual string MobilePhone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public virtual string NickName { get; set; }
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
        /// 数据修改时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }
        /// <summary>
        /// 用户Token
        /// </summary>
        public virtual string Token { get; set; }
        /// <summary>
        /// 头像实体
        /// </summary>
        public virtual BaseImage BaseImage { get; set; }
        /// <summary>
        /// 角色实体
        /// </summary>
        public virtual SysRole SysRole { get; set; }
        /// <summary>
        /// 大学实体
        /// </summary>
        public virtual University University { get; set; }

    }
}
