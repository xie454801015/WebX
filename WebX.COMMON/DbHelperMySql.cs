using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WebX.COMMON
{
    public class DbHelperMySql
    {
        //数据库连接字符串
        private static string connstring;
        //private static string connstring = "server = localhost; port=3306;user=root;password=11111; database=xxxx;SslMode = none;charset='utf8';pooling=true";

        public static string ConnString{ get { return connstring; } set {connstring=value; } }

        // 用于缓存参数的HASH表
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// ExcuteReader查询
        /// </summary>
        /// <param name="connectionString">数据库连接string</param>
        /// <param name="mysqlString">mysql语句</param>
        /// <returns></returns>
        public static MySqlDataReader ExcutReader(string mysqlString)
        {   
            //新建一个数据库连接
            using (MySqlConnection conn = new MySqlConnection( ConnString))
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
        public static DataSet Query(string mysqlString)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnString))
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

        /// <summary>
        /// ExceuteScalar执行一条计算查询结果语句，返回查询结果（object），用于获取数量，结果建议用Conver.ToInt32转换；
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="mysqlString"></param>
        /// <returns></returns>
        public static object GetSingele(string mysqlString)
        {
            MySqlConnection connp = new MySqlConnection(ConnString);
            connp.Open();
            using (MySqlConnection conn = new MySqlConnection(ConnString))
            {
                using (MySqlCommand cmd = new MySqlCommand(mysqlString, conn))
                {
                    try
                    {
                        conn.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((object.Equals(obj, null)) || (object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 赠删改数据，使用 ExecuteNonQuery数据受影响条数
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="mysqlString"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static int ExcuteNonQuery(MySqlConnection conn,string mysqlString,MySqlParameter[] cmdParms)
        {   
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, conn, null, mysqlString, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
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
