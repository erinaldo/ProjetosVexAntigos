using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoAFaturarMap : EntityTypeConfiguration<DocumentoAFaturar>
    {
        public DocumentoAFaturarMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoAFaturar);

            // Properties
            this.Property(t => t.IdDocumentoAFaturar)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeFatura)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentoAFaturar");
            this.Property(t => t.IdDocumentoAFaturar).HasColumnName("IdDocumentoAFaturar");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.TipoDeFatura).HasColumnName("TipoDeFatura");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoAFaturars)
                .HasForeignKey(d => d.IdDocumento);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.DocumentoAFaturars)
                .HasForeignKey(d => d.IdFilial);

        }
    }
}
