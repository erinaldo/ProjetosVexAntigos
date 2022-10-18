using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TituloDuplicataMap : EntityTypeConfiguration<TituloDuplicata>
    {
        public TituloDuplicataMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTituloDuplicata);

            // Properties
            this.Property(t => t.IDTituloDuplicata)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NossoNumero)
                .HasMaxLength(30);

            this.Property(t => t.NumeroDoPagamento)
                .HasMaxLength(20);

            this.Property(t => t.Conciliado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ORIGEM)
                .HasMaxLength(50);

            this.Property(t => t.LinhaDigitavel)
                .HasMaxLength(50);

            this.Property(t => t.Previsao)
                .HasMaxLength(3);

            this.Property(t => t.nomedoarquivo)
                .HasMaxLength(100);

            this.Property(t => t.TipoLinhaDigitavel)
                .HasMaxLength(20);

            this.Property(t => t.CodigoDeBarra)
                .HasMaxLength(100);

            this.Property(t => t.CodigoDigitavel)
                .HasMaxLength(100);

            this.Property(t => t.Contabilidade)
                .HasMaxLength(3);

            this.Property(t => t.StatusPagFor)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TituloDuplicata");
            this.Property(t => t.IDTituloDuplicata).HasColumnName("IDTituloDuplicata");
            this.Property(t => t.IDTitulo).HasColumnName("IDTitulo");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDBancoConta).HasColumnName("IDBancoConta");
            this.Property(t => t.Parcela).HasColumnName("Parcela");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.ValorPagoAcumulado).HasColumnName("ValorPagoAcumulado");
            this.Property(t => t.ValorUltimoPagamento).HasColumnName("ValorUltimoPagamento");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.DataDeVencimento).HasColumnName("DataDeVencimento");
            this.Property(t => t.DataDeLiquidacao).HasColumnName("DataDeLiquidacao");
            this.Property(t => t.DataDeAgendamento).HasColumnName("DataDeAgendamento");
            this.Property(t => t.TaxaBancaria).HasColumnName("TaxaBancaria");
            this.Property(t => t.JurosDiario).HasColumnName("JurosDiario");
            this.Property(t => t.Multa).HasColumnName("Multa");
            this.Property(t => t.Acrescimo).HasColumnName("Acrescimo");
            this.Property(t => t.Desconto).HasColumnName("Desconto");
            this.Property(t => t.DescontoPrevisto).HasColumnName("DescontoPrevisto");
            this.Property(t => t.DataLimiteDesconto).HasColumnName("DataLimiteDesconto");
            this.Property(t => t.NossoNumero).HasColumnName("NossoNumero");
            this.Property(t => t.NumeroDoPagamento).HasColumnName("NumeroDoPagamento");
            this.Property(t => t.Conciliado).HasColumnName("Conciliado");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.DataDeVencimentoReal).HasColumnName("DataDeVencimentoReal");
            this.Property(t => t.ORIGEM).HasColumnName("ORIGEM");
            this.Property(t => t.Juros).HasColumnName("Juros");
            this.Property(t => t.ICMS).HasColumnName("ICMS");
            this.Property(t => t.ISS).HasColumnName("ISS");
            this.Property(t => t.INSS).HasColumnName("INSS");
            this.Property(t => t.PIS).HasColumnName("PIS");
            this.Property(t => t.COFINS).HasColumnName("COFINS");
            this.Property(t => t.CSLL).HasColumnName("CSLL");
            this.Property(t => t.IRRF).HasColumnName("IRRF");
            this.Property(t => t.LinhaDigitavel).HasColumnName("LinhaDigitavel");
            this.Property(t => t.NCC_NCF).HasColumnName("NCC_NCF");
            this.Property(t => t.DataDeVencimentoOriginal).HasColumnName("DataDeVencimentoOriginal");
            this.Property(t => t.IdBordero).HasColumnName("IdBordero");
            this.Property(t => t.UltimaCobrancaPorEmail).HasColumnName("UltimaCobrancaPorEmail");
            this.Property(t => t.UltimaCobrancaPorFone).HasColumnName("UltimaCobrancaPorFone");
            this.Property(t => t.Previsao).HasColumnName("Previsao");
            this.Property(t => t.nomedoarquivo).HasColumnName("nomedoarquivo");
            this.Property(t => t.TipoLinhaDigitavel).HasColumnName("TipoLinhaDigitavel");
            this.Property(t => t.CodigoDeBarra).HasColumnName("CodigoDeBarra");
            this.Property(t => t.CodigoDigitavel).HasColumnName("CodigoDigitavel");
            this.Property(t => t.IdModalidadeDePagamento).HasColumnName("IdModalidadeDePagamento");
            this.Property(t => t.IdModalidadeDocTed).HasColumnName("IdModalidadeDocTed");
            this.Property(t => t.IdFinalidadeDocTed).HasColumnName("IdFinalidadeDocTed");
            this.Property(t => t.IDBancoContaBloqueto).HasColumnName("IDBancoContaBloqueto");
            this.Property(t => t.DataDeVencimentoAnterior).HasColumnName("DataDeVencimentoAnterior");
            this.Property(t => t.DataUltimoPagamento).HasColumnName("DataUltimoPagamento");
            this.Property(t => t.IdDDA).HasColumnName("IdDDA");
            this.Property(t => t.Contabilidade).HasColumnName("Contabilidade");
            this.Property(t => t.NumeroDoArquivo).HasColumnName("NumeroDoArquivo");
            this.Property(t => t.IDBancoOcorrenciaRetorno).HasColumnName("IDBancoOcorrenciaRetorno");
            this.Property(t => t.DataHoraRecebimentoRetorno).HasColumnName("DataHoraRecebimentoRetorno");
            this.Property(t => t.StatusPagFor).HasColumnName("StatusPagFor");

            // Relationships
            this.HasOptional(t => t.BancoContaBloqueto)
                .WithMany(t => t.TituloDuplicatas)
                .HasForeignKey(d => d.IDBancoContaBloqueto);
            this.HasOptional(t => t.DDA)
                .WithMany(t => t.TituloDuplicatas)
                .HasForeignKey(d => d.IdDDA);
            this.HasRequired(t => t.Titulo)
                .WithMany(t => t.TituloDuplicatas)
                .HasForeignKey(d => d.IDTitulo);

        }
    }
}
