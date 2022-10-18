using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PreFaturaDocumentoMap : EntityTypeConfiguration<PreFaturaDocumento>
    {
        public PreFaturaDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPreFaturaDocumento);

            // Properties
            this.Property(t => t.IdPreFaturaDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SerieDaNotaFiscal)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("PreFaturaDocumento");
            this.Property(t => t.IdPreFaturaDocumento).HasColumnName("IdPreFaturaDocumento");
            this.Property(t => t.IdPreFatura).HasColumnName("IdPreFatura");
            this.Property(t => t.SerieDaNotaFiscal).HasColumnName("SerieDaNotaFiscal");
            this.Property(t => t.IdNotaFiscal).HasColumnName("IdNotaFiscal");
            this.Property(t => t.NotaFiscal).HasColumnName("NotaFiscal");
            this.Property(t => t.IdCtrc).HasColumnName("IdCtrc");
            this.Property(t => t.Ctrc).HasColumnName("Ctrc");
            this.Property(t => t.ValorFrete).HasColumnName("ValorFrete");

            // Relationships
            this.HasRequired(t => t.PreFatura)
                .WithMany(t => t.PreFaturaDocumentoes)
                .HasForeignKey(d => d.IdPreFatura);

        }
    }
}
