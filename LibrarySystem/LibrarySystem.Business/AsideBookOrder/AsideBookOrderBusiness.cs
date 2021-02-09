using LibrarySystem.Core.Data;
using LibrarySystem.Domain;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Business
{
    public class AsideBookOrderBusiness : IAsideBookOrderBusiness
    {
        private IRepository<AsideBookOrder> _repoasideBookOrder;

        public AsideBookOrderBusiness(IRepository<AsideBookOrder> repoAsideBookOrder)
        {
            this._repoasideBookOrder = repoAsideBookOrder;
        }
        public void Delete(AsideBookOrder model)
        {
            this._repoasideBookOrder.Delete(model);
        }

        public AsideBookOrder GetById(int id)
        {
            return this._repoasideBookOrder.GetById(id);
        }

        public AsideBookOrder GetByIsbn(string isbn)
        {
            if (!String.IsNullOrWhiteSpace(isbn))
            {
                var where = PredicateBuilder.True<AsideBookOrder>();
                return this._repoasideBookOrder.Table.Where(c => c.AsideBookInfo.Isbn.Contains(isbn)).FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        /// 获取闲置图书所在大学的订单记录
        /// </summary>
        /// <param name="uinversityId">闲置图书所在大学</param>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public AsideBookOrder GetByUniversityIsbn(int uinversityId, string isbn)
        {
            return this._repoasideBookOrder.Table.Where(c => c.AsideBookInfo.UniversityId == uinversityId && c.AsideBookInfo.Isbn.Equals(isbn)).FirstOrDefault();
        }

        public List<AsideBookOrder> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<AsideBookOrder>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                where = where.And(m => m.AsideBookInfo.Title.Contains(name));
            }

            totalCount = this._repoasideBookOrder.Table.Where(where).Count();
            return this._repoasideBookOrder.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<AsideBookOrder> GetManagerList(string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<AsideBookOrder>();
            if (queryUId > 0)
            {
                where = where.And(m => m.AsideBookInfo.UniversityId == queryUId);
            }
            if (!string.IsNullOrWhiteSpace(queryName))
            {
                where = where.And(m => m.AsideBookInfo.Title.Contains(queryName));
            }
            if (!string.IsNullOrWhiteSpace(queryIsbn))
            {
                where = where.And(m => m.AsideBookInfo.Isbn.Contains(queryIsbn));
            }
            if (!string.IsNullOrWhiteSpace(queryCategory))
            {
                where = where.And(m => m.AsideBookInfo.Category.Contains(queryCategory));
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效);
            totalCount = this._repoasideBookOrder.Table.Where(where).Count();
            return this._repoasideBookOrder.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }


        /// <summary>
        /// 多条件查询订单列表 用户界面
        /// </summary>
        /// <param name="querySysAccount">下单人账户</param>
        /// <param name="queryTrafType">运输类型</param>
        /// <param name="queryOrderStatus">订单状态</param>
        /// <param name="queryName">书名</param>
        /// <param name="queryIsbn">书号</param>
        /// <param name="queryUId">图书所在大学</param>
        /// <param name="queryCategory">类别</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="totalCount">数据总量</param>
        /// <returns></returns>
        public List<AsideBookOrder> GetAccountManagerList(int accountId, int queryTrafType, int queryOrderStatus, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<AsideBookOrder>();
            if (accountId > 0)
            {
                where = where.And(m => m.SysAccountId == accountId);
            }
            if (queryTrafType > 0)
            {
                where = where.And(m => m.TrafficType == queryTrafType);
            }
            if (queryOrderStatus > 0)
            {
                where = where.And(m => m.OrderStatus == queryOrderStatus);
            }
            if (queryUId > 0)
            {
                where = where.And(m => m.AsideBookInfo.UniversityId == queryUId);
            }
            if (!string.IsNullOrWhiteSpace(queryName))
            {
                where = where.And(m => m.AsideBookInfo.Title.Contains(queryName));
            }
            if (!string.IsNullOrWhiteSpace(queryIsbn))
            {
                where = where.And(m => m.AsideBookInfo.Isbn.Contains(queryIsbn));
            }
            if (!string.IsNullOrWhiteSpace(queryCategory))
            {
                where = where.And(m => m.AsideBookInfo.Category.Contains(queryCategory));
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效);
            totalCount = this._repoasideBookOrder.Table.Where(where).Count();
            return this._repoasideBookOrder.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public AsideBookOrder Insert(AsideBookOrder model)
        {
            return this._repoasideBookOrder.Insert(model);
        }

        public void Update(AsideBookOrder model)
        {
            this._repoasideBookOrder.Update(model);
        }

        /// <summary>
        /// 多条件查询订单列表    管理员列表界面
        /// </summary>
        /// <param name="uid">管理员所在大学</param>
        /// <param name="querySysAccount">下单人账户</param>
        /// <param name="queryTrafType">运输类型</param>
        /// <param name="queryOrderStatus">订单状态</param>
        /// <param name="queryName">书名</param>
        /// <param name="queryIsbn">书号</param>
        /// <param name="queryUId">漂书人所在大学</param>
        /// <param name="queryCategory">类别</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="totalCount">数据总量</param>
        /// <returns></returns>
        public List<AsideBookOrder> GetManagerList(int uid, string querySysAccount, int queryTrafType, int queryOrderStatus, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<AsideBookOrder>();
            if (!string.IsNullOrWhiteSpace(querySysAccount))
            {
                where = where.And(m => m.SysAccount.Account.Contains(querySysAccount));
            }
            if (queryTrafType > 0)
            {
                where = where.And(m => m.TrafficType == queryTrafType);
            }
            if (queryOrderStatus > 0)
            {
                where = where.And(m => m.OrderStatus == queryOrderStatus);
            }
            if (queryUId > 0)
            {
                where = where.And(m => m.SysAccount.UniversityId == queryUId);
            }
            if (!string.IsNullOrWhiteSpace(queryName))
            {
                where = where.And(m => m.AsideBookInfo.Title.Contains(queryName));
            }
            if (!string.IsNullOrWhiteSpace(queryIsbn))
            {
                where = where.And(m => m.AsideBookInfo.Isbn.Contains(queryIsbn));
            }
            if (!string.IsNullOrWhiteSpace(queryCategory))
            {
                where = where.And(m => m.AsideBookInfo.Category.Contains(queryCategory));
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && (c.SysAccount.UniversityId == uid || c.AsideBookInfo.UniversityId == uid));
            totalCount = this._repoasideBookOrder.Table.Where(where).Count();
            return this._repoasideBookOrder.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// 根据用户Id,图书id判断该用户是否已漂入此书 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="asideBookInfoId"></param>
        /// <returns></returns>
        public AsideBookOrder GetByOrdered(int accountId, int asideBookInfoId)
        {
            var where = PredicateBuilder.True<AsideBookOrder>();
            if (accountId > 0 && asideBookInfoId > 0)
            {
                where = where.And(m => m.SysAccountId == accountId && m.AsideBookInfoId == asideBookInfoId && (m.OrderStatus == (int)EnumHelp.BookOrderStatus.已完结 || m.OrderStatus == (int)EnumHelp.BookOrderStatus.待自取 || m.OrderStatus == (int)EnumHelp.BookOrderStatus.待邮寄 || m.OrderStatus == (int)EnumHelp.BookOrderStatus.待顺风送 || m.OrderStatus == (int)EnumHelp.BookOrderStatus.运输中));
                where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效);
                return this._repoasideBookOrder.Table.Where(where).FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        /// 用户“顺路送书”列表
        /// </summary>
        /// <param name="queryUId"></param>
        /// <param name="queryTrafType"></param>
        /// <param name="queryIsMyRecord"></param>
        /// <param name="queryIsbn"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<AsideBookOrder> GetTransList(int queryUId, int queryPayType, int queryIsMyRecord, int sysAccountId, int sysAccountUid, string queryIsbn, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<AsideBookOrder>();
            if (queryUId > 0)
            {
                where = where.And(m => m.SysAccount.UniversityId == queryUId);
            }
            if (queryPayType > 0)
            {
                if (queryPayType == (int)EnumHelp.ResearchPayType.无偿)
                {
                    where = where.And(m => m.TrafficFee <= 0);
                }
                else if (queryPayType == (int)EnumHelp.ResearchPayType.有偿)
                {
                    where = where.And(m => m.TrafficFee > 0);
                }
            }
            if (queryIsMyRecord == (int)EnumHelp.MyTransferStatus.已负责接送)//0：全部   1：已负责接送 2：未负责接送
            {
                where = where.And(m => m.TrafficAccountId == sysAccountId);
            }
            else if (queryIsMyRecord == (int)EnumHelp.MyTransferStatus.待顺路接送)
            {
                where = where.And(m => m.TrafficAccountId == 0);
            }
            if (!string.IsNullOrWhiteSpace(queryIsbn))
            {
                where = where.And(m => m.AsideBookInfo.Isbn.Contains(queryIsbn));
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.TrafficType == (int)EnumHelp.TrafficType.顺路送书 && c.SysAccount.UniversityId != sysAccountUid);
            totalCount = this._repoasideBookOrder.Table.Where(where).Count();
            return this._repoasideBookOrder.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据用户id获取用户送书成功的列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<AsideBookOrder> getSuccessfulTrasList(int accountId)
        {
            var where = PredicateBuilder.True<AsideBookOrder>();
            if (accountId > 0)
            {
                where = where.And(m => m.TrafficAccountId == accountId);
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.TrafficType == (int)EnumHelp.TrafficType.顺路送书 && c.OrderStatus == (int)EnumHelp.BookOrderStatus.已完结);
            return this._repoasideBookOrder.Table.Where(where).OrderByDescending(p => p.CreateTime).ToList();
        }

        /// <summary>
        /// 获取用户在此平台上成功漂的图书列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<AsideBookOrder> GetMyList(int accountId)
        {
            var where = PredicateBuilder.True<AsideBookOrder>();
            if (accountId > 0)
            {
                where = where.And(m => m.SysAccountId == accountId);
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 &&  c.OrderStatus == (int)EnumHelp.BookOrderStatus.已完结);
            return this._repoasideBookOrder.Table.Where(where).OrderByDescending(p => p.CreateTime).ToList();
        }
    }
}
