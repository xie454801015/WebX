using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebX.MODEL;
using System.Data.SqlClient;
using MySql.Data;

namespace WebX.DbAccess
{
    public class MySqlContext:DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
            
        }

        public virtual DbSet<AccountMD> AccountMD { get; set; }
    }
}
