using Exam.Common;
using LibrarySystem.Admin.Models;
using LibrarySystem.Common;
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

namespace LibrarySystem.Controllers
{
    public class AsideBookOrderController : BaseController
    {
        /// <summary>
        /// 声明AsideBookOrderService接口服务对象
        /// </summary>
        private readonly IAsideBookOrderService _asideBookOrderService;
        /// <summary>
        /// 声明UniversityService接口服务对象
        /// </summary>
        private readonly IUniversityService _universityService;
        /// <summary>
        ///  声明ExpressService接口服务对象
        /// </summary>
        private readonly IExpressService _expressService;
        /// <summary>
        /// 声明AsideBookInfoService接口服务对象
        /// </summary>
        private readonly IAsideBookInfoService _asideBookInfoService;

        /// <summary>
        /// 声明ISysAccountService接口服务对象
        /// </summary>
        private readonly ISysAccountService _sysAccountService;

        /// <summary>
        /// 构造函数进行对象初始化
        /// </summary>
        /// <param name="asideBookOrderService"></param>
        /// <param name="universityService"></param>
        public AsideBookOrderController(IAsideBookOrderService asideBookOrderService, IUniversityService universityService, IExpressService expressService, IAsideBookInfoService asideBookInfoService, ISysAccountService sysAccountService)
        {
            this._asideBookOrderService = asideBookOrderService;
            this._universityService = universityService;
            this._expressService = expressService;
            this._asideBookInfoService = asideBookInfoService;
            this._sysAccountService = sysAccountService;
        }


        #region 用户操作界面
        /// <summary>
        /// 添加漂流订单、修改订单(下单人地址)操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult PreBook(AsideBookOrderModel model)
        {
            //1.0 更新订单运输信息
            var sysAccount = _sysAccountService.GetById(model.SysAccountId);
            var asideBookInfo = _asideBookInfoService.GetById(model.AsideBookInfoId);
            if (sysAccount != null)
            {
                if (asideBookInfo != null && sysAccount.UniversityId != asideBookInfo.UniversityId)
                {
                    sysAccount.Email = model.Email;
                    sysAccount.MobilePhone = model.MobilePhone;
                    sysAccount.EditTime = DateTime.Now;
                }
            }
            if (model.AsideBookOrderId > 0)
            {
                var asideBookOrder = _asideBookOrderService.GetById(model.AsideBookOrderId);
                asideBookOrder.EditTime = DateTime.Now;
                if (model.TrafficType == (int)EnumHelp.TrafficType.顺路送书)
                {
                    asideBookOrder.OrderStatus = (int)EnumHelp.BookOrderStatus.待顺风送;
                    asideBookOrder.TrafficType = model.TrafficType;
                    asideBookOrder.TrafficFee = model.TrafficFee;
                    sysAccount.Address = model.Address;
                }
                else if (model.TrafficType == (int)EnumHelp.TrafficType.邮寄)
                {
                    asideBookOrder.OrderStatus = (int)EnumHelp.BookOrderStatus.待邮寄;
                    sysAccount.Address = model.Address;
                    asideBookOrder.TrafficType = model.TrafficType;
                    asideBookOrder.TrafficFee = 0;
                }
                else if (model.TrafficType == (int)EnumHelp.TrafficType.自取)
                {
                    asideBookOrder.OrderStatus = (int)EnumHelp.BookOrderStatus.待自取;
                    asideBookOrder.TrafficType = model.TrafficType;
                }

                try
                {
                    _sysAccountService.Update(sysAccount);
                    _asideBookOrderService.Update(asideBookOrder);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            else//2.0 插入新的记录
            {
                int accountId = LibrarySystem.Admin.Common.Loginer.AccountId;

                var asideBookOrdered = _asideBookOrderService.GetByOrdered(accountId, model.AsideBookInfoId);
                if (asideBookInfo.Available >= 1 && asideBookOrdered == null)
                {
                    //添加
                    var Order = new AsideBookOrder()
                    {
                        SysAccountId = accountId,
                        ExpressId = 0,
                        TrafficAccountId = 0,
                        Count = 1,
                        Status = (int)EnumHelp.EnabledEnum.有效,
                        IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                        CreateTime = DateTime.Now,
                        EditTime = DateTime.Now,
                        TrafficType = model.TrafficType,
                        AsideBookInfoId = model.AsideBookInfoId,
                        Remark = model.Remark,
                        TrafficFee = model.TrafficFee//补偿费用
                    };
                    if (model.TrafficType == (int)EnumHelp.TrafficType.自取 || asideBookInfo.UniversityId == LibrarySystem.Admin.Common.Loginer.UniversityId)
                    {
                        Order.OrderStatus = (int)EnumHelp.BookOrderStatus.待自取;
                        Order.TrafficType = (int)EnumHelp.TrafficType.自取;
                    }
                    else if (model.TrafficType == (int)EnumHelp.TrafficType.邮寄)
                    {
                        Order.OrderStatus = (int)EnumHelp.BookOrderStatus.待邮寄;
                        sysAccount.Address = model.Address;
                    }
                    else if (model.TrafficType == (int)EnumHelp.TrafficType.顺路送书)
                    {
                        Order.OrderStatus = (int)EnumHelp.BookOrderStatus.待顺风送;
                        sysAccount.Address = model.Address;
                    }
                    try
                    {
                        var asideBookOrder = _asideBookOrderService.Insert(Order);
                        if (asideBookOrder.AsideBookOrderId > 0)
                        {
                            asideBookInfo.Available -= 1;
                            _asideBookInfoService.Update(asideBookInfo);
                            _sysAccountService.Update(sysAccount);
                            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    catch (Exception ex)
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (asideBookOrdered != null)
                {
                    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 顺路送书界面
        /// </summary>
        /// <param name="AsideBookOrderVM"></param>
        /// <returns></returns>
        public ActionResult MyTransRecord(AsideBookOrderVM vm, int pn = 1)
        {
            //1.0 页面初始化
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //2.0 条件查询   目的地（求书人所在学校） 是否有偿 是否已接单 图书isbn
            var blist = _asideBookOrderService.GetTransList(vm.QueryUId, vm.QueryPayType, vm.QueryIsMyRecord, LibrarySystem.Admin.Common.Loginer.AccountId, LibrarySystem.Admin.Common.Loginer.UniversityId, vm.QueryIsbn, pageIndex, pageSize, out totalCount);
            var trafficList = _asideBookOrderService.getSuccessfulTrasList(LibrarySystem.Admin.Common.Loginer.AccountId);
            //成功顺路运书次数
            if (trafficList != null && trafficList.Count > 0)
            {
                vm.TrafficCount = trafficList.Count;
                vm.TotalTrafficFee = trafficList.Sum(c => c.TrafficFee);
            }
            else
            {
                vm.TrafficCount = 0;
                vm.TotalTrafficFee = 0;
            }
            var paging = new Paging<AsideBookOrder>()
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
            //3.0 返回查询结果
            vm.UinversityList = ulist;
            vm.Paging = paging;
            return View(vm);
        }

        /// <summary>
        /// 用户漂入订单
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult MyGetAsideBook(AsideBookOrderVM vm, int pn = 1)
        {
            //1.0 页面初始化
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //2.0 条件查询
            var blist = _asideBookOrderService.GetAccountManagerList(LibrarySystem.Admin.Common.Loginer.AccountId, vm.QueryTrafType, vm.QueryOrderStatus, vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, pageIndex, pageSize, out totalCount);
            var myGetList = _asideBookOrderService.GetMyList(LibrarySystem.Admin.Common.Loginer.AccountId);
            //成功顺路运书次数
            if (myGetList != null && myGetList.Count > 0)
            {
                vm.GetBookCount = myGetList.Count;
            }
            else
            {
                vm.GetBookCount = 0;
            }
            var paging = new Paging<AsideBookOrder>()
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
            //3.0 返回查询结果
            vm.UinversityList = ulist;
            vm.Paging = paging;
            return View(vm);
        }

        /// <summary>
        /// 修改运书方式  修改个人信息界面
        /// </summary>
        /// <param name="AsideBookOrderVM"></param>
        /// <returns></returns>
        public ActionResult EditTransInfo(AsideBookOrderVM AsideBookOrderVM)
        {
            //1.0 根据订单id获取订单详情
            var asidebookorder = _asideBookOrderService.GetById(AsideBookOrderVM.AsideBookOrderId);
            var sender = _sysAccountService.GetById(AsideBookOrderVM.TrafficAccountId);
            var requester = _sysAccountService.GetById(AsideBookOrderVM.SysAccountId);
            if (asidebookorder != null)
            {
                AsideBookOrderVM.Account = asidebookorder.SysAccount.Account;
                AsideBookOrderVM.TrafficType = asidebookorder.TrafficType;
                AsideBookOrderVM.TrafficFee = asidebookorder.TrafficFee;//补偿金额
                if (AsideBookOrderVM.TrafficAccountId > 0 && sender != null)//操作人为送书人
                {
                    AsideBookOrderVM.Address = asidebookorder.SysAccount != null ? asidebookorder.SysAccount.Address : "";
                    AsideBookOrderVM.Email = sender.Email;
                    AsideBookOrderVM.SenderAddress = sender.Address;
                    AsideBookOrderVM.MobilePhone = sender.MobilePhone;
                }
                else if (AsideBookOrderVM.SysAccountId > 0 && requester != null)//操作人为收书人
                {
                    AsideBookOrderVM.Email = requester.Email;
                    AsideBookOrderVM.Address = requester.Address;
                    AsideBookOrderVM.MobilePhone = requester.MobilePhone;

                }
                //AsideBookOrderVM.CustomerInfo = asidebookorder.CustomerInfo;
                AsideBookOrderVM.SenderInfo = asidebookorder.SenderInfo;
                //2.0 对订单中的快递id进行判断
                //if (AsideBookOrderVM.SysAccountId > 0 && AsideBookOrderVM.SysAccountId == LibrarySystem.Admin.Common.Loginer.AccountId)//操作人为收书人
                //{

                //}
                //if (AsideBookOrderVM.TrafficAccountId > 0 && AsideBookOrderVM.TrafficAccountId == asidebookorder.TrafficAccountId)//操作人为运输人
                //{

                //}
            }
            //3.0 返回结果
            AsideBookOrderVM.Paging = new Paging<AsideBookOrder>();
            AsideBookOrderVM.UinversityList = new List<University>();
            return View(AsideBookOrderVM);
        }

        /// <summary>
        /// 修改运书方式  修改个人信息界面
        /// </summary>
        /// <param name="AsideBookOrderVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditTransInfo(AsideBookOrderModel model)
        {
            //1.0 根据订单id获取订单详情
            var asidebookorder = _asideBookOrderService.GetById(model.AsideBookOrderId);
            var sysAccount = model.SysAccountId > 0 ? _sysAccountService.GetById(model.SysAccountId) : _sysAccountService.GetById(model.TrafficAccountId);
            if (sysAccount != null)
            {
                sysAccount.Email = model.Email;
                sysAccount.MobilePhone = model.MobilePhone;
                sysAccount.EditTime = DateTime.Now;
                sysAccount.Address = model.SysAccountId > 0 ? model.Address : model.SenderAddress;
            }
            if (asidebookorder != null)
            {
                asidebookorder.EditTime = DateTime.Now;
                //2.0 对订单中的快递id进行判断  //操作人为收书人
                if (model.SysAccountId > 0 && (model.SysAccountId == LibrarySystem.Admin.Common.Loginer.AccountId || model.SysAccountId == asidebookorder.SysAccountId))
                {
                    asidebookorder.TrafficType = model.TrafficType;
                    asidebookorder.TrafficFee = model.TrafficFee;//修改补偿金额

                    if (model.TrafficType == (int)EnumHelp.TrafficType.自取 || model.TrafficType == (int)EnumHelp.TrafficType.邮寄)
                    {
                        asidebookorder.SenderInfo = "";
                        asidebookorder.TrafficAccountId = 0;
                    }
                }//操作人为运输人
                if (model.TrafficAccountId > 0 && (model.TrafficAccountId == asidebookorder.TrafficAccountId || model.TrafficAccountId == LibrarySystem.Admin.Common.Loginer.AccountId))
                {
                    asidebookorder.TrafficAccountId = model.TrafficAccountId;
                    asidebookorder.SenderInfo = "账号：" + sysAccount.Account + ";联系方式：" + sysAccount.MobilePhone + ";地址：" + model.SenderAddress;
                }
                try
                {
                    _asideBookOrderService.Update(asidebookorder);
                    _sysAccountService.Update(sysAccount);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int AsideBookInfoId = 0, int Status = 0)
        {
            if (AsideBookInfoId <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                var asideBookOrder = _asideBookOrderService.GetById(AsideBookInfoId);
                asideBookOrder.Status = Status;
                _asideBookOrderService.Update(asideBookOrder);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 确认领取  取消订单
        /// </summary>
        /// <param name="AsideBookOrderId">订单Id</param>
        /// <param name="OrderStatus">订单状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateOrderStatus(int AsideBookOrderId = 0, int OrderStatus = 0)
        {
            //1.0 参数验证
            if (AsideBookOrderId <= 0 || OrderStatus <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                //2.0 获取订单数据 更新订单状态
                var asideBookOrder = _asideBookOrderService.GetById(AsideBookOrderId);
                if (OrderStatus == (int)EnumHelp.BookOrderStatus.待顺风送)
                {
                    if (asideBookOrder.TrafficAccountId > 0)
                    {
                        asideBookOrder.TrafficAccountId = 0;
                        asideBookOrder.SenderInfo = "";
                    }
                }
                else if (OrderStatus == (int)EnumHelp.BookOrderStatus.待邮寄)
                {
                    asideBookOrder.ExpressId = 0;
                }

                asideBookOrder.OrderStatus = OrderStatus;
                asideBookOrder.EditTime = DateTime.Now;
                _asideBookOrderService.Update(asideBookOrder);

                int Type = 0;
                string subject = string.Empty;
                string temp = string.Empty;
                string body = string.Empty;
                if (OrderStatus == (int)EnumHelp.BookOrderStatus.已完结)
                {
                    if (asideBookOrder.SysAccountId == LibrarySystem.Admin.Common.Loginer.AccountId|| asideBookOrder.SysAccount.UniversityId== LibrarySystem.Admin.Common.Loginer.UniversityId)//本人为收书人
                    {
                        //判断是否为求书???
                        Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统图书漂入服务;
                        subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统图书漂入服务.ToString();
                        temp = EmailTemplate.EmailDictionary[Type];
                        body = temp.Replace("#Title#", asideBookOrder.AsideBookInfo.Title);
                        SendEmailHelp.SendMail(subject, body, asideBookOrder.SysAccount.Email, "", "", "");
                    }
                    else if (asideBookOrder.TrafficAccountId == LibrarySystem.Admin.Common.Loginer.AccountId)//本人为运书人
                    {
                        Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统图书送书服务;
                        subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统图书送书服务.ToString();
                        temp = EmailTemplate.EmailDictionary[Type];
                        body = temp.Replace("#Title#", asideBookOrder.AsideBookInfo.Title);
                        SendEmailHelp.SendMail(subject, body, asideBookOrder.SysAccount.Email, "", "", "");
                    }
                }

                //3.0 如果是取消订单 则需要将图书库存加1   因为在用户点击“漂入”时，库存已经-1
                if (OrderStatus == (int)EnumHelp.BookOrderStatus.已取消)
                {
                    if (asideBookOrder.TrafficType != (int)EnumHelp.TrafficType.顺路送书)
                    {
                        var asideBook = _asideBookInfoService.GetById(asideBookOrder.AsideBookInfoId);
                        asideBook.Available += 1;
                        asideBook.EditTime = DateTime.Now;
                        _asideBookInfoService.Update(asideBook);
                    }
                }
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(AsideBookOrderVM vm, int pn = 1)
        {
            //1.0 页面初始化
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //2.0 条件查询
            var blist = _asideBookOrderService.GetManagerList(LibrarySystem.Admin.Common.Loginer.UniversityId, vm.QuerySysAccount, vm.QueryTrafType, vm.QueryOrderStatus, vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, pageIndex, pageSize, out totalCount);
            var paging = new Paging<AsideBookOrder>()
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
            //3.0 返回查询结果
            vm.UinversityList = ulist;
            vm.Paging = paging;
            return View(vm);
        }


        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="AsideBookOrderVM"></param>
        /// <returns></returns>
        public ActionResult Edit(AsideBookOrderVM AsideBookOrderVM)
        {
            var asidebookorder = _asideBookOrderService.GetById(AsideBookOrderVM.AsideBookOrderId);
            if (asidebookorder != null)
            {
                //AsideBookOrderVM.Author = asidebookinfo.Author;
                // AsideBookOrderVM.Category = asidebookinfo.Category;
                //AsideBookInfoVM.Count = bookinfo.Count;
                AsideBookOrderVM.Isbn = asidebookorder.AsideBookInfo.Isbn;
                AsideBookOrderVM.Title = asidebookorder.AsideBookInfo.Title;
                AsideBookOrderVM.Paging = new Paging<AsideBookOrder>();
                AsideBookOrderVM.UniversityId = asidebookorder.AsideBookInfo.UniversityId;
                AsideBookOrderVM.UniversityName = asidebookorder.AsideBookInfo.University.Name;
                //AsideBookOrderVM.Available = asidebookinfo.Available;
                AsideBookOrderVM.QueryUId = 0;
                AsideBookOrderVM.QueryName = "";
                AsideBookOrderVM.QueryIsbn = "";
                AsideBookOrderVM.QueryCategory = "";
                AsideBookOrderVM.UinversityList = new List<University>();
            }
            else
            {
                AsideBookOrderVM.Paging = new Paging<AsideBookOrder>();
                AsideBookOrderVM.QueryUId = 0;
                AsideBookOrderVM.QueryName = "";
                AsideBookOrderVM.QueryIsbn = "";
                AsideBookOrderVM.QueryCategory = "";
                AsideBookOrderVM.UinversityList = new List<University>();
                //AsideBookOrderVM.Author = "";
                //AsideBookOrderVM.Category = "";
                //AsideBookOrderVM.PublicDate = "";
                AsideBookOrderVM.Isbn = "";
                AsideBookOrderVM.Title = "";
                AsideBookOrderVM.UniversityId = 0;
                AsideBookOrderVM.UniversityName = "";
                //AsideBookOrderVM.Available = 0;
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

            return View(AsideBookOrderVM);
        }

        /// <summary>
        /// 添加、修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(AsideBookOrder model)
        {
            //更新
            if (model.AsideBookOrderId > 0)
            {
                var asideBookOrder = _asideBookOrderService.GetById(model.AsideBookInfoId);
                asideBookOrder.EditTime = DateTime.Now;
                // asideBookInfo.Title = model.Title;
                //asideBookInfo.Author = model.Author;
                //asideBookInfo.Category = model.Category;
                // asideBookInfo.Available = model.Available;
                //asideBookInfo.PublicDate = model.PublicDate;
                try
                {
                    _asideBookOrderService.Update(asideBookOrder);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }//插入
            else
            {
                //var asideBook = _asideBookOrderService.GetByIsbn(model.Isbn);
                //if (asideBook != null)
                //   return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                //添加
                model.Status = (int)EnumHelp.EnabledEnum.有效;
                model.IsDelete = (int)EnumHelp.IsDeleteEnum.有效;
                model.CreateTime = DateTime.Now;
                model.EditTime = DateTime.Now;
                var asideBookOrder = _asideBookOrderService.Insert(model);
                return Json(new { Status = asideBookOrder.AsideBookOrderId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
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
                var asideBookOrder = _asideBookOrderService.GetById(id);
                asideBookOrder.IsDelete = (int)EnumHelp.IsDeleteEnum.已删除;
                _asideBookOrderService.Update(asideBookOrder);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 订单邮编数据填写
        /// </summary>
        /// <param name="AsideBookOrderVM"></param>
        /// <returns></returns>
        public ActionResult BookEmail(AsideBookOrderVM AsideBookOrderVM)
        {
            //1.0 根据订单id获取订单详情
            var asidebookorder = _asideBookOrderService.GetById(AsideBookOrderVM.AsideBookOrderId);
            if (asidebookorder != null)
            {
                //2.0 对订单中的快递id进行判断
                if (asidebookorder.ExpressId != 0)
                {
                    //2.1 进行订单数据初始化
                    var express = _expressService.GetById(asidebookorder.ExpressId);
                    AsideBookOrderVM.ExpressId = express.ExpressId;
                    AsideBookOrderVM.ExpressName = express.ExpressName;
                    AsideBookOrderVM.ExpressNo = express.ExpressNo;
                    AsideBookOrderVM.TrafficFee = express.TrafficFee;
                }
                else
                {
                    //3.0 进行订单数据初始化
                    AsideBookOrderVM.ExpressId = 0;
                    AsideBookOrderVM.ExpressName = "";
                    AsideBookOrderVM.ExpressNo = "";
                    AsideBookOrderVM.TrafficFee = 0;
                }
                AsideBookOrderVM.Address = asidebookorder.SysAccount.Address;
                AsideBookOrderVM.Email = asidebookorder.SysAccount.Email;
                AsideBookOrderVM.MobilePhone = asidebookorder.SysAccount.MobilePhone;
                AsideBookOrderVM.Isbn = asidebookorder.AsideBookInfo.Isbn;
                AsideBookOrderVM.BaseImageId = asidebookorder.AsideBookInfo.BaseImageId;
                AsideBookOrderVM.BaseImage = asidebookorder.AsideBookInfo.BaseImage == null ? new BaseImage() : asidebookorder.AsideBookInfo.BaseImage;
                AsideBookOrderVM.Account = asidebookorder.SysAccount.Account;
                AsideBookOrderVM.Title = asidebookorder.AsideBookInfo.Title;
            }
            else
            {
                AsideBookOrderVM.ExpressId = 0;
                AsideBookOrderVM.ExpressName = "";
                AsideBookOrderVM.ExpressNo = "";
                AsideBookOrderVM.TrafficFee = 0;
            }
            //4.0 返回结果
            AsideBookOrderVM.Paging = new Paging<AsideBookOrder>();
            AsideBookOrderVM.UinversityList = new List<University>();
            return View(AsideBookOrderVM);
        }

        /// <summary>
        /// 订单邮编信息提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult BookEmail(ExpressVM model)
        {
            //1.0 判断id有效
            if (model.AsideBookOrderId > 0)
            {
                if (model.ExpressId > 0)
                {
                    var express = _expressService.GetById(model.ExpressId);
                    express.ExpressName = model.ExpressName;
                    express.ExpressNo = model.ExpressNo;
                    express.TrafficFee = model.TrafficFee;
                    try
                    {
                        _expressService.Update(express);

                     
                        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    try
                    {
                        //2.0 插入邮编信息
                        var expr = new Express()
                        {
                            ExpressId = 0,
                            ExpressName = model.ExpressName,
                            ExpressNo = model.ExpressNo,
                            TrafficFee = model.TrafficFee
                        };
                        var express = _expressService.Insert(expr);
                        //3.0 更新订单状态 邮编id
                        if (express.ExpressId > 0)
                        {
                            var asideBookOrder = _asideBookOrderService.GetById(model.AsideBookOrderId);
                            //asideBookOrder.TrafficFee = model.TrafficFee;
                            asideBookOrder.EditTime = DateTime.Now;
                            asideBookOrder.ExpressId = express.ExpressId;
                            asideBookOrder.OrderStatus = (int)EnumHelp.BookOrderStatus.运输中;
                            _asideBookOrderService.Update(asideBookOrder);

                            int Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统图书寄送服务;
                            string subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统图书寄送服务.ToString();
                            string temp = EmailTemplate.EmailDictionary[Type];
                            string body = temp.Replace("#Title#", asideBookOrder.AsideBookInfo.Title).Replace("#ExpressNo#", express.ExpressNo).Replace("#ExpressName#", express.ExpressName).Replace("#TrafficFee#", express.TrafficFee.ToString());
                            SendEmailHelp.SendMail(subject, body, asideBookOrder.SysAccount.Email, "", "", "");

                            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }
    }
}