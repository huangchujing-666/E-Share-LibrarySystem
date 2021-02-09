using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpdate
{
    public static class EnumHelp
    {
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
        public enum UpdateStartEnum
        {
            启用 = 1,
            未启用 = 0
        }
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
