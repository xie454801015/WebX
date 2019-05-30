using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebX.COMMON;
using WebX.Utility;
using WebX.BLL;
using Newtonsoft.Json;

namespace WebX.Areas.Article.Controllers
{   
    [Area("Article")]
    public class ArticlesController : Controller
    {

        WebX.BLL.ArticlesBLL articlesBusiness = new WebX.BLL.ArticlesBLL();


        
        public ArticlesController(IOptions<DBsetting> configurationOption)
        {   
            //将数据库连接string 存储在DbHelperMysql的静态属性中
            WebX.COMMON.DbHelperMySql.ConnString = configurationOption.Value.MySqlConnection;
        }
        #region 视图功能
        public IActionResult Index()
        {
            ////检查 Connstring 是否正常获取
            //string a = WebX.COMMON.DbHelperMySql.Connstring;

            return View();
        }

        public IActionResult Form(string id)
        {
            try
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 查询数据（已调试基本无问题）；
        public IActionResult GetDataLiset(Pagination pagination)
        {
            //由请求自动反序列化生成实例输入。自动匹配熟悉；
            try
            {
                var a = Request.Form["title"];
                List<object> data = new List<object>();
                string strWhere = "";
                if (Request.Form["title"].ToString().IsNotNullOrEmpty())
                    strWhere += $" TITLE like '%{ Request.Form["title"]}%'";
                pagination.OrderField = "LineNo";
                var dataList = articlesBusiness.GetPageList(strWhere, pagination);
                if (dataList != null)
                {
                    string jsonlist = JsonConvert.SerializeObject(pagination.BuildTablerResult_DataTable(dataList));
                    return Json(jsonlist);
                    //return Content(jsonlist);
                    //return Json(data,JsonRequestBehavior.AllowGet);
                }
                else { return null; }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 增改数据
        public IActionResult SaveData(MODEL.ArticlesMD theData)
        {
            bool blnResult = false;
            try
            {
                //如何ID是0，则为增加数据
                if (theData.LineNo == 0)
                {
                    blnResult = articlesBusiness.Add(theData);
                }
                // 不为0，更改数据
                else
                {
                    blnResult = articlesBusiness.Update(theData);
                }
                string a = "success";
                return Json(a);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 删除数据
        public ActionResult DeleteData(string ids)
        {
            try
            {
                var idlist = JsonConvert.DeserializeObject<Array>(ids);
                if (articlesBusiness.DeleteList(string.Join(",", idlist)))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
}