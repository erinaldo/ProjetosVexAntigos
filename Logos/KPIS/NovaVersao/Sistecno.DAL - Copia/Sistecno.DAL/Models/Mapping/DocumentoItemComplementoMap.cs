using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoItemComplementoMap : EntityTypeConfiguration<DocumentoItemComplemento>
    {
        public DocumentoItemComplementoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoItemComplemento);

            // Properties
            this.Property(t => t.IdDocumentoItemComplemento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Complemento)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("DocumentoItemComplemento");
            this.Property(t => t.IdDocumentoItemComplemento).HasColumnName("IdDocumentoItemComplemento");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.Complemento).HasColumnName("Complemento");

            // Relationships
            this.HasRequired(t => t.DocumentoItem)
                .WithMany(t => t.DocumentoItemComplementoes)
                .HasForeignKey(d => d.IdDocumentoItem);

        }
    }
}
