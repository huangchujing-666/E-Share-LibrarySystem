using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpdate.Dal
{
    public class OriginDataDal
    {
        ErrorLogDal elg;
        ViewConfigDal v;
        public OriginDataDal()
        {
            elg = new ErrorLogDal();
            v = new ViewConfigDal();
        }

        /// <summary>
        /// 获取数据源的数量
        /// </summary>
        /// <param name="sourceConfigId"></param>
        /// <param name="sqlStr"></param>
        /// <param name="sqlConn"></param>
        /// <param name="mess"></param>
        /// <returns></returns>
        public int GetDataSourceCount(int sourceConfigId, string sqlStr, string sqlConn, out string mess)
        {
            mess = "";
            return v.GetViewTotalCount(CommHelp.DataSourceConn, out mess);
        }


    }
}
