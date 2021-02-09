using Exam.Common;
using LibrarySystem.Admin.Models;
using LibrarySystem.Controllers;
using LibrarySystem.Core.Utils;
using LibrarySystem.Domain;
using LibrarySystem.Domain.Model;
using LibrarySystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Admin.Controllers
{
    public class AsideBookInfoController : BaseController
    {
        /// <summary>
        /// 声明asideBookInfo接口服务对象
        /// </summary>
        private readonly IAsideBookInfoService _asideBookInfoService;
        /// <summary>
        /// 声明University接口服务对象
        /// </summary>
        private readonly IUniversityService _universityService;
        /// <summary>
        /// 声明University接口服务对象
        /// </summary>
        private readonly IAsideBookOrderService _asideBookOrderService;

        /// <summary>
        /// 声明ISysAccountService接口服务对象
        /// </summary>
        private readonly ISysAccountService _sysAccountService;
        /// <summary>
        /// 构造函数进行对象初始化
        /// </summary>
        /// <param name="asideBookInfoService"></param>
        /// <param name="universityService"></param>
        public AsideBookInfoController(IAsideBookInfoService asideBookInfoService, IUniversityService universityService, IAsideBookOrderService asideBookOrderService, ISysAccountService sysAccountService)
        {
            this._asideBookInfoService = asideBookInfoService;
            this._universityService = universityService;
            this._asideBookOrderService = asideBookOrderService;
            this._sysAccountService = sysAccountService;
        }

        #region  用户界面、操作

        /// <summary>
        /// 用户闲置图书信息列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult BookList(AsideBookInfoVM vm, int pn = 1)
        {
            //1.0 页码 页容量初始化
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //2.0 条件获取数据
            var blist = _asideBookInfoService.GetManagerList(vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, pageIndex, pageSize, out totalCount);
            var paging = new Paging<AsideBookInfo>()
            {
                Items = blist,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            //获取大学下拉列选项
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
            vm.Paging = paging;
            //3.0 返回视图（数据）
            return View(vm);
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="AsideBookInfoVM"></param>
        /// <returns></returns>
        public ActionResult Edit(AsideBookInfoVM AsideBookInfoVM)
        {
            //1.0 根据主键查询漂流图书
            var asidebookinfo = _asideBookInfoService.GetById(AsideBookInfoVM.AsideBookInfoId);
            if (asidebookinfo != null)
            {
                AsideBookInfoVM.Author = asidebookinfo.Author;
                AsideBookInfoVM.Category = asidebookinfo.Category;
                AsideBookInfoVM.Isbn = asidebookinfo.Isbn;
                AsideBookInfoVM.PublicDate = asidebookinfo.PublicDate;
                AsideBookInfoVM.Title = asidebookinfo.Title;
                AsideBookInfoVM.UniversityId = asidebookinfo.UniversityId;
                AsideBookInfoVM.UniversityName = asidebookinfo.University.Name;
                AsideBookInfoVM.Available = asidebookinfo.Available;
                AsideBookInfoVM.BaseImageId = asidebookinfo.BaseImageId;
                AsideBookInfoVM.ImgInfo = asidebookinfo.BaseImage == null ? new BaseImage() : asidebookinfo.BaseImage;
                AsideBookInfoVM.QueryUId = 0;
                AsideBookInfoVM.QueryName = "";
                AsideBookInfoVM.QueryIsbn = "";
                AsideBookInfoVM.QueryCategory = "";
            }
            else
            {
                AsideBookInfoVM.QueryUId = 0;
                AsideBookInfoVM.QueryName = "";
                AsideBookInfoVM.BaseImageId = 0;
                AsideBookInfoVM.ImgInfo = new BaseImage();
                AsideBookInfoVM.QueryIsbn = "";
                AsideBookInfoVM.QueryCategory = "";
                AsideBookInfoVM.Author = "";
                AsideBookInfoVM.Category = "";
                AsideBookInfoVM.PublicDate = "";
                AsideBookInfoVM.Isbn = "";
                AsideBookInfoVM.Title = "";
                AsideBookInfoVM.UniversityId = 0;
                AsideBookInfoVM.UniversityName = "";
                AsideBookInfoVM.Available = 0;
            }
            //2.0 获取大学列表
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
            AsideBookInfoVM.UinversityList = ulist;
            AsideBookInfoVM.Paging = new Paging<AsideBookInfo>();
            //3.0 返回对象
            return View(AsideBookInfoVM);
        }

        /// <summary>
        /// 添加、修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(AsideBookInfo model)
        {
            //1.0 修改对象字段
            if (model.AsideBookInfoId > 0)
            {
                var asideBookInfo = _asideBookInfoService.GetById(model.AsideBookInfoId);
                asideBookInfo.EditTime = DateTime.Now;
                asideBookInfo.BaseImageId = model.BaseImageId;
                asideBookInfo.Title = model.Title;
                asideBookInfo.Author = model.Author;
                asideBookInfo.Category = model.Category;
                asideBookInfo.Available = model.Available;
                asideBookInfo.PublicDate = model.PublicDate;
                try
                {
                    _asideBookInfoService.Update(asideBookInfo);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            //2.0 插入新的对象
            else
            {
                var asideBook = _asideBookInfoService.GetByUniversityIsbn(model.UniversityId, model.Isbn);
                //2.1 判断表中此大学是否已经有此书
                if (asideBook != null && asideBook.AsideBookInfoId > 0)
                {
                    asideBook.Available += model.Available;
                    asideBook.EditTime = DateTime.Now;
                    try
                    {
                        //2.1.1 更新库存
                        _asideBookInfoService.Update(asideBook);
                        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
                //2.2 添加
                model.Status = (int)EnumHelp.EnabledEnum.有效;
                model.IsDelete = (int)EnumHelp.IsDeleteEnum.有效;
                model.CreateTime = DateTime.Now;
                model.EditTime = DateTime.Now;
                try
                {
                    var asideBookInfo = _asideBookInfoService.Insert(model);
                    return Json(new { Status = asideBookInfo.AsideBookInfoId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
               
            }
        }

        /// <summary>
        /// 漂入图书配送信息编辑
        /// </summary>
        /// <param name="AsideBookInfoVM"></param>
        /// <returns></returns>
        public ActionResult PreBook(AsideBookOrderVM AsideBookOrderVM)
        {
            //1.0 根据主键查询漂流图书
            var asidebookinfo = _asideBookInfoService.GetById(AsideBookOrderVM.AsideBookInfoId);
            var asideBookOrder = _asideBookOrderService.GetById(AsideBookOrderVM.AsideBookOrderId);
            var sysAccount = _sysAccountService.GetById(AsideBookOrderVM.SysAccountId);
            //var sysaccount = _sysAccountService.GetById(AsideBookOrderVM.SysAccountId);
            //AsideBookOrderVM.SysAccount = sysaccount;
            if (sysAccount != null)
            {
                AsideBookOrderVM.Email = sysAccount.Email;
                AsideBookOrderVM.MobilePhone = sysAccount.MobilePhone;
                AsideBookOrderVM.Address = sysAccount.Address;
            }
            if (asidebookinfo != null && asidebookinfo.Available > 0)
            {
                //2.0 字段初始化

                AsideBookOrderVM.TrafficType = 0;
                AsideBookOrderVM.TrafficFee = 0;
               // AsideBookOrderVM.CustomerInfo = "";
            }
            if (asideBookOrder != null)
            {
                AsideBookOrderVM.TrafficType = asideBookOrder.TrafficType;
                AsideBookOrderVM.TrafficFee = asideBookOrder.TrafficFee;
               // AsideBookOrderVM.CustomerInfo = asideBookOrder.CustomerInfo;
                AsideBookOrderVM.OrderStatus = asideBookOrder.OrderStatus;
                AsideBookOrderVM.QueryOrderStatus = asideBookOrder.OrderStatus;
                AsideBookOrderVM.Remark = asideBookOrder.Remark;
                AsideBookOrderVM.UniversityId = asideBookOrder.SysAccount.UniversityId;
            }
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
            AsideBookOrderVM.UinversityList = ulist;
            AsideBookOrderVM.Paging = new Paging<AsideBookOrder>();
            //3.0 返回对象
            return View(AsideBookOrderVM);
        }
        #endregion

        #region 管理员操作界面
        /// <summary>
        /// 管理员闲置图书信息列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(AsideBookInfoVM vm, int pn = 1)
        {
            //1.0 页码 页容量初始化
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //2.0 条件获取数据
            var blist = _asideBookInfoService.GetManagerList(vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, pageIndex, pageSize, out totalCount);
            var paging = new Paging<AsideBookInfo>()
            {
                Items = blist,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            //获取大学下拉列选项
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
            vm.Paging = paging;
            //3.0 返回视图（数据）
            return View(vm);
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
                var asideBookInfo = _asideBookInfoService.GetById(id);
                asideBookInfo.IsDelete = (int)EnumHelp.IsDeleteEnum.已删除;
                _asideBookInfoService.Update(asideBookInfo);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int AsideBookInfoId = 0, int Status = 0)
        {
            //1.0 数据验证
            if (AsideBookInfoId <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            { 
                //2.0 修改Status
                var asideBookInfo = _asideBookInfoService.GetById(AsideBookInfoId);
                asideBookInfo.Status = Status;
                //3.0 更新数据
                _asideBookInfoService.Update(asideBookInfo);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}