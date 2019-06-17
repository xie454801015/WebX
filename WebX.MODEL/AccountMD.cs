using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebX.MODEL
{

    [Table("accounts")]
    public class AccountMD
    {
        public enum Gender
        {   
            female=0,
            male=1,
            secret=2,
        };


        public AccountMD()
        {
            UserType = 1;
        }
        /// <summary>
        /// 流水号
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("line_no")]
        public int LineNo { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Column("user_name")]
        [Required, MaxLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称/绰号
        /// </summary>
        [Column("user_nickname")]
        public string UserNickname
        { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Column("user_password")]
        [Required, DataType(DataType.Password), MaxLength(256)]
        public string Password { get; set; }

        /// <summary>
        /// 用户Email
        /// </summary>
        [Column("user_email")]
        [Required, DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        /// <summary>
        /// 用户性别 0为女性，1为男性,2被保密
        /// </summary>
        [Column("user_sex")]
        public Gender? UserSex { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [Column("user_type")]
        public int UserType { get; set; }

        /// <summary>
        /// 用户注册时间
        /// </summary>
        [Column("register_time")]
        public DateTime RegisterTime { get; set; }
    }
 
}


