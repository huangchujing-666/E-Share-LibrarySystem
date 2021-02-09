using LibrarySystem.Core.Data;
using LibrarySystem.Domain;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibrarySystem.Domain.EnumHelp;

namespace LibrarySystem.Business
{
    public class SysAccountBusiness : ISysAccountBusiness
    {
        private IRepository<SysAccount> _repoSysAccount;

        public SysAccountBusiness(
          IRepository<SysAccount> repoSysAccount
          )
        {
            _repoSysAccount = repoSysAccount;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysAccount GetById(int id)
        {
            return this._repoSysAccount.GetById(id);
        }

        public SysAccount Insert(SysAccount model)
        {
            return this._repoSysAccount.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysAccount model)
        {
            this._repoSysAccount.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysAccount model)
        {
            this._repoSysAccount.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SysAccount> GetManagerList(string name, int sysRoleId, int UniversityId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<SysAccount>();
            if (sysRoleId > 0 && sysRoleId != (int)EnumHelp.RoleTypeEnum.学生)
            {
                where = where.And(m => m.SysRoleId == sysRoleId);
            }
            else
            {
                if (sysRoleId == (int)EnumHelp.RoleTypeEnum.学生)
                {
                    where = where.And(m => m.SysRoleId == (int)EnumHelp.RoleTypeEnum.学生);
                }
                else
                {
                    where = where.And(m => m.SysRoleId != (int)EnumHelp.RoleTypeEnum.学生);
                }
            }
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.NickName.Contains(name) || m.Account.Contains(name));
            }
            if (UniversityId > 0)
            {
                where = where.And(m => m.UniversityId == UniversityId);
            }
            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoSysAccount.Table.Where(where).Count();
            return this._repoSysAccount.Table.Where(where).OrderBy(p => p.SysAccountId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<SysAccount> GetManagerStudentList(string queryName, string queryPhoneNo, int querySysRoleId, int queryUId, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<SysAccount>();

            // name过滤
            if (!string.IsNullOrEmpty(queryName))
            {
                where = where.And(m => m.NickName.Contains(queryName) || m.Account.Contains(queryName));
            }
            if (queryUId == 0)//超级管理员
            {
                where = where.And(m => m.UniversityId > 0 && m.SysRoleId == querySysRoleId);
            }
            else if (queryUId > 0)//高校管理员
            {
                where = where.And(m => m.UniversityId == queryUId && m.SysRoleId == querySysRoleId);
            }
            if (!string.IsNullOrWhiteSpace(queryPhoneNo))
            {
                where = where.And(c => c.MobilePhone.Contains(queryPhoneNo));
            }
            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoSysAccount.Table.Where(where).Count();
            return this._repoSysAccount.Table.Where(where).OrderBy(p => p.SysAccountId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<SysAccount> GetAll()
        {
            var where = PredicateBuilder.True<SysAccount>();

            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);

            return this._repoSysAccount.Table.Where(where).OrderBy(p => p.SysAccountId).ToList();
        }


        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoSysAccount.Table.Any(p => p.NickName == name);
        }

        public bool GetAccount(string account)
        {
            return this._repoSysAccount.Table.Any(p => p.Account == account);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysAccount Login(string account, string password)
        {
            return this._repoSysAccount.Table.Where(p => p.Account == account && p.PassWord == password).FirstOrDefault();
        }

        public SysAccount Login(string account, string password, int universityId)
        {
            return this._repoSysAccount.Table.Where(p => p.Account == account && p.PassWord == password && p.UniversityId == universityId).FirstOrDefault();
        }

        public SysAccount GetAccountByToken(string token_Str)
        {
            return this._repoSysAccount.Table.Where(p => p.Token.Equals(token_Str)).FirstOrDefault();
        }

        public bool UpdatePassWord(int account_id, string account, string oldPassword, string newPassword, out string msg)
        {
            msg = "";
            bool result = false;
            //  var sysBusiness = _repoSysAccount.Table.Where(c => c.BusinessInfoId == account_id && c.Account == account && c.PassWord == oldPassword).FirstOrDefault();
            var sysBusiness = _repoSysAccount.Table.Where(c => c.Account == account && c.PassWord == oldPassword).FirstOrDefault();
            if (sysBusiness != null)
            {
                sysBusiness.PassWord = newPassword;
                _repoSysAccount.Update(sysBusiness);
                result = true;
                msg = "更改密码成功";
            }
            else
            {
                msg = "您输入的原始密码错误，请重新输入";
            }
            return result;
        }

        public SysAccount GetAccountByAccount(string account)
        {
            return this._repoSysAccount.Table.Where(c => c.Account.Equals(account) && c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).FirstOrDefault();
        }
        public SysAccount GetAccountByAccountUid(string account, int universityId)
        {
            return this._repoSysAccount.Table.Where(c => c.Account.Equals(account) && c.UniversityId == universityId && c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).FirstOrDefault();
        }

        public bool IsExitAccountByAccountUid(string account, int universityId)
        {
            return this._repoSysAccount.Table.Any(p => p.Account.Equals(account) && p.UniversityId == universityId);
        }

        /// <summary>
        /// 根据邮箱获取有效用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public SysAccount GetByEmail(string email)
        {
            return this._repoSysAccount.Table.Where(c=>c.Email.Equals(email)&&c.IsDelete==(int)EnumHelp.IsDeleteEnum.有效&&c.Status==(int)EnumHelp.EnabledEnum.有效).FirstOrDefault();
        }


        /// <summary>
        /// 根据账户  大学 邮箱 获取有效账户
        /// </summary>
        /// <param name="account"></param>
        /// <param name="universityId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public SysAccount GetAccountByMultiCond(string account, int universityId, string email)
        {
            return this._repoSysAccount.Table.Where(c =>c.Account.Equals(account)&&c.UniversityId==universityId&&c.Email.Equals(email) && c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).FirstOrDefault();
        }
    }
}


