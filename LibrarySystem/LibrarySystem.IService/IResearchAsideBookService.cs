using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.IService
{
    public interface IResearchAsideBookService
    {
        ResearchAsideBook GetById(int id);

        ResearchAsideBook Insert(ResearchAsideBook model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(ResearchAsideBook model);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(ResearchAsideBook model);

        ResearchAsideBook GetByIsbn(string isbn);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<ResearchAsideBook> GetManagerList(string querySysAccount,int queryPayType,int queryResearchStatus,string queryName, string queryIsbn,int queryUId,string queryCategory, int pageNum, int pageSize, out int totalCount);
      
        /// <summary>
        /// 用户后台 求书列表
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
        List<ResearchAsideBook> GetManagerList(int querySysAccountId,string queryName, string queryIsbn, int queryUId, string queryCategory,int queryPayType,int queryResearchStatus, int pageIndex, int pageSize, out int totalCount);

        ResearchAsideBook GetByUniversityIsbn(int uinversityId, string isbn);

        /// <summary>
        /// 获取当前账户id是否发布过该isbn求书信息
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        ResearchAsideBook GetByIsbnAccountId(string isbn, int accountId);

        /// <summary>
        /// 获取用户求书成功订单数量
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        List<ResearchAsideBook> GetResearchSucessFulList(int accountId);
    }
}
