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
    
    public partial class PreFatura
    {
        public PreFatura()
        {
            this.PreFaturaDocumento = new HashSet<PreFaturaDocumento>();
        }
    
        public int IdPreFatura { get; set; }
        public string Tipo { get; set; }
        public string Cnpj { get; set; }
        public string NumeroPreFatura { get; set; }
        public Nullable<System.DateTime> Emissao { get; set; }
        public Nullable<System.DateTime> Vencimento { get; set; }
        public Nullable<int> QuantidadeDeDocumentos { get; set; }
        public Nullable<decimal> ValorPreFatura { get; set; }
        public string Acao { get; set; }
        public string Situacao { get; set; }
        public Nullable<decimal> Desconto { get; set; }
        public Nullable<decimal> Complemento { get; set; }
        public Nullable<int> IdTitulo { get; set; }
    
        public virtual ICollection<PreFaturaDocumento> PreFaturaDocumento { get; set; }
    }
}