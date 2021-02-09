using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Common;
using LibrarySystem.Admin.Models;
using LibrarySystem.Core.Utils;
using LibrarySystem.Domain;
using LibrarySystem.Domain.Model;
using LibrarySystem.IService;
using static LibrarySystem.Domain.EnumHelp;
using LibrarySystem.Admin.Common;

namespace LibrarySystem.Controllers
{
    public class SysAccountController : BaseController
    {

        private readonly ISysAccountService _SysAccountService;
        private readonly ISysRoleService _SysRoleService;
        private readonly IUniversityService _universityService;

        public SysAccountController(ISysAccountService SysAccountService, ISysRoleService SysRoleService, IUniversityService universityServic)
        {
            _SysAccountService = SysAccountService;
            _SysRoleService = SysRoleService;
            _universityService = universityServic;
        }

        // GET: SysAccount
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="_SysAccountVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(SysAccountVM _SysAccountVM, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _SysAccountService.GetManagerList(_SysAccountVM.QueryName, _SysAccountVM.QuerySysRoleId, _SysAccountVM.QueryUId, pageIndex, pageSize, out totalCount).ToList();
            var paging = new Paging<SysAccount>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };

            var getlist = _universityService.GetAllList().Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            var ulist = new List<University>();
            foreach (var item in getlist)
            {
                ulist.Add(new University()
                {
                    UniversityId = item.UniversityId,
                    Name = item.Name
                });
            }
            _SysAccountVM.SysRoles = _SysRoleService.GetAll();

            _SysAccountVM.UinversityList = ulist;

            _SysAccountVM.Paging = paging;
            return View(_SysAccountVM);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="_SysAccountVM"></param>
        /// <returns></returns>
        public ActionResult Edit(SysAccountVM _SysAccountVM)
        {
            _SysAccountVM.SysAccount = _SysAccountService.GetById(_SysAccountVM.Id) ?? new SysAccount();
            _SysAccountVM.ImgInfo = _SysAccountVM.SysAccount.BaseImage ?? new BaseImage();

            _SysAccountVM.SysRoles = _SysRoleService.GetAll();
            var getlist = _universityService.GetAllList().Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            var ulist = new List<University>();
            foreach (var item in getlist)
            {
                ulist.Add(new University()
                {
                    UniversityId = item.UniversityId,
                    Name = item.Name
                });
            }
            _SysAccountVM.UinversityList = ulist;
            return View(_SysAccountVM);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(SysAccount model)
        {
            try
            {
                if (model.SysAccountId > 0)
                {
                    var entity = _SysAccountService.GetById(model.SysAccountId);
                    if (model.SysRoleId == (int)EnumHelp.RoleTypeEnum.学生)
                    {
                        if (!model.Account.Equals(entity.Account))
                        {
                            if (_SysAccountService.IsExitAccountByAccountUid(model.Account, model.UniversityId))
                            {
                                return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        //_SysAccountService.GetAccountByAccountUid
                    }
                    var entitySysAccount = _SysAccountService.GetById(model.SysAccountId);
                    //修改  
                    if (model.SysRoleId == (int)EnumHelp.RoleTypeEnum.系统管理员)
                    {
                        entitySysAccount.UniversityId = 0;
                    }
                    else
                    {
                        entitySysAccount.UniversityId = model.UniversityId;
                    }
                    entitySysAccount.Account = model.Account;
                    entitySysAccount.SysRoleId = model.SysRoleId;
                    entitySysAccount.EditTime = DateTime.Now;
                    entitySysAccount.NickName = model.NickName;
                    entitySysAccount.MobilePhone = model.MobilePhone;
                    entitySysAccount.BaseImageId = model.BaseImageId;
                    entitySysAccount.Remarks = string.IsNullOrWhiteSpace(model.Remarks) ? "" : model.Remarks;
                    _SysAccountService.Update(entitySysAccount);
                }
                else
                {
                    var sysAccount=_SysAccountService.GetAccountByAccountUid(model.Account, model.UniversityId);
                    if (sysAccount!=null&& sysAccount.SysAccountId>0)
                    {
                        return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.SysRoleId == (int)EnumHelp.RoleTypeEnum.系统管理员)
                    {
                        model.UniversityId = 0;
                    }
                    //添加

                    model.PassWord = MD5Util.GetMD5_32(model.PassWord);
                    model.EditTime = DateTime.Now;
                    model.CreateTime = DateTime.Now;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.Status = (int)EnabledEnum.有效;
                    model.Remarks = string.IsNullOrWhiteSpace(model.Remarks) ? "" : model.Remarks;
                    model.Token = "";
                    model.Address = "";
                    model.Email = "";
                    if (_SysAccountService.GetAccount(model.Account))
                        return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    _SysAccountService.Insert(model);
                }
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
            try
            {
                var entity = _SysAccountService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _SysAccountService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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
        public JsonResult UpdateStatus(int id = 0, LibrarySystem.Domain.EnumHelp.EnabledEnum isEnabled = EnumHelp.EnabledEnum.有效)
        {
            try
            {
                var entity = _SysAccountService.GetById(id);
                entity.Status = (int)isEnabled;

                _SysAccountService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPwd()
        {
            return View();
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="accountId">账号id</param>
        /// <param name="account">账号</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditPwd(int accountId, string account, string oldPassword, string newPassword)
        {
            try
            {
                //获取用户
                var _user = _SysAccountService.Login(account, MD5Util.GetMD5_32(oldPassword));
                if (_user == null)
                {
                    //获取用户失败
                    return Json(new { Status = Successed.Empty }, JsonRequestBehavior.AllowGet);
                }
                _user.PassWord = MD5Util.GetMD5_32(newPassword);
                //修改密码
                _SysAccountService.Update(_user);

                //修改密码成功，清除缓存
                Loginer.DelAccountCache();
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult StudentList(SysAccountVM _SysAccountVM, int pn = 1)
        {
            int totalCount,
              pageIndex = pn,
              pageSize = PagingConfig.PAGE_SIZE;
            var list = _SysAccountService.GetManagerStudentList(_SysAccountVM.QueryName, _SysAccountVM.QueryPhoneNo, (int)EnumHelp.RoleTypeEnum.学生,LibrarySystem.Admin.Common.Loginer.UniversityId,  pageIndex, pageSize, out totalCount).ToList();
            if (_SysAccountVM.QueryUId>0)
            {
                list = list.Where(c => c.UniversityId == _SysAccountVM.QueryUId).ToList();
            }
            var paging = new Paging<SysAccount>()
            {
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            paging.Items = list;

            var getlist = _universityService.GetAllList().Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            var ulist = new List<University>();
            foreach (var item in getlist)
            {
                ulist.Add(new University()
                {
                    UniversityId = item.UniversityId,
                    Name = item.Name
                });
            }
            //_SysAccountVM.SysRoles = _SysRoleService.GetAll();

            _SysAccountVM.UinversityList = ulist;

            _SysAccountVM.Paging = paging;
            return View(_SysAccountVM);
        }


        public ActionResult StudentEdit(SysAccountVM _SysAccountVM)
        {
            if (_SysAccountVM.Id<=0)
            {
                _SysAccountVM.Id = LibrarySystem.Admin.Common.Loginer.AccountId;
            }
            _SysAccountVM.SysAccount = _SysAccountService.GetById(_SysAccountVM.Id) ?? new SysAccount();
            _SysAccountVM.ImgInfo = _SysAccountVM.SysAccount.BaseImage ?? new BaseImage();

            _SysAccountVM.SysRoles = new List<SysRole>();
            _SysAccountVM.UinversityList = new List<University>();
            return View(_SysAccountVM);
        }
        [HttpPost]
        public JsonResult StudentEdit(SysAccount model)
        {
            try
            {
                if (model.SysAccountId > 0)
                {
                    var entity = _SysAccountService.GetById(model.SysAccountId);
                    if (!entity.Account.Equals(model.Account))
                    {
                        if (_SysAccountService.GetAccountByAccountUid(model.Account, model.UniversityId) != null)
                        {
                            return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    entity.EditTime = DateTime.Now;
                    entity.NickName = model.NickName;
                    entity.Email = model.Email;
                    entity.Address = model.Address;
                    entity.MobilePhone = model.MobilePhone;
                    entity.BaseImageId = model.BaseImageId;
                    entity.Remarks = string.IsNullOrWhiteSpace(model.Remarks) ? "" : model.Remarks;
                    _SysAccountService.Update(entity);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}