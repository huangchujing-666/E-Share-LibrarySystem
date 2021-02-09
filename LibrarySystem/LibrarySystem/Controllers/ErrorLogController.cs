using LibrarySystem.Admin.Models;
using LibrarySystem.Core.Utils;
using LibrarySystem.Domain;
using LibrarySystem.Domain.Model;
using LibrarySystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Controllers
{
    public class ErrorLogController : BaseController
    {
        private readonly IUniversityService _universityService;
        private readonly IErrorLogService _errorLogService;

        public ErrorLogController(IUniversityService universityService, IErrorLogService errorLogService)
        {
            _universityService = universityService;
            _errorLogService = errorLogService;
        }


        /// <summary>
        /// 错误日志列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(ErrorLogVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _universityService.GetAllList().Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            var ulist = new List<University>();
            foreach (var item in list)
            {
                ulist.Add(new University()
                {
                    UniversityId = item.UniversityId,
                    Name = item.Name
                });
            }
            vm.UinversityList = ulist;
           var dataList=  _errorLogService.GetManagerList(vm.QueryCount,vm.QueryUniversityId, vm.QueryType, vm.QueryStatus, pageIndex, pageSize, out totalCount);
         
            var paging = new Paging<ErrorLog>()
            {
                Items = dataList == null ? dataList : dataList.OrderByDescending(c=>c.ErrorLogId).ToList(),
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;
            return View(vm);
        }


    }
}