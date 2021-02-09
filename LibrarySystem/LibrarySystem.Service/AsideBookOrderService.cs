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
    public class AsideBookOrderService : IAsideBookOrderService
    {
        private IAsideBookOrderBusiness _AsideBookOrderBiz;

        public AsideBookOrderService(IAsideBookOrderBusiness AsideBookOrderBiz)
        {
            this._AsideBookOrderBiz = AsideBookOrderBiz;
        }
        public void Delete(AsideBookOrder model)
        {
            this._AsideBookOrderBiz.Delete(model);
        }

        public AsideBookOrder GetById(int id)
        {
            return this._AsideBookOrderBiz.GetById(id);
        }

        public AsideBookOrder GetByIsbn(string isbn)
        {
            return this._AsideBookOrderBiz.GetByIsbn(isbn);
        }

        public AsideBookOrder GetByUniversityIsbn(int uinversityId, string isbn)
        {
            return this.GetByUniversityIsbn(uinversityId, isbn);
        }

        public List<AsideBookOrder> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount)
        {
            return this.GetManagerList(name, type, pageNum, pageSize, out totalCount);
        }

        public List<AsideBookOrder> GetManagerList(string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            return this._AsideBookOrderBiz.GetManagerList(queryName, queryIsbn, queryUId, queryCategory, pageIndex, pageSize, out totalCount);
        }

        public AsideBookOrder Insert(AsideBookOrder model)
        {
            return this._AsideBookOrderBiz.Insert(model);
        }

        public void Update(AsideBookOrder model)
        {
            this._AsideBookOrderBiz.Update(model);
        }

        /// <summary>
        /// 多条件查询订单列表
        /// </summary>
        /// <param name="querySysAccount">下单人账户</param>
        /// <param name="queryTrafType">运输类型</param>
        /// <param name="queryOrderStatus">订单状态</param>
        /// <param name="queryName">书名</param>
        /// <param name="queryIsbn">书号</param>
        /// <param name="queryUId">图书所在大学</param>
        /// <param name="queryCategory">类别</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="totalCount">数据总量</param>
        /// <returns></returns>
        public List<AsideBookOrder> GetManagerList(int uid,string querySysAccount, int queryTrafType, int queryOrderStatus, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            return this._AsideBookOrderBiz.GetManagerList(uid,querySysAccount, queryTrafType, queryOrderStatus, queryName, queryIsbn, queryUId, queryCategory, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据用户Id,图书id判断该用户是否已漂入此书 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="asideBookInfoId"></param>
        /// <returns></returns>
        public AsideBookOrder GetByOrdered(int accountId, int asideBookInfoId)
        {
            return this._AsideBookOrderBiz.GetByOrdered(accountId, asideBookInfoId);
        }

        /// <summary>
        /// 多条件查询订单列表 用户界面
        /// </summary>
        /// <param name="querySysAccount">下单人账户</param>
        /// <param name="queryTrafType">运输类型</param>
        /// <param name="queryOrderStatus">订单状态</param>
        /// <param name="queryName">书名</param>
        /// <param name="queryIsbn">书号</param>
        /// <param name="queryUId">图书所在大学</param>
        /// <param name="queryCategory">类别</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="totalCount">数据总量</param>
        /// <returns></returns>
        public List<AsideBookOrder> GetAccountManagerList(int accountId, int queryTrafType, int queryOrderStatus, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            return this._AsideBookOrderBiz.GetAccountManagerList(accountId, queryTrafType, queryOrderStatus, queryName, queryIsbn, queryUId, queryCategory, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 用户“顺路送书”列表
        /// </summary>
        /// <param name="queryUId"></param>
        /// <param name="queryTrafType"></param>
        /// <param name="queryIsMyRecord"></param>
        /// <param name="queryIsbn"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<AsideBookOrder> GetTransList(int queryUId, int queryPayType, int queryIsMyRecord,int sysAccountId,int sysAccountUid, string queryIsbn, int pageIndex, int pageSize, out int totalCount)
        {
            return this._AsideBookOrderBiz.GetTransList(queryUId, queryPayType, queryIsMyRecord, sysAccountId, sysAccountUid, queryIsbn, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据用户id获取用户送书成功的列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<AsideBookOrder> getSuccessfulTrasList(int accountId)
        {
            return this._AsideBookOrderBiz.getSuccessfulTrasList(accountId);
        }

        /// <summary>
        /// 获取用户在此平台上成功漂的图书列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<AsideBookOrder> GetMyList(int accountId)
        {
            return this._AsideBookOrderBiz.GetMyList(accountId);
        }
    }
}
