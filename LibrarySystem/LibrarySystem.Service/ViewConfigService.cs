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
    public class ViewConfigService : IViewConfigService
    {
        private IViewConfigBusiness _ViewConfigBusinessBiz;

        public ViewConfigService(IViewConfigBusiness ViewConfigBusinessBiz)
        {
            _ViewConfigBusinessBiz = ViewConfigBusinessBiz;
        }
        public void Delete(ViewConfig model)
        {
            this._ViewConfigBusinessBiz.Delete(model);
        }

        public List<ViewConfig> GetAllList()
        {
            return this._ViewConfigBusinessBiz.GetAllList();
        }

        public ViewConfig GetById(int id)
        {
            return this._ViewConfigBusinessBiz.GetById(id);
        }

        public List<ViewConfig> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._ViewConfigBusinessBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        public ViewConfig Insert(ViewConfig model)
        {
            return this._ViewConfigBusinessBiz.Insert(model);
        }

        public void Update(ViewConfig model)
        {
            this._ViewConfigBusinessBiz.Update(model);
        }
    }
}
