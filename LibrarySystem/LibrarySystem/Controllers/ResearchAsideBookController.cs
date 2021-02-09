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
    public class ResearchAsideBookController : BaseController
    {
        /// <summary>
        /// 声明ResearchAsideBookService接口服务对象
        /// </summary>
        private readonly IResearchAsideBookService _ResearchAsideBookService;
        /// <summary>
        /// 声明IShareAsideBookService接口服务对象
        /// </summary>
        private readonly IShareAsideBookService _ShareAsideBookService;
        /// <summary>
        /// 声明AsideBookInfoService接口服务对象
        /// </summary>
        private readonly IAsideBookInfoService _AsideBookService;
        /// <summary>
        /// 声明University接口服务对象
        /// </summary>
        private readonly IUniversityService _universityService;
        /// <summary>
        /// 声明IExpressService接口服务对象
        /// </summary>
        private readonly IExpressService _expressService;

        /// <summary>
        /// 声明IExpressService接口服务对象
        /// </summary>
        private readonly ISysAccountService _sysAccountService;
        /// <summary>
        /// 构造函数进行对象初始化
        /// </summary>
        /// <param name="ResearchAsideBookService"></param>
        /// <param name="universityService"></param>
        public ResearchAsideBookController(IResearchAsideBookService ResearchAsideBookService, IUniversityService universityService, IAsideBookInfoService AsideBookService, IShareAsideBookService ShareAsideBookService, IExpressService expressService, ISysAccountService sysAccountService)
        {
            this._ResearchAsideBookService = ResearchAsideBookService;
            this._universityService = universityService;
            this._AsideBookService = AsideBookService;
            this._ShareAsideBookService = ShareAsideBookService;
            this._expressService = expressService;
            this._sysAccountService = sysAccountService;
        }

        #region 用户操作界面
        /// <summary>
        /// 我的求书列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult MyResearchList(ResearchAsideBookVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //QuerySysAccount  QueryPayType QueryResearchStatus
            var blist = _ResearchAsideBookService.GetManagerList(vm.QueryIsMyResearch > 0 ? LibrarySystem.Admin.Common.Loginer.AccountId : 0, vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory,vm.QueryPayType,vm.QueryResearchStatus, pageIndex, pageSize, out totalCount);
            var sucessfulList = _ResearchAsideBookService.GetResearchSucessFulList(LibrarySystem.Admin.Common.Loginer.AccountId);
            if(sucessfulList!=null&& sucessfulList.Count>0)//查询求书成功数量
            {
                vm.ResearchSuccessfulCount = sucessfulList.Count;
            }
            else
            {
                vm.ResearchSuccessfulCount = 0;
            }
            var paging = new Paging<ResearchAsideBook>()
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
        /// 修改求书人地址界面
        /// </summary>
        /// <param name="ResearchAsideBookVM"></param>
        /// <returns></returns>
        public ActionResult EditCustomerInfo(ResearchAsideBookVM ResearchAsideBookVM)
        {
            //1.0 根据id获取实体
            var researchAsideBook = _ResearchAsideBookService.GetById(ResearchAsideBookVM.ResearchAsideBookId);
            if (researchAsideBook != null)//求书列表页面
            {
                ResearchAsideBookVM.ResearchStatus = researchAsideBook.ResearchStatus;
                if (ResearchAsideBookVM.ShareSysAccountId > 0)//出书人编辑
                {
                    var ShareSysAccount = _sysAccountService.GetById(ResearchAsideBookVM.ShareSysAccountId);
                    ResearchAsideBookVM.Address = ShareSysAccount.Address;// researchAsideBook.ShareCustomerInfo;
                    ResearchAsideBookVM.MobilePhone = ShareSysAccount.MobilePhone;
                    ResearchAsideBookVM.Email = ShareSysAccount.Email;
                    ResearchAsideBookVM.ShareSysAccountId = researchAsideBook.ShareSysAccountId;
                    var shareObj = _ShareAsideBookService.GetResearchAsideBookId(ResearchAsideBookVM.ResearchAsideBookId);
                    ResearchAsideBookVM.TrafficType = shareObj == null ? 0 : shareObj.TrafficType;//出书人的出书方式 
                }
                else if (ResearchAsideBookVM.SysAccountId > 0)//求书人编辑
                {
                    var researchSysAccount = _sysAccountService.GetById(LibrarySystem.Admin.Common.Loginer.AccountId);
                    ResearchAsideBookVM.Address = researchSysAccount.Address;
                    ResearchAsideBookVM.MobilePhone = researchSysAccount.MobilePhone;
                    ResearchAsideBookVM.Email = researchSysAccount.Email;
                    ResearchAsideBookVM.SysAccountId = researchSysAccount.SysAccountId;

                    //ResearchAsideBookVM.CustomerInfo = researchAsideBook.CustomerInfo;
                    ResearchAsideBookVM.TrafficType = researchAsideBook.TrafficType;
                }
                //else
                //{
                //    ResearchAsideBookVM.Address = researchAsideBook.SysAccount.Address;
                //    ResearchAsideBookVM.TrafficType = researchAsideBook.TrafficType;
                //    ResearchAsideBookVM.ShareCustomerInfo = researchAsideBook.ShareCustomerInfo;
                //}
            }
            else
            {
                ResearchAsideBookVM.Address = "";// researchAsideBook.ShareCustomerInfo;
                ResearchAsideBookVM.MobilePhone = "";
                ResearchAsideBookVM.Email = "";
                ResearchAsideBookVM.ShareSysAccountId = 0;

                //ResearchAsideBookVM.CustomerInfo = "";
                ResearchAsideBookVM.TrafficType = 0;
            }
            return View(ResearchAsideBookVM);
        }

        /// <summary>
        /// 订单邮编信息提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditCustomerInfo(ResearchAsideBookModel model)
        {
            //1.0 判断id有效
            var obj = _ResearchAsideBookService.GetById(model.ResearchAsideBookId);
            if (obj != null && obj.ResearchAsideBookId > 0)
            {
                if (model.SysAccountId > 0)//求书人编辑
                {
                    var sysaccount = _sysAccountService.GetById(model.SysAccountId);
                    if (model.TrafficType==(int)EnumHelp.TrafficType.邮寄)
                        sysaccount.Address = model.Address;
                    sysaccount.Email = model.Email;
                    sysaccount.MobilePhone = model.MobilePhone;
                    sysaccount.EditTime = DateTime.Now;
                    //obj.CustomerInfo = model.CustomerInfo;
                    obj.TrafficType = model.TrafficType;
                    obj.EditTime = DateTime.Now;
                    try
                    {
                        _ResearchAsideBookService.Update(obj);
                        _sysAccountService.Update(sysaccount);
                        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (model.ShareSysAccountId > 0)//出书人编辑
                {
                    var sysaccount = _sysAccountService.GetById(model.ShareSysAccountId);
                    var shareObj = _ShareAsideBookService.GetResearchAsideBookId(model.ResearchAsideBookId);
                    sysaccount.Address = model.Address;
                    sysaccount.Email = model.Email;
                    sysaccount.MobilePhone = model.MobilePhone;
                    sysaccount.EditTime = DateTime.Now;
                    // shareObj.ShareCustomerInfo = model.CustomerInfo;
                    shareObj.TrafficType = model.TrafficType;
                    shareObj.EditTime = DateTime.Now;
                    //obj.ShareCustomerInfo = model.CustomerInfo;
                    //obj.EditTime = DateTime.Now;
                    try
                    {
                        _sysAccountService.Update(sysaccount);
                        //_ResearchAsideBookService.Update(obj);
                        _ShareAsideBookService.Update(shareObj);
                        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 共用
        /// <summary>
        /// 求书订单状态修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateResearchStatus(ResearchAsideBook model)
        {
            //1.0 判断id有效
            var obj = _ResearchAsideBookService.GetById(model.ResearchAsideBookId);
            if (obj != null && obj.ResearchAsideBookId > 0)
            {
                obj.ResearchStatus = model.ResearchStatus;
                obj.EditTime = DateTime.Now; 
                try
                {
                    if (model.ResearchStatus == (int)EnumHelp.ResearchStatus.取消求书)
                    {
                        obj.UniversityId = 0;
                        obj.ShareSysAccountId = 0;
                        ShareAsideBook shareBook = _ShareAsideBookService.GetResearchAsideBookId(model.ResearchAsideBookId);
                        if (shareBook != null)
                        {
                            shareBook.ResearchAsideBookId = 0;
                            shareBook.EditTime = DateTime.Now;
                            _ShareAsideBookService.Update(shareBook);
                        }
                    }
                    else if (model.ResearchStatus == (int)EnumHelp.ResearchStatus.求书成功)
                    {
                        int Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统图书求书服务;
                        string subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统图书求书服务.ToString();
                        string temp = EmailTemplate.EmailDictionary[Type];
                        string body = temp.Replace("#Title#", obj.Title);
                        SendEmailHelp.SendMail(subject, body, obj.SysAccount.Email, "", "", "");
                    }
                    _ResearchAsideBookService.Update(obj);
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
        /// 求书编辑页面
        /// </summary>
        /// <param name="ResearchAsideBookVM"></param>
        /// <returns></returns>
        public ActionResult Edit(ResearchAsideBookVM ResearchAsideBookVM)
        {
            var researchasidebookinfo = _ResearchAsideBookService.GetById(ResearchAsideBookVM.ResearchAsideBookId);
            if (researchasidebookinfo != null)
            {
                ResearchAsideBookVM.Author = researchasidebookinfo.Author;
                ResearchAsideBookVM.Category = researchasidebookinfo.Category;
                ResearchAsideBookVM.Isbn = researchasidebookinfo.Isbn;
                ResearchAsideBookVM.PublicDate = researchasidebookinfo.PublicDate;
                ResearchAsideBookVM.Title = researchasidebookinfo.Title;
                ResearchAsideBookVM.Remark = researchasidebookinfo.Remark;
                //ResearchAsideBookVM.BaseImageId = researchasidebookinfo.BaseImageId;
                ResearchAsideBookVM.UniversityId = researchasidebookinfo.SysAccount.UniversityId;
                ResearchAsideBookVM.UniversityName = researchasidebookinfo.SysAccount.University.Name;
                ResearchAsideBookVM.SysAccountId = researchasidebookinfo.SysAccountId;
                ResearchAsideBookVM.SysAccount = researchasidebookinfo.SysAccount;
                ResearchAsideBookVM.ShareSysAccountId = researchasidebookinfo.ShareSysAccountId;
                ResearchAsideBookVM.ResearchStatus = researchasidebookinfo.ResearchStatus;
                ResearchAsideBookVM.SearchAccountName = researchasidebookinfo.SysAccount == null ? "" : researchasidebookinfo.SysAccount.Account;
                ResearchAsideBookVM.ImgInfo = researchasidebookinfo.BaseImage == null ? new BaseImage() : researchasidebookinfo.BaseImage;
                //ResearchAsideBookVM.CustomerInfo = researchasidebookinfo.CustomerInfo;
                ResearchAsideBookVM.QuerySysAccount = "";
                ResearchAsideBookVM.QueryUId = 0;
                ResearchAsideBookVM.QueryName = "";
                ResearchAsideBookVM.QueryIsbn = "";
                ResearchAsideBookVM.QueryCategory = "";
            }
            else
            {
                ResearchAsideBookVM.Author = "";
                ResearchAsideBookVM.Category = "";
                ResearchAsideBookVM.Isbn = "";
                ResearchAsideBookVM.PublicDate = "";
                ResearchAsideBookVM.Title = "";
                ResearchAsideBookVM.Remark = "";
                //ResearchAsideBookVM.BaseImageId = 0;
                ResearchAsideBookVM.ImgInfo = new BaseImage();
                ResearchAsideBookVM.SysAccount = _sysAccountService.GetById(LibrarySystem.Admin.Common.Loginer.AccountId);
                ResearchAsideBookVM.UniversityId = 0;
                ResearchAsideBookVM.UniversityName = "";
                ResearchAsideBookVM.SysAccountId = LibrarySystem.Admin.Common.Loginer.AccountId;
                ResearchAsideBookVM.ShareSysAccountId = 0;
                ResearchAsideBookVM.ResearchStatus = 0;
                //ResearchAsideBookVM.CustomerInfo = "";
                ResearchAsideBookVM.SearchAccountName = LibrarySystem.Admin.Common.Loginer.Account;
                ResearchAsideBookVM.QuerySysAccount = "";
                ResearchAsideBookVM.QueryUId = 0;
                ResearchAsideBookVM.QueryName = "";
                ResearchAsideBookVM.QueryIsbn = "";
                ResearchAsideBookVM.QueryCategory = "";
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
            ResearchAsideBookVM.UinversityList = ulist;
            ResearchAsideBookVM.Paging = new Paging<ResearchAsideBook>();
            return View(ResearchAsideBookVM);
        }


        /// <summary>
        /// 求书编辑页面
        /// </summary>
        /// <param name="ResearchAsideBookVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(ResearchAsideBookModel model)
        {
            //1.0 获取求书人
            var sysAccount = _sysAccountService.GetById(model.SysAccountId > 0 ? model.SysAccountId : LibrarySystem.Admin.Common.Loginer.AccountId);
            //2.0 判断是否为新增求书信息
            if (model.ResearchAsideBookId > 0 && sysAccount != null)
            {
                var researchasidebookinfo = _ResearchAsideBookService.GetById(model.ResearchAsideBookId);
                researchasidebookinfo.Author = model.Author;
                researchasidebookinfo.Category = model.Category;
                researchasidebookinfo.Isbn = model.Isbn;
                researchasidebookinfo.PublicDate = model.PublicDate;
                researchasidebookinfo.Title = model.Title;
                researchasidebookinfo.Remark = model.Remark;
                researchasidebookinfo.BaseImageId = model.BaseImageId;
                researchasidebookinfo.TrafficType = model.TrafficType;
                researchasidebookinfo.PayType = model.PayType;
                researchasidebookinfo.PayMoney = model.PayMoney;
                researchasidebookinfo.EditTime = DateTime.Now;

                sysAccount.Address = model.Address;
                sysAccount.Email = model.Email;
                sysAccount.MobilePhone = model.MobilePhone;
                sysAccount.EditTime = DateTime.Now;
                //researchasidebookinfo.CustomerInfo = researchasidebookinfo.CustomerInfo;
                try
                {
                    //2.1 更新求书信息  求书人地址等
                    _ResearchAsideBookService.Update(researchasidebookinfo);
                    _sysAccountService.Update(sysAccount);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            //3.0 新增求书信息
            else if (sysAccount != null)
            {
                //3.1 判断是否已经发起此isbn求书
                var researchBook = _ResearchAsideBookService.GetByIsbnAccountId(model.Isbn, model.SysAccountId > 0 ? model.SysAccountId : LibrarySystem.Admin.Common.Loginer.AccountId);
                if (researchBook != null&& researchBook.ResearchAsideBookId>0)
                    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                //3.2 新增求书
                var result = new ResearchAsideBook()
                {
                    Author = model.Author,
                    Category = model.Category,
                    Isbn = model.Isbn,
                    PublicDate = model.PublicDate,
                    Title = model.Title,
                    Remark = model.Remark,
                    BaseImageId = model.BaseImageId,
                    TrafficType = model.TrafficType,
                    PayType = model.PayType,
                    PayMoney = model.PayMoney,
                    ResearchStatus = (int)EnumHelp.ResearchStatus.求书中,
                    Status = (int)EnumHelp.EnabledEnum.有效,
                    IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                    CreateTime = DateTime.Now,
                    EditTime = DateTime.Now,
                    ExpressId = 0,
                    ShareSysAccountId = 0,
                    UniversityId = LibrarySystem.Admin.Common.Loginer.UniversityId,
                    SysAccountId = LibrarySystem.Admin.Common.Loginer.AccountId
                };
                sysAccount.Address = model.Address;
                sysAccount.Email = model.Email;
                sysAccount.MobilePhone = model.MobilePhone;
                sysAccount.EditTime = DateTime.Now;
                try
                {
                    var researchAsideBook = _ResearchAsideBookService.Insert(result);
                    //3.3 更新求书人信息
                    _sysAccountService.Update(sysAccount);
                    return Json(new { Status = researchAsideBook.ResearchAsideBookId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }


            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        /// <summary>
        /// 求书列表
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(ResearchAsideBookVM vm, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            List<ResearchAsideBook> blist = new List<ResearchAsideBook>();
            if (vm.ResearchAsideBookId > 0)//从“漂”出列表跳转至“求”书列表
            {
                var researchresult = _ResearchAsideBookService.GetById(vm.ResearchAsideBookId);
                blist.Add(researchresult);
                totalCount = 1;
            }
            else
            {
                blist = _ResearchAsideBookService.GetManagerList(vm.QuerySysAccount, vm.QueryPayType, vm.QueryResearchStatus, vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, pageIndex, pageSize, out totalCount);
            }
            //var blist = _ResearchAsideBookService.GetManagerList(0, vm.QueryName, vm.QueryIsbn, vm.QueryUId, vm.QueryCategory, pageIndex, pageSize, out totalCount);
            var paging = new Paging<ResearchAsideBook>()
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
        /// 管理员邮寄界面
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ActionResult ResearchBookEmail(ResearchAsideBookVM vm)
        {
            var researchObj = _ResearchAsideBookService.GetById(vm.ResearchAsideBookId);
            if (researchObj != null)
            {
                vm.CustomerInfo = researchObj.SysAccount.MobilePhone + researchObj.SysAccount.Address;
                vm.QuerySysAccount = researchObj.SysAccount.Account;
                vm.BaseImage = researchObj.BaseImage == null ? new BaseImage() : researchObj.BaseImage;
                vm.Isbn = researchObj.Isbn;
                vm.Title = researchObj.Title;
                vm.TrafficType = researchObj.TrafficType;
                if (researchObj.Express != null)
                {
                    vm.ExpressName = researchObj.Express.ExpressName;
                    vm.ExpressNo = researchObj.Express.ExpressNo;
                    vm.TrafficFee = researchObj.Express.TrafficFee;
                }
            }
            return View(vm);
        }

        /// <summary>
        /// 管理员邮寄界面
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ResearchBookEmail(ExpressVM model)
        {
            //1.0 判断id有效
            if (model.ResearchAsideBookId > 0)
            {
                var researchAsideBook = _ResearchAsideBookService.GetById(model.ResearchAsideBookId);
                try
                {
                    if (model.ExpressId > 0)//修改邮寄信息
                    {
                        var expObj = _expressService.GetById(model.ExpressId);
                        expObj.ExpressName = model.ExpressName;
                        expObj.ExpressNo = model.ExpressNo;
                        expObj.TrafficFee = model.TrafficFee;
                        researchAsideBook.EditTime = DateTime.Now;
                        //researchAsideBook.TrafficFee = model.TrafficFee;
                        _expressService.Update(expObj);
                        _ResearchAsideBookService.Update(researchAsideBook);
                        return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                       // var shareAsideBook = _ShareAsideBookService.GetResearchAsideBookId(model.ResearchAsideBookId);
                        var shareAsideBook = _ShareAsideBookService.GetResearchAsideBookId(model.ResearchAsideBookId,(int)EnumHelp.BookShareStatus.已共享);
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
                        if (express.ExpressId > 0 && shareAsideBook != null)
                        {
                            var asideBook = _AsideBookService.GetByUniversityIsbn(shareAsideBook.SysAccount.UniversityId, researchAsideBook.Isbn);
                            if (asideBook == null && asideBook.Available <= 0)
                                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                            //researchAsideBook.TrafficFee = model.TrafficFee;
                            researchAsideBook.EditTime = DateTime.Now;
                            researchAsideBook.ExpressId = express.ExpressId;
                            asideBook.Available -= 1;
                            _AsideBookService.Update(asideBook);
                            _ResearchAsideBookService.Update(researchAsideBook);

                            int Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统图书寄送服务;
                            string subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统图书寄送服务.ToString();
                            string temp = EmailTemplate.EmailDictionary[Type];
                            string body = temp.Replace("#Title#", researchAsideBook.Title).Replace("#ExpressNo#", express.ExpressNo).Replace("#ExpressName#", express.ExpressName).Replace("#TrafficFee#", express.TrafficFee.ToString());
                            SendEmailHelp.SendMail(subject, body, researchAsideBook.SysAccount.Email, "", "", "");

                            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }














        #region 多余的
        /// <summary>
        /// 出书人填写出书方式界面
        /// </summary>
        /// <param name="ResearchAsideBookVM"></param>
        /// <returns></returns>
        public ActionResult OrderResearchAsideBook(ResearchAsideBookVM ResearchAsideBookVM)
        {
            //1.0 根据求书id获取求书数据
            var researchasidebookinfo = _ResearchAsideBookService.GetById(ResearchAsideBookVM.ResearchAsideBookId);
            if (researchasidebookinfo != null)
            {
                ResearchAsideBookVM.TrafficType = researchasidebookinfo.TrafficType;
                //ResearchAsideBookVM.ShareCustomerInfo = researchasidebookinfo.ShareCustomerInfo;
                ResearchAsideBookVM.Isbn = researchasidebookinfo.Isbn;
                ResearchAsideBookVM.Title = researchasidebookinfo.Title;
                ResearchAsideBookVM.Remark = researchasidebookinfo.Remark;
                ResearchAsideBookVM.UniversityId = researchasidebookinfo.UniversityId;
                ResearchAsideBookVM.UniversityName = researchasidebookinfo.University.Name;
                ResearchAsideBookVM.SysAccountId = researchasidebookinfo.SysAccountId;
                ResearchAsideBookVM.ShareSysAccountId = researchasidebookinfo.ShareSysAccountId;
                ResearchAsideBookVM.ResearchStatus = researchasidebookinfo.ResearchStatus;
                ResearchAsideBookVM.ImgInfo = researchasidebookinfo.BaseImage == null ? new BaseImage() : researchasidebookinfo.BaseImage;
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
            ResearchAsideBookVM.UinversityList = ulist;
            ResearchAsideBookVM.Paging = new Paging<ResearchAsideBook>();
            return View(ResearchAsideBookVM);
        }

        /// <summary>
        /// 添加、修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult OrderResearchAsideBook(ResearchAsideBook model)
        {
            //1.0 更新记录
            if (model.ResearchAsideBookId > 0)
            {
                var researchAsideBook = _ResearchAsideBookService.GetById(model.ResearchAsideBookId);
                researchAsideBook.EditTime = DateTime.Now;
                if (researchAsideBook.TrafficType != 0)
                    researchAsideBook.ResearchStatus = (int)EnumHelp.ResearchStatus.找到书源;
                try
                {
                    _ResearchAsideBookService.Update(researchAsideBook);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}