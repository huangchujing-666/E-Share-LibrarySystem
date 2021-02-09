using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Core.Data;
using LibrarySystem.Domain.Model;
using LibrarySystem.Domain;

namespace LibrarySystem.Business
{
    public class BookOrderBusiness : IBookOrderBusiness
    {
        private IRepository<BookOrder> _repoBookOrder;

        public BookOrderBusiness(
         IRepository<BookOrder> repoBookOrder
          )
        {
            _repoBookOrder = repoBookOrder;
        }

        public void Delete(BookOrder model)
        {
            this._repoBookOrder.Delete(model);
        }

        public BookOrder GetById(int id)
        {
            return this._repoBookOrder.GetById(id);
        }

        public List<BookOrder> GetManagerList(int UniversityId,string isbn, int suniversityId, string sysAccount, int borrowStatus, int universityId, string bookName, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BookOrder>();
            if (!string.IsNullOrWhiteSpace(isbn))
            {
                where = where.And(m => m.BookInfo.Isbn.Contains(isbn));
            }
            if (suniversityId>0)
            {
                where = where.And(m => m.SysAccount.University.UniversityId== suniversityId);
            }
            if (!string.IsNullOrWhiteSpace(sysAccount))
            {
                where = where.And(m => m.SysAccount.Account.Equals(sysAccount));
            }
            if (borrowStatus > 0)
            {
                if (borrowStatus == (int)EnumHelp.BorrowStatus.审核中 || borrowStatus == (int)EnumHelp.BorrowStatus.审核驳回 || borrowStatus == (int)EnumHelp.BorrowStatus.已还书 || borrowStatus == (int)EnumHelp.BorrowStatus.续借||borrowStatus== (int)EnumHelp.BorrowStatus.取消)
                {
                    where = where.And(m => m.BorrowStatus == borrowStatus);
                }
                else if (borrowStatus == (int)EnumHelp.BorrowStatus.审核通过)
                {
                    where = where.And(m => m.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || m.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中);
                }
                else if (borrowStatus == (int)EnumHelp.BorrowStatus.库存不足)
                {
                    where = where.And(m => m.BookInfo.Available <= 0);
                }
                else if (borrowStatus == (int)EnumHelp.BorrowStatus.借书中)
                {
                    where = where.And(m => m.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || m.BorrowStatus == (int)EnumHelp.BorrowStatus.续借);
                }
                else if (borrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)
                {
                    where = where.And(m => m.BackTime < System.DateTime.Now && (m.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || m.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || m.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费 || m.BorrowStatus == (int)EnumHelp.BorrowStatus.续借));
                } 
            }
            if (universityId > 0)
            {
                where = where.And(m => m.BookInfo.UniversityId == universityId);
            }
            if (!string.IsNullOrWhiteSpace(bookName))
            {
                where = where.And(m => m.BookInfo.Title.Equals(bookName));
            }
            where = where.And(m => m.BookInfo.UniversityId== UniversityId || m.SysAccount.UniversityId== UniversityId);
            totalCount = this._repoBookOrder.Table.Where(where).Count();
            return this._repoBookOrder.Table.Where(where).OrderByDescending(c => c.CreateTime).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public BookOrder Insert(BookOrder model)
        {
            return this._repoBookOrder.Insert(model);
        }

        public void Update(BookOrder model)
        {
            this._repoBookOrder.Update(model);
        }

        public BookOrder GetByBookIdAccountId(int bookInfoId, int sysAccountId)
        {
            return this._repoBookOrder.Table.Where(c => c.BookInfoId == bookInfoId && c.SysAccountId == sysAccountId).FirstOrDefault();
        }

        public List<BookOrder> GetManagerListByUser(int accountId, string queryName, string queryIsbn, int queryUId, string queryCategory, int queryBorrowStatus, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            if (accountId <= 0)
                return new List<BookOrder>();
            var where = PredicateBuilder.True<BookOrder>();
            where = where.And(c => c.SysAccountId == accountId);
            if (!string.IsNullOrWhiteSpace(queryName))
            {
                where = where.And(m => m.BookInfo.Title.Contains(queryName));
            }
            if (!string.IsNullOrWhiteSpace(queryIsbn))
            {
                where = where.And(m => m.BookInfo.Isbn.Contains(queryIsbn));
            }
            if (queryUId > 0)
            {
                where = where.And(m => m.BookInfo.UniversityId == queryUId);
            }
            if (!string.IsNullOrWhiteSpace(queryCategory))
            {
                where = where.And(m => m.BookInfo.Category.Contains(queryCategory));
            }
            if (queryBorrowStatus > 0)
            {
                if (queryBorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || queryBorrowStatus == (int)EnumHelp.BorrowStatus.审核驳回 || queryBorrowStatus == (int)EnumHelp.BorrowStatus.已还书 || queryBorrowStatus == (int)EnumHelp.BorrowStatus.续借 || queryBorrowStatus == (int)EnumHelp.BorrowStatus.取消)
                {
                    where = where.And(m => m.BorrowStatus == queryBorrowStatus);
                }
                else if (queryBorrowStatus == (int)EnumHelp.BorrowStatus.审核通过)
                {
                    where = where.And(m => m.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || m.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中);
                }
                else if (queryBorrowStatus == (int)EnumHelp.BorrowStatus.库存不足)
                {
                    where = where.And(m => m.BookInfo.Available <= 0);
                }
                else if (queryBorrowStatus == (int)EnumHelp.BorrowStatus.借书中)
                {
                    where = where.And(m => m.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || m.BorrowStatus == (int)EnumHelp.BorrowStatus.续借);
                }
                else if (queryBorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)
                {
                    where = where.And(m => m.BackTime > System.DateTime.Now && m.BorrowStatus != (int)EnumHelp.BorrowStatus.已还书);
                }
            }
            totalCount = this._repoBookOrder.Table.Where(where).Count();
            return this._repoBookOrder.Table.Where(where).OrderByDescending(c => c.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetMyBorrowCount(int accountId)
        {
            var list= this._repoBookOrder.Table.Where(c=>c.SysAccountId== accountId&&(c.BorrowStatus==(int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过|| c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)).ToList();
            if (list != null)
                return list.Sum(c => c.Count);
            return 0;
        }
    }
}
