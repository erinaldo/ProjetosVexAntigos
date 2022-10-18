using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoTipoMap : EntityTypeConfiguration<DocumentoTipo>
    {
        public DocumentoTipoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoTipo);

            // Properties
            this.Property(t => t.IdDocumentoTipo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeDocumento)
                .IsFixedLength()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("DocumentoTipo");
            this.Property(t => t.IdDocumentoTipo).HasColumnName("IdDocumentoTipo");
            this.Property(t => t.TipoDeDocumento).HasColumnName("TipoDeDocumento");
        }
    }
}
