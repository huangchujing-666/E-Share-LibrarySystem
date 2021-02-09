using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Model;
using LibrarySystem.Core.Data;

namespace LibrarySystem.Business
{
    public class ViewConfigBusiness : IViewConfigBusiness
    {
        private IRepository<ViewConfig> _repoViewConfig;

        public ViewConfigBusiness(IRepository<ViewConfig> repoViewConfig)
        {
            _repoViewConfig = repoViewConfig;
        }
        public void Delete(ViewConfig model)
        {
            this._repoViewConfig.Delete(model);
        }

        public List<ViewConfig> GetAllList()
        {
            return this._repoViewConfig.Table.Where(c=>c.ViewConfigId>0).ToList();
        }

        public ViewConfig GetById(int id)
        {
            return this._repoViewConfig.GetById(id);
        }

        public List<ViewConfig> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<ViewConfig>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                where = where.And(m => m.Title.Contains(name));
            }

            totalCount = this._repoViewConfig.Table.Where(where).Count();
            return this._repoViewConfig.Table.Where(where).OrderBy(p => p.ViewConfigId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public ViewConfig Insert(ViewConfig model)
        {
            return this._repoViewConfig.Insert(model);
        }

        public void Update(ViewConfig model)
        {
            this._repoViewConfig.Update(model);
        }
    }
}
