using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_VEICULOMap : EntityTypeConfiguration<ZID_VEICULO>
    {
        public ZID_VEICULOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_VEICULO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
