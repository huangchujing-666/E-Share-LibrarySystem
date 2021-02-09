using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Business
{
    public interface IAsideBookInfoBusiness
    {
        /// <summary>
        /// 根据主键获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AsideBookInfo GetById(int id);

        /// <summary>
        /// 插入新记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AsideBookInfo Insert(AsideBookInfo model);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(AsideBookInfo model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(AsideBookInfo model);


        /// <summary>
        /// 管理后台用户列表
        /// </summary>
        /// <param name="name">书名</param>
        /// <param name="type"></param>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        List<AsideBookInfo> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 多条件查询数据列表
        /// </summary>
        /// <param name="queryName">书名</param>
        /// <param name="queryIsbn">ISBN</param>
        /// <param name="queryUId">大学id</param>
        /// <param name="queryCategory">图书类别</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        List<AsideBookInfo> GetManagerList(string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 查询某大学中的指定书号
        /// </summary>
        /// <param name="uinversityId">大学编号</param>
        /// <param name="isbn">书号</param>
        /// <returns></returns>
        AsideBookInfo GetByUniversityIsbn(int uinversityId, string isbn);

        /// <summary>
        /// 根据isbn获取结果集
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
       List<AsideBookInfo> GetByIsbn(string isbn);

        // List<AsideBookInfo> GetManagerListByUser(int AccountId, int isBorrow, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount);
    }
}
