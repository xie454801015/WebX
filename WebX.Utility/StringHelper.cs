using System;
using System.Collections.Generic;
using System.Text;

namespace WebX.Utility
{
    public  static class StringHelper
    {   
        /// <summary>
        /// 字符串是否不为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return str != null && str != string.Empty && str != "";
        }
    }
}
