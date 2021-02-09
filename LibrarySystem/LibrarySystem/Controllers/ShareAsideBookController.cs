using Exam.Common;
using LibrarySystem.Admin.Models;
using LibrarySystem.Common;
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
    public class ShareAsideBookController : BaseController
    {
        /// <summary>
        /// 声明AsideBookOrderService接口服务对象
        /// </summary>
        private readonly IShareAsideBookService _shareAsideBookService;
        /// <summary>
        /// 声明UniversityService接口服务对象
        /// </summary>
        private readonly IUniversityService _universityService;
        /// <summary>
        /// 声明IAsideBookInfoService接口服务对象
        /// </summary>
        private readonly IAsideBookInfoService _asideBookInfoService;
        /// <summary>
        /// 声明IResearchAsideBookService接口服务对象
        /// </summary>
        private readonly IResearchAsideBookService _researchAsideBookService;
        /// <summary>
        /// 声明ISysAccountService接口服务对象
        /// </summary>
        private readonly ISysAccountService _SysAccountService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shareAsideBookService"></param>
        /// <param name="universityService"></param>
        public ShareAsideBookController(IShareAsideBookService shareAsideBookService, IUniversityService universityService, IAsideBookInfoService asideBookInfoService, IResearchAsideBookService researchAsideBookService, ISysAccountService SysAccountService)
        {
            _shareAsideBookService = shareAsideBookService;
            _universityService = universityService;
            _asideBookInfoService = asideBookInfoService;
            _researchAsideBookService = researchAsideBookService;
            _SysAccountService = SysAccountService;
        }



        #region 用户操作界面
        /// <summary>
        /// 用户分享的图书列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult MyShareList(ShareAsideBookVM vm, int pn = 1)
        {
            //1.0 页面初始化
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //2.0 条件查询
            //if (vm.QueryUId == 0)//当前账户、当前管理员只能看到本校或本人的图书
            //{
            //    vm.QueryUId = LibrarySystem.Admin.Common.Loginer.UniversityId;
            //}

            var blist = _shareAsideBookService.GetManagerList(LibrarySystem.Admin.Common.Loginer.AccountId, vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, vm.QuerySysAccount, vm.QueryShareStatus, vm.QueryPayType, vm.QueryTrafficType, pageIndex, pageSize, out totalCount);
            var myShareList = _shareAsideBookService.GetShareList(LibrarySystem.Admin.Common.Loginer.AccountId);
            //成功顺路运书次数
            if (myShareList != null && myShareList.Count > 0)
            {
                vm.ShareCount = myShareList.Sum(c=>c.Count);
            }
            else
            {
                vm.ShareCount = 0;
            }

            var paging = new Paging<ShareAsideBook>()
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
        /// 编辑
        /// </summary>
        /// <param name="ShareAsideBookVM"></param>
        /// <returns></returns>
        public ActionResult Edit(ShareAsideBookVM ShareAsideBookVM)
        {
            //1.0 根据主键查询漂流图书
            var shareasidebookinfo = _shareAsideBookService.GetById(ShareAsideBookVM.ShareAsideBookId);
            if (shareasidebookinfo != null)
            {
                ShareAsideBookVM.Isbn = shareasidebookinfo.Isbn;
                ShareAsideBookVM.Title = shareasidebookinfo.Title;
                ShareAsideBookVM.Author = shareasidebookinfo.Author;
                ShareAsideBookVM.Category = shareasidebookinfo.Category;
                ShareAsideBookVM.PublicDate = shareasidebookinfo.PublicDate;
                ShareAsideBookVM.PayMoney = shareasidebookinfo.PayMoney;
                ShareAsideBookVM.PayType = shareasidebookinfo.PayType;
                ShareAsideBookVM.Count = shareasidebookinfo.Count;
                ShareAsideBookVM.UniversityId = shareasidebookinfo.SysAccount.UniversityId;
                ShareAsideBookVM.UniversityName = shareasidebookinfo.SysAccount.University.Name;
                ShareAsideBookVM.SysAccountId = shareasidebookinfo.SysAccountId;
                ShareAsideBookVM.SysAccount = shareasidebookinfo.SysAccount;
                ShareAsideBookVM.Account = shareasidebookinfo.SysAccount == null ? "" : shareasidebookinfo.SysAccount.Account;
                ShareAsideBookVM.BaseImageId = shareasidebookinfo.BaseImageId;
                ShareAsideBookVM.BaseImage = shareasidebookinfo.BaseImage == null ? new BaseImage() : shareasidebookinfo.BaseImage;
                ShareAsideBookVM.TrafficType = ShareAsideBookVM.TrafficType;
                ShareAsideBookVM.Address = shareasidebookinfo.SysAccount.Address;
                ShareAsideBookVM.Email = shareasidebookinfo.SysAccount.Email;
                ShareAsideBookVM.MobilePhone = shareasidebookinfo.SysAccount.MobilePhone;
                //ShareAsideBookVM.ShareCustomerInfo = ShareAsideBookVM.ShareCustomerInfo;
                ShareAsideBookVM.ShareStatus = ShareAsideBookVM.ShareStatus;
                ShareAsideBookVM.QueryUId = 0;
                ShareAsideBookVM.QueryName = "";
                ShareAsideBookVM.QueryIsbn = "";
                ShareAsideBookVM.QueryCategory = "";
            }
            else
            {
                var sysAccount = _SysAccountService.GetById(LibrarySystem.Admin.Common.Loginer.AccountId);
                ShareAsideBookVM.Isbn = "";
                ShareAsideBookVM.Title = "";
                ShareAsideBookVM.Author = "";
                ShareAsideBookVM.Category = "";
                ShareAsideBookVM.PublicDate = "";
                ShareAsideBookVM.PayMoney = 0;
                ShareAsideBookVM.PayType = 0;
                ShareAsideBookVM.Count = 0;
                ShareAsideBookVM.UniversityId = LibrarySystem.Admin.Common.Loginer.UniversityId;
                ShareAsideBookVM.UniversityName = "";
                ShareAsideBookVM.SysAccountId = LibrarySystem.Admin.Common.Loginer.AccountId;
                ShareAsideBookVM.Account = LibrarySystem.Admin.Common.Loginer.Account;
                ShareAsideBookVM.SysAccount = new SysAccount();
                ShareAsideBookVM.BaseImageId = 0;
                ShareAsideBookVM.BaseImage = new BaseImage();
                ShareAsideBookVM.TrafficType = 0;
                ShareAsideBookVM.Address = sysAccount.Address;
                ShareAsideBookVM.Email = sysAccount.Email;
                ShareAsideBookVM.MobilePhone = sysAccount.MobilePhone;
                //ShareAsideBookVM.ShareCustomerInfo = "";
                ShareAsideBookVM.ShareStatus = 0;
                ShareAsideBookVM.QueryUId = 0;
                ShareAsideBookVM.QueryName = "";
                ShareAsideBookVM.QueryIsbn = "";
                ShareAsideBookVM.QueryCategory = "";
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
            ShareAsideBookVM.UinversityList = ulist;
            ShareAsideBookVM.Paging = new Paging<ShareAsideBook>();
            //3.0 返回对象
            return View(ShareAsideBookVM);
        }

        /// <summary>
        /// 添加、修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(ShareAsideBookModel model)
        {
            //更新
            var sysAccount = _SysAccountService.GetById(LibrarySystem.Admin.Common.Loginer.AccountId);
            if (model.ShareAsideBookId > 0)
            {
                var shareAsideBook = _shareAsideBookService.GetById(model.ShareAsideBookId);
                shareAsideBook.EditTime = DateTime.Now;
                shareAsideBook.Title = model.Title;
                shareAsideBook.Isbn = model.Isbn;
                shareAsideBook.Author = model.Author;
                shareAsideBook.Category = model.Category;
                shareAsideBook.PublicDate = model.PublicDate;
                if (model.PayType == (int)EnumHelp.ResearchPayType.有偿)
                {
                    shareAsideBook.PayType = model.PayType;
                    shareAsideBook.PayMoney = model.PayMoney;
                }
                else
                    shareAsideBook.PayType = model.PayType;
                shareAsideBook.Count = model.Count;
                shareAsideBook.BaseImageId = model.BaseImageId;
                try
                {
                    sysAccount.Email = model.Email;
                    sysAccount.MobilePhone = model.MobilePhone;
                    sysAccount.EditTime = DateTime.Now;
                    if (model.TrafficType == (int)EnumHelp.ResearchTrafficType.工作人员上门取书)
                    {
                        shareAsideBook.TrafficType = model.TrafficType;
                        //shareAsideBook.ShareCustomerInfo = model.ShareCustomerInfo;
                        sysAccount.Address = model.Address;   
                    }
                    else
                        shareAsideBook.TrafficType = model.TrafficType;

                    _SysAccountService.Update(sysAccount);
                    _shareAsideBookService.Update(shareAsideBook);
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
                if (model.PayType != (int)EnumHelp.ResearchPayType.有偿)
                {
                    model.PayMoney = 0;
                }
                //if (model.TrafficType != (int)EnumHelp.ResearchTrafficType.工作人员上门取书)
                //{
                //    model.ShareCustomerInfo = "";
                //}
                var result = new ShareAsideBook()
                {
                    SysAccountId = LibrarySystem.Admin.Common.Loginer.AccountId,
                    ShareStatus = (int)EnumHelp.BookShareStatus.待入库,
                    OperaAccountId = LibrarySystem.Admin.Common.Loginer.AccountId,
                    Status = (int)EnumHelp.EnabledEnum.有效,
                    IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                    CreateTime = DateTime.Now,
                    EditTime = DateTime.Now,
                    Author = model.Author,
                    BaseImageId = model.BaseImageId,
                    Category = model.Category,
                    Count = model.Count,
                    Isbn = model.Isbn,
                    PayMoney = model.PayMoney,
                    PayType = model.PayType,
                    PublicDate = model.PublicDate,
                    ResearchAsideBookId = model.ResearchAsideBookId,
                    Title = model.Title,
                    TrafficType = model.TrafficType                     
                };
                sysAccount.Email = model.Email;
                sysAccount.MobilePhone = model.MobilePhone;
                sysAccount.EditTime = DateTime.Now;
                if (model.TrafficType == (int)EnumHelp.ResearchTrafficType.工作人员上门取书)
                {
                    sysAccount.Address = model.Address;               
                    _SysAccountService.Update(sysAccount);
                }
                var shareAsideBook = _shareAsideBookService.Insert(result);
                return Json(new { Status = shareAsideBook.ShareAsideBookId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 漂出图书(求书界面：我有此书)配送信息编辑
        /// </summary>
        /// <param name="ShareAsideBookVM"></param>
        /// <returns></returns>
        public ActionResult ShareBook(ShareAsideBookVM ShareAsideBookVM)
        {
            //1.0 根据主键查询共享的图书
            var shareasidebookinfo = _shareAsideBookService.GetById(ShareAsideBookVM.ShareAsideBookId);
            var sysAccount = _SysAccountService.GetById(ShareAsideBookVM.SysAccountId);
            if (sysAccount != null)
            {
                ShareAsideBookVM.Email = sysAccount.Email;
                ShareAsideBookVM.MobilePhone = sysAccount.MobilePhone;
                ShareAsideBookVM.Address = sysAccount.Address;
            }
            if (shareasidebookinfo != null && shareasidebookinfo.ShareAsideBookId > 0)
            {
                //2.0 字段初始化
                ShareAsideBookVM.TrafficType = shareasidebookinfo.TrafficType;
                 //ShareAsideBookVM.ShareCustomerInfo = shareasidebookinfo.ShareCustomerInfo;
            }
            ShareAsideBookVM.UinversityList = new List<University>();
            ShareAsideBookVM.Paging = new Paging<ShareAsideBook>();
            //3.0 返回对象
            return View(ShareAsideBookVM);
        }

        /// <summary>
        /// 添加漂流订单、修改订单(下单人地址)操作   漂出图书(求书界面：我有此书)配送信息编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ShareBook(ShareAsideBookModel model)
        {
            if (model.ResearchAsideBookId > 0)//1.0 判断为求书页面跳转    我有此书 跳转
            {
                var shareasideBook = _shareAsideBookService.GetResearchAsideBookId(model.ResearchAsideBookId);
                var researchAsideBook = _researchAsideBookService.GetById(model.ResearchAsideBookId);
                var sysAccount = _SysAccountService.GetById(model.SysAccountId);
                if (sysAccount != null)
                {
                    sysAccount.Email = model.Email;
                    sysAccount.EditTime=DateTime.Now;
                    sysAccount.MobilePhone = model.MobilePhone;
                    sysAccount.Address = model.Address;
                }
                if (shareasideBook == null)//1.2 共享记录不存在  
                {
                    ShareAsideBook obj = new ShareAsideBook()
                    {
                        Author = researchAsideBook.Author,
                        BaseImageId = researchAsideBook.BaseImageId,
                        Category = researchAsideBook.Category,
                        Count = 1,
                        CreateTime = DateTime.Now,
                        EditTime = DateTime.Now,
                        Isbn = researchAsideBook.Isbn,
                        IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                        OperaAccountId = 0,
                        PublicDate = researchAsideBook.PublicDate,
                        ResearchAsideBookId = model.ResearchAsideBookId,
                        //ShareCustomerInfo = model.ShareCustomerInfo,
                        SysAccountId = model.SysAccountId,
                        Title = researchAsideBook.Title,
                        TrafficType = model.TrafficType,
                        ShareStatus = (int)EnumHelp.BookShareStatus.待入库,
                        Status = (int)EnumHelp.EnabledEnum.有效,
                        PayType = researchAsideBook.PayType,
                        PayMoney = researchAsideBook.PayMoney
                    };
                    //if (model.TrafficType == (int)EnumHelp.TrafficType.邮寄)
                    //{
                    //    sysAccount.Address = model.Address;
                    //}
                    //researchAsideBook.ShareCustomerInfo = model.ShareCustomerInfo;
                    researchAsideBook.ShareSysAccountId = model.SysAccountId;
                    researchAsideBook.UniversityId = sysAccount.UniversityId;
                    researchAsideBook.EditTime = DateTime.Now;
                    // researchAsideBook.ResearchStatus = (int)EnumHelp.ResearchStatus.找到书源;
                    try
                    {
                        //1.3 添加共享记录
                        var result = _shareAsideBookService.Insert(obj);
                        if (result.ShareAsideBookId > 0)
                        {
                            //1.4 修改求书状态
                            _SysAccountService.Update(sysAccount);
                            _researchAsideBookService.Update(researchAsideBook);
                            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
                else//2.0 共享记录已存在  
                {
                    //2.1 修改共享记录信息
                    shareasideBook.EditTime = DateTime.Now;

                    if (model.TrafficType == (int)EnumHelp.ResearchTrafficType.工作人员上门取书)
                    {
                        shareasideBook.TrafficType = model.TrafficType;
                        sysAccount.Address = model.Address;
                        //shareasideBook.ShareCustomerInfo = model.ShareCustomerInfo;
                    }
                    else
                    {
                        shareasideBook.TrafficType = model.TrafficType;
                    }
                    //2.2 修改求书信息
                    if (researchAsideBook != null)
                    {
                        //researchAsideBook.ResearchStatus = (int)EnumHelp.ResearchStatus.找到书源;
                        researchAsideBook.EditTime = DateTime.Now;
                        researchAsideBook.ShareSysAccountId = model.SysAccountId;
                        researchAsideBook.UniversityId = sysAccount.UniversityId;
                        //researchAsideBook.ShareCustomerInfo = model.ShareCustomerInfo;
                    }
                    try
                    {
                        //3.0 更新数据并返回结果
                        _SysAccountService.Update(sysAccount);
                        _shareAsideBookService.Update(shareasideBook);
                        _researchAsideBookService.Update(researchAsideBook);
                        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else if (model.ShareAsideBookId > 0)//2.0 非求书页面跳转 更新订单运输信息   共享人编辑地址等信息
            {
                var sysAccount = _SysAccountService.GetById(model.SysAccountId);
                var shareasideBook = _shareAsideBookService.GetById(model.ShareAsideBookId);
                shareasideBook.EditTime = DateTime.Now;
                sysAccount.Email = model.Email;
                sysAccount.MobilePhone = model.MobilePhone;
                sysAccount.EditTime = DateTime.Now;
                if (model.TrafficType == (int)EnumHelp.ResearchTrafficType.工作人员上门取书)
                {
                    shareasideBook.TrafficType = model.TrafficType;
                    //shareasideBook.ShareCustomerInfo = model.ShareCustomerInfo;
                    sysAccount.Address = model.Address;
                }
                else
                {
                    shareasideBook.TrafficType = model.TrafficType;
                }
                try
                {
                    _SysAccountService.Update(sysAccount);
                    _shareAsideBookService.Update(shareasideBook);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 取消漂出
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        
         public JsonResult BookCancle(ShareAsideBook model)
          //  public JsonResult BookCancle(int ShareAsideBookId = 0,int ResearchAsideBookId=0)
        {
            //1.0 出书人  出书界面取消
            if (model.ShareAsideBookId > 0)
            {
                var shareAsideBook = _shareAsideBookService.GetById(model.ShareAsideBookId);
                shareAsideBook.ShareStatus = (int)EnumHelp.BookShareStatus.已取消;
                shareAsideBook.EditTime = DateTime.Now;
                shareAsideBook.OperaAccountId = LibrarySystem.Admin.Common.Loginer.AccountId;
                try
                {
                    //1.1 修改求书状态
                    if (shareAsideBook.ResearchAsideBookId > 0)
                    {
                        var researchBook = _researchAsideBookService.GetById(shareAsideBook.ResearchAsideBookId);
                        researchBook.ResearchStatus = (int)EnumHelp.ResearchStatus.求书中;
                        researchBook.EditTime = DateTime.Now;
                        researchBook.UniversityId = 0;
                        researchBook.ShareSysAccountId = 0;
                        _researchAsideBookService.Update(researchBook);
                    }
                    //1.2 修改出书状态
                    _shareAsideBookService.Update(shareAsideBook);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            //2.0 出书人 求书界面取消
            else if (model.ResearchAsideBookId > 0)
            {
                var researchBook = _researchAsideBookService.GetById(model.ResearchAsideBookId);
                var shareBook=  _shareAsideBookService.GetResearchAsideBookId(model.ResearchAsideBookId);
                researchBook.ResearchStatus = (int)EnumHelp.ResearchStatus.求书中;
                researchBook.ShareSysAccountId = 0;
                researchBook.EditTime = DateTime.Now;
                //2.1 取消出书
                if (shareBook != null)
                {
                    shareBook.ShareStatus = (int)EnumHelp.BookShareStatus.已取消;
                    shareBook.EditTime = DateTime.Now;
                    shareBook.OperaAccountId = LibrarySystem.Admin.Common.Loginer.AccountId;
                    _shareAsideBookService.Update(shareBook);
                }
                //2.2 修改出书状态
                _researchAsideBookService.Update(researchBook);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// 管理员界面   分享的图书列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(ShareAsideBookVM vm, int pn = 1)
        {
            //1.0 页面初始化
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //if (vm.QueryUId == 0)//当前账户、当前管理员只能看到本校或本人的图书
            //{
            //    vm.QueryUId = LibrarySystem.Admin.Common.Loginer.UniversityId;
            //}
            //2.0 条件查询
            var blist = _shareAsideBookService.GetAdminManagerList(vm.QueryShareType, vm.QueryName, vm.QueryIsbn, LibrarySystem.Admin.Common.Loginer.UniversityId, vm.QueryCategory, vm.QuerySysAccount, vm.QueryShareStatus, vm.QueryPayType, vm.QueryTrafficType, pageIndex, pageSize, out totalCount);
            totalCount = 0;
            var paging = new Paging<ShareAsideBook>()
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
        /// 入库 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BookConfirm(int ShareAsideBookId = 0, int UniversityId = 0, string Isbn = "", int ResearchAsideBookId = 0)
        {
            if (ShareAsideBookId <= 0 || UniversityId < 0 || string.IsNullOrWhiteSpace(Isbn))
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            try
            {
                var asideBookInfo = _asideBookInfoService.GetByUniversityIsbn(UniversityId, Isbn);
                var researchAsideBook = _researchAsideBookService.GetById(ResearchAsideBookId);
                var shareAsideBook = _shareAsideBookService.GetById(ShareAsideBookId);
                if (asideBookInfo != null && shareAsideBook != null)
                {
                    asideBookInfo.Available += shareAsideBook.Count;
                    asideBookInfo.Count += shareAsideBook.Count;
                    asideBookInfo.EditTime = DateTime.Now;
                    shareAsideBook.ShareStatus = (int)EnumHelp.BookShareStatus.已共享;
                    shareAsideBook.EditTime = DateTime.Now;
                    _shareAsideBookService.Update(shareAsideBook);
                    _asideBookInfoService.Update(asideBookInfo);

                    //获取邮件正文
                    string body = EmailTemplate.EmailDictionary[(int)EmailTemplate.EmailTemplete.E建漂流图书服务系统图书漂出服务];
                    string sendString = body.Replace("#Title#", asideBookInfo.Title).Replace("#Count#", shareAsideBook.Count.ToString());
                    string subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统图书漂出服务.ToString();
                    bool result = SendEmailHelp.SendMail(EmailTemplate.EmailTemplete.E建漂流图书服务系统图书漂出服务.ToString(), sendString, shareAsideBook.SysAccount.Email, "", "", "");
                }
                else if (asideBookInfo == null)
                {

                    var model = new AsideBookInfo()
                    {
                        Author = shareAsideBook.Author,
                        Available = shareAsideBook.Count,
                        BaseImageId = shareAsideBook.BaseImageId,
                        Category = shareAsideBook.Category,
                        CreateTime = DateTime.Now,
                        EditTime = DateTime.Now,
                        Isbn = shareAsideBook.Isbn,
                        IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                        SysAccountId = shareAsideBook.SysAccountId,
                        Title = shareAsideBook.Title,
                        Count = shareAsideBook.Count,
                        UniversityId = shareAsideBook.SysAccount.UniversityId,
                        PublicDate = shareAsideBook.PublicDate,
                        Status = (int)EnumHelp.EnabledEnum.有效
                    };
                    AsideBookInfo result = _asideBookInfoService.Insert(model);
                    if (result.AsideBookInfoId > 0)
                    {
                        shareAsideBook.OperaAccountId = LibrarySystem.Admin.Common.Loginer.AccountId;
                        shareAsideBook.ShareStatus = (int)EnumHelp.BookShareStatus.已共享;
                        shareAsideBook.EditTime = DateTime.Now;
                        _shareAsideBookService.Update(shareAsideBook);
                    }
                }
                if (ResearchAsideBookId > 0 && researchAsideBook != null && shareAsideBook != null)//求书共享
                {
                    researchAsideBook.ResearchStatus = (int)EnumHelp.ResearchStatus.找到书源;
                    researchAsideBook.EditTime = DateTime.Now;
                    if (researchAsideBook.SysAccount.UniversityId == UniversityId)
                    {
                        researchAsideBook.TrafficType = (int)EnumHelp.TrafficType.自取;
                    }
                    else if (researchAsideBook.SysAccount.UniversityId != UniversityId)
                    {
                        researchAsideBook.TrafficType = (int)EnumHelp.TrafficType.邮寄;
                    }
                    if (researchAsideBook.SysAccount.UniversityId == shareAsideBook.SysAccount.UniversityId)
                    {
                        researchAsideBook.TrafficType = (int)EnumHelp.TrafficType.自取;
                    }
                    _researchAsideBookService.Update(researchAsideBook);
                }
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}