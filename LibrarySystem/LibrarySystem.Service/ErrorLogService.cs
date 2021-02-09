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
    public class ErrorLogService : IErrorLogService
    {

        private IErrorLogBusiness _ErrorLogBusinessBiz;

        public ErrorLogService(IErrorLogBusiness ErrorLogBusinessBiz)
        {
            _ErrorLogBusinessBiz = ErrorLogBusinessBiz;
        }
        public void Delete(ErrorLog model)
        {
            this.Delete(model);
        }

        public ErrorLog GetById(int id)
        {
            return this._ErrorLogBusinessBiz.GetById(id);
        }

        public List<ErrorLog> GetManagerList(int QueryCount,int universityId, int type, int status, int pageNum, int pageSize, out int totalCount)
        {
            return this._ErrorLogBusinessBiz.GetManagerList(QueryCount,universityId, type, status, pageNum, pageSize, out totalCount);
        }

        public ErrorLog Insert(ErrorLog model)
        {
            return this._ErrorLogBusinessBiz.Insert(model);
        }

        public void Update(ErrorLog model)
        {
            this._ErrorLogBusinessBiz.Update(model);
        }
    }
}
