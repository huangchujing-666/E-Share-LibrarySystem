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
    public class AsideBookInfoBusiness : IAsideBookInfoBusiness
    {
        private IRepository<AsideBookInfo> _repoAsideBookInfo;

        public AsideBookInfoBusiness(
         IRepository<AsideBookInfo> repoAsideBookInfo
          )
        {
            _repoAsideBookInfo = repoAsideBookInfo;
        }
        public void Delete(AsideBookInfo model)
        {
            this._repoAsideBookInfo.Delete(model);
        }

        public AsideBookInfo GetById(int id)
        {
            return this._repoAsideBookInfo.GetById(id);
        }

        public AsideBookInfo Insert(AsideBookInfo model)
        {
            return this._repoAsideBookInfo.Insert(model);
        }

        public void Update(AsideBookInfo model)
        {
            this._repoAsideBookInfo.Update(model);
        }

        public AsideBookInfo GetByUniversityIsbn(int uinversityId, string isbn)
        {
            return this._repoAsideBookInfo.Table.Where(c => c.UniversityId == uinversityId && c.Isbn.Equals(isbn)&&c.IsDelete==(int)EnumHelp.IsDeleteEnum.有效).FirstOrDefault();
        }

        public List<AsideBookInfo> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<AsideBookInfo>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                where = where.And(m => m.Title.Contains(name));
            }

            totalCount = this._repoAsideBookInfo.Table.Where(where).Count();
            return this._repoAsideBookInfo.Table.Where(where).OrderByDescending(p => p.CreateTime).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<AsideBookInfo> GetManagerList(string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<AsideBookInfo>();
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
            totalCount = this._repoAsideBookInfo.Table.Where(where).Count();
            return this._repoAsideBookInfo.Table.Where(where).OrderByDescending(c=>c.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<AsideBookInfo> GetByIsbn(string isbn)
        {
            if (!String.IsNullOrWhiteSpace(isbn))
            {
                var where = PredicateBuilder.True<AsideBookInfo>();
                return this._repoAsideBookInfo.Table.Where(c => c.Isbn.Equals(isbn)).ToList();
            }
            return null;
        }

        //public List<AsideBookInfo> GetManagerListByUser(int AccountId, int isBorrow, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        //{
        //    var where = PredicateBuilder.True<AsideBookInfo>();
        //    if (isBorrow > 0)
        //    {
        //        if (isBorrow == (int)EnumHelp.IsBorrow.可预借)
        //        {
        //            var bolist = this._repoAsideBookInfo.Table.Where(c => c.SysAccountId == AccountId && (c.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)).ToList();
        //            if (bolist != null && bolist.Count > 0)//预借中的书
        //            {
        //                int[] bookInfoId = bolist.Select(c => c.BookInfoId).ToArray();
        //                where = where.And(m => !bookInfoId.Contains(m.BookInfoId));
        //            }
        //            //where = where.And(m => !m.BookOrderList.Where(c=>c.BorrowStatus==(int)EnumHelp.BorrowStatus.借书中|| c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中|| c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过|| c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借|| c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费).Select(n=>n.SysAccountId).ToArray().Contains(AccountId));
        //            //where = where.And(m => m.BookOrderList.Where(c => c.SysAccountId == AccountId&&(c.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)));
        //        }
        //        else if (isBorrow == (int)EnumHelp.IsBorrow.其他)
        //        {
        //            var bolist = this._repoBookOrder.Table.Where(c => c.SysAccountId == AccountId && (c.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)).ToList();
        //            if (bolist != null && bolist.Count > 0)//预借中的书
        //            {
        //                int[] bookInfoId = bolist.Select(c => c.BookInfoId).ToArray();
        //                where = where.And(m => bookInfoId.Contains(m.BookInfoId) || m.Available <= 0);
        //            }
        //            //where = where.And(m =>m.Available<=0||m.BookOrderList.Where(c => c.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费).Select(n => n.SysAccountId).ToArray().Contains(AccountId));
        //        }
        //    }
        //    if (queryUId > 0)
        //    {
        //        where = where.And(m => m.UniversityId == queryUId);
        //    }
        //    if (!string.IsNullOrWhiteSpace(queryName))
        //    {
        //        where = where.And(m => m.Title.Contains(queryName));
        //    }
        //    if (!string.IsNullOrWhiteSpace(queryIsbn))
        //    {
        //        where = where.And(m => m.Isbn.Contains(queryIsbn));
        //    }
        //    if (!string.IsNullOrWhiteSpace(queryCategory))
        //    {
        //        where = where.And(m => m.Category.Contains(queryCategory));
        //    }
        //    totalCount = this._repoBookInfo.Table.Where(where).Count();
        //    return this._repoBookInfo.Table.Where(where).OrderBy(p => p.UniversityId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //}

    }
}
