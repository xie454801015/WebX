using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebX.DbAccess.Interface;
using WebX.MidWares;
using WebX.MODEL;
using WebX.Utility;

namespace WebX.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : BaseController
    {

        private  IAccount _accountBLL;
        public AccountController(IAccount accountBLL)
        {
            _accountBLL = accountBLL;
        }

        #region 获取视图
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        #region 功能类接口
        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <returns></returns>
        public IActionResult GetVerifyCode()
        {
            string code = null;
            Byte[] img = VerifyCode.CreatePic(out code).ToArray();

            return File(img, @"image/jpeg");
        }

        public IActionResult SignUp(AccountMD account)
        {
            bool resultState = false;
            if (ModelState.IsValid)
            {
                //初始化用户昵称
                if (!account.UserNickname.IsNotNullOrEmpty())
                {
                    account.UserNickname = "用户_" + account.UserName;
                }
                account.RegisterTime = DateTime.Now;
                account.UserId = AccountHelper.CreateUserId();

                _accountBLL.CreateAccount(account);
            }

            return Json(resultState);
        }

        public IActionResult Check()
        {
            var sq = Request.Form;
            
            string key = Request.Form["key"];
            string value = Request.Form["value"];
            string sql = "select *from accounts where " + key + " = " + value;
            int count = 0;
            return Json(count);
        }

        public IActionResult SetSession()
        {
            HttpContext.Session.Set("apptest", Encoding.UTF8.GetBytes("apptestvalue"));
            return null;
        }
        public IActionResult GetSession()
        {
            byte[] temp;
            if (HttpContext.Session.TryGetValue("apptest", out temp))
            {
                ViewData["Redis"] = Encoding.UTF8.GetString(temp);
            }

            return null;
        }

        #endregion

    }
}