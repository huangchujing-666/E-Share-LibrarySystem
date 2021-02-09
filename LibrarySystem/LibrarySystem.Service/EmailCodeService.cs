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
    public class EmailCodeService : IEmailCodeService
    {
        /// <summary>
        /// The ActivityDiscount biz
        /// </summary>
        private IEmailCodeBusiness _EmailCodeBiz;

        public EmailCodeService(IEmailCodeBusiness EmailCodeBiz)
        {
            _EmailCodeBiz = EmailCodeBiz;
        }
        public void Delete(EmailCode model)
        {
            this._EmailCodeBiz.Delete(model);
        }

        public EmailCode GetByEmailWithType(string email, int type)
        {
            return this._EmailCodeBiz.GetByEmailWithType(email, type);
        }

        public EmailCode Insert(EmailCode model)
        {
            return this._EmailCodeBiz.Insert(model);
        }

        public void Update(EmailCode model)
        {
            this._EmailCodeBiz.Update(model);
        }
    }
}
