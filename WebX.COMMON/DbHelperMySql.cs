using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WebX.COMMON
{
    class DbHelperMySql
    {
        //数据库连接字符串
        public static string connname = "server = localhost; port=3306;user=root;password=chenliji1; database=aspnet;SslMode = none;charset='utf8';pooling=true";

        // 用于缓存参数的HASH表
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// ExcutReader查询
        /// </summary>
        /// <param name="connectionString">数据库连接string</param>
        /// <param name="mysqlString"></param>
        /// <returns></returns>
        public static MySqlDataReader ExcutReader(string connectionString,string mysqlString)
        {   
            //新建一个数据库连接
            using (MySqlConnection conn = new MySqlConnection( connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(mysqlString,conn))
                {
                    try
                    {
                        MySqlDataReader msqdr = null;
                        //开启连接
                        conn.Open();
                        // CommandBehavior.CloseConnection 会让读取完之后关闭连接
                        msqdr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                        return msqdr;
                    }
                    catch (MySqlException e)
                    {
                        // 关闭连接,抛出异常
                        if (conn.State != System.Data.ConnectionState.Closed)
                        {
                            conn.Close();
                        } 
                        throw new Exception(e.Message);
                    }
                    
                }

            }
        }
        /// <summary>
        /// Dataset查询
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="mysqlString"></param>
        /// <returns></returns>
        public static DataSet Query(string connectionString,string mysqlString)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    using (MySqlDataAdapter cmd = new MySqlDataAdapter(mysqlString, conn))
                    {
                        cmd.Fill(ds, "ds");
                    }

                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
                return ds;
            }

        }

        public static int ExcuteNonQuery(MySqlConnection conn,string mysqlString)
        {
            int a = 0;
            return a;
        }

        private static void PrepareCommand(MySqlCommand cmd,MySqlConnection conn,MySqlTransaction trans,string mysqlString, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = mysqlString;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;//cmdType,默认为Text（CommandType.Text,CommandType.StoredProcedur与CommandType.TableDirect）
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }

        }
    }
}
