using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.IService
{
    public interface IBookInfoService
    {
        BookInfo GetById(int id);

        BookInfo Insert(BookInfo model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BookInfo model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BookInfo model);


        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BookInfo> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount);


        List<BookInfo> GetManagerList(string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount);
        BookInfo GetByUniversityIsbn(int uinversityId, string isbn);
        List<BookInfo> GetManagerListByUser(int AccountId,int isBorrow, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount);
    }
}
