using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_BorderoTituloDuplicataMap : EntityTypeConfiguration<ZID_BorderoTituloDuplicata>
    {
        public ZID_BorderoTituloDuplicataMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_BorderoTituloDuplicata");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
