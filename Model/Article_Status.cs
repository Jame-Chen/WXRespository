using Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    [Table("Article_Status")]
    public  class Article_Status:Entity
    {
        [StringLength(50)]
        public string article_id { get; set; }
        public int article_status { get; set; }
        public int article_read_number { get; set; }
        public int article_read_max { get; set; }
        [ForeignKey("article_id")]
        public virtual Article_Record Article_Record { get; set; }
    }
}
