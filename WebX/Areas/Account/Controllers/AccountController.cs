using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebX.COMMON;
using WebX.DbCONT;
using WebX.MidWares;
using WebX.MODEL;
using WebX.Utility;

namespace WebX.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : BaseController
    {

        private readonly MySqlContext _context;
        public AccountController(MySqlContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult GetVerifyCode()
        {
            string code = null;
            Byte[] img= VerifyCode.CreatePic(out code).ToArray();
            
            return File(img,@"image/jpeg");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="searchString"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken] // 防止csrf攻击
        public IActionResult GetList(string sortOrder,string searchString,string currentFilter, int? pageNumber)
        {
            var accounts = from user in _context.AccountMD select user;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            //搜索内容过滤
            if (searchString.IsNotNullOrEmpty())
            {
                accounts = accounts.Where(user => user.UserNickname.Contains(searchString) || user.UserName.Contains(searchString));
            }
            //排序
            switch (sortOrder)
            {
                case "LineNO":
                    accounts = accounts.OrderByDescending(user =>user.LineNo);
                    break;
                case "UserID":
                    accounts = accounts.OrderByDescending(user => user.UserId);
                    break;
                default:
                    accounts = accounts.OrderByDescending(user => user.RegisterTime);
                    break;
            }
            int pageSize = 3;
            return View(PaginatedList<AccountMD>.CreateAsync(accounts.AsNoTracking(), pageNumber ?? 1, pageSize));
            //要将数据转换

        }

        [HttpPost]
        //[ValidateAntiForgeryToken] // 防止csrf攻击
        public IActionResult Add(AccountMD account)
        {
            try
            {
                if (ModelState.IsValid)
                {   
                    _context.Add(account);

                    _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return null;
        }

        [HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken] // 防止csrf攻击
        public IActionResult Update(int LineNO,AccountMD account)
        {
            if (LineNO !=account.LineNo)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return null;
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken] // 防止csrf攻击
        public IActionResult Delete(int LineNO)
        {
            try
            {
                AccountMD accountToDelete = new AccountMD() { LineNo = LineNO};
                _context.Entry(accountToDelete).State = EntityState.Deleted;
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { LineNO = LineNO, saveChangesError = true });
            }
        }

    }
}