using LibrarySystem.Core.Data;
using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Business
{
    public class ExpressBusiness : IExpressBusiness
    {
        private IRepository<Express> _repExpress;
        public ExpressBusiness(IRepository<Express> repExpress) {
            this._repExpress = repExpress;
        }
        public void Delete(Express model)
        {
            this._repExpress.Delete(model);
        }

        public Express GetById(int id)
        {
            return this._repExpress.GetById(id);
        }

        public Express Insert(Express model)
        {
            return this._repExpress.Insert(model);
        }

        public void Update(Express model)
        {
            this._repExpress.Update(model);
        }
    }
}
