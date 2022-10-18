using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_VEICULOMODELOMap : EntityTypeConfiguration<ZID_VEICULOMODELO>
    {
        public ZID_VEICULOMODELOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_VEICULOMODELO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
