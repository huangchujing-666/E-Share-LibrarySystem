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
    public class BookInfoBusiness : IBookInfoBusiness
    {
        private IRepository<BookInfo> _repoBookInfo;
        private IRepository<BookOrder> _repoBookOrder;

        public BookInfoBusiness(
         IRepository<BookInfo> repoBookInfo,
         IRepository<BookOrder> repoBookOrder
          )
        {
            _repoBookInfo = repoBookInfo;
            _repoBookOrder = repoBookOrder;
        }
        public void Delete(BookInfo model)
        {
            this._repoBookInfo.Delete(model);
        }

        public BookInfo GetById(int id)
        {
            return this._repoBookInfo.GetById(id);
        }

        public List<BookInfo> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BookInfo>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                where = where.And(m => m.Title.Contains(name));
            }

            totalCount = this._repoBookInfo.Table.Where(where).Count();
            return this._repoBookInfo.Table.Where(where).OrderBy(p => p.UniversityId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<BookInfo> GetManagerList(string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BookInfo>();
            if (queryUId > 0)
            {
                where = where.And(m => m.UniversityId == queryUId);
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
            totalCount = this._repoBookInfo.Table.Where(where).Count();
            return this._repoBookInfo.Table.Where(where).OrderBy(p => p.UniversityId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public BookInfo Insert(BookInfo model)
        {
            return this._repoBookInfo.Insert(model);
        }

        public void Update(BookInfo model)
        {
            this._repoBookInfo.Update(model);
        }

        public BookInfo GetByUniversityIsbn(int uinversityId, string isbn)
        {
            return this._repoBookInfo.Table.Where(c => c.UniversityId == uinversityId && c.Isbn.Equals(isbn)).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="isBorrow"></param>
        /// <param name="queryName"></param>
        /// <param name="queryIsbn"></param>
        /// <param name="queryUId"></param>
        /// <param name="queryCategory"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<BookInfo> GetManagerListByUser(int AccountId,int isBorrow, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BookInfo>();
            if (isBorrow>0)
            {

                if (isBorrow==(int)EnumHelp.IsBorrow.可预借)
                {
                    //筛选用户已经预借或在借的书
                   var bolist=  this._repoBookOrder.Table.Where(c => c.SysAccountId == AccountId && (c.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)).ToList();
                    if (bolist!=null && bolist.Count>0)//预借中的书
                    {
                        int[] bookInfoId=bolist.Select(c => c.BookInfoId).ToArray();
                        //除去在借或者预借中的书即是可借的书
                        where = where.And(m=>!bookInfoId.Contains(m.BookInfoId));
                    }
                    //where = where.And(m => !m.BookOrderList.Where(c=>c.BorrowStatus==(int)EnumHelp.BorrowStatus.借书中|| c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中|| c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过|| c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借|| c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费).Select(n=>n.SysAccountId).ToArray().Contains(AccountId));
                    //where = where.And(m => m.BookOrderList.Where(c => c.SysAccountId == AccountId&&(c.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)));
                }
                else if(isBorrow == (int)EnumHelp.IsBorrow.其他)
                {
                    //除去可借的书
                    var  bolist = this._repoBookOrder.Table.Where(c => c.SysAccountId == AccountId && (c.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)).ToList();
                    if (bolist != null && bolist.Count > 0)//预借中的书
                    {
                        int[] bookInfoId = bolist.Select(c => c.BookInfoId).ToArray();
                        where = where.And(m => bookInfoId.Contains(m.BookInfoId)|| m.Available <= 0);
                    }
                    //where = where.And(m =>m.Available<=0||m.BookOrderList.Where(c => c.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费).Select(n => n.SysAccountId).ToArray().Contains(AccountId));
                }
            }
            if (queryUId > 0)
            {
                where = where.And(m => m.UniversityId == queryUId);
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
            totalCount = this._repoBookInfo.Table.Where(where).Count();
            return this._repoBookInfo.Table.Where(where).OrderBy(p => p.UniversityId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
