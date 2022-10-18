using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TituloDuplicataremessaMap : EntityTypeConfiguration<ZID_TituloDuplicataremessa>
    {
        public ZID_TituloDuplicataremessaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TituloDuplicataremessa");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
