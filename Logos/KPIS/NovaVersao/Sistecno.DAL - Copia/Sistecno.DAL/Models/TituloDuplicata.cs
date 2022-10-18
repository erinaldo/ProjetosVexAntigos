using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TituloDuplicata
    {
        public TituloDuplicata()
        {
            this.BorderoTituloDuplicatas = new List<BorderoTituloDuplicata>();
            this.MovimentacaoBancarias = new List<MovimentacaoBancaria>();
            this.TituloDuplicataHistoricoes = new List<TituloDuplicataHistorico>();
            this.TituloDuplicataRemessas = new List<TituloDuplicataRemessa>();
        }

        public int IDTituloDuplicata { get; set; }
        public int IDTitulo { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<int> IDBancoConta { get; set; }
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
        public Nullable<decimal> ValorPagoAcumulado { get; set; }
        public Nullable<decimal> ValorUltimoPagamento { get; set; }
        public decimal Saldo { get; set; }
        public System.DateTime DataDeVencimento { get; set; }
        public Nullable<System.DateTime> DataDeLiquidacao { get; set; }
        public Nullable<System.DateTime> DataDeAgendamento { get; set; }
        public Nullable<decimal> TaxaBancaria { get; set; }
        public Nullable<decimal> JurosDiario { get; set; }
        public Nullable<decimal> Multa { get; set; }
        public Nullable<decimal> Acrescimo { get; set; }
        public Nullable<decimal> Desconto { get; set; }
        public Nullable<decimal> DescontoPrevisto { get; set; }
        public Nullable<System.DateTime> DataLimiteDesconto { get; set; }
        public string NossoNumero { get; set; }
        public string NumeroDoPagamento { get; set; }
        public string Conciliado { get; set; }
        public string Ativo { get; set; }
        public Nullable<System.DateTime> DataDeVencimentoReal { get; set; }
        public string ORIGEM { get; set; }
        public Nullable<decimal> Juros { get; set; }
        public Nullable<decimal> ICMS { get; set; }
        public Nullable<decimal> ISS { get; set; }
        public Nullable<decimal> INSS { get; set; }
        public Nullable<decimal> PIS { get; set; }
        public Nullable<decimal> COFINS { get; set; }
        public Nullable<decimal> CSLL { get; set; }
        public Nullable<decimal> IRRF { get; set; }
        public string LinhaDigitavel { get; set; }
        public Nullable<int> NCC_NCF { get; set; }
        public Nullable<System.DateTime> DataDeVencimentoOriginal { get; set; }
        public Nullable<int> IdBordero { get; set; }
        public Nullable<System.DateTime> UltimaCobrancaPorEmail { get; set; }
        public Nullable<System.DateTime> UltimaCobrancaPorFone { get; set; }
        public string Previsao { get; set; }
        public string nomedoarquivo { get; set; }
        public string TipoLinhaDigitavel { get; set; }
        public string CodigoDeBarra { get; set; }
        public string CodigoDigitavel { get; set; }
        public Nullable<int> IdModalidadeDePagamento { get; set; }
        public Nullable<int> IdModalidadeDocTed { get; set; }
        public Nullable<int> IdFinalidadeDocTed { get; set; }
        public Nullable<int> IDBancoContaBloqueto { get; set; }
        public Nullable<System.DateTime> DataDeVencimentoAnterior { get; set; }
        public Nullable<System.DateTime> DataUltimoPagamento { get; set; }
        public Nullable<int> IdDDA { get; set; }
        public string Contabilidade { get; set; }
        public Nullable<int> NumeroDoArquivo { get; set; }
        public Nullable<int> IDBancoOcorrenciaRetorno { get; set; }
        public Nullable<System.DateTime> DataHoraRecebimentoRetorno { get; set; }
        public string StatusPagFor { get; set; }
        public virtual BancoContaBloqueto BancoContaBloqueto { get; set; }
        public virtual ICollection<BorderoTituloDuplicata> BorderoTituloDuplicatas { get; set; }
        public virtual DDA DDA { get; set; }
        public virtual ICollection<MovimentacaoBancaria> MovimentacaoBancarias { get; set; }
        public virtual Titulo Titulo { get; set; }
        public virtual ICollection<TituloDuplicataHistorico> TituloDuplicataHistoricoes { get; set; }
        public virtual ICollection<TituloDuplicataRemessa> TituloDuplicataRemessas { get; set; }
    }
}
