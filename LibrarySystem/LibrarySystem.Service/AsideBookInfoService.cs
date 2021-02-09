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
    public class AsideBookInfoService : IAsideBookInfoService
    {
        private IAsideBookInfoBusiness _AsideBookInfoBiz;

        public AsideBookInfoService(IAsideBookInfoBusiness AsideBookInfoBiz)
        {
            _AsideBookInfoBiz = AsideBookInfoBiz;
        }

        public void Delete(AsideBookInfo model)
        {
            _AsideBookInfoBiz.Delete(model);
        }

        public AsideBookInfo GetById(int id)
        {
           return  _AsideBookInfoBiz.GetById(id);
        }

        public AsideBookInfo GetByUniversityIsbn(int uinversityId, string isbn)
        {
            return _AsideBookInfoBiz.GetByUniversityIsbn(uinversityId, isbn);
        }

        public List<AsideBookInfo> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount)
        {
            return _AsideBookInfoBiz.GetManagerList(name, type, pageNum, pageSize, out totalCount);
        }

        public List<AsideBookInfo> GetManagerList(string queryName, string queryIsbn, int queryUId, string queryCategory, int pageIndex, int pageSize, out int totalCount)
        {
            return _AsideBookInfoBiz.GetManagerList(queryName, queryIsbn, queryUId, queryCategory, pageIndex, pageSize, out totalCount);
        }

        public AsideBookInfo Insert(AsideBookInfo model)
        {
            return _AsideBookInfoBiz.Insert(model);
        }

        public void Update(AsideBookInfo model)
        {
            _AsideBookInfoBiz.Update(model);
        }

        public List<AsideBookInfo> GetByIsbn(string isbn)
        {
            return _AsideBookInfoBiz.GetByIsbn(isbn);
        }
    }
}
