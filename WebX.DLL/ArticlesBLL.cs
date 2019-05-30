using System;
using System.Collections.Generic;
using System.Data;
using WebX.DAL;
using WebX.MODEL;
using WebX.Utility;
using WebX.COMMON;
using System.Linq;
using MySql.Data.MySqlClient;

namespace WebX.BLL
{
    public class ArticlesBLL
    {   

        //创建一个DAL实例，输入的参数为表名字
        WebX.DAL.ArticlesDBL dal = new WebX.DAL.ArticlesDBL("articles");


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


        /// <summary>
        /// 获取数据库查询集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public List<WebX.MODEL.ArticlesMD> GetPageList(string where, WebX.Utility.Pagination pagination)
        {
            try
            {
               
                string strWhere = "1=1";
                if (where.IsNotNullOrEmpty())
                {//@强制不转义
                    strWhere += $@" and {where}";
                }
                pagination.Records = GetCount(strWhere);
                DataSet pagelist = dal.GetPageList(strWhere, pagination.OrderField, pagination.PageIndex, pagination.PageRows);
                return DataSetHelper.DataSetToEntityList<MODEL.ArticlesMD>(pagelist, -1).ToList();

            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(WebX.MODEL.ArticlesMD model)
        {
            using (MySqlConnection conn = new MySqlConnection(WebX.COMMON.DbHelperMySql.ConnString))
            {
                conn.Open();
                // 创建事务对象
                using (MySqlTransaction scope = conn.BeginTransaction())
                {
                    try
                    {
                        if (dal.Add(conn, model))
                        {
                            scope.Commit();
                            return true;
                        }

                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return true;
        }

        public bool Update(WebX.MODEL.ArticlesMD model)
        {
            using (MySqlConnection conn = new MySqlConnection(WebX.COMMON.DbHelperMySql.ConnString))
            {
                conn.Open();
                // 创建事务对象
                using (MySqlTransaction scope = conn.BeginTransaction())
                {
                    try
                    {
                        if (dal.Update(conn, model))
                        {
                            scope.Commit();
                            return true;
                        }
                        else { return false; }

                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public bool DeleteList(string strLineNoList)
        {
            using (MySqlConnection conn = new MySqlConnection(WebX.COMMON.DbHelperMySql.ConnString))
            {
                conn.Open();
                // 创建事务对象
                using (MySqlTransaction scope = conn.BeginTransaction())
                {
                    try
                    {
                        if (dal.DeleteList(conn,strLineNoList))
                        {
                            scope.Commit();
                            return true;
                        }

                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return true;
        }

    }
}
