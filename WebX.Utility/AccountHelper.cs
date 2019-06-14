using System;
using System.Collections.Generic;
using System.Text;

namespace WebX.Utility
{
    public class AccountHelper
    {
        public static string CreateUserId()
        {
            Random random = new Random();

            string userid = "hello" + random.Next(1000, 9999);
            return userid;
        }
    }
}
