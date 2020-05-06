using Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    [Table("Article")]
    public class Article : Entity
    {
        [StringLength(100)]
        public string Url { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(20)]
        public string Author { get; set; }
        public int ReadNum { get; set; }
        public int LikeNum { get; set; }
        [StringLength(300)]
        public string PicUrl { get; set; }
        [StringLength(20)]
        public string PubTime { get; set; }
        [StringLength(20)]
        public string WXId { get; set; }
    }
}
