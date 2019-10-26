using Sys.Reponsitory.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sys.Reponsitory.Domain.Model
{

    /// <summary>
    /// 文件
    /// </summary>
    [Table("UploadFile")]
    public partial class UploadFile : Entity
    {


        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
	    /// 文件路径
	    /// </summary>
        public string FilePath { get; set; }
        /// <summary>
	    /// 描述
	    /// </summary>
        public string Description { get; set; }
        /// <summary>
	    /// 文件类型
	    /// </summary>
        public string FileType { get; set; }
        /// <summary>
	    /// 文件大小
	    /// </summary>
        public long FileSize { get; set; }
        /// <summary>
	    /// 扩展名称
	    /// </summary>
        public string Extension { get; set; }
        /// <summary>
	    /// 是否可用
	    /// </summary>
        public bool Enable { get; set; }
        /// <summary>
	    /// 排序
	    /// </summary>
        public int SortCode { get; set; }
        /// <summary>
	    /// 删除标识
	    /// </summary>
        public bool DeleteMark { get; set; }
        /// <summary>
	    /// 上传人
	    /// </summary>
        public System.Guid? CreateUserId { get; set; }
        /// <summary>
	    /// 上传人姓名
	    /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
	    /// 上传时间
	    /// </summary>
        public Nullable< DateTime> CreateTime { get; set; }
        /// <summary>
	    /// 缩略图
	    /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
	    /// 所属表
	    /// </summary>
        public string OwnTable { get; set; }
        /// <summary>
	    /// 所属ID
	    /// </summary>
        public string OwnId { get; set; }

    }
}
