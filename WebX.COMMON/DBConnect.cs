using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace WebX.COMMON
{
    class DBConnect 
    {
        public string ConnectionString { get; set; }

        public DBConnect(string connectionstring)
        {
            this.ConnectionString = connectionstring;
        }
        // 返回一个连接数据库的对象
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
