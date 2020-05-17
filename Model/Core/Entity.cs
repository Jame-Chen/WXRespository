using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Core
{
    public abstract class Entity
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        public DateTime gmt_create { get; set; }
        public DateTime gmt_modified { get; set; }
        public int is_delete { get; set; }

        public Entity()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.gmt_create = DateTime.Now;
            this.gmt_modified = DateTime.Now;
            this.is_delete = 0;
        }
    }
}
