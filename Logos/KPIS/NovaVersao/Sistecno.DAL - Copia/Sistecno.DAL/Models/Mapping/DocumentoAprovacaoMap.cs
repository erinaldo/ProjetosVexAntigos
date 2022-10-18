using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoAprovacaoMap : EntityTypeConfiguration<DocumentoAprovacao>
    {
        public DocumentoAprovacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoAprovacao);

            // Properties
            this.Property(t => t.IdDocumentoAprovacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoAprovacao");
            this.Property(t => t.IdDocumentoAprovacao).HasColumnName("IdDocumentoAprovacao");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.UltimaSequenciaAprovacao).HasColumnName("UltimaSequenciaAprovacao");
            this.Property(t => t.UltimoIdUsuario).HasColumnName("UltimoIdUsuario");
        }
    }
}
