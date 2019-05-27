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
        public DBsetting Configuration { get; set; }

        public ArticlesController(IOptions<DBsetting> configurationOption)
        {
            this.Configuration = configurationOption.Value;
        }
        public IActionResult Index()
        {
            string a = Configuration.MySqlConnection;
            Console.WriteLine(a);
            return View();
        }
    }
}