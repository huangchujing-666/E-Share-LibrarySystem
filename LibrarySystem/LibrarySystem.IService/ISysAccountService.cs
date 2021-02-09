﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Model;

namespace LibrarySystem.IService
{
    public interface ISysAccountService
    {
        SysAccount GetById(int id);

        SysAccount Insert(SysAccount model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(SysAccount model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(SysAccount model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<SysAccount> GetManagerList(string name, int sysRoleId ,int UniversityId,int pageNum, int pageSize, out int totalCount);

        List<SysAccount> GetAll();

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsExistName(string name);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysAccount Login(string account, string password);
        SysAccount GetAccountByToken(string token_Str);
        bool UpdatePassWord(int account_id, string account, string oldPassword, string newPassword, out string msg);
        bool GetAccount(string account);
        SysAccount GetAccountByAccount(string account);
        SysAccount Login(string account, string v, int universityId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryName"></param>
        /// <param name="queryPhoneNo"></param>
        /// <param name="querySysRoleId">查找的角色</param>
        /// <param name="queryUId">管理人员的大学ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<SysAccount> GetManagerStudentList(string queryName, string queryPhoneNo, int querySysRoleId, int queryUId, int pageIndex, int pageSize, out int totalCount);
        SysAccount GetAccountByAccountUid(string account, int universityId);
        bool IsExitAccountByAccountUid(string account, int universityId);
        /// <summary>
        /// 根据邮箱获取有效用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        SysAccount GetByEmail(string email);

        /// <summary>
        /// 根据账户  大学 邮箱 获取有效账户
        /// </summary>
        /// <param name="account"></param>
        /// <param name="universityId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        SysAccount GetAccountByMultiCond(string account, int universityId, string email);
    }
}