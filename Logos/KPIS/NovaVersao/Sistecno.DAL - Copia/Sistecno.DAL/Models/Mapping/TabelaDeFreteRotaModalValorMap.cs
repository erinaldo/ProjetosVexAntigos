using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TabelaDeFreteRotaModalValorMap : EntityTypeConfiguration<TabelaDeFreteRotaModalValor>
    {
        public TabelaDeFreteRotaModalValorMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTabelaDeFreteRotaModalValor);

            // Properties
            this.Property(t => t.IDTabelaDeFreteRotaModalValor)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UnidadeDeMedida)
                .HasMaxLength(15);

            this.Property(t => t.Operador)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IcmsIssIncluso)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PedagioFracaoExata)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("TabelaDeFreteRotaModalValor");
            this.Property(t => t.IDTabelaDeFreteRotaModalValor).HasColumnName("IDTabelaDeFreteRotaModalValor");
            this.Property(t => t.IDTabelaDeFreteRotaModal).HasColumnName("IDTabelaDeFreteRotaModal");
            this.Property(t => t.IDTabelaDeFreteVigencia).HasColumnName("IDTabelaDeFreteVigencia");
            this.Property(t => t.Ate).HasColumnName("Ate");
            this.Property(t => t.UnidadeDeMedida).HasColumnName("UnidadeDeMedida");
            this.Property(t => t.Operador).HasColumnName("Operador");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.ValorMinimo).HasColumnName("ValorMinimo");
            this.Property(t => t.ValorExcedente).HasColumnName("ValorExcedente");
            this.Property(t => t.PesoExcedente).HasColumnName("PesoExcedente");
            this.Property(t => t.PercentualFreteValorAte).HasColumnName("PercentualFreteValorAte");
            this.Property(t => t.PercentualFreteValorAcima).HasColumnName("PercentualFreteValorAcima");
            this.Property(t => t.ValorDaNota).HasColumnName("ValorDaNota");
            this.Property(t => t.FreteValorMinimo).HasColumnName("FreteValorMinimo");
            this.Property(t => t.Cat).HasColumnName("Cat");
            this.Property(t => t.Tde).HasColumnName("Tde");
            this.Property(t => t.DevCan).HasColumnName("DevCan");
            this.Property(t => t.PercentualTde).HasColumnName("PercentualTde");
            this.Property(t => t.ValorDespacho).HasColumnName("ValorDespacho");
            this.Property(t => t.PedagioAte).HasColumnName("PedagioAte");
            this.Property(t => t.PedagioPeso).HasColumnName("PedagioPeso");
            this.Property(t => t.PedagioAcima).HasColumnName("PedagioAcima");
            this.Property(t => t.PercentualGris).HasColumnName("PercentualGris");
            this.Property(t => t.ValorGrisMinimo).HasColumnName("ValorGrisMinimo");
            this.Property(t => t.PercentualSeguro).HasColumnName("PercentualSeguro");
            this.Property(t => t.Outros).HasColumnName("Outros");
            this.Property(t => t.ValorPorNotaFiscal).HasColumnName("ValorPorNotaFiscal");
            this.Property(t => t.TaxaDeColeta).HasColumnName("TaxaDeColeta");
            this.Property(t => t.TaxaDeEntrega).HasColumnName("TaxaDeEntrega");
            this.Property(t => t.ValorMinimoDoFrete).HasColumnName("ValorMinimoDoFrete");
            this.Property(t => t.DescargaPaletizada).HasColumnName("DescargaPaletizada");
            this.Property(t => t.DescargaNaoPaletizada).HasColumnName("DescargaNaoPaletizada");
            this.Property(t => t.PercentualDoFreteParaReEntrega).HasColumnName("PercentualDoFreteParaReEntrega");
            this.Property(t => t.PercentualDoFreteParaDevolucao).HasColumnName("PercentualDoFreteParaDevolucao");
            this.Property(t => t.IcmsIssIncluso).HasColumnName("IcmsIssIncluso");
            this.Property(t => t.Suframa).HasColumnName("Suframa");
            this.Property(t => t.Ajudante).HasColumnName("Ajudante");
            this.Property(t => t.Diaria).HasColumnName("Diaria");
            this.Property(t => t.Ted).HasColumnName("Ted");
            this.Property(t => t.PercentualTed).HasColumnName("PercentualTed");
            this.Property(t => t.Despacho).HasColumnName("Despacho");
            this.Property(t => t.PedagioFracaoExata).HasColumnName("PedagioFracaoExata");

            // Relationships
            this.HasOptional(t => t.TabelaDeFreteRotaModal)
                .WithMany(t => t.TabelaDeFreteRotaModalValors)
                .HasForeignKey(d => d.IDTabelaDeFreteRotaModal);
            this.HasRequired(t => t.TabelaDeFreteVigencia)
                .WithMany(t => t.TabelaDeFreteRotaModalValors)
                .HasForeignKey(d => d.IDTabelaDeFreteVigencia);

        }
    }
}
