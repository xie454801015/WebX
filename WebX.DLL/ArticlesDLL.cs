using System;
using WebX.DAL;


namespace WebX.DLL
{
    public class ArticlesDLL
    {   

        //创建一个DAL实例，输入的参数为表名字
        WebX.DAL.ArticlesDBL dal = new WebX.DAL.ArticlesDBL("article");


        /// <summary>
        /// 获取数据总条数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetCount(string strWhere)
        {
            try
            {
                return dal.GetCount(strWhere);
            }
            //catch (Exception ex)
            catch
            {
                return 0;
            }
        }

        public 

    }
}
