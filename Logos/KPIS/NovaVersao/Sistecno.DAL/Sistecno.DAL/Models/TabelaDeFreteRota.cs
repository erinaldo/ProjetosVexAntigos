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
    
    public partial class TabelaDeFreteRota
    {
        public TabelaDeFreteRota()
        {
            this.TabelaDeFreteRotaModal = new HashSet<TabelaDeFreteRotaModal>();
        }
    
        public int IDTabelaDeFreteRota { get; set; }
        public int IDTabelaDeFrete { get; set; }
        public Nullable<int> IDFilialOrigem { get; set; }
        public Nullable<int> IDFilialDestino { get; set; }
        public Nullable<int> IDRegiaoOrigem { get; set; }
        public Nullable<int> IDRegiaoDestino { get; set; }
        public Nullable<int> IDCidadeOrigem { get; set; }
        public Nullable<int> IDCidadeDestino { get; set; }
        public Nullable<int> IDEstadoOrigem { get; set; }
        public Nullable<int> IDEstadoDestino { get; set; }
        public Nullable<int> IDRemetente { get; set; }
        public Nullable<int> IDDestinatario { get; set; }
        public Nullable<int> IDTabelaDeFreteProduto { get; set; }
        public Nullable<decimal> FatorDeCubagem { get; set; }
        public string Observacao { get; set; }
        public Nullable<int> PrazoDeEntrega { get; set; }
    
        public virtual Cidade Cidade { get; set; }
        public virtual Cidade Cidade1 { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Estado Estado1 { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Filial Filial1 { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual Regiao Regiao1 { get; set; }
        public virtual TabelaDeFrete TabelaDeFrete { get; set; }
        public virtual ICollection<TabelaDeFreteRotaModal> TabelaDeFreteRotaModal { get; set; }
    }
}
