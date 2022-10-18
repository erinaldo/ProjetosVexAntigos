using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoNotaFiscalMap : EntityTypeConfiguration<DocumentoNotaFiscal>
    {
        public DocumentoNotaFiscalMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoNotaFiscal);

            // Properties
            this.Property(t => t.IDDocumentoNotaFiscal)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoNotaFiscal");
            this.Property(t => t.IDDocumentoNotaFiscal).HasColumnName("IDDocumentoNotaFiscal");
            this.Property(t => t.IDDocumentoOrigem).HasColumnName("IDDocumentoOrigem");
            this.Property(t => t.IDNotaFiscal).HasColumnName("IDNotaFiscal");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoNotaFiscals)
                .HasForeignKey(d => d.IDDocumentoOrigem);
            this.HasRequired(t => t.Documento1)
                .WithMany(t => t.DocumentoNotaFiscals1)
                .HasForeignKey(d => d.IDNotaFiscal);

        }
    }
}
