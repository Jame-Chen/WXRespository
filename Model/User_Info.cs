using System;
using System.Collections.Generic;
using System.Text;
using Model.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("User_Info")]
    public class User_Info : Entity
    {
        [StringLength(200)]
        public string wechat_code { get; set; }
        [StringLength(500)]
        public string profile_pic { get; set; }
        [StringLength(100)]
        public string user_nickname { get; set; }
        [StringLength(11)]
        public string user_phone { get; set; }

        public int fatiecard_nm { get; set; }
        [StringLength(200)]
        public string invite_code { get; set; }
    }
}
