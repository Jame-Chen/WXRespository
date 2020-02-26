using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data
{

    public class DepartmentTypeConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentTypeConfiguration()
        {
            HasKey(c => c.ID);
            Property(c => c.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         
            ToTable("Department");
        }
    }
}
