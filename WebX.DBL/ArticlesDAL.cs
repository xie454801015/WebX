using System;
using System.Data;
using System.Text;
using WebX.COMMON;

namespace WebX.DAL
{
    public partial class ArticlesDBL
    {

        private string table_name;
        public ArticlesDBL(string tableName)
        {
            table_name = tableName;
        }

        //public bool Exists(string field,var field_value)
        //{
        //    StringBuilder mysqlString = new StringBuilder();
        //    mysqlString.Append("select count(1) from");
        //    mysqlString.Append(" "+ table_name+" where");
        //    mysqlString.Append(blank+field+" = :")
        //}

        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetCount(string strWhere)
        {
            StringBuilder mysqlString = new StringBuilder();
            mysqlString.Append("select count(1)");
            mysqlString.Append(" from " + table_name);
            mysqlString.Append(" where 1=1 ");
            // 查看strWhere是否有效
            if (strWhere.Trim() != "")
            {
                mysqlString.Append(" and " + strWhere);
            }
            return Convert.ToInt32(DbHelperMySql.GetSingele(mysqlString.ToString()));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWherr"></param>
        /// <param name="rows">每页行数</param>
        /// <param name="page">页数</param>
        /// <param name="rows">每页行数</param>
        /// <returns></returns>
        public DataSet GetPageList(string strWhere, string  orderfiled, int page, int rows)
        {
            StringBuilder mysqlString = new StringBuilder();
            mysqlString.Append("select * from " + table_name + " where " + strWhere + " order by " + orderfiled + " limit ");
            mysqlString.Append($"{ (page - 1) * rows},{rows}");

            //mysqlString.Append("SELECT * FROM ( SELECT A.*, ROWUNM RN FORM ( SELECT * FROM vw_T_HELP_LINE ) A WHERE ROWNUM <=20 )WHREN RN>=10");
            return DbHelperMySql.Query(mysqlString.ToString());
        }

    }
}
