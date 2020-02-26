using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data
{

    public class MenuTypeConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuTypeConfiguration()
        {
            HasKey(c => c.ID);
            Property(c => c.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         
            ToTable("Menu");
        }
    }
}
