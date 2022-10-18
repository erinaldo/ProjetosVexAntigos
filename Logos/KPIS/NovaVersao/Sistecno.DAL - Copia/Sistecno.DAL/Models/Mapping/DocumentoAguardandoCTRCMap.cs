using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoAguardandoCTRCMap : EntityTypeConfiguration<DocumentoAguardandoCTRC>
    {
        public DocumentoAguardandoCTRCMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoAguardandoCTRC);

            // Properties
            this.Property(t => t.IdDocumentoAguardandoCTRC)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoAguardandoCTRC");
            this.Property(t => t.IdDocumentoAguardandoCTRC).HasColumnName("IdDocumentoAguardandoCTRC");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdDocumentoFrete).HasColumnName("IdDocumentoFrete");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoAguardandoCTRCs)
                .HasForeignKey(d => d.IdDocumento);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.DocumentoAguardandoCTRCs)
                .HasForeignKey(d => d.IdFilial);

        }
    }
}
