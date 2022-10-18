using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoRelacionadoMap : EntityTypeConfiguration<DocumentoRelacionado>
    {
        public DocumentoRelacionadoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdDocumentoRelacionado, t.IdDocumentoPai });

            // Properties
            this.Property(t => t.IdDocumentoRelacionado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdDocumentoPai)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoRelacionado");
            this.Property(t => t.IdDocumentoRelacionado).HasColumnName("IdDocumentoRelacionado");
            this.Property(t => t.IdDocumentoPai).HasColumnName("IdDocumentoPai");
            this.Property(t => t.IdDocumentoFilho).HasColumnName("IdDocumentoFilho");
            this.Property(t => t.IdAgrupamento).HasColumnName("IdAgrupamento");

            // Relationships
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.DocumentoRelacionadoes)
                .HasForeignKey(d => d.IdDocumentoFilho);
            this.HasRequired(t => t.Documento1)
                .WithMany(t => t.DocumentoRelacionadoes1)
                .HasForeignKey(d => d.IdDocumentoPai);

        }
    }
}
