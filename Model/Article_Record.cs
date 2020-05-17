using Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    [Table("Article_Record")]
    public class Article_Record : Entity
    {
        [StringLength(500)]
        [Required]
        public string article_url { get; set; }
        [StringLength(40)]
        [Required]
        public string article_title_auto { get; set; }
        [StringLength(40)]
        public string article_title_self { get; set; }
        [StringLength(500)]
        [Required]
        public string article_cdnurl_auto { get; set; }
        [StringLength(500)]
        public string article_cdnurl_self { get; set; }
        [StringLength(50)]
        [Required]
        public string article_userid { get; set; }
  
    }
}
