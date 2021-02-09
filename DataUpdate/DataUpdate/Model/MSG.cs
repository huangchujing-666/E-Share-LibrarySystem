using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpdate.Model
{
    public class MSG
    {
        /// <summary>
        /// 线程号
        /// </summary>
        public int i { get; set; }

        /// <summary>
        /// 数据错误点
        /// </summary>
        public List<int[]> list { get; set; }
    }
}
