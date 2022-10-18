using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoObservacaoMap : EntityTypeConfiguration<DocumentoObservacao>
    {
        public DocumentoObservacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoObservacao);

            // Properties
            this.Property(t => t.IDDocumentoObservacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Observacao)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("DocumentoObservacao");
            this.Property(t => t.IDDocumentoObservacao).HasColumnName("IDDocumentoObservacao");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.Observacao).HasColumnName("Observacao");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoObservacaos)
                .HasForeignKey(d => d.IDDocumento);

        }
    }
}
