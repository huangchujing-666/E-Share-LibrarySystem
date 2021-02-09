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
    public class BookOrderService : IBookOrderService
    {
        /// <summary>
        /// The ActivityDiscount biz
        /// </summary>
        private IBookOrderBusiness _BookOrderBiz;

        public BookOrderService(IBookOrderBusiness BookOrderBiz)
        {
            _BookOrderBiz = BookOrderBiz;
        }
        public void Delete(BookOrder model)
        {
            this._BookOrderBiz.Delete(model);
        }

        public BookOrder GetById(int id)
        {
            return this._BookOrderBiz.GetById(id);
        }

        public List<BookOrder> GetManagerList(int UniversityId,string isbn, int suniversityId, string sysAccount, int borrowStatus, int universityId, string bookName, int pageNum, int pageSize, out int totalCount)
        {
            return this._BookOrderBiz.GetManagerList(UniversityId,isbn, suniversityId,sysAccount, borrowStatus, universityId, bookName, pageNum, pageSize, out totalCount);
        }

        public BookOrder Insert(BookOrder model)
        {
            return this._BookOrderBiz.Insert(model);
        }

        public void Update(BookOrder model)
        {
            this._BookOrderBiz.Update(model);
        }

        public BookOrder GetByBookIdAccountId(int bookInfoId, int sysAccountId)
        {
            return this._BookOrderBiz.GetByBookIdAccountId(bookInfoId, sysAccountId);
        }

        public List<BookOrder> GetManagerListByUser(int accountId, string queryName, string queryIsbn, int queryUId, string queryCategory, int queryBorrowStatus,int pageIndex, int pageSize, out int totalCount)
        {
            return this._BookOrderBiz.GetManagerListByUser(accountId, queryName, queryIsbn, queryUId, queryCategory, queryBorrowStatus, pageIndex, pageSize,out totalCount);
        }

        public int GetMyBorrowCount(int accountId)
        {
            return this._BookOrderBiz.GetMyBorrowCount(accountId);
        }
    }
}
