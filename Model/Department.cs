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
    /// 部门表
    /// </summary>
    [Table("Department")]
    public class Department : Entity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [Description("部门名称")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 上级部门Id
        /// </summary>
        [Description("上级部门Id")]
        [StringLength(50)]
        public int ParentId { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        [Description("当前状态")]
        [StringLength(20)]
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public Nullable<DateTime> CreateTime { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        [Description("创建人ID")]
        [StringLength(50)]
        public string CreateId { get; set; }
    }
}
