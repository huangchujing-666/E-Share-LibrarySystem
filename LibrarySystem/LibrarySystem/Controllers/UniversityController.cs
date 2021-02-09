using Exam.Common;
using LibrarySystem.Admin.Models;
using LibrarySystem.Controllers;
using LibrarySystem.Core.Utils;
using LibrarySystem.Domain;
using LibrarySystem.Domain.Model;
using LibrarySystem.IService;
using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Admin.Controllers
{
    public class UniversityController : BaseController
    {
        private readonly IUniversityService _universityService;
        private readonly IViewConfigService _viewCongifService;

        public UniversityController(IUniversityService universityService, IViewConfigService viewCongifService)
        {
            _universityService = universityService;
            _viewCongifService = viewCongifService;
        }
        /// <summary>
        /// 数据源列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(UniversityViewModel vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _universityService.GetManagerList(vm.QueryName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<University>()
            {
                Items = list == null ? list : list.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效).ToList(),
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;
            vm.UinversityList = new List<University>();
            return View(vm);
        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int UniversityId = 0, int Status = 0)
        {
            if (UniversityId <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                var university = _universityService.GetById(UniversityId);
                university.Status = Status;
                _universityService.Update(university);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int id = 0)
        {
            if (id <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                var university = _universityService.GetById(id);
                university.IsDelete = (int)EnumHelp.IsDeleteEnum.已删除;
                _universityService.Update(university);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="ActivityDiscountVM"></param>
        /// <returns></returns>
        public ActionResult Edit(UniversityViewModel UniversityVM)
        {
            var university = _universityService.GetById(UniversityVM.UniversityId);
            if (university != null)
            {

                UniversityVM.Name = university.Name;
                UniversityVM.Service = university.Service;
                UniversityVM.DataBase = university.DataBase;
                UniversityVM.TimeStart = university.TimeStart;
                UniversityVM.UserId = university.UserId;
                UniversityVM.UserPwd = university.UserPwd;
                UniversityVM.IsUpdate = university.IsUpdate;
                UniversityVM.Paging = new Paging<University>();
                UniversityVM.QueryName = "";
                UniversityVM.UinversityList = new List<University>();
            }
            else
            {
                UniversityVM.Paging = new Paging<University>();
                UniversityVM.Name = "";
                UniversityVM.QueryName = "";
                UniversityVM.TimeStart = 0;
                UniversityVM.IsUpdate = 0;
                UniversityVM.UserId = "";
                UniversityVM.UserPwd = "";
                UniversityVM.Service = "";
                UniversityVM.DataBase = "";
                UniversityVM.UinversityList = new List<University>();
            }
            return View(UniversityVM);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(University model)
        {
            //更新
            if (model.UniversityId > 0)
            {
                var university = _universityService.GetById(model.UniversityId);
                university.EditTime = DateTime.Now;
                university.Name = model.Name;
                university.DataBase = model.DataBase;
                university.Service = model.Service;
                university.TimeStart = model.TimeStart;
                university.UserId = model.UserId;
                university.UserPwd = model.UserPwd;
                university.IsUpdate = model.IsUpdate;
                try
                {
                    _universityService.Update(university);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }//插入
            else
            {
                //添加
                model.Status = (int)EnumHelp.EnabledEnum.有效;
                model.IsDelete = (int)EnumHelp.IsDeleteEnum.有效;
                model.CreateTime = DateTime.Now;
                model.EditTime = DateTime.Now;
                model.IsUpdate = (int)EnumHelp.UpdateStartEnum.未启用;
                var university = _universityService.Insert(model);
                return Json(new { Status = university.UniversityId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateStart(ViewConfigVM ViewConfigVM)
        {
            var viewConfig = _viewCongifService.GetAllList().FirstOrDefault();
            if (viewConfig != null)
            {
                ViewConfigVM.ViewConfigId = viewConfig.ViewConfigId;
                ViewConfigVM.ViewName = viewConfig.ViewName;
                ViewConfigVM.Isbn = viewConfig.Isbn;
                ViewConfigVM.PublicDate = viewConfig.PublicDate;
                ViewConfigVM.Title = viewConfig.Title;
                ViewConfigVM.Author = viewConfig.Author;
                ViewConfigVM.Available = viewConfig.Available;
                ViewConfigVM.Category = viewConfig.Category;
                ViewConfigVM.Count = viewConfig.Count;
            }

            var list = _universityService.GetAllList().Where(c => c.IsUpdate == (int)EnumHelp.UpdateStartEnum.未启用 && c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            if (Common.Loginer.UniversityId>0)
            {
                list= list.Where(c => c.UniversityId == Common.Loginer.UniversityId).ToList();
            }
            var ulist = new List<University>();
            foreach (var item in list)
            {
                ulist.Add(new University()
                {
                    UniversityId = item.UniversityId,
                    Name = item.Name
                });
            }
            ViewConfigVM.UinversityList = ulist;
            return View(ViewConfigVM);
        }

        [HttpPost]
        public JsonResult UpdateStart(ViewConfigPostVM ViewConfigVM)
        {
            if (ViewConfigVM.ViewConfigId > 0)
            {
                var vc = _viewCongifService.GetById(ViewConfigVM.ViewConfigId);
                vc.Author = string.IsNullOrWhiteSpace(ViewConfigVM.Author) ? "" : ViewConfigVM.Author;
                vc.Available = string.IsNullOrWhiteSpace(ViewConfigVM.Available) ? "" : ViewConfigVM.Available;
                vc.Category = string.IsNullOrWhiteSpace(ViewConfigVM.Category) ? "" : ViewConfigVM.Category;
                vc.Count = string.IsNullOrWhiteSpace(ViewConfigVM.Count) ? "" : ViewConfigVM.Count;
                vc.Isbn = string.IsNullOrWhiteSpace(ViewConfigVM.Isbn) ? "" : ViewConfigVM.Isbn;
                vc.PublicDate = string.IsNullOrWhiteSpace(ViewConfigVM.PublicDate) ? "" : ViewConfigVM.PublicDate;
                vc.Title = string.IsNullOrWhiteSpace(ViewConfigVM.Title) ? "" : ViewConfigVM.Title;
                vc.ViewName = string.IsNullOrWhiteSpace(ViewConfigVM.ViewName) ? "" : ViewConfigVM.ViewName;
                try
                {
                    _viewCongifService.Update(vc);
                    if (ViewConfigVM.UniversityId <= 0)
                        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                var vc = new ViewConfig();
                vc.Author = string.IsNullOrWhiteSpace(ViewConfigVM.Author) ? "" : ViewConfigVM.Author;
                vc.Available = string.IsNullOrWhiteSpace(ViewConfigVM.Available) ? "" : ViewConfigVM.Available;
                vc.Category = string.IsNullOrWhiteSpace(ViewConfigVM.Category) ? "" : ViewConfigVM.Category;
                vc.Count = string.IsNullOrWhiteSpace(ViewConfigVM.Count) ? "" : ViewConfigVM.Count;
                vc.Isbn = string.IsNullOrWhiteSpace(ViewConfigVM.Isbn) ? "" : ViewConfigVM.Isbn;
                vc.PublicDate = string.IsNullOrWhiteSpace(ViewConfigVM.PublicDate) ? "" : ViewConfigVM.PublicDate;
                vc.Title = string.IsNullOrWhiteSpace(ViewConfigVM.Title) ? "" : ViewConfigVM.Title;
                vc.ViewName = string.IsNullOrWhiteSpace(ViewConfigVM.ViewName) ? "" : ViewConfigVM.ViewName;
                var v = _viewCongifService.Insert(vc);
                if (v.ViewConfigId <= 0)
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                else if (ViewConfigVM.UniversityId <= 0)
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }

            if (ViewConfigVM.UniversityId > 0)//获取下拉列表
            {
                var u = _universityService.GetById(ViewConfigVM.UniversityId);
                if (u != null)
                {
                    if (u.IsUpdate == (int)EnumHelp.UpdateStartEnum.未启用)
                    {
                        u.IsUpdate = (int)EnumHelp.UpdateStartEnum.启用;
                        try
                        {
                            _universityService.Update(u);
                            string ss = ViewConfigVM.UniversityId.ToString() + (ViewConfigVM.UpdateCount > 0 ? " true " : " false ") + Common.Loginer.AccountId;
                            string path = System.Configuration.ConfigurationManager.AppSettings["UpdatePath"].ToString();
                            System.Diagnostics.Process.Start(path, ss);
                            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception)
                        {
                            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Start(ViewConfigPostVM ViewConfigVM)
        {
            if (ViewConfigVM.UniversityId > 0)//获取下拉列表
            {
                var u = _universityService.GetById(ViewConfigVM.UniversityId);
                if (u != null)
                {
                    if (u.IsUpdate == (int)EnumHelp.UpdateStartEnum.未启用)
                    {
                        u.IsUpdate = (int)EnumHelp.UpdateStartEnum.启用;
                        try
                        {
                            _universityService.Update(u);
                            string ss = ViewConfigVM.UniversityId.ToString() + (ViewConfigVM.UpdateCount > 0 ? " true " : " false ") + Common.Loginer.AccountId;
                            string path = System.Configuration.ConfigurationManager.AppSettings["UpdatePath"].ToString();
                            System.Diagnostics.Process.Start(path, ss);
                            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception)
                        {
                            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }
    }
}