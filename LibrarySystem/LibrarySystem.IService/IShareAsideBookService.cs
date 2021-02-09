using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.IService
{
    public interface IShareAsideBookService
    {
        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ShareAsideBook GetById(int id);

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ShareAsideBook Insert(ShareAsideBook model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(ShareAsideBook model);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(ShareAsideBook model);

        /// <summary>
        /// 多条件查询  用户界面
        /// </summary>
        /// <param name="queryName">书名</param>
        /// <param name="queryIsbn">书号</param>
        /// <param name="queryUId">出书人大学id</param>
        /// <param name="queryCategory">类别</param>
        /// <param name="querySysAccount">出书人账号</param>
        /// <param name="queryShareStatus">出书状态</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        List<ShareAsideBook> GetManagerList(int accountId,string queryName, string queryIsbn, int queryUId, string queryCategory, string querySysAccount, int queryShareStatus,int queryPayType,int queryTrafficType, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 多条件查询  管理员界面
        /// </summary>
        /// <param name="QueryShareType"></param>
        /// <param name="QueryName"></param>
        /// <param name="QueryIsbn"></param>
        /// <param name="QueryUId"></param>
        /// <param name="QueryCategory"></param>
        /// <param name="QuerySysAccount"></param>
        /// <param name="QueryShareStatus"></param>
        /// <param name="QueryPayType"></param>
        /// <param name="QueryTrafficType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<ShareAsideBook> GetAdminManagerList(int QueryShareType,string QueryName,string QueryIsbn,int QueryUId, string QueryCategory, string QuerySysAccount,int QueryShareStatus,int QueryPayType,int QueryTrafficType,int  pageIndex,int  pageSize, out int totalCount);
     
        /// <summary>
        /// 根据求书id获取共享图书对象
        /// </summary>
        /// <param name="researchAsideBookId"></param>
        /// <returns></returns>
        ShareAsideBook GetResearchAsideBookId(int researchAsideBookId);
        /// <summary>
        /// 管理员邮寄界面 获取共享图书是否已经共享至平台
        /// </summary>
        /// <param name="researchAsideBookId"></param>
        /// <param name="shareStatus"></param>
        /// <returns></returns>
        ShareAsideBook GetResearchAsideBookId(int researchAsideBookId, int shareStatus);

        /// <summary>
        /// 根据id获取用户共享的图书数量
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        List<ShareAsideBook> GetShareList(int accountId);

    }
}
