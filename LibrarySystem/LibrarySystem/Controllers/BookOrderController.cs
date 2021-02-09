using Exam.Common;
using LibrarySystem.Admin.Common;
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
    public class BookOrderController : BaseController
    {
        private readonly IBookOrderService _bookOrderService;
        private readonly IUniversityService _universityService;
        private readonly IBookInfoService _bookInfoService;
        private readonly ISysAccountService _sysAccountService;
        private readonly IExpressService _expressService;
        private readonly IViewConfigService _viewConfigService;
        public BookOrderController(IBookOrderService bookOrderService, IUniversityService universityService, IBookInfoService bookInfoService, ISysAccountService sysAccountService, IExpressService expressService, IViewConfigService viewConfigService)
        {
            _bookOrderService = bookOrderService;
            _universityService = universityService;
            _bookInfoService = bookInfoService;
            _sysAccountService = sysAccountService;
            _expressService = expressService;
            _viewConfigService = viewConfigService;
        }

        /// <summary>
        /// 借书信息列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(BookOrderVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var blist = _bookOrderService.GetManagerList(LibrarySystem.Admin.Common.Loginer.UniversityId, vm.QueryIsbn, vm.QuerySUId, vm.QueryAccountName, vm.QueryBorrowStatus, vm.QueryUId, vm.QueryName, pageIndex, pageSize, out totalCount);

            var paging = new Paging<BookOrder>()
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
            return View(vm);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="BookInfoVM"></param>
        /// <returns></returns>
        public ActionResult Edit(BookOrderVM BookOrderVM)
        {
            BookOrderVM.Account = "";
            BookOrderVM.BookOrderId = 0;
            BookOrderVM.BorrowStatus = 0;
            BookOrderVM.Count = 0;
            BookOrderVM.Isbn = "";
            BookOrderVM.Paging = new Paging<BookOrder>();
            BookOrderVM.QueryAccountName = "";
            BookOrderVM.QueryBorrowStatus = 0;
            BookOrderVM.QueryName = "";
            BookOrderVM.QueryUId = 0;
            BookOrderVM.UniversityId = 0;
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
            return View(BookOrderVM);
        }

        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(BookOrderPostVM model)
        {
            ////更新
            //if (model.BookInfoId > 0)
            //{
            //    var bookInfo = _bookInfoService.GetById(model.BookInfoId);
            //    bookInfo.EditTime = DateTime.Now;
            //    bookInfo.Title = model.Title;
            //    bookInfo.Author = model.Author;
            //    bookInfo.Category = model.Category;
            //    bookInfo.Available = model.Available;
            //    bookInfo.PublicDate = model.PublicDate;
            //    try
            //    {
            //        _bookInfoService.Update(bookInfo);
            //        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            //    }
            //    catch (Exception ex)
            //    {
            //        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            //    }
            //}//插入
            //else
            //{
            //添加
            var bookInfo = _bookInfoService.GetByUniversityIsbn(model.UniversityId, model.Isbn);
            var account = _sysAccountService.GetAccountByAccount(model.Account);
            if (account != null && account.SysAccountId > 0 && bookInfo != null && bookInfo.BookInfoId > 0)
            {
                var bookOrder = _bookOrderService.GetByBookIdAccountId(bookInfo.BookInfoId, account.SysAccountId);
                if (bookOrder != null && bookOrder.BookOrderId > 0)
                    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                if (bookInfo.Available < model.Count)
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                var date = System.DateTime.Now;
                var br = new BookOrder()
                {
                    BookInfoId = bookInfo.BookInfoId,
                    BorrowStatus = (int)EnumHelp.BorrowStatus.借书中,
                    Count = model.Count,
                    CreateTime = date,
                    EditTime = date,
                    BackTime = date.AddDays(50),
                    IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                    Status = (int)EnumHelp.EnabledEnum.有效,
                    EditPersonId = Loginer.AccountId,
                    Ticket = 0,
                    SysAccountId = account.SysAccountId,
                };
                var result = _bookOrderService.Insert(br);
                return Json(new { Status = result.BookOrderId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 借书管理操作
        /// </summary>
        /// <param name="borrowStatusModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BorrowUpdate(BorrowStatusModel borrowStatusModel)
        {
            if (borrowStatusModel.BookOrderId <= 0 || borrowStatusModel.BorrowStatus <= 0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            var bookOrder = _bookOrderService.GetById(borrowStatusModel.BookOrderId);
            double d = (bookOrder.BackTime - System.DateTime.Now).TotalDays;
            int dd = (bookOrder.BackTime - System.DateTime.Now).Days;
            if (bookOrder != null)
            {
                var subject = string.Empty;
                var body = string.Empty;
                int Type = 0;
                var bookInfo = _bookInfoService.GetById(bookOrder.BookInfoId);
                if (bookInfo != null && bookInfo.BookInfoId > 0)
                {
                    switch (borrowStatusModel.BorrowStatus)
                    {
                        case (int)EnumHelp.BorrowStatus.逾期欠费://缴费并还书
                            if ((bookOrder.BackTime - System.DateTime.Now).Days > 0)
                            {
                                bookOrder.Ticket = (bookOrder.BackTime - System.DateTime.Now).Days * LibrarySystem.Common.CommonPara.TICKET;
                                bookOrder.UniversityId = LibrarySystem.Admin.Common.Loginer.UniversityId;
                                bookOrder.BorrowStatus = (int)EnumHelp.BorrowStatus.已还书;
                                bookInfo.Available += bookOrder.Count;
                                //主题
                                Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统还书服务;
                                subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统还书服务.ToString();
                                string temp = EmailTemplate.EmailDictionary[Type];
                                body = temp.Replace("#Title#", bookOrder.BookInfo.Title);
                            }
                            break;
                        case (int)EnumHelp.BorrowStatus.续借://续借  续借
                            if (bookOrder.BackTime > System.DateTime.Now && bookOrder.BorrowStatus == (int)EnumHelp.BorrowStatus.借书中)
                            {
                                bookOrder.BackTime = bookOrder.BackTime.AddDays(LibrarySystem.Common.CommonPara.MAX_BORROW_COUNT);
                                bookOrder.BorrowStatus = (int)EnumHelp.BorrowStatus.续借;
                                //主题
                                Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统续借服务;
                                subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统续借服务.ToString();
                                string temp = EmailTemplate.EmailDictionary[Type];
                                body = temp.Replace("#Title#", bookOrder.BookInfo.Title).Replace("#BackTime#", bookOrder.BackTime.ToString("yyyy-MM-dd"));
                            }
                            break;
                        case (int)EnumHelp.BorrowStatus.已还书://已还书  还书
                            if (bookOrder.BackTime >= System.DateTime.Now)
                            {
                                bookOrder.UniversityId = LibrarySystem.Admin.Common.Loginer.UniversityId;
                                bookOrder.BorrowStatus = (int)EnumHelp.BorrowStatus.已还书;
                                bookInfo.Available += bookOrder.Count;
                                //主题
                                Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统还书服务;
                                subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统还书服务.ToString();
                                string temp = EmailTemplate.EmailDictionary[Type];
                                body = temp.Replace("#Title#", bookOrder.BookInfo.Title);
                            }
                            else
                                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                            break;
                        case (int)EnumHelp.BorrowStatus.审核通过://审核通过 通过
                            if (bookOrder.Count <= bookOrder.BookInfo.Available)
                            {
                                var university = _universityService.GetById(bookOrder.BookInfo.UniversityId);
                                int totalCount = 0;
                                var v = _viewConfigService.GetManagerList("", 1, 1, out totalCount).FirstOrDefault();
                                int result = ViewHelp.GetCountByUinversityId(v, university, bookOrder.BookInfo.Isbn);
                                if (result < bookOrder.Count)
                                {
                                    bookOrder.BorrowStatus = (int)EnumHelp.BorrowStatus.审核驳回;
                                    bookOrder.ExpressId = 0;
                                    //更新订单状态
                                    _bookOrderService.Update(bookOrder);
                                    //库存不足 审核驳回
                                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                                }
                                //var viewConfig = _viewConfigService.GetManagerList("", 1, 1, out totalCount).FirstOrDefault();
                                ////根据视图对象、图书所属大学id、图书isbn获取高校数据源图书库存
                                //int StoreCount = ViewHelp.GetCountByUinversityId(viewConfig, university, bookOrder.BookInfo.Isbn);
                                ////判断库存是否与平台库存一致
                                //if (StoreCount < bookOrder.BookInfo.Count)
                                //{
                                //    //获取所借图书对象
                                //    var bookinfo = _bookInfoService.GetByUniversityIsbn(bookOrder.BookInfo.UniversityId, bookOrder.BookInfo.Isbn);
                                //    //更新库存
                                //    bookinfo.Count = StoreCount;
                                //    //当数据源库存小于平台图书库存时审核驳回
                                //    bookOrder.BorrowStatus = (int)EnumHelp.BorrowStatus.审核驳回;
                                //    bookOrder.ExpressId = 0;
                                //    //更新订单状态 图书库存
                                //    _bookInfoService.Update(bookinfo);
                                //    _bookOrderService.Update(bookOrder);
                                //    //库存不足 审核驳回
                                //    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                                //}
                                else
                                {
                                    bookOrder.BorrowStatus = (int)EnumHelp.BorrowStatus.审核通过;
                                    bookInfo.Available -= bookOrder.Count;
                                }
                            }
                        
                            else
                                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                            break;
                        case (int)EnumHelp.BorrowStatus.审核驳回://审核驳回 驳回
                            bookOrder.BorrowStatus = (int)EnumHelp.BorrowStatus.审核驳回;
                            bookOrder.ExpressId = 0;
                            break;
                        case (int)EnumHelp.BorrowStatus.取消:
                            bookOrder.BorrowStatus = (int)EnumHelp.BorrowStatus.取消;
                            bookOrder.ExpressId = 0;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
                try
                {
                    _bookOrderService.Update(bookOrder);
                    if (borrowStatusModel.BorrowStatus == (int)EnumHelp.BorrowStatus.逾期欠费 || borrowStatusModel.BorrowStatus == (int)EnumHelp.BorrowStatus.已还书 || borrowStatusModel.BorrowStatus == (int)EnumHelp.BorrowStatus.审核通过)
                    {
                        _bookInfoService.Update(bookInfo);
                    }
                    if (Type != 0 && !string.IsNullOrWhiteSpace(subject) && !string.IsNullOrWhiteSpace(body))
                    {
                         SendEmailHelp.SendMail(subject, body, bookOrder.SysAccount.Email, "", "", "");
                    }
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 借书管理操作
        /// </summary>
        /// <param name="borrowStatusModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SendEmailForBack(int BookOrderId,int BorrowStatus=0)
        {
           var bookOrder=  _bookOrderService.GetById(BookOrderId);
            if(bookOrder ==null|| BorrowStatus<=0)
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                //获取邮件正文
                string body = EmailTemplate.EmailDictionary[(int)EmailTemplate.EmailTemplete.E建漂流图书服务系统图书催还服务];
                //验证码
                string code = VerificationCode.CreateValidateNumber(6);
                string sendString = body.Replace("#Title#", bookOrder.BookInfo.Title);
                string subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统图书催还服务.ToString();
                bool result = SendEmailHelp.SendMail(EmailTemplate.EmailTemplete.E建漂流图书服务系统图书催还服务.ToString(), sendString, bookOrder.SysAccount.Email, "", "", "");
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 收到图书确认
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ReceivedUpdate(BookOrderVM bookOrderVM)
        {
            try
            {
                var bookOrder = _bookOrderService.GetById(bookOrderVM.BookOrderId);
                //1.0 判断参数
                if (bookOrderVM.IsReceived > 0 && bookOrder != null)
                {
                    bookOrder.IsReceived = bookOrderVM.IsReceived;
                    bookOrder.EditTime = DateTime.Now;
                    var date = System.DateTime.Now;
                    //用户已收到 修改借书状态 归还图书时间
                    if (bookOrderVM.IsReceived == (int)EnumHelp.IsReceived.已收到)
                    {
                        bookOrder.BorrowStatus = (int)EnumHelp.BorrowStatus.借书中;
                        bookOrder.CreateTime = date;
                        bookOrder.EditTime = date;
                        bookOrder.BackTime = date.AddDays(CommonPara.BORROW_DAY);

                        //发送邮件

                        //获取邮件正文
                        string body = EmailTemplate.EmailDictionary[(int)EmailTemplate.EmailTemplete.E建漂流图书服务系统借书服务];
                        //验证码
                        string code = VerificationCode.CreateValidateNumber(6);
                        string sendString = body.Replace("#BackTime#", bookOrder.BackTime.ToString("yyyy-MM-dd")).Replace("#Title#", bookOrder.BookInfo.Title);
                        string subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统借书服务.ToString();
                        bool result = SendEmailHelp.SendMail(EmailTemplate.EmailTemplete.E建漂流图书服务系统借书服务.ToString(), sendString, bookOrder.SysAccount.Email, "", "", "");
                    }
                    _bookOrderService.Update(bookOrder);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑邮寄信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditExpress(int BookOrderId = 0)
        {
            var order = _bookOrderService.GetById(BookOrderId);
            var vm = new BookOrderVM();
            vm.BookOrderId = BookOrderId;
            if (BookOrderId > 0 && order != null)
            {
                vm.ExpressId = order.ExpressId;
                vm.ExpressName = order.Express == null ? "" : order.Express.ExpressName;
                vm.ExpressNo = order.Express == null ? "" : order.Express.ExpressNo;
                vm.TrafficFee = order.Express == null ? 0 : order.Express.TrafficFee;
                vm.MobilePhone = order.SysAccount.MobilePhone;
                vm.Email = order.SysAccount.Email;
                vm.Address = order.SysAccount.Address;
                vm.Isbn = order.BookInfo.Isbn;
                vm.Title = order.BookInfo.Isbn;
                vm.Account = order.SysAccount.Account;
            }
            else
            {

                vm.ExpressId = 0;
                vm.ExpressName = "";
                vm.ExpressNo = "";
                vm.TrafficFee = 0;
            }
            return View(vm);
        }

        /// <summary>
        /// 订单邮编信息提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditExpress(ExpressVM model)
        {
            //1.0 判断id有效
            if (model.BookOrderId > 0)
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
                            var BookOrder = _bookOrderService.GetById(model.BookOrderId);
                            //asideBookOrder.TrafficFee = model.TrafficFee;
                            BookOrder.EditTime = DateTime.Now;
                            BookOrder.ExpressId = express.ExpressId;
                            //asideBookOrder.OrderStatus = (int)EnumHelp.BookOrderStatus.运输中;
                            _bookOrderService.Update(BookOrder);

                            //判断是否为求书???
                           int  Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统图书寄送服务;
                           string subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统图书寄送服务.ToString();
                           string  temp = EmailTemplate.EmailDictionary[Type];
                            string body = temp.Replace("#Title#", BookOrder.BookInfo.Title).Replace("#ExpressName#", model.ExpressName).Replace("#ExpressNo#", model.ExpressNo).Replace("#TrafficFee#", model.TrafficFee.ToString());
                            SendEmailHelp.SendMail(subject, body, BookOrder.SysAccount.Email, "", "", "");

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