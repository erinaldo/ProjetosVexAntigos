using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoReclamacaoMap : EntityTypeConfiguration<DocumentoReclamacao>
    {
        public DocumentoReclamacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoReclamacao);

            // Properties
            this.Property(t => t.IdDocumentoReclamacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoReclamacao");
            this.Property(t => t.IdDocumentoReclamacao).HasColumnName("IdDocumentoReclamacao");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdReclamacao).HasColumnName("IdReclamacao");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.DataDaReclamacao).HasColumnName("DataDaReclamacao");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoReclamacaos)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
