using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ParametroFluxoDeCaixaMap : EntityTypeConfiguration<ParametroFluxoDeCaixa>
    {
        public ParametroFluxoDeCaixaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdParametroFluxoDeCaixa);

            // Properties
            this.Property(t => t.IdParametroFluxoDeCaixa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TituloContaPagar)
                .HasMaxLength(20);

            this.Property(t => t.ConsideraSaldoAnterior)
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.HabilitarCompensacao)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ParametroFluxoDeCaixa");
            this.Property(t => t.IdParametroFluxoDeCaixa).HasColumnName("IdParametroFluxoDeCaixa");
            this.Property(t => t.IdEmpresa).HasColumnName("IdEmpresa");
            this.Property(t => t.TituloContaPagar).HasColumnName("TituloContaPagar");
            this.Property(t => t.ConsideraSaldoAnterior).HasColumnName("ConsideraSaldoAnterior");
            this.Property(t => t.DiasAnteriores).HasColumnName("DiasAnteriores");
            this.Property(t => t.DiasPosteriores).HasColumnName("DiasPosteriores");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.DataInicial).HasColumnName("DataInicial");
            this.Property(t => t.DataFinal).HasColumnName("DataFinal");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.HabilitarCompensacao).HasColumnName("HabilitarCompensacao");

            // Relationships
            this.HasOptional(t => t.Empresa)
                .WithMany(t => t.ParametroFluxoDeCaixas)
                .HasForeignKey(d => d.IdEmpresa);

        }
    }
}
