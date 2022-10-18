using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoAteItemMap : EntityTypeConfiguration<DocumentoAteItem>
    {
        public DocumentoAteItemMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDDocumentoAteItem, t.IDDocumentoAte });

            // Properties
            this.Property(t => t.IDDocumentoAteItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDDocumentoAte)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDocumento)
                .HasMaxLength(50);

            this.Property(t => t.NumeroDoc)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("DocumentoAteItem");
            this.Property(t => t.IDDocumentoAteItem).HasColumnName("IDDocumentoAteItem");
            this.Property(t => t.IDDocumentoAte).HasColumnName("IDDocumentoAte");
            this.Property(t => t.TipoDocumento).HasColumnName("TipoDocumento");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.IDEstado).HasColumnName("IDEstado");
            this.Property(t => t.ValorDoc).HasColumnName("ValorDoc");
            this.Property(t => t.NumeroDoc).HasColumnName("NumeroDoc");
            this.Property(t => t.IDCidadeColeta).HasColumnName("IDCidadeColeta");
            this.Property(t => t.IDEstadoColeta).HasColumnName("IDEstadoColeta");
        }
    }
}
