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
    public class ExpressService : IExpressService
    {
        private IExpressBusiness _ExpressBiz;

        public ExpressService(IExpressBusiness ExpressBiz)
        {
            _ExpressBiz = ExpressBiz;
        }
        public void Delete(Express model)
        {
            this._ExpressBiz.Delete(model);
        }

        public Express GetById(int id)
        {
            return this._ExpressBiz.GetById(id);
        }

        public Express Insert(Express model)
        {
            return this._ExpressBiz.Insert(model);
        }

        public void Update(Express model)
        {
            this._ExpressBiz.Update(model);
        }
    }
}
