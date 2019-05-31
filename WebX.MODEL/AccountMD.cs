using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebX.MODEL
{
    public class AccountMD
    {
        public AccountMD()
        {
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required, MaxLength(256)]
        public string Username { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required, DataType(DataType.Password), MaxLength(256)]
        public string Password { get; set; }

        /// <summary>
        /// 用户Email
        /// </summary>
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// 用户性别 0为女性，1为男性
        /// </summary>
        public string Sex { get; set; }

        public DateTime CreatTime { get; set; }
    }
 
}


