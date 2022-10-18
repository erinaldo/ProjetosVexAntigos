using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LancamentoContabilCCMap : EntityTypeConfiguration<LancamentoContabilCC>
    {
        public LancamentoContabilCCMap()
        {
            // Primary Key
            this.HasKey(t => t.IdLancamentoContabilCC);

            // Properties
            this.Property(t => t.IdLancamentoContabilCC)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("LancamentoContabilCC");
            this.Property(t => t.IdLancamentoContabilCC).HasColumnName("IdLancamentoContabilCC");
            this.Property(t => t.IdCentroDeCustoFilial).HasColumnName("IdCentroDeCustoFilial");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.IdLancamento).HasColumnName("IdLancamento");
            this.Property(t => t.IdLancamentoContabil).HasColumnName("IdLancamentoContabil");
        }
    }
}
