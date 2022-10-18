//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistecno.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DocumentoFrete
    {
        public int IDDocumentoFrete { get; set; }
        public int IDDocumento { get; set; }
        public int IDFilial { get; set; }
        public int IDPagadorDoFrete { get; set; }
        public int IDServico { get; set; }
        public Nullable<int> IDCidadeOrigemCalculo { get; set; }
        public Nullable<int> IDCidadeDestinoCalculo { get; set; }
        public Nullable<int> IDCidadeColeta { get; set; }
        public Nullable<int> IDCidadeEntrega { get; set; }
        public Nullable<decimal> FretePeso { get; set; }
        public Nullable<decimal> FreteExcedente { get; set; }
        public Nullable<decimal> FretePorNotaFiscal { get; set; }
        public Nullable<decimal> FretePercentual { get; set; }
        public Nullable<decimal> FreteValor { get; set; }
        public Nullable<decimal> Gris { get; set; }
        public Nullable<decimal> Cat { get; set; }
        public Nullable<decimal> Despacho { get; set; }
        public Nullable<decimal> Tas { get; set; }
        public Nullable<decimal> Ted { get; set; }
        public Nullable<decimal> Pedagio { get; set; }
        public Nullable<decimal> Seguro { get; set; }
        public Nullable<decimal> Suframa { get; set; }
        public Nullable<decimal> TaxaDeColeta { get; set; }
        public Nullable<decimal> TaxaDeEntrega { get; set; }
        public Nullable<decimal> Descarga { get; set; }
        public Nullable<decimal> Paletizacao { get; set; }
        public Nullable<decimal> Ajudante { get; set; }
        public Nullable<decimal> Diaria { get; set; }
        public Nullable<decimal> Aliquota { get; set; }
        public Nullable<decimal> IcmsIss { get; set; }
        public Nullable<decimal> Frete { get; set; }
        public Nullable<decimal> DescargaNaoPaletizada { get; set; }
        public Nullable<decimal> DescargaPaletizada { get; set; }
        public Nullable<decimal> DevCan { get; set; }
        public Nullable<decimal> ImpostoARecolher { get; set; }
        public Nullable<decimal> Outros { get; set; }
        public string Proprietario { get; set; }
        public Nullable<decimal> DiferencaDeFrete { get; set; }
        public Nullable<decimal> BaseDeCalculo { get; set; }
        public Nullable<decimal> FreteSimulado { get; set; }
        public Nullable<int> IdTabelaDeFreteRotaModal { get; set; }
        public string Observacao { get; set; }
        public Nullable<decimal> PercentualDoFrete { get; set; }
        public Nullable<decimal> TaxaDeSevico { get; set; }
        public Nullable<decimal> ValorAgregado { get; set; }
        public Nullable<decimal> ValorDoServico { get; set; }
        public Nullable<int> IdDt { get; set; }
        public Nullable<int> IdLinhaPlanilhaRoge { get; set; }
        public string EntregaEfetuada { get; set; }
    
        public virtual Documento Documento { get; set; }
    }
}
