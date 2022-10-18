using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoRemessaMap : EntityTypeConfiguration<DocumentoRemessa>
    {
        public DocumentoRemessaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoRemessa);

            // Properties
            this.Property(t => t.IdDocumentoRemessa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Situacao)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("DocumentoRemessa");
            this.Property(t => t.IdDocumentoRemessa).HasColumnName("IdDocumentoRemessa");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdUsuarioEnvio).HasColumnName("IdUsuarioEnvio");
            this.Property(t => t.DataEnvio).HasColumnName("DataEnvio");
            this.Property(t => t.IdUsuarioRecebeu).HasColumnName("IdUsuarioRecebeu");
            this.Property(t => t.DataRecebimento).HasColumnName("DataRecebimento");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
        }
    }
}
