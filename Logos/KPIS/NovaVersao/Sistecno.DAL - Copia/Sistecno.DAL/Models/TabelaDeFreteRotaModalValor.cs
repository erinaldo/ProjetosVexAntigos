using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TabelaDeFreteRotaModalValor
    {
        public int IDTabelaDeFreteRotaModalValor { get; set; }
        public Nullable<int> IDTabelaDeFreteRotaModal { get; set; }
        public int IDTabelaDeFreteVigencia { get; set; }
        public Nullable<decimal> Ate { get; set; }
        public string UnidadeDeMedida { get; set; }
        public string Operador { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> ValorMinimo { get; set; }
        public Nullable<decimal> ValorExcedente { get; set; }
        public Nullable<decimal> PesoExcedente { get; set; }
        public Nullable<decimal> PercentualFreteValorAte { get; set; }
        public Nullable<decimal> PercentualFreteValorAcima { get; set; }
        public Nullable<decimal> ValorDaNota { get; set; }
        public Nullable<decimal> FreteValorMinimo { get; set; }
        public Nullable<decimal> Cat { get; set; }
        public Nullable<decimal> Tde { get; set; }
        public Nullable<decimal> DevCan { get; set; }
        public Nullable<decimal> PercentualTde { get; set; }
        public Nullable<decimal> ValorDespacho { get; set; }
        public Nullable<decimal> PedagioAte { get; set; }
        public Nullable<decimal> PedagioPeso { get; set; }
        public Nullable<decimal> PedagioAcima { get; set; }
        public Nullable<decimal> PercentualGris { get; set; }
        public Nullable<decimal> ValorGrisMinimo { get; set; }
        public Nullable<decimal> PercentualSeguro { get; set; }
        public Nullable<decimal> Outros { get; set; }
        public Nullable<decimal> ValorPorNotaFiscal { get; set; }
        public Nullable<decimal> TaxaDeColeta { get; set; }
        public Nullable<decimal> TaxaDeEntrega { get; set; }
        public Nullable<decimal> ValorMinimoDoFrete { get; set; }
        public Nullable<decimal> DescargaPaletizada { get; set; }
        public Nullable<decimal> DescargaNaoPaletizada { get; set; }
        public Nullable<decimal> PercentualDoFreteParaReEntrega { get; set; }
        public Nullable<decimal> PercentualDoFreteParaDevolucao { get; set; }
        public string IcmsIssIncluso { get; set; }
        public Nullable<decimal> Suframa { get; set; }
        public Nullable<decimal> Ajudante { get; set; }
        public Nullable<decimal> Diaria { get; set; }
        public Nullable<decimal> Ted { get; set; }
        public Nullable<decimal> PercentualTed { get; set; }
        public Nullable<decimal> Despacho { get; set; }
        public string PedagioFracaoExata { get; set; }
        public virtual TabelaDeFreteRotaModal TabelaDeFreteRotaModal { get; set; }
        public virtual TabelaDeFreteVigencia TabelaDeFreteVigencia { get; set; }
    }
}
