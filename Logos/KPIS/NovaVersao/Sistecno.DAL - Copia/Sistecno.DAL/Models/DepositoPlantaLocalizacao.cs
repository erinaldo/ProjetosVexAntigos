using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DepositoPlantaLocalizacao
    {
        public DepositoPlantaLocalizacao()
        {
            this.AgendaRecebimentoes = new List<AgendaRecebimento>();
            this.ConferenciaPallets = new List<ConferenciaPallet>();
            this.DepositoPlantaLocalizacaoRegras = new List<DepositoPlantaLocalizacaoRegra>();
            this.InventarioContagemProdutoes = new List<InventarioContagemProduto>();
            this.MovimentacaoRomaneioItems = new List<MovimentacaoRomaneioItem>();
            this.ProdutoClientes = new List<ProdutoCliente>();
            this.ProdutoClienteRegras = new List<ProdutoClienteRegra>();
            this.Romaneios = new List<Romaneio>();
            this.UnidadeDeArmazenagems = new List<UnidadeDeArmazenagem>();
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
        public virtual ICollection<AgendaRecebimento> AgendaRecebimentoes { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ConferenciaPallet> ConferenciaPallets { get; set; }
        public virtual DepositoPlanta DepositoPlanta { get; set; }
        public virtual ICollection<DepositoPlantaLocalizacaoRegra> DepositoPlantaLocalizacaoRegras { get; set; }
        public virtual ICollection<InventarioContagemProduto> InventarioContagemProdutoes { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItems { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoClientes { get; set; }
        public virtual ICollection<ProdutoClienteRegra> ProdutoClienteRegras { get; set; }
        public virtual ICollection<Romaneio> Romaneios { get; set; }
        public virtual ICollection<UnidadeDeArmazenagem> UnidadeDeArmazenagems { get; set; }
    }
}
