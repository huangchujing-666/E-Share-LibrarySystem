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
    public class ShareAsideBookService : IShareAsideBookService
    {
        private IShareAsideBookBusiness _ShareAsideBookBiz;
        public ShareAsideBookService(IShareAsideBookBusiness ShareAsideBookBiz)
        {
            this._ShareAsideBookBiz = ShareAsideBookBiz;
        }
        public void Delete(ShareAsideBook model)
        {
            this._ShareAsideBookBiz.Delete(model);
        }

        public ShareAsideBook GetById(int id)
        {
            return this._ShareAsideBookBiz.GetById(id);
        }

        public List<ShareAsideBook> GetManagerList(int accountId, string queryName, string queryIsbn, int queryUId, string queryCategory, string querySysAccount, int queryShareStatus, int queryPayType, int queryTrafficType, int pageIndex, int pageSize, out int totalCount)
        {
            return this._ShareAsideBookBiz.GetManagerList(accountId, queryName, queryIsbn, queryUId, queryCategory, querySysAccount, queryShareStatus, queryPayType, queryTrafficType, pageIndex, pageSize, out totalCount);
        }

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
        public List<ShareAsideBook> GetAdminManagerList(int QueryShareType, string QueryName, string QueryIsbn, int QueryUId, string QueryCategory, string QuerySysAccount, int QueryShareStatus, int QueryPayType, int QueryTrafficType, int pageIndex, int pageSize, out int totalCount)
        {
            return this._ShareAsideBookBiz.GetAdminManagerList(QueryShareType, QueryName, QueryIsbn, QueryUId, QueryCategory, QuerySysAccount, QueryShareStatus, QueryPayType, QueryTrafficType, pageIndex, pageSize, out totalCount);
        }
        public ShareAsideBook Insert(ShareAsideBook model)
        {
            return this._ShareAsideBookBiz.Insert(model);
        }

        public void Update(ShareAsideBook model)
        {
            this._ShareAsideBookBiz.Update(model);
        }

        /// <summary>
        /// 根据求书id获取共享图书对象
        /// </summary>
        /// <param name="researchAsideBookId"></param>
        /// <returns></returns>
        public ShareAsideBook GetResearchAsideBookId(int researchAsideBookId)
        {
            return this._ShareAsideBookBiz.GetResearchAsideBookId(researchAsideBookId);
        }

        /// <summary>
        /// 管理员邮寄界面 获取共享图书是否已经共享至平台
        /// </summary>
        /// <param name="researchAsideBookId"></param>
        /// <param name="shareStatus"></param>
        /// <returns></returns>
        public ShareAsideBook GetResearchAsideBookId(int researchAsideBookId, int shareStatus) {
            return this._ShareAsideBookBiz.GetResearchAsideBookId(researchAsideBookId, shareStatus);
        }

        /// <summary>
        /// 根据id获取用户共享的图书数量
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<ShareAsideBook> GetShareList(int accountId)
        {
            return this._ShareAsideBookBiz.GetShareList(accountId);
        }
    }
}
