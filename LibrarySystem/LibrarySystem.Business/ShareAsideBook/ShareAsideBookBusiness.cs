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
    public class ShareAsideBookBusiness : IShareAsideBookBusiness
    {
        private IRepository<ShareAsideBook> _reposhareAsideBook;

        public ShareAsideBookBusiness(IRepository<ShareAsideBook> reposhareAsideBook)
        {
            this._reposhareAsideBook = reposhareAsideBook;
        }
        public void Delete(ShareAsideBook model)
        {
            this._reposhareAsideBook.Delete(model);
        }

        public ShareAsideBook GetById(int id)
        {
            return this._reposhareAsideBook.GetById(id);
        }

        public List<ShareAsideBook> GetManagerList(int accountId, string queryName, string queryIsbn, int queryUId, string queryCategory, string querySysAccount, int queryShareStatus, int queryPayType, int queryTrafficType, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ShareAsideBook>();
            if (!string.IsNullOrWhiteSpace(queryName))
            {
                where = where.And(m => m.Title.Contains(queryName));
            }
            if (!string.IsNullOrWhiteSpace(queryIsbn))
            {
                where = where.And(m => m.Isbn.Contains(queryIsbn));
            }
            if (queryUId > 0)
            {
                where = where.And(m => m.SysAccount.UniversityId == queryUId);
            }
            if (!string.IsNullOrWhiteSpace(queryCategory))
            {
                where = where.And(m => m.Category.Contains(queryCategory));
            }
            if (!string.IsNullOrWhiteSpace(querySysAccount))
            {
                where = where.And(m => m.SysAccount.Account.Contains(querySysAccount));
            }
            if (queryShareStatus != 0)
            {
                where = where.And(m => m.ShareStatus == queryShareStatus);
            }
            if (queryPayType != 0)
            {
                where = where.And(m => m.PayType == queryPayType);
            }
            if (queryTrafficType != 0)
            {
                where = where.And(m => m.TrafficType == queryTrafficType);
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.SysAccountId == accountId);
            totalCount = this._reposhareAsideBook.Table.Where(where).Count();
            return this._reposhareAsideBook.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 多条件查询  管理员界面
        /// </summary>
        /// <param name="QueryShareType"></param>
        /// <param name="QueryName"></param>
        /// <param name="QueryIsbn"></param>
        /// <param name="QueryUId"></param>
        /// <param name="QueryCategory"></param>
        /// <param name="QuerySysAccount"></param>
        /// <param name="QueryShareStatus"></param>
        /// <param name="QueryPayType"></param>
        /// <param name="QueryTrafficType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ShareAsideBook> GetAdminManagerList(int QueryShareType, string QueryName, string QueryIsbn, int QueryUId, string QueryCategory, string QuerySysAccount, int QueryShareStatus, int QueryPayType, int QueryTrafficType, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ShareAsideBook>();
            if (QueryShareType > 0)
            {
                if (QueryShareType == (int)EnumHelp.BookShareType.求书共享)
                {
                    where = where.And(m => m.ResearchAsideBookId > 0);
                }
                else if (QueryShareType == (int)EnumHelp.BookShareType.自主共享)
                {
                    where = where.And(m => m.ResearchAsideBookId == 0);
                }
            }
            if (!string.IsNullOrWhiteSpace(QueryName))
            {
                where = where.And(m => m.Title.Contains(QueryName));
            }
            if (!string.IsNullOrWhiteSpace(QuerySysAccount))
            {
                where = where.And(m => m.SysAccount.Account.Contains(QuerySysAccount));
            }
            if (!string.IsNullOrWhiteSpace(QueryIsbn))
            {
                where = where.And(m => m.Isbn.Contains(QueryIsbn));
            }
            if (!string.IsNullOrWhiteSpace(QueryCategory))
            {
                where = where.And(m => m.Category.Contains(QueryCategory));
            }
            if (QueryUId > 0)
            {
                where = where.And(m => m.SysAccount.UniversityId == QueryUId);
            }
            if (QueryShareStatus != 0)
            {
                where = where.And(m => m.ShareStatus == QueryShareStatus);
            }
            if (QueryPayType != 0)
            {
                where = where.And(m => m.PayType == QueryPayType);
            }
            if (QueryTrafficType != 0)
            {
                where = where.And(m => m.TrafficType == QueryTrafficType);
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效);
            totalCount = this._reposhareAsideBook.Table.Where(where).Count();
            return this._reposhareAsideBook.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        public ShareAsideBook Insert(ShareAsideBook model)
        {
            return this._reposhareAsideBook.Insert(model);
        }

        public void Update(ShareAsideBook model)
        {
            this._reposhareAsideBook.Update(model);
        }

        /// <summary>
        /// 根据求书id获取共享图书对象
        /// </summary>
        /// <param name="researchAsideBookId"></param>
        /// <returns></returns>
        public ShareAsideBook GetResearchAsideBookId(int researchAsideBookId)
        {
            var where = PredicateBuilder.True<ShareAsideBook>();
            if (researchAsideBookId > 0)
            {
                where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.ResearchAsideBookId == researchAsideBookId && c.ShareStatus == (int)EnumHelp.BookShareStatus.待入库);
                return this._reposhareAsideBook.Table.Where(where).FirstOrDefault();
            }
            return null;

        }

        /// <summary>
        /// 管理员邮寄界面 获取共享图书是否已经共享至平台
        /// </summary>
        /// <param name="researchAsideBookId"></param>
        /// <param name="shareStatus"></param>
        /// <returns></returns>
        public ShareAsideBook GetResearchAsideBookId(int researchAsideBookId, int shareStatus)
        {
            var where = PredicateBuilder.True<ShareAsideBook>();
            if (researchAsideBookId > 0)
            {
                where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.ResearchAsideBookId == researchAsideBookId && c.ShareStatus == shareStatus);
                return this._reposhareAsideBook.Table.Where(where).FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        /// 根据id获取用户共享的图书数量
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<ShareAsideBook> GetShareList(int accountId)
        {
            var where = PredicateBuilder.True<ShareAsideBook>();
            if (accountId > 0)
            {
                where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.SysAccountId == accountId && c.ShareStatus ==(int)EnumHelp.BookShareStatus.已共享);
                return this._reposhareAsideBook.Table.Where(where).OrderByDescending(c=>c.CreateTime).ToList();
            }
            return null;
        }
    }
}
