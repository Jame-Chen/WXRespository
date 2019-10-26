using Sys.Reponsitory.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sys.Reponsitory.Domain.Model
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table("Role")]
    public class Role : Entity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Description("角色名称")]
        public string Name { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        [Description("当前状态")]
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
        public string CreateId { get; set; }
    }
}
