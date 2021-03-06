﻿using Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("User")]
    public class User : Entity
    {
        /// <summary>
        /// 用户登录帐号
        /// </summary>
        [Description("用户登录帐号")]
        [StringLength(50)]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        [StringLength(50)]
        public string Password { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Description("用户姓名")]
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Description("性别")]
        public int Sex { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        [Description("用户状态")]
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public Nullable<DateTime> CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Description("创建人")]
        [StringLength(50)]
        public string CreateId { get; set; }
    }
}
