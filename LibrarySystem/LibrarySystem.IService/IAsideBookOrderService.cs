using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.IService
{
    public interface IAsideBookOrderService
    {
        AsideBookOrder GetById(int id);

        AsideBookOrder Insert(AsideBookOrder model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(AsideBookOrder model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(AsideBookOrder model);


        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<AsideBookOrder> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount);

        List<AsideBookOrder> GetManagerList(string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount);
        AsideBookOrder GetByUniversityIsbn(int uinversityId, string isbn);
        AsideBookOrder GetByIsbn(string isbn);
        /// <summary>
        /// 多条件查询订单列表 管理员界面
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
        List<AsideBookOrder> GetManagerList(int uid,string querySysAccount, int queryTrafType, int queryOrderStatus, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 根据用户Id,图书id判断该用户是否已漂入此书 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="asideBookInfoId"></param>
        /// <returns></returns>
        AsideBookOrder GetByOrdered(int accountId, int asideBookInfoId);

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
        List<AsideBookOrder> GetAccountManagerList(int accountId, int queryTrafType, int queryOrderStatus, string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount);

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
        List<AsideBookOrder> GetTransList(int queryUId, int queryPayType, int queryIsMyRecord,int sysAccountId,int sysAccountUid, string queryIsbn, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 根据用户id获取用户送书成功的列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        List<AsideBookOrder> getSuccessfulTrasList(int accountId);

        /// <summary>
        /// 获取用户在此平台上成功漂的图书列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        List<AsideBookOrder> GetMyList(int accountId);
    }
}
