using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTLancamentoMap : EntityTypeConfiguration<DTLancamento>
    {
        public DTLancamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDtLancamento);

            // Properties
            this.Property(t => t.IdDtLancamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.DebitoCredito)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(7);

            this.Property(t => t.SolicitarDocumento)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.TipoLancamento)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("DTLancamento");
            this.Property(t => t.IdDtLancamento).HasColumnName("IdDtLancamento");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.DebitoCredito).HasColumnName("DebitoCredito");
            this.Property(t => t.SolicitarDocumento).HasColumnName("SolicitarDocumento");
            this.Property(t => t.TipoLancamento).HasColumnName("TipoLancamento");
        }
    }
}
