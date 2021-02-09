using LibrarySystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Model;
using LibrarySystem.Business;

namespace OrderingSystem.Service
{
    public class BookInfoService : IBookInfoService
    {
        /// <summary>
        /// The ActivityDiscount biz
        /// </summary>
        private IBookInfoBusiness _BookInfoBiz;

        public BookInfoService(IBookInfoBusiness BookInfoBiz)
        {
            _BookInfoBiz = BookInfoBiz;
        }
        public void Delete(BookInfo model)
        {
            _BookInfoBiz.Delete(model);
        }

        public BookInfo GetById(int id)
        {
            return this._BookInfoBiz.GetById(id);
        }

        public List<BookInfo> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount)
        {
            return this._BookInfoBiz.GetManagerList(name, type, pageNum, pageSize, out totalCount);
        }

        public BookInfo Insert(BookInfo model)
        {
            return this._BookInfoBiz.Insert(model);
        }

        public void Update(BookInfo model)
        {
            this._BookInfoBiz.Update(model);
        }

        public List<BookInfo> GetManagerList(string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
           return  this._BookInfoBiz.GetManagerList(queryName, queryIsbn, queryUId, queryCategory, pageIndex, pageSize, out totalCount);
        }

        public BookInfo GetByUniversityIsbn(int uinversityId, string isbn)
        {
            return this._BookInfoBiz.GetByUniversityIsbn(uinversityId, isbn);
        }

        public List<BookInfo> GetManagerListByUser(int AccountId,int isBorrow, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            return this._BookInfoBiz.GetManagerListByUser(AccountId,isBorrow, queryName, queryIsbn, queryUId, queryCategory, pageIndex, pageSize,out totalCount);
        }
    }
}
