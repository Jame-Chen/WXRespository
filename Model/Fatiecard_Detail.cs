using System;
using System.Collections.Generic;
using System.Text;
using Model.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Fatiecard_Detail")]
    public  class Fatiecard_Detail:Entity
    {
        [StringLength(50)]
        public string user_id { get; set; }
        public int fatiecard_acquire_spend { get; set; }
        public int fatiecard_num { get; set; }
        public int fatiecard_channel { get; set; }
        [ForeignKey("user_id")]
        public virtual User_Info User_Info { get; set; }
    }
}
