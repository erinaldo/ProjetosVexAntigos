using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TituloDuplicataMap : EntityTypeConfiguration<ZID_TituloDuplicata>
    {
        public ZID_TituloDuplicataMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TituloDuplicata");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
