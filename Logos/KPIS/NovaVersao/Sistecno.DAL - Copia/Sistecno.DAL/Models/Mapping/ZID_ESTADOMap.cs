using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ESTADOMap : EntityTypeConfiguration<ZID_ESTADO>
    {
        public ZID_ESTADOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ESTADO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
