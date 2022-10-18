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
    
    public partial class DepositoPlantaLocalizacao
    {
        public DepositoPlantaLocalizacao()
        {
            this.AgendaRecebimento = new HashSet<AgendaRecebimento>();
            this.ConferenciaPallet = new HashSet<ConferenciaPallet>();
            this.DepositoPlantaLocalizacaoRegra = new HashSet<DepositoPlantaLocalizacaoRegra>();
            this.InventarioContagemProduto = new HashSet<InventarioContagemProduto>();
            this.MovimentacaoRomaneioItem = new HashSet<MovimentacaoRomaneioItem>();
            this.ProdutoCliente = new HashSet<ProdutoCliente>();
            this.ProdutoClienteRegra = new HashSet<ProdutoClienteRegra>();
            this.Romaneio = new HashSet<Romaneio>();
            this.UnidadeDeArmazenagem = new HashSet<UnidadeDeArmazenagem>();
        }
    
        public int IDDepositoPlantaLocalizacao { get; set; }
        public int IDDepositoPlanta { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public Nullable<decimal> Largura { get; set; }
        public Nullable<decimal> Profundidade { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public Nullable<decimal> CapacidadeEmKg { get; set; }
        public string MultiplosProdutos { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string TipoDeMovimentacao { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> Ordem { get; set; }
        public Nullable<int> OrdemArmazenagem { get; set; }
    
        public virtual ICollection<AgendaRecebimento> AgendaRecebimento { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ConferenciaPallet> ConferenciaPallet { get; set; }
        public virtual DepositoPlanta DepositoPlanta { get; set; }
        public virtual ICollection<DepositoPlantaLocalizacaoRegra> DepositoPlantaLocalizacaoRegra { get; set; }
        public virtual ICollection<InventarioContagemProduto> InventarioContagemProduto { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItem { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoCliente { get; set; }
        public virtual ICollection<ProdutoClienteRegra> ProdutoClienteRegra { get; set; }
        public virtual ICollection<Romaneio> Romaneio { get; set; }
        public virtual ICollection<UnidadeDeArmazenagem> UnidadeDeArmazenagem { get; set; }
    }
}
