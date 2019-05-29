using System;
using System.Data;
using System.Text;
using WebX.COMMON;
using MySql.Data.MySqlClient;
using WebX.MODEL;

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

        public bool Add(MySqlConnection conn, MODEL.ArticlesMD model)
        {
            StringBuilder strSql = new StringBuilder();
            int n = 0;
            strSql.Append("insert into " + table_name+" (");
            //添加插入字段
            strSql.Append(" ");
            strSql.Append(") value (");
            strSql.Append(" @LineNO,@Type,@Title,@Content,@FillTime,@FillOper,@FillOperName");
            strSql.Append(") ");

            //构建MySqlParameter
            MySqlParameter[] parameters = {
                new MySqlParameter("@LineNO",MySqlDbType.Int32),
                new MySqlParameter("@Type",MySqlDbType.Int32),
                new MySqlParameter("@Title",MySqlDbType.VarChar,256),
                new MySqlParameter("@Content",MySqlDbType.VarChar,4000),
                new MySqlParameter("@FillTime",MySqlDbType.Date),
                new MySqlParameter("@FilleOper",MySqlDbType.VarChar,256),
                new MySqlParameter("@FillOperName",MySqlDbType.VarChar,256),
            };
            parameters[n++].Value = model.LineNo;
            parameters[n++].Value = model.Type;
            parameters[n++].Value = model.Title;
            parameters[n++].Value = model.Content;
            parameters[n++].Value = model.FillTime;
            parameters[n++].Value = model.FillOper;
            parameters[n++].Value = model.FillOperName;

            int rows = DbHelperMySql.ExcuteNonQuery(conn, strSql.ToString(), parameters);
            return true;
        }


        public bool Update(MySqlConnection conn, MODEL.ArticlesMD model)
        {
            StringBuilder strSql = new StringBuilder();
            int n = 0;
            strSql.Append("updatae " + table_name + " set ");
            //strSql.Append("LineNO = @LineNO ,");
            strSql.Append("Type = @Type ,");
            strSql.Append("Title = @Title ,");
            strSql.Append("Content = @Content ");
            //strSql.Append("FillTime = @FillTime ,");
            //strSql.Append("FillOper = @FillOper ,");
            //strSql.Append("FillOperName = @FillOperName ");
            strSql.Append("where LineNO = @LineNO");

            MySqlParameter[] parameters = {
                new MySqlParameter("@Type",MySqlDbType.Int32),
                new MySqlParameter("@Title",MySqlDbType.VarChar,256),
                new MySqlParameter("@Content",MySqlDbType.VarChar,4000),
                new MySqlParameter("@LineNO",MySqlDbType.Int32),
            };
            parameters[n++].Value = model.Type;
            parameters[n++].Value = model.Title;
            parameters[n++].Value = model.Content;
            parameters[n++].Value = model.LineNo;
            DbHelperMySql.ExcuteNonQuery(conn, strSql.ToString(), parameters);

            return true;
        }
    }
}
