using System;
using System.Collections.Generic;
using System.Text;
using WebX.MODEL;
using MySql.Data.MySqlClient;

namespace WebX.BLL
{
    public class AccountBLL
    {
        WebX.DAL.AccountDAL dal = new WebX.DAL.AccountDAL();

        //WebX.DAL.AccountDAL dal = new WebX.DAL.AccountDAL("accounts");
        public bool Add(WebX.MODEL.AccountMD thedata)
        {
            using (MySqlConnection conn = new MySqlConnection(WebX.COMMON.DbHelperMySql.ConnString))
            {
                conn.Open();
                using (MySqlTransaction scope = conn.BeginTransaction())
                {
                    try
                    {
                        if (dal.Add(conn,thedata))
                        {
                            scope.Commit();
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        scope.Rollback();
                        conn.Close();
                        throw(e);
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
            return false;

        }

    }
}
