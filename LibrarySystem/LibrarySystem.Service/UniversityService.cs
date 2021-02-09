using LibrarySystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Model;
using LibrarySystem.Business;

namespace OrderingSystem.Service
{
    public class UniversityService : IUniversityService
    {
        /// <summary>
        /// The ActivityDiscount biz
        /// </summary>
        private IUniversityBusiness _UniversityBusinessBiz;

        public UniversityService(IUniversityBusiness UniversityBusinessBiz)
        {
            _UniversityBusinessBiz = UniversityBusinessBiz;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        public void Delete(University model)
        {
            this._UniversityBusinessBiz.Delete(model);
        }

        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public University GetById(int id)
        {
            return this._UniversityBusinessBiz.GetById(id);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<University> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._UniversityBusinessBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public University Insert(University model)
        {
            return this._UniversityBusinessBiz.Insert(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        public void Update(University model)
        {
            this._UniversityBusinessBiz.Update(model);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<University> GetAllList()
        {
            return this._UniversityBusinessBiz.GetAllList();
        }
    }
}
