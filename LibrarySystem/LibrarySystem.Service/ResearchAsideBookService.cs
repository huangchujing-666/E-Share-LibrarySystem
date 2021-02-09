using LibrarySystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Model;
using LibrarySystem.Business;

namespace LibrarySystem.Service
{
    public class ResearchAsideBookService : IResearchAsideBookService
    {
        private IResearchAsideBookBusiness _ResearchAsideBookBiz;
        public ResearchAsideBookService(IResearchAsideBookBusiness ResearchAsideBookBiz)
        {
            this._ResearchAsideBookBiz = ResearchAsideBookBiz;
        }
        public void Delete(ResearchAsideBook model)
        {
            this._ResearchAsideBookBiz.Delete(model);
        }

        public ResearchAsideBook GetById(int id)
        {
            return this._ResearchAsideBookBiz.GetById(id);
        }

        public ResearchAsideBook GetByIsbn(string isbn)
        {
            return _ResearchAsideBookBiz.GetByIsbn(isbn);
        }

        public ResearchAsideBook GetByIsbnAccountId(string isbn, int accountId)
        {
            return _ResearchAsideBookBiz.GetByIsbnAccountId(isbn, accountId);
        }

        public ResearchAsideBook GetByUniversityIsbn(int uinversityId, string isbn)
        {
            return _ResearchAsideBookBiz.GetByUniversityIsbn(uinversityId, isbn);
        }

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<ResearchAsideBook> GetManagerList( string querySysAccount, int queryPayType, int queryResearchStatus, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageNum, int pageSize, out int totalCount)
        {
            return _ResearchAsideBookBiz.GetManagerList(querySysAccount, queryPayType, queryResearchStatus, queryName, queryIsbn, queryUId, queryCategory, pageNum, pageSize, out totalCount);
        }

        public List<ResearchAsideBook> GetManagerList(int querySysAccountId, string queryName, string queryIsbn, int queryUId, string queryCategory, int queryPayType, int queryResearchStatus, int pageIndex, int pageSize, out int totalCount)
        {
            return _ResearchAsideBookBiz.GetManagerList(querySysAccountId, queryName, queryIsbn, queryUId, queryCategory, queryPayType, queryResearchStatus,pageIndex, pageSize, out totalCount);
        }

        public ResearchAsideBook Insert(ResearchAsideBook model)
        {
            return _ResearchAsideBookBiz.Insert(model);
        }

        public void Update(ResearchAsideBook model)
        {
            _ResearchAsideBookBiz.Update(model);
        }

        /// <summary>
        /// 获取用户求书成功订单数量
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<ResearchAsideBook> GetResearchSucessFulList(int accountId)
        {
            return _ResearchAsideBookBiz.GetResearchSucessFulList(accountId);
        }
    }
}
