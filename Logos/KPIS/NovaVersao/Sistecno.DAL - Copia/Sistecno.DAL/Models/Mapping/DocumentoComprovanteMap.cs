using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoComprovanteMap : EntityTypeConfiguration<DocumentoComprovante>
    {
        public DocumentoComprovanteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoComprovante);

            // Properties
            this.Property(t => t.IdDocumentoComprovante)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoComprovante");
            this.Property(t => t.IdDocumentoComprovante).HasColumnName("IdDocumentoComprovante");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdDocumentoNotaFiscal).HasColumnName("IdDocumentoNotaFiscal");

            // Relationships
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.DocumentoComprovantes)
                .HasForeignKey(d => d.IdDocumento);
            this.HasOptional(t => t.Documento1)
                .WithMany(t => t.DocumentoComprovantes1)
                .HasForeignKey(d => d.IdDocumentoNotaFiscal);

        }
    }
}
