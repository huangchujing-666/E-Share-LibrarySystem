
using LibrarySystem.Admin.Common;
using LibrarySystem.Core.Utils;
using LibrarySystem.Domain.Model;
using LibrarySystem.IService;
using LibrarySystem.Admin.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Exam.Common;
using LibrarySystem.Domain;
using LibrarySystem.Common;
using LibrarySystem.Models;

namespace LibrarySystem.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private readonly ISysAccountService _sysAccountService;
        private readonly IUniversityService _universityService;
        private readonly IEmailCodeService _emailCodeService;
        public LoginController(ISysAccountService sysAccountService,
            IUniversityService universityService, IEmailCodeService emailCodeService)
        {
            _sysAccountService = sysAccountService;
            _universityService = universityService;
            _emailCodeService = emailCodeService;
        }

        public ActionResult Login()
        {
            RegisterVM rvm = new RegisterVM();
            List<University> ulist = _universityService.GetAllList();//.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            if (ulist != null && ulist.Count > 0)
            {
                rvm.UinversityList = ulist;
            }
            else
                rvm.UinversityList = new List<University>();
            return View(rvm);
        }


        public ActionResult BackEnd()
        {
            RegisterVM rvm = new RegisterVM();
            List<University> ulist = _universityService.GetAllList();//.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            if (ulist != null && ulist.Count > 0)
            {
                rvm.UinversityList = ulist;
            }
            else
                rvm.UinversityList = new List<University>();
            return View(rvm);
        }


        public ActionResult LoginTest()
        {
            RegisterVM rvm = new RegisterVM();
            List<University> ulist = _universityService.GetAllList();//.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            if (ulist != null && ulist.Count > 0)
            {
                rvm.UinversityList = ulist;
            }
            else
                rvm.UinversityList = new List<University>();
            return View(rvm);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string account = "", string password = "", int RoleId = 0, int UniversityId = 0)
        {
            SysAccount info = new SysAccount();
            if (RoleId == (int)EnumHelp.RoleTypeEnum.学生 || RoleId == (int)EnumHelp.RoleTypeEnum.管理员)
            {
                if (UniversityId <= 0)
                {
                    return Json(new { Status = -3 }, JsonRequestBehavior.AllowGet);
                }
                info = _sysAccountService.Login(account, MD5Util.GetMD5_32(password), UniversityId);
            }
            else
            {
                info = _sysAccountService.Login(account, MD5Util.GetMD5_32(password));
            }

            if (info == null)
            {
                //无此账号信息
                return Json(new { Status = -1 }, JsonRequestBehavior.AllowGet);
            }
            if (info.Status == 0)
            {
                //该账号被禁用
                return Json(new { Status = -2 }, JsonRequestBehavior.AllowGet);
            }
            var imgInfo = info.BaseImage ?? new Domain.Model.BaseImage();
            //缓存用户信息
            SessionHelper.Add(LoginerConst.ACCOUNT_ID, info.SysAccountId.ToString());
            SessionHelper.Add(LoginerConst.ACCOUNT, info.Account);
            SessionHelper.Add(LoginerConst.NICKNAME, info.NickName);
            SessionHelper.Add(LoginerConst.ACCOUNT_IMG, imgInfo.Source + imgInfo.Path);
            SessionHelper.Add(LoginerConst.ROLE_ID, info.SysRoleId.ToString());
            SessionHelper.Add(LoginerConst.ROLE_NAME, info.SysRole == null ? "" : info.SysRole.Name);
            //SessionHelper.Add(LoginerConst.BUSINESS_ID, info.BusinessInfoId.ToString());
            SessionHelper.Add(LoginerConst.UNIVERSITY_ID, info.UniversityId.ToString());
            SessionHelper.Add(LoginerConst.UNIVERSITY_NAME, info.University == null ? "" : info.University.Name);
            return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            bool IsAdmin = LibrarySystem.Admin.Common.Loginer.RoleId == (int)EnumHelp.RoleTypeEnum.系统管理员 ? true : false;
            Loginer.DelAccountCache();
            if (IsAdmin)
            {
                return Redirect("/Login/BackEnd");
            }
            return Redirect("/Login/Login");
        }
        public ActionResult RegisterTest()
        {
            RegisterVM rvm = new RegisterVM();
            List<University> ulist = _universityService.GetAllList();//.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            if (ulist != null && ulist.Count > 0)
            {
                rvm.UinversityList = ulist;
            }
            else
                rvm.UinversityList = new List<University>();
            return View(rvm);
        }
        public ActionResult Register()
        {
            RegisterVM rvm = new RegisterVM();
            List<University> ulist = _universityService.GetAllList();//.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            if (ulist != null && ulist.Count > 0)
            {
                rvm.UinversityList = ulist;
            }
            else
                rvm.UinversityList = new List<University>();
            return View(rvm);
        }


        public ActionResult ResetPassword()
        {
            RegisterVM rvm = new RegisterVM();
            List<University> ulist = _universityService.GetAllList();//.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效);
            if (ulist != null && ulist.Count > 0)
            {
                rvm.UinversityList = ulist;
            }
            else
                rvm.UinversityList = new List<University>();
            return View(rvm);
        }

        [HttpPost]
        public ActionResult ResetPassword(SysAccountVM sysAccount)
        {
            var emailCode = _emailCodeService.GetByEmailWithType(sysAccount.Email, (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统重置密码发送验证码);
            if (emailCode == null || (emailCode != null && !sysAccount.Code.Equals(emailCode.Code)))
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);

            var obj = _sysAccountService.GetAccountByMultiCond(sysAccount.Account, sysAccount.UniversityId,sysAccount.Email);
            if (obj != null)
            {
                obj.PassWord = MD5Util.GetMD5_32(sysAccount.PassWord);
                obj.EditTime = System.DateTime.Now;
                try
                {
                    _sysAccountService.Update(obj);
                    return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
                }
                catch (System.Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Register(SysAccountVM sysAccount)
        {
            var obj = _sysAccountService.GetAccountByAccountUid(sysAccount.Account, sysAccount.UniversityId);
            if (obj != null)
            {
                return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
            }
            var objbyemail=_sysAccountService.GetByEmail(sysAccount.Email);
            if (objbyemail != null)
            {
                return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
            }
            var emailCode = _emailCodeService.GetByEmailWithType(sysAccount.Email, (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统注册验证码);
            if (emailCode==null||(emailCode != null && !sysAccount.Code.Equals(emailCode.Code)))
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            string pwd = MD5Util.GetMD5_32(sysAccount.PassWord);
            var model = new SysAccount()
            {
                Status = (int)EnumHelp.EnabledEnum.有效,
                IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                CreateTime = System.DateTime.Now,
                SysRoleId = (int)EnumHelp.RoleTypeEnum.学生,
                BaseImageId = 0,
                EditTime = System.DateTime.Now,
                NickName = sysAccount.Account,
                Email = sysAccount.Email,
                Remarks = "",
                Token = "",
                PassWord = pwd,
                Address = "",
                MobilePhone = "",
                UniversityId = sysAccount.UniversityId,
                Account = sysAccount.Account
            };
            var result = _sysAccountService.Insert(model);

            int Type = (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统;
            string subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统.ToString();
            string temp = EmailTemplate.EmailDictionary[Type];
            string body = temp.Replace("#Account#", result.Account);
            SendEmailHelp.SendMail(subject, body, result.Email, "", "", "");

            return Json(new { Status = result.SysAccountId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="Email">邮箱</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SendCode(string Email,int Type)
        {
            //验证邮箱
            if (string.IsNullOrWhiteSpace(Email))
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {

                    //获取邮件正文
                    string body = EmailTemplate.EmailDictionary[Type];//(int)EmailTemplate.EmailTemplete.E建漂流图书服务系统注册验证码
                    //验证码
                    string code = VerificationCode.CreateValidateNumber(6);
                    string sendString = string.Empty;
                    string subject = string.Empty;
                    switch (Type)
                    {
                        case (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统注册验证码:
                            sendString = body + code;
                            subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统注册验证码.ToString();
                            break;
                        case (int)EmailTemplate.EmailTemplete.E建漂流图书服务系统重置密码发送验证码:
                            sendString = body.Replace("#Code#", code);
                            subject = EmailTemplate.EmailTemplete.E建漂流图书服务系统重置密码发送验证码.ToString();
                            break;
                        default:
                            break;
                    }
                    //发送邮件
                    bool result = SendEmailHelp.SendMail(subject, sendString, Email, "", "", "");
                    EmailCode e = new EmailCode()
                    {
                        CreateTime = System.DateTime.Now,
                        Email = Email,
                        Code = code,
                        Type = Type
                    };
                    var emailresult = _emailCodeService.Insert(e);
                    return Json(new { Status = emailresult.EmailCodeId > 0 ? Successed.Ok : Successed.Error }, JsonRequestBehavior.AllowGet);
                }
                catch (System.Exception ex)
                {
                    return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}