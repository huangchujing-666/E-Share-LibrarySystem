using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain
{
    public class EnumHelp
    {
        /// <summary>
        /// 求书记录状态
        /// </summary>
        public enum ResearchStatus
        {
            取消求书 = 0,
            求书中 = 1,
            找到书源 = 2,
            求书成功 = 3
        }

        /// <summary>
        /// 求书类型
        /// </summary>
        public enum ResearchPayType
        {
            有偿 = 1,
            无偿 = 2
        }
        /// <summary>
        /// 求书人  我的求书=1  全部求书=2
        /// </summary>
        public enum IsMyResearch
        {
            我的求书 = 1,
            全部求书 = -1
        }

        /// <summary>
        /// 图书运输类型
        /// </summary>
        public enum TrafficType
        {
            自取 = 1,
            邮寄 = 2,
            顺路送书 = 3
        }

        /// <summary>
        /// 共享图书运输方式 出书
        /// </summary>
        public enum ResearchTrafficType
        {
            自行送至漂流书屋 = 1,
            工作人员上门取书 = 2
        }

        /// <summary>
        /// 共享图书状态
        /// </summary>
        public enum BookShareStatus
        {
            待入库 = 1,
            已共享 = 2,
            已取消 = 3
        }

        /// <summary>
        /// 共享数据类型
        /// </summary>
        public enum BookShareType {
            自主共享=1,
            求书共享=2
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum BookOrderStatus
        {

            待自取 = 1,
            待邮寄 = 2,
            待顺风送 = 3,
            运输中 = 4,
            已完结 = 5,
            已取消 = 6
        }
        /// <summary>
        /// 顺路送书 接送状态
        /// </summary>
        public enum MyTransferStatus
        {
            已负责接送 = 1,
            待顺路接送 = 2
        }

        /// <summary>
        /// 是否已收到图书
        /// </summary>
        public enum IsReceived
        {
            未收到=1,
            已收到=2
        }
        public enum IsBorrow
        {
            可预借 = 1,
            其他 = 2
        }
        public enum BorrowStatus
        {
            审核中 = 1,
            审核通过 = 2,
            审核驳回 = 3,
            库存不足 = 4,
            借书中 = 5,
            逾期欠费 = 6,
            已还书 = 7,
            续借 = 8,
            取消=9
        }



        /// <summary>
        /// 用户角色别枚举
        /// </summary>
        public enum RoleTypeEnum
        {
            系统管理员 = 1000,
            学生 = 1002,
            管理员 = 1003
        }
        public enum ErrorLogType
        {
            更新异常记录 = -1,
            更新记录 = 1,
            更新异常 = 2,
            插入异常 = 3,
            获取数据源异常 = 4,
            获取更新次数异常 = 5
        }
        /// <summary>
        /// 数据更新状态
        /// </summary>
        public enum UpdateStatus
        {
            更新中 = 2,
            更新成功 = 1,
            更新失败 = 3
        }

        /// <summary>
        /// 启用更新枚举
        /// </summary>
        public enum UpdateStartEnum
        {
            启用 = 1,
            未启用 = 0
        }

        /// <summary>
        /// 启用状态枚举
        /// </summary>
        public enum EnabledEnum
        {
            无效 = 0,
            有效 = 1,
        }

        public enum IsDeleteEnum
        {
            已删除 = 1,
            有效 = 0,
        }
    }
}
