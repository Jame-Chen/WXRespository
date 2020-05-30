using Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    [Table("Article_ReadHistory")]
    public class Article_ReadHistory : Entity
    {
        public string article_id { get; set; }
        public string user_id { get; set; }
    }
}
