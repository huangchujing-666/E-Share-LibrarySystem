using LibrarySystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Business;
using LibrarySystem.Domain.Model;

namespace LibrarySystem.Service
{
    public class SysAccountService : ISysAccountService
    {
        /// <summary>
        /// The SysAccount biz
        /// </summary>
        private ISysAccountBusiness _SysAccountBiz;

        public SysAccountService(ISysAccountBusiness SysAccountBiz)
        {
            _SysAccountBiz = SysAccountBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysAccount GetById(int id)
        {
            return this._SysAccountBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SysAccount Insert(SysAccount model)
        {
            return this._SysAccountBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysAccount model)
        {
            this._SysAccountBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysAccount model)
        {
            this._SysAccountBiz.Delete(model);
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param> 
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._SysAccountBiz.IsExistName(name);
        }


        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SysAccount> GetManagerList(string name, int sysRoleId, int UniversityId, int pageNum, int pageSize, out int totalCount)
        {
            return this._SysAccountBiz.GetManagerList(name, sysRoleId, UniversityId, pageNum, pageSize, out totalCount);
        }
        public List<SysAccount> GetAll()
        {
            return this._SysAccountBiz.GetAll();
        }

        public SysAccount Login(string account, string password)
        {
            return this._SysAccountBiz.Login(account, password);
        }

        public SysAccount Login(string account, string password, int universityId)
        {
            return this._SysAccountBiz.Login(account, password, universityId);
        }

        public SysAccount GetAccountByToken(string token_Str)
        {
            return this._SysAccountBiz.GetAccountByToken(token_Str);
        }

        public bool UpdatePassWord(int account_id, string account, string oldPassword, string newPassword, out string msg)
        {
            return this._SysAccountBiz.UpdatePassWord(account_id, account, oldPassword, newPassword, out msg);
        }

        public bool GetAccount(string account)
        {
            return this._SysAccountBiz.GetAccount(account);
        }

        public SysAccount GetAccountByAccount(string account)
        {
            return this._SysAccountBiz.GetAccountByAccount(account);
        }

        public List<SysAccount> GetManagerStudentList(string queryName, string queryPhoneNo, int querySysRoleId, int queryUId, int pageIndex, int pageSize, out int totalCount)
        {
            return this._SysAccountBiz.GetManagerStudentList( queryName,  queryPhoneNo,  querySysRoleId,  queryUId,  pageIndex,  pageSize, out  totalCount);
        }

        public SysAccount GetAccountByAccountUid(string account, int universityId)
        {
            return this._SysAccountBiz.GetAccountByAccountUid(account, universityId);
        }

        public bool IsExitAccountByAccountUid(string account, int universityId)
        {
            return this._SysAccountBiz.IsExitAccountByAccountUid(account, universityId);
        }

        /// <summary>
        /// 根据邮箱获取有效用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public SysAccount GetByEmail(string email)
        {
            return this._SysAccountBiz.GetByEmail(email);
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
            return this._SysAccountBiz.GetAccountByMultiCond(account, universityId,email);
        }
    }
}
