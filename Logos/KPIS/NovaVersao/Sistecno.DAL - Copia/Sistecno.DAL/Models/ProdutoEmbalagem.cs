using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProdutoEmbalagem
    {
        public ProdutoEmbalagem()
        {
            this.ConferenciaPalletProdutoXXXXXes = new List<ConferenciaPalletProdutoXXXXX>();
            this.InventarioContagemProdutoes = new List<InventarioContagemProduto>();
            this.Lotes = new List<Lote>();
            this.MovimentacaoRomaneioItems = new List<MovimentacaoRomaneioItem>();
            this.ProdutoEmbalagem1 = new List<ProdutoEmbalagem>();
            this.RomaneioDocumentoConferencias = new List<RomaneioDocumentoConferencia>();
        }

        public int IDProdutoEmbalagem { get; set; }
        public int IDProdutoCliente { get; set; }
        public int IDProduto { get; set; }
        public Nullable<int> IDProdutoInterno { get; set; }
        public string Conteudo { get; set; }
        public decimal UnidadeDoCliente { get; set; }
        public Nullable<decimal> UnidadeLogistica { get; set; }
        public string UnidadeDeMedida { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> UniFor { get; set; }
        public Nullable<int> UniCli { get; set; }
        public Nullable<System.DateTime> DataUltimaAlteracao { get; set; }
        public string ArquivoQueAlterou { get; set; }
        public string OperadorParaConversao { get; set; }
        public string DumEan { get; set; }
        public Nullable<System.DateTime> DadosLogisticos { get; set; }
        public virtual ICollection<ConferenciaPalletProdutoXXXXX> ConferenciaPalletProdutoXXXXXes { get; set; }
        public virtual ICollection<InventarioContagemProduto> InventarioContagemProdutoes { get; set; }
        public virtual ICollection<Lote> Lotes { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItems { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
        public virtual ICollection<ProdutoEmbalagem> ProdutoEmbalagem1 { get; set; }
        public virtual ProdutoEmbalagem ProdutoEmbalagem2 { get; set; }
        public virtual ICollection<RomaneioDocumentoConferencia> RomaneioDocumentoConferencias { get; set; }
    }
}
