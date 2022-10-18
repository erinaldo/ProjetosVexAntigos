using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProjetoMap : EntityTypeConfiguration<Projeto>
    {
        public ProjetoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdProjeto);

            // Properties
            this.Property(t => t.IdProjeto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ContatoCliente)
                .HasMaxLength(100);

            this.Property(t => t.ContatoContratado)
                .HasMaxLength(100);

            this.Property(t => t.UtilizaAreaClimatizada)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.Edi)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.DigitalizarComprovante)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.EmitirCTRC)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.EmitirNotaFiscalServico)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.FormaFaturamento)
                .HasMaxLength(50);

            this.Property(t => t.Vencimento)
                .HasMaxLength(50);

            this.Property(t => t.OrigemDaColeta)
                .HasMaxLength(50);

            this.Property(t => t.ValorPorColeta)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Projeto");
            this.Property(t => t.IdProjeto).HasColumnName("IdProjeto");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.ContatoCliente).HasColumnName("ContatoCliente");
            this.Property(t => t.ContatoContratado).HasColumnName("ContatoContratado");
            this.Property(t => t.UtilizaAreaClimatizada).HasColumnName("UtilizaAreaClimatizada");
            this.Property(t => t.InicioDaProducao).HasColumnName("InicioDaProducao");
            this.Property(t => t.FinalDaProducao).HasColumnName("FinalDaProducao");
            this.Property(t => t.InicioDaEntrega).HasColumnName("InicioDaEntrega");
            this.Property(t => t.FinalDaEntrega).HasColumnName("FinalDaEntrega");
            this.Property(t => t.TotalDeKits).HasColumnName("TotalDeKits");
            this.Property(t => t.FatorPorCaixa).HasColumnName("FatorPorCaixa");
            this.Property(t => t.FatorPorPallet).HasColumnName("FatorPorPallet");
            this.Property(t => t.PesoPorKit).HasColumnName("PesoPorKit");
            this.Property(t => t.FretePorKit).HasColumnName("FretePorKit");
            this.Property(t => t.TempoDeProducao).HasColumnName("TempoDeProducao");
            this.Property(t => t.Turnos).HasColumnName("Turnos");
            this.Property(t => t.PessoasPorTurno).HasColumnName("PessoasPorTurno");
            this.Property(t => t.MaoDeObra).HasColumnName("MaoDeObra");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Edi).HasColumnName("Edi");
            this.Property(t => t.DigitalizarComprovante).HasColumnName("DigitalizarComprovante");
            this.Property(t => t.EmitirCTRC).HasColumnName("EmitirCTRC");
            this.Property(t => t.EmitirNotaFiscalServico).HasColumnName("EmitirNotaFiscalServico");
            this.Property(t => t.FormaFaturamento).HasColumnName("FormaFaturamento");
            this.Property(t => t.Vencimento).HasColumnName("Vencimento");
            this.Property(t => t.OrigemDaColeta).HasColumnName("OrigemDaColeta");
            this.Property(t => t.ValorPorColeta).HasColumnName("ValorPorColeta");
            this.Property(t => t.PlanejamentoDeTransferencia).HasColumnName("PlanejamentoDeTransferencia");
            this.Property(t => t.Observacao).HasColumnName("Observacao");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Projetoes)
                .HasForeignKey(d => d.IdFilial);

        }
    }
}
