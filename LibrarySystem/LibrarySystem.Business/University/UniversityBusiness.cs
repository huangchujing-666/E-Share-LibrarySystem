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
    public class UniversityBusiness : IUniversityBusiness
    {
        private IRepository<University> _repoUniversity;

        public UniversityBusiness(IRepository<University> repoUniversity)
        {
            _repoUniversity = repoUniversity;
        }

        public void Delete(University model)
        {
            this._repoUniversity.Delete(model);
        }

        public University GetById(int id)
        {
            return this._repoUniversity.GetById(id);
        }

        public List<University> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<University>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }

            totalCount = this._repoUniversity.Table.Where(where).Count();
            return this._repoUniversity.Table.Where(where).OrderBy(p => p.UniversityId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public University Insert(University model)
        {
            return this._repoUniversity.Insert(model);
        }

        public void Update(University model)
        {
            this._repoUniversity.Update(model);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<University> GetAllList()
        {
            return this._repoUniversity.Table.Where(c => c.Status == (int)EnumHelp.EnabledEnum.有效 && c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效).OrderByDescending(c=>c.CreateTime).ToList();
        }
    }
}
