﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Common
{
    /// <summary>
    /// 登录公共常量
    /// </summary>
    public static class LoginerConst
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        public const string ACCOUNT_ID = "AccountId";
        /// <summary>
        /// 账号
        /// </summary>
        public const string ACCOUNT = "Account";
        /// <summary>
        /// 昵称
        /// </summary>
        public const string NICKNAME = "NickName";  
        /// <summary>
        /// 头像图片
        /// </summary>
        public const string ACCOUNT_IMG = "AccountImg";
        /// <summary>
        /// 用户组ID
        /// </summary>
        public const string ROLE_ID = "RoleId";

        public const string ROLE_NAME = "RoleName";

        /// <summary>
        /// 商家ID
        /// </summary>
        public const string BUSINESS_ID = "BusinessId";

        /// <summary>
        /// 学校名称
        /// </summary>
        public const string UNIVERSITY_ID = "UniversityId";
        /// <summary>
        /// 学校名称
        /// </summary>
        public const string UNIVERSITY_NAME = "UniversityName";
    }

    /// <summary>
    /// 用户登录信息
    /// </summary>
    public static class Loginer
    {
        /// <summary>
        /// 商家ID
        /// </summary>
        public static string BusinessId
        {
            get
            {
                if (SessionHelper.Get(LoginerConst.BUSINESS_ID) == null)
                {
                    return null;
                }
                return SessionHelper.Get(LoginerConst.BUSINESS_ID);
            }
        }

        /// <summary>
        /// 账号
        /// </summary>
        public static string Account
        {
            get
            {
                if (SessionHelper.Get(LoginerConst.ACCOUNT) == null)
                {
                    return null;
                }
                return SessionHelper.Get(LoginerConst.ACCOUNT);
            }
        }

        /// <summary>
        /// 账号
        /// </summary>
        public static string NickName
        {
            get
            {
                if (SessionHelper.Get(LoginerConst.NICKNAME) == null)
                {
                    return null;
                }
                return SessionHelper.Get(LoginerConst.NICKNAME);
            }
        }

        /// <summary>
        /// 账号Id
        /// </summary>
        public static int AccountId
        {
            get
            {
                if (SessionHelper.Get(LoginerConst.ACCOUNT_ID) == null)
                {
                    return 0;
                }
                return Convert.ToInt32(SessionHelper.Get(LoginerConst.ACCOUNT_ID));
            }
        }

        /// <summary>
        /// 头像图片
        /// </summary>
        public static string AccountImg
        {
            get
            {
                if (SessionHelper.Get(LoginerConst.ACCOUNT_IMG) == null)
                {
                    return null;
                }
                return SessionHelper.Get(LoginerConst.ACCOUNT_IMG);
            }
        }

        /// <summary>
        /// 用户组ID
        /// </summary>
        public static int RoleId
        {
            get
            {
                if (SessionHelper.Get(LoginerConst.ROLE_ID) == null)
                {
                    return 0;
                }
                return Convert.ToInt32(SessionHelper.GetSession(LoginerConst.ROLE_ID));
            }
        }

        /// <summary>
        /// 账号Id
        /// </summary>
        public static string RoleName
        {
            get
            {
                if (SessionHelper.Get(LoginerConst.ROLE_NAME) == null)
                {
                    return "";
                }
                return SessionHelper.Get(LoginerConst.ROLE_NAME);
            }
        }

        public static int UniversityId
        {
            get
            {
                if (SessionHelper.Get(LoginerConst.UNIVERSITY_ID) == null)
                {
                    return 0;
                }
                return Convert.ToInt32(SessionHelper.GetSession(LoginerConst.UNIVERSITY_ID));
            }
        }


        public static string UniversityName
        {
            get
            {
                if (SessionHelper.Get(LoginerConst.UNIVERSITY_NAME) == null)
                {
                    return null;
                }
                return SessionHelper.Get(LoginerConst.UNIVERSITY_NAME);
            }
        }

        /// <summary>
        /// 删除账号缓存
        /// </summary>
        public static void DelAccountCache()
        {
            SessionHelper.Del(LoginerConst.ACCOUNT);
            SessionHelper.Del(LoginerConst.ACCOUNT_ID);
            SessionHelper.Del(LoginerConst.NICKNAME);
            SessionHelper.Del(LoginerConst.ACCOUNT_IMG);
            SessionHelper.Del(LoginerConst.ROLE_ID);
            SessionHelper.Del(LoginerConst.BUSINESS_ID);
            SessionHelper.Del(LoginerConst.UNIVERSITY_ID);
            SessionHelper.Del(LoginerConst.UNIVERSITY_NAME);
        }
    }
}