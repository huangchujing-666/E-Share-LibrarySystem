using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Model;
using LibrarySystem.Core.Data;

namespace LibrarySystem.Business
{
    public class EmailCodeBusiness : IEmailCodeBusiness
    {
        private IRepository<EmailCode> _repoEmailCode;
        public EmailCodeBusiness(
      IRepository<EmailCode> repoEmailCode
       )
        {
            _repoEmailCode = repoEmailCode;
        }

        public void Delete(EmailCode model)
        {
            this._repoEmailCode.Delete(model);
        }

        public EmailCode GetByEmailWithType(string email, int type)
        {
            var where = PredicateBuilder.True<EmailCode>();
            if (!string.IsNullOrWhiteSpace(email) && type > 0)
            {
                where = where.And(c=>c.Email.Equals(email)&&c.Type==type);
            }
            return this._repoEmailCode.Table.Where(where).OrderByDescending(c => c.CreateTime).FirstOrDefault();
        }

        public EmailCode Insert(EmailCode model)
        {
            return this._repoEmailCode.Insert(model);
        }

        public void Update(EmailCode model)
        {
            this._repoEmailCode.Update(model);
        }
    }
}
