using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Model;
using LibrarySystem.Core.Data;
using LibrarySystem.Domain;

namespace LibrarySystem.Business
{
    public class ResearchAsideBookBusiness : IResearchAsideBookBusiness
    {

        private IRepository<ResearchAsideBook> _repoResearchAsideBook;

        public ResearchAsideBookBusiness(IRepository<ResearchAsideBook> repoResearchAsideBook)
        {
            this._repoResearchAsideBook = repoResearchAsideBook;
        }
        public void Delete(ResearchAsideBook model)
        {
            this._repoResearchAsideBook.Delete(model);
        }

        public ResearchAsideBook GetById(int id)
        {
            return this._repoResearchAsideBook.GetById(id);
        }

        public ResearchAsideBook GetByIsbn(string isbn)
        {
            if (!String.IsNullOrWhiteSpace(isbn))
            {
                var where = PredicateBuilder.True<ResearchAsideBook>();
                return this._repoResearchAsideBook.Table.Where(c => c.Isbn.Contains(isbn)).FirstOrDefault();
            }
            return null;
        }

        public ResearchAsideBook GetByIsbnAccountId(string isbn, int accountId)
        {
            if (!String.IsNullOrWhiteSpace(isbn) && accountId > 0)
            {
                var where = PredicateBuilder.True<ResearchAsideBook>();
                return this._repoResearchAsideBook.Table.Where(c => c.Isbn.Equals(isbn) && c.SysAccountId == accountId && (c.ResearchStatus == (int)EnumHelp.ResearchStatus.求书中 || c.ResearchStatus == (int)EnumHelp.ResearchStatus.找到书源)).FirstOrDefault();
            }
            return null;
        }

        public ResearchAsideBook GetByUniversityIsbn(int uinversityId, string isbn)
        {
            return this._repoResearchAsideBook.Table.Where(c => c.UniversityId == uinversityId && c.Isbn.Equals(isbn)).FirstOrDefault();
        }

        /// <summary>
        /// 管理员后台列表
        /// </summary>
        /// <param name="querySysAccount"></param>
        /// <param name="queryPayType"></param>
        /// <param name="queryResearchStatus"></param>
        /// <param name="queryName"></param>
        /// <param name="queryIsbn"></param>
        /// <param name="queryUId"></param>
        /// <param name="queryCategory"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ResearchAsideBook> GetManagerList(string querySysAccount, int queryPayType, int queryResearchStatus, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ResearchAsideBook>();

            if (!string.IsNullOrWhiteSpace(querySysAccount))
            {
                where = where.And(m => m.SysAccount.Account.Contains(querySysAccount));
            }
            if (queryPayType > 0)
            {
                where = where.And(m => m.PayType == queryPayType);
            }
            if (queryResearchStatus >= 0)
            {
                where = where.And(m => m.ResearchStatus == queryResearchStatus);
            }
            if (queryUId > 0)
            {
                where = where.And(m => m.SysAccount.UniversityId == queryUId);
            }
            if (!string.IsNullOrWhiteSpace(queryName))
            {
                where = where.And(m => m.Title.Contains(queryName));
            }
            if (!string.IsNullOrWhiteSpace(queryIsbn))
            {
                where = where.And(m => m.Isbn.Contains(queryIsbn));
            }
            if (!string.IsNullOrWhiteSpace(queryCategory))
            {
                where = where.And(m => m.Category.Contains(queryCategory));
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效);
            totalCount = this._repoResearchAsideBook.Table.Where(where).Count();
            return this._repoResearchAsideBook.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 用户后台列表
        /// </summary>
        /// <param name="querySysAccountId"></param>
        /// <param name="queryName"></param>
        /// <param name="queryIsbn"></param>
        /// <param name="queryUId"></param>
        /// <param name="queryCategory"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ResearchAsideBook> GetManagerList(int querySysAccountId, string queryName, string queryIsbn, int queryUId, string queryCategory, int queryPayType, int queryResearchStatus, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ResearchAsideBook>();
            if (queryUId > 0)
            {
                where = where.And(m => m.SysAccount.UniversityId == queryUId);
            }
            if (!string.IsNullOrWhiteSpace(queryName))
            {
                where = where.And(m => m.Title.Contains(queryName));
            }
            if (!string.IsNullOrWhiteSpace(queryIsbn))
            {
                where = where.And(m => m.Isbn.Contains(queryIsbn));
            }
            if (!string.IsNullOrWhiteSpace(queryCategory))
            {
                where = where.And(m => m.Category.Contains(queryCategory));
            }
            if (queryPayType > 0)
            {
                where = where.And(m => m.PayType == queryPayType);
            }
            if (queryResearchStatus >= 0)
            {
                where = where.And(m => m.ResearchStatus == queryResearchStatus);
            }
            if (querySysAccountId > 0)
            {
                where = where.And(m => m.SysAccountId == querySysAccountId);
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            totalCount = this._repoResearchAsideBook.Table.Where(where).Count();
            return this._repoResearchAsideBook.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public ResearchAsideBook Insert(ResearchAsideBook model)
        {
            return this._repoResearchAsideBook.Insert(model);
        }

        public void Update(ResearchAsideBook model)
        {
            this._repoResearchAsideBook.Update(model);
        }

        /// <summary>
        /// 获取用户求书成功订单数量
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<ResearchAsideBook> GetResearchSucessFulList(int accountId)
        {
            var where = PredicateBuilder.True<ResearchAsideBook>();
            if (accountId > 0)
            {
                where = where.And(m => m.SysAccount.SysAccountId == accountId);
            }
            where = where.And(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效&&c.ResearchStatus==(int)EnumHelp.ResearchStatus.求书成功);
            return this._repoResearchAsideBook.Table.Where(where).OrderByDescending(p => p.CreateTime).ToList();
        }
    }
}
