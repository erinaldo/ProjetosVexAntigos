using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTFaturamentoClienteDocumentoMap : EntityTypeConfiguration<DTFaturamentoClienteDocumento>
    {
        public DTFaturamentoClienteDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDtFaturamentoClienteDocumento);

            // Properties
            this.Property(t => t.IdDtFaturamentoClienteDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DTFaturamentoClienteDocumento");
            this.Property(t => t.IdDtFaturamentoClienteDocumento).HasColumnName("IdDtFaturamentoClienteDocumento");
            this.Property(t => t.IdDtFaturamentoCliente).HasColumnName("IdDtFaturamentoCliente");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DTFaturamentoClienteDocumentoes)
                .HasForeignKey(d => d.IdDocumento);
            this.HasRequired(t => t.DTFaturamentoCliente)
                .WithMany(t => t.DTFaturamentoClienteDocumentoes)
                .HasForeignKey(d => d.IdDtFaturamentoCliente);

        }
    }
}
