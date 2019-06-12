using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebX.MODEL;

namespace WebX.DbCONT
{
    public class MySqlContext:DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }

        public virtual DbSet<AccountMD> AccountMD { get; set; }
    }
}
