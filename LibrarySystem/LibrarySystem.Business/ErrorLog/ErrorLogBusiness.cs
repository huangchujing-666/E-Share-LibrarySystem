using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Model;
using LibrarySystem.Core.Data;
using LibrarySystem.Domain;

namespace LibrarySystem.Business
{
    public class ErrorLogBusiness : IErrorLogBusiness
    {
        private IRepository<ErrorLog> _repoErrorLog;

        public ErrorLogBusiness(IRepository<ErrorLog> repoErrorLog)
        {
            _repoErrorLog = repoErrorLog;
        }
        public void Delete(ErrorLog model)
        {
            this._repoErrorLog.Delete(model);
        }

        public ErrorLog GetById(int id)
        {
            return this._repoErrorLog.GetById(id);
        }

        public List<ErrorLog> GetManagerList(int QueryCount,int universityId, int type, int status, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ErrorLog>();

            if (universityId>0)
            {
                where = where.And(m => m.UniversityId== universityId);
            }
            if (type >0)
            {
                where = where.And(m => m.Type==(int)EnumHelp.ErrorLogType.更新记录);
            }
            if (type < 0)
            {
                where = where.And(m => m.Type!= (int)EnumHelp.ErrorLogType.更新记录);
            }
            if (status > 0)
            {
                where = where.And(m => m.Status==status);
            }
            if (QueryCount>0)
            {
                where = where.And(m => m.UpdateCount == QueryCount);
            }
            totalCount = this._repoErrorLog.Table.Where(where).Count();
            return this._repoErrorLog.Table.Where(where).OrderByDescending(p => p.ErrorTime).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public ErrorLog Insert(ErrorLog model)
        {
            return this._repoErrorLog.Insert(model);
        }

        public void Update(ErrorLog model)
        {
            this._repoErrorLog.Update(model);
        }
    }
}
