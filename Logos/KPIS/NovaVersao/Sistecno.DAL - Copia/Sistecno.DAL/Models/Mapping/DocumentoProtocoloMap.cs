using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoProtocoloMap : EntityTypeConfiguration<DocumentoProtocolo>
    {
        public DocumentoProtocoloMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoComprovante);

            // Properties
            this.Property(t => t.IdDocumentoComprovante)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoProtocolo");
            this.Property(t => t.IdDocumentoComprovante).HasColumnName("IdDocumentoComprovante");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
        }
    }
}
