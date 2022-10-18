using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LancamentoContabilMap : EntityTypeConfiguration<LancamentoContabil>
    {
        public LancamentoContabilMap()
        {
            // Primary Key
            this.HasKey(t => t.IDLancamentoContabil);

            // Properties
            this.Property(t => t.IDLancamentoContabil)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.Tabela)
                .HasMaxLength(50);

            this.Property(t => t.DebitoCredito)
                .HasMaxLength(15);

            this.Property(t => t.LoteParaAgrupamento)
                .HasMaxLength(20);

            this.Property(t => t.Conciliado)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("LancamentoContabil");
            this.Property(t => t.IDLancamentoContabil).HasColumnName("IDLancamentoContabil");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");
            this.Property(t => t.IDCentroDeCusto).HasColumnName("IDCentroDeCusto");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDDocumentoOrigem).HasColumnName("IDDocumentoOrigem");
            this.Property(t => t.DataDeLancamento).HasColumnName("DataDeLancamento");
            this.Property(t => t.DataDeCompetencia).HasColumnName("DataDeCompetencia");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.ValorCredito).HasColumnName("ValorCredito");
            this.Property(t => t.ValorDebito).HasColumnName("ValorDebito");
            this.Property(t => t.Tabela).HasColumnName("Tabela");
            this.Property(t => t.Lote).HasColumnName("Lote");
            this.Property(t => t.Ano).HasColumnName("Ano");
            this.Property(t => t.IdTitulo).HasColumnName("IdTitulo");
            this.Property(t => t.IdContaContabilFilial).HasColumnName("IdContaContabilFilial");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.IdLancamento).HasColumnName("IdLancamento");
            this.Property(t => t.DebitoCredito).HasColumnName("DebitoCredito");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.LoteParaAgrupamento).HasColumnName("LoteParaAgrupamento");
            this.Property(t => t.Conciliado).HasColumnName("Conciliado");
            this.Property(t => t.CodigoDeConciliacao).HasColumnName("CodigoDeConciliacao");

            // Relationships
            this.HasOptional(t => t.CentroDeCusto)
                .WithMany(t => t.LancamentoContabils)
                .HasForeignKey(d => d.IDCentroDeCusto);
            this.HasOptional(t => t.ContaContabil)
                .WithMany(t => t.LancamentoContabils)
                .HasForeignKey(d => d.IDContaContabil);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.LancamentoContabils)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
