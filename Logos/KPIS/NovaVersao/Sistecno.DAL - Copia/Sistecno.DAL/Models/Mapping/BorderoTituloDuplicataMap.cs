using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BorderoTituloDuplicataMap : EntityTypeConfiguration<BorderoTituloDuplicata>
    {
        public BorderoTituloDuplicataMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBorderoTituloDuplicata);

            // Properties
            this.Property(t => t.IDBorderoTituloDuplicata)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("BorderoTituloDuplicata");
            this.Property(t => t.IDBorderoTituloDuplicata).HasColumnName("IDBorderoTituloDuplicata");
            this.Property(t => t.IDBordero).HasColumnName("IDBordero");
            this.Property(t => t.IDTituloDuplicata).HasColumnName("IDTituloDuplicata");

            // Relationships
            this.HasRequired(t => t.Bordero)
                .WithMany(t => t.BorderoTituloDuplicatas)
                .HasForeignKey(d => d.IDBordero);
            this.HasRequired(t => t.TituloDuplicata)
                .WithMany(t => t.BorderoTituloDuplicatas)
                .HasForeignKey(d => d.IDTituloDuplicata);

        }
    }
}
