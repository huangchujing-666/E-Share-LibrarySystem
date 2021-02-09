using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.IService
{
    public interface IBookOrderService
    {

        BookOrder GetById(int id);

        BookOrder Insert(BookOrder model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BookOrder model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BookOrder model);


        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BookOrder> GetManagerList(int UniversityId, string isbn,int suniversityId,string sysAccount, int borrowStatus, int universityId, string bookName, int pageNum, int pageSize, out int totalCount);
        BookOrder GetByBookIdAccountId(int bookInfoId, int sysAccountId);
        List<BookOrder> GetManagerListByUser(int accountId, string queryName, string queryIsbn, int queryUId, string queryCategory,int queryBorrowStatus, int pageIndex, int pageSize, out int totalCount);
        int GetMyBorrowCount(int accountId);
    }
}
