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
    
    public partial class CotacaoFornecedor
    {
        public CotacaoFornecedor()
        {
            this.CotacaoFornecedorCondPgto = new HashSet<CotacaoFornecedorCondPgto>();
        }
    
        public int IdCotacaoFornecedor { get; set; }
        public Nullable<int> IdFornecedor { get; set; }
        public Nullable<int> IdCotacaoDeCompra { get; set; }
        public Nullable<System.DateTime> DataDeCotacao { get; set; }
        public Nullable<System.DateTime> ValidadeDeCotacao { get; set; }
        public Nullable<System.DateTime> DataDeEntrega { get; set; }
        public Nullable<int> IdCondicaoDePagamento { get; set; }
        public Nullable<decimal> ValorTotalDeCompra { get; set; }
        public Nullable<decimal> BaseDeIcms { get; set; }
        public Nullable<decimal> ValorDeIcms { get; set; }
        public Nullable<decimal> Desconto { get; set; }
        public Nullable<decimal> Acrescimo { get; set; }
        public string Aprovada { get; set; }
        public Nullable<int> PrazoDeEntrega { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public string ConcluidoSite { get; set; }
        public string Responsavel { get; set; }
        public Nullable<decimal> ValorDeIpi { get; set; }
        public Nullable<decimal> ValorDeFrete { get; set; }
    
        public virtual ICollection<CotacaoFornecedorCondPgto> CotacaoFornecedorCondPgto { get; set; }
    }
}
