using Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table("Menu")]
    public class Menu : Entity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Description("菜单名称")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// </summary>
        [Description("上级菜单Id")]
        public int ParentId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Description("图标")]
        [StringLength(50)]
        public string Icon { get; set; }
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
        [StringLength(50)]
        public string CreateId { get; set; }
    }
}
