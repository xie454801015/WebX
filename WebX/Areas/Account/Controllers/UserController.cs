using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebX.COMMON;
using WebX.MODEL;

namespace WebX.Areas.Account.Controllers
{
    [Area("Account")]
    public class UserController : Controller
    {

        WebX.BLL.AccountBLL accountBusiness = new WebX.BLL.AccountBLL();
        public UserController(IOptions<DBsetting> configurationOption)
        {
            //将数据库连接string 存储在DbHelperMysql的静态属性中
            WebX.COMMON.DbHelperMySql.ConnString = configurationOption.Value.MySqlConnection;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountMD thedata)
        {
            bool blnResult = false;
            string a ="defeat";
            try
            {
                blnResult = accountBusiness.Add(thedata);
                if (blnResult)
                {
                    a = "success";

                }
                return Json(a);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return Json(a);
            }
        }

        [HttpPost]
        public IActionResult IsOccupied(string field, string fieldValue)
        {
            return Json(field + fieldValue);
        }

    }
}