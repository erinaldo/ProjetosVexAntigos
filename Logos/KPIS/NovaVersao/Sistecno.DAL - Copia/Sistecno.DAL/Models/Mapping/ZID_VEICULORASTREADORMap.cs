using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_VEICULORASTREADORMap : EntityTypeConfiguration<ZID_VEICULORASTREADOR>
    {
        public ZID_VEICULORASTREADORMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_VEICULORASTREADOR");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
