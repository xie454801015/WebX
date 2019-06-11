using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebX.DbCONT
{
    public class BaseContext:DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        { }
    }
}
