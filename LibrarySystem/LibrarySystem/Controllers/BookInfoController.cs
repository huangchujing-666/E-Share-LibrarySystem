using Exam.Common;
using LibrarySystem.Admin.Common;
using LibrarySystem.Admin.Models;
using LibrarySystem.Common;
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
    public class BookInfoController : BaseController
    {
        private readonly IBookInfoService _bookInfoService;
        private readonly IUniversityService _universityService;
        private readonly IBookOrderService _bookOrderService;
        private readonly ISysAccountService _sysAccountService;
        public BookInfoController(IBookInfoService bookInfoService, IUniversityService universityService, IBookOrderService bookOrderService, ISysAccountService sysAccountService)
        {
            this._bookInfoService = bookInfoService;
            this._universityService = universityService;
            this._bookOrderService = bookOrderService;
            this._sysAccountService = sysAccountService;
        }
        /// <summary>
        /// 图书信息列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(BookInfoVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var blist = _bookInfoService.GetManagerList(vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, pageIndex, pageSize, out totalCount);
            var paging = new Paging<BookInfo>()
            {
                Items = blist,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };

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
            vm.UserPaging = new Paging<UserBookInfoVM>();
            return View(vm);
        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int BookInfoId = 0, int Status = 0)
        {
            if (BookInfoId <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                var bookInfo = _bookInfoService.GetById(BookInfoId);
                bookInfo.Status = Status;
                _bookInfoService.Update(bookInfo);
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
                var bookInfo = _bookInfoService.GetById(id);
                bookInfo.IsDelete = (int)EnumHelp.IsDeleteEnum.已删除;
                _bookInfoService.Update(bookInfo);
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
        /// <param name="BookInfoVM"></param>
        /// <returns></returns>
        public ActionResult Edit(BookInfoVM BookInfoVM)
        {
            var bookinfo = _bookInfoService.GetById(BookInfoVM.BookInfoId);
            if (bookinfo != null)
            {
                BookInfoVM.Author = bookinfo.Author;
                BookInfoVM.Category = bookinfo.Category;
                BookInfoVM.Count = bookinfo.Count;
                BookInfoVM.Isbn = bookinfo.Isbn;
                BookInfoVM.PublicDate = bookinfo.PublicDate;
                BookInfoVM.Title = bookinfo.Title;
                BookInfoVM.Paging = new Paging<BookInfo>();
                BookInfoVM.UniversityId = bookinfo.UniversityId;
                BookInfoVM.UniversityName = bookinfo.University.Name;
                BookInfoVM.Available = bookinfo.Available;
                BookInfoVM.QueryUId = 0;
                BookInfoVM.QueryName = "";
                BookInfoVM.QueryIsbn = "";
                BookInfoVM.QueryCategory = "";
                BookInfoVM.UinversityList = new List<University>();
            }
            else
            {
                BookInfoVM.Paging = new Paging<BookInfo>();
                BookInfoVM.QueryUId = 0;
                BookInfoVM.QueryName = "";
                BookInfoVM.QueryIsbn = "";
                BookInfoVM.QueryCategory = "";
                BookInfoVM.UinversityList = new List<University>();
                BookInfoVM.Author = "";
                BookInfoVM.Category = "";
                BookInfoVM.Count = 0;
                BookInfoVM.PublicDate = "";
                BookInfoVM.Isbn = "";
                BookInfoVM.Title = "";
                BookInfoVM.UniversityId = 0;
                BookInfoVM.UniversityName = "";
                BookInfoVM.Available = 0;
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
            BookInfoVM.UinversityList = ulist;

            return View(BookInfoVM);
        }

        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(BookInfo model)
        {
            //更新
            if (model.BookInfoId > 0)
            {
                var bookInfo = _bookInfoService.GetById(model.BookInfoId);
                bookInfo.EditTime = DateTime.Now;
                bookInfo.Title = model.Title;
                bookInfo.Author = model.Author;
                bookInfo.Category = model.Category;
                bookInfo.Available = model.Available;
                bookInfo.PublicDate = model.PublicDate;
                try
                {
                    _bookInfoService.Update(bookInfo);
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
                var bookInfo = _bookInfoService.Insert(model);
                return Json(new { Status = bookInfo.BookInfoId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }











        /// <summary>
        /// 用户界面  我要借书
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult BookList(BookInfoVM vm, int pn = 1)
        {
            int totalCount,
               pageIndex = pn,
               pageSize = PagingConfig.PAGE_SIZE;

            var blist = _bookInfoService.GetManagerListByUser(Loginer.AccountId, vm.IsBorrow, vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, pageIndex, pageSize, out totalCount);
            var ublist = new List<UserBookInfoVM>();
            if (blist != null && blist.Count > 0)
            {
                foreach (var item in blist)
                {
                    bool IsBorrow = true; ;
                    if (item.BookOrderList != null && item.BookOrderList.Count > 0)
                    {
                        if (item.BookOrderList.Where(c => c.SysAccountId == Loginer.AccountId).FirstOrDefault() != null)
                            IsBorrow = false;
                        else if (item.Available <= 0)
                            IsBorrow = false;
                    }
                    ublist.Add(new UserBookInfoVM()
                    {
                        Author = item.Author,
                        Count = item.Count,
                        Available = item.Available,
                        BookInfoId = item.BookInfoId,
                        Category = item.Category,
                        CreateTime = item.CreateTime,
                        EditTime = item.EditTime,
                        Isbn = item.Isbn,
                        IsDelete = item.IsDelete,
                        PublicDate = item.PublicDate,
                        Status = item.Status,
                        Title = item.Title,
                        UniversityName = item.University.Name,
                        IsBorrow = IsBorrow,
                        UniversityId = item.UniversityId
                    });
                }
            }
            var paging = new Paging<UserBookInfoVM>()
            {
                Items = ublist,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };

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
            int borrowCount = _bookOrderService.GetMyBorrowCount(Loginer.AccountId);
            int availiable = (LibrarySystem.Common.CommonPara.MAX_BORROW_COUNT - borrowCount) <= 0 ? 0 : LibrarySystem.Common.CommonPara.MAX_BORROW_COUNT - borrowCount;
            vm.UinversityList = ulist;
            vm.UserPaging = paging;
            vm.BorrowCount = borrowCount;
            vm.AvaliableCount = availiable;
            return View(vm);
        }

        /// <summary>
        /// 预借图书配送信息编辑
        /// </summary>
        /// <param name="BookOrderVM"></param>
        /// <returns></returns>
        public ActionResult PreBook(BookOrderVM BookOrderVM)
        {
            //1.0 根据主键查询图书  订单  下单人
            var bookinfo = _bookInfoService.GetById(BookOrderVM.BookInfoId);
            var sysAccount = _sysAccountService.GetById(BookOrderVM.SysAccountId);
            var bookOrder = _bookOrderService.GetById(BookOrderVM.BookOrderId);
            //2.0 字段初始化
            if (sysAccount != null)
            {
                BookOrderVM.Email = sysAccount.Email;
                BookOrderVM.MobilePhone = sysAccount.MobilePhone;
                BookOrderVM.Address = sysAccount.Address;
            }
            if (bookOrder != null && bookOrder.BookOrderId > 0)
            {
                BookOrderVM.TrafficType = bookOrder.TrafficType;
            }
            if (bookinfo != null && bookinfo.Available > 0)
            {

                BookOrderVM.Isbn = bookinfo.Isbn;
                BookOrderVM.Title = bookinfo.Title;
                BookOrderVM.UniversityId = bookinfo.UniversityId;
                BookOrderVM.UniversityName = bookinfo.University.Name;
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
            BookOrderVM.UinversityList = ulist;
            //3.0 返回对象
            return View(BookOrderVM);
        }

        [HttpPost]
        public JsonResult PreBook(BookInfoVM vm)
        {
            //1.0 预借按钮 首先判断学生借书数量是否超过规定数量
            if (vm.BookOrderId <= 0)
            {
                int borrowCount = _bookOrderService.GetMyBorrowCount(Loginer.AccountId);
                if (borrowCount > (int)LibrarySystem.Common.CommonPara.MAX_BORROW_COUNT)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            var sysAccount = _sysAccountService.GetById(vm.SysAccountId);
            var bookInfo = _bookInfoService.GetById(vm.BookInfoId);
            var bookOrder = _bookOrderService.GetById(vm.BookOrderId);
            //2.0 预借订单进行地址编辑
            if (vm.BookOrderId > 0 && bookOrder != null && sysAccount != null)
            {
                try
                {
                    //2.1 邮寄方式 更新地址 联系方式
                    if (vm.TrafficType == (int)EnumHelp.TrafficType.邮寄)
                    {
                        sysAccount.Email = vm.Email;
                        sysAccount.MobilePhone = vm.MobilePhone;
                        sysAccount.Address = vm.Address;
                        _sysAccountService.Update(sysAccount);
                    }
                    //2.2 修改运输方式
                    bookOrder.TrafficType = vm.TrafficType;
                    _bookOrderService.Update(bookOrder);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            //3.0 新增订单
            else if (vm.BookOrderId <= 0 && bookInfo != null && sysAccount != null)
            {
                //3.1 判断库存
                if (bookInfo.Available <= 0)
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                else if (bookInfo.BookOrderList != null && bookInfo.Count > 0)
                {
                    //3.2 判断是否已借此书
                    var order = bookInfo.BookOrderList.Where(c => c.SysAccountId == Loginer.AccountId && (c.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核中 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.续借 || c.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费)).FirstOrDefault();
                    if (order != null)
                        return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    else
                    {
                        //3.3 本校学生借阅本校图书
                        if (bookInfo.UniversityId == sysAccount.UniversityId)
                        {
                            DateTime date = System.DateTime.Now;
                            var obj = new BookOrder()
                            {
                                BackTime = date.AddDays(CommonPara.BORROW_DAY),
                                BookInfoId = vm.BookInfoId,
                                BorrowStatus = (int)EnumHelp.BorrowStatus.审核中,
                                Count = 1,
                                CreateTime = date,
                                EditPersonId = Loginer.AccountId,
                                EditTime = date,
                                ExpressId = 0,
                                IsReceived = (int)EnumHelp.IsReceived.未收到,
                                TrafficType = (int)EnumHelp.TrafficType.自取,
                                IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                                Status = (int)EnumHelp.EnabledEnum.有效,
                                SysAccountId = Loginer.AccountId,
                                Ticket = 0m
                            };
                            var insertResult = _bookOrderService.Insert(obj);
                            return Json(new { Status = insertResult.BookOrderId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
                        }
                        //3.4 借阅人与图书不同校
                        else
                        {
                            DateTime date = System.DateTime.Now;
                            var obj = new BookOrder()
                            {
                                BackTime = date.AddDays(CommonPara.BORROW_DAY),
                                BookInfoId = vm.BookInfoId,
                                BorrowStatus = (int)EnumHelp.BorrowStatus.审核中,
                                Count = 1,
                                CreateTime = date,
                                EditPersonId = Loginer.AccountId,
                                EditTime = date,
                                ExpressId = 0,
                                IsReceived = (int)EnumHelp.IsReceived.未收到,
                                TrafficType = vm.TrafficType,
                                IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                                Status = (int)EnumHelp.EnabledEnum.有效,
                                SysAccountId = Loginer.AccountId,
                                Ticket = 0m
                            };
                            sysAccount.Email = vm.Email;
                            sysAccount.MobilePhone = vm.MobilePhone;
                            //3.5 邮寄方式 更新地址 联系方式
                            if (vm.TrafficType == (int)EnumHelp.TrafficType.邮寄)
                            {                     
                                sysAccount.Address = vm.Address;
                            }
                            _sysAccountService.Update(sysAccount);
                            var insertResult = _bookOrderService.Insert(obj);
                            return Json(new { Status = insertResult.BookOrderId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户界面 我的借书
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult MyList(BookInfoVM vm, int pn = 1)
        {
            int totalCount,
               pageIndex = pn,
               pageSize = PagingConfig.PAGE_SIZE;

            var blist = _bookOrderService.GetManagerListByUser(Loginer.AccountId, vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, vm.QueryBorrowStatus, pageIndex, pageSize, out totalCount);
            var ublist = new List<UserBookInfoVM>();
            if (blist != null && blist.Count > 0)
            {
                foreach (var item in blist)
                {
                    string borrowStatus = "";
                    switch (item.BorrowStatus)
                    {
                        case (int)EnumHelp.BorrowStatus.借书中:
                            if ((item.BackTime - System.DateTime.Now).Days > 0)
                            {
                                borrowStatus = "借书中";
                            }
                            else
                            {
                                borrowStatus = "已欠费" + (System.DateTime.Now - item.BackTime).Days * 0.2m + "元";
                            }
                            break;
                        case (int)EnumHelp.BorrowStatus.审核中:
                            borrowStatus = "审核中";
                            break;
                        case (int)EnumHelp.BorrowStatus.审核通过:
                            if ((item.BackTime - System.DateTime.Now).Days > 0)
                            {
                                borrowStatus = "审核通过";
                            }
                            else
                            {
                                borrowStatus = "已欠费" + (System.DateTime.Now - item.BackTime).Days * 0.2m + "元";
                            }
                            break;
                        case (int)EnumHelp.BorrowStatus.审核驳回:
                            borrowStatus = "借书驳回";
                            break;
                        case (int)EnumHelp.BorrowStatus.已还书:
                            borrowStatus = "已还书";
                            break;
                        case (int)EnumHelp.BorrowStatus.库存不足:
                            borrowStatus = "库存不足";
                            break;
                        case (int)EnumHelp.BorrowStatus.续借:
                            if ((item.BackTime - System.DateTime.Now).Days > 0)
                            {
                                borrowStatus = "续借中";
                            }
                            else
                            {
                                borrowStatus = "续借过期，欠费" + (System.DateTime.Now - item.BackTime).Days * 0.2m + "元";
                            }
                            break;
                        case (int)EnumHelp.BorrowStatus.逾期欠费:
                            if (item.Ticket > 0.0m)
                            {
                                borrowStatus = "已还欠费" + item.Ticket + "元";
                            }
                            else
                            {
                                borrowStatus = "已欠费" + (System.DateTime.Now - item.BackTime).Days * 0.2m + "元";
                            }
                            break;
                        case (int)EnumHelp.BorrowStatus.取消:
                            borrowStatus = "已取消";
                            break;
                        default:
                            break;
                    }
                    ublist.Add(new UserBookInfoVM()
                    {
                        Author = item.BookInfo.Author,
                        BookOrderId = item.BookOrderId,
                        Count = item.Count,
                        Available = item.BookInfo.Available,
                        BookInfoId = item.BookInfoId,
                        Category = item.BookInfo.Category,
                        CreateTime = item.CreateTime,
                        EditTime = item.EditTime,
                        Isbn = item.BookInfo.Isbn,
                        IsDelete = item.IsDelete,
                        PublicDate = item.BookInfo.PublicDate,
                        Status = item.Status,
                        Title = item.BookInfo.Title,
                        UniversityName = item.BookInfo.University.Name,
                        IsBorrow = false,
                        ExpressId = item.ExpressId,
                        IsReceived = item.IsReceived,
                        TrafficFee = item.Express == null ? 0 : item.Express.TrafficFee,
                        ExpressName = item.Express == null ? "" : item.Express.ExpressName,
                        ExpressNo = item.Express == null ? "" : item.Express.ExpressNo,
                        AccountUniversityId = item.SysAccount.UniversityId,
                        TrafficType = item.TrafficType,
                        BorrowStatus = borrowStatus,
                        BookStatus = item.BorrowStatus,
                        UniversityId = item.BookInfo.University.UniversityId
                    });
                }
            }
            var paging = new Paging<UserBookInfoVM>()
            {
                Items = ublist,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };

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
            int borrowCount = _bookOrderService.GetMyBorrowCount(Loginer.AccountId);
            int availiable = (LibrarySystem.Common.CommonPara.MAX_BORROW_COUNT - borrowCount) <= 0 ? 0 : LibrarySystem.Common.CommonPara.MAX_BORROW_COUNT - borrowCount;
            vm.UinversityList = ulist;
            vm.UserPaging = paging;
            vm.BorrowCount = borrowCount;
            vm.AvaliableCount = availiable;
            return View(vm);
        }
    }
}