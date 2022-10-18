using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TipoDeMonitoramentoMap : EntityTypeConfiguration<TipoDeMonitoramento>
    {
        public TipoDeMonitoramentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTipoDeMonitoramento);

            // Properties
            this.Property(t => t.IdTipoDeMonitoramento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("TipoDeMonitoramento");
            this.Property(t => t.IdTipoDeMonitoramento).HasColumnName("IdTipoDeMonitoramento");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
