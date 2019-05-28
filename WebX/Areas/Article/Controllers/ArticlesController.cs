using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebX.COMMON;

namespace WebX.Areas.Article.Controllers
{   
    [Area("Article")]
    public class ArticlesController : Controller
    {

        public ArticlesController(IOptions<DBsetting> configurationOption)
        {   
            //将数据库连接string 存储在DbHelperMysql的静态属性中
            WebX.COMMON.DbHelperMySql.ConnString = configurationOption.Value.MySqlConnection;
        }
        public IActionResult Index()
        {
            ////检查 Connstring 是否正常获取
            //string a = WebX.COMMON.DbHelperMySql.Connstring;

            return View();
        }

        
        public IActionResult GetDataLiset()
        {
            //构造泛型转换成Json
            try
            {
                List<object> data = new List<object>();
                string strWhere = "";
                strWhere += $" TITLE like '%{ Request.Form["title"]}%'";
                // 判断字符串非空
                //if (Request.Form["title"])
                //{
                //    strWhere += $" TITLE like '%{ Request["title"]}%'";
                //}
                return Json(data);
                //return Json(data,JsonRequestBehavior.AllowGet);

            }

            catch
            {
                return null;
            }



        }
    }
}