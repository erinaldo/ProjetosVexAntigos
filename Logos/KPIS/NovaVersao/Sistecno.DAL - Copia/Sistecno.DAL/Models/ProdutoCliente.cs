using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProdutoCliente
    {
        public ProdutoCliente()
        {
            this.ConferenciaPalletDocVolItems = new List<ConferenciaPalletDocVolItem>();
            this.ConferenciaPalletEntradas = new List<ConferenciaPalletEntrada>();
            this.ConferenciaPalletEntradaLotes = new List<ConferenciaPalletEntradaLote>();
            this.DepositoPlantaLocalizacaoRegras = new List<DepositoPlantaLocalizacaoRegra>();
            this.Estoques = new List<Estoque>();
            this.InventarioContagemProdutoes = new List<InventarioContagemProduto>();
            this.Lotes = new List<Lote>();
            this.ProdutoClienteAvarias = new List<ProdutoClienteAvaria>();
            this.ProdutoClienteRegras = new List<ProdutoClienteRegra>();
            this.ProdutoEmbalagems = new List<ProdutoEmbalagem>();
            this.ProdutoEstruturas = new List<ProdutoEstrutura>();
            this.ProdutoEstruturas1 = new List<ProdutoEstrutura>();
            this.RomaneioDocumentoConferencias = new List<RomaneioDocumentoConferencia>();
        }

        public int IDProdutoCliente { get; set; }
        public int IDCliente { get; set; }
        public Nullable<int> IDGrupoDeProduto { get; set; }
        public Nullable<int> IDDepositoPlantaLocalizacao { get; set; }
        public Nullable<int> IDClienteTipoDeMaterial { get; set; }
        public Nullable<int> IDUnidadeDeMedida { get; set; }
        public Nullable<int> IDContaContabilCredito { get; set; }
        public Nullable<int> IDContaContabilDebito { get; set; }
        public Nullable<int> IDCentroDeCustoCredito { get; set; }
        public Nullable<int> IDCentroDeCustoDebito { get; set; }
        public Nullable<int> IDCfop { get; set; }
        public string Codigo { get; set; }
        public string CodigoDoCliente { get; set; }
        public string Descricao { get; set; }
        public string DesmembraNaNF { get; set; }
        public string MetodoDeMovimentacao { get; set; }
        public string SolicitarDataDeValidade { get; set; }
        public Nullable<decimal> SaldoMinimo { get; set; }
        public Nullable<decimal> ConsumoMensal { get; set; }
        public Nullable<decimal> Ressuprimento { get; set; }
        public Nullable<decimal> UnidadeDoFornecedor { get; set; }
        public Nullable<decimal> UnidadeDoCliente { get; set; }
        public Nullable<System.DateTime> DataLimiteDeUso { get; set; }
        public string IsentoDeICMS { get; set; }
        public Nullable<decimal> ReducaoDeICMS { get; set; }
        public string DecretoDoICMS { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<decimal> Lastro { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public string Ativo { get; set; }
        public string GrupoDeProdutoCliente { get; set; }
        public string CodigoNBM { get; set; }
        public string CodigoNCM { get; set; }
        public Nullable<int> CodigoGenero { get; set; }
        public Nullable<decimal> UnidadeDoFornecedorAlterado { get; set; }
        public Nullable<decimal> UnidadeDoClienteAlterada { get; set; }
        public Nullable<System.DateTime> DataAlteracaoDaUnidade { get; set; }
        public string NomeDoArquivo { get; set; }
        public Nullable<int> IDFornecedor { get; set; }
        public string Marca { get; set; }
        public Nullable<int> FatorUsoPosicaoPallet { get; set; }
        public Nullable<int> ShelfLife { get; set; }
        public Nullable<int> IdContaContabil { get; set; }
        public Nullable<int> IdCentroDeCusto { get; set; }
        public string CodigoDoFornecedor { get; set; }
        public string OrigemDaMercadoria { get; set; }
        public Nullable<decimal> SLDEntrada { get; set; }
        public Nullable<decimal> SLDRetorno { get; set; }
        public Nullable<decimal> SLDARetornar { get; set; }
        public Nullable<decimal> SLDUA { get; set; }
        public string InseridoPor { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual CentroDeCusto CentroDeCusto1 { get; set; }
        public virtual Cfop Cfop { get; set; }
        public virtual ClienteTipoDeMaterial ClienteTipoDeMaterial { get; set; }
        public virtual ICollection<ConferenciaPalletDocVolItem> ConferenciaPalletDocVolItems { get; set; }
        public virtual ICollection<ConferenciaPalletEntrada> ConferenciaPalletEntradas { get; set; }
        public virtual ICollection<ConferenciaPalletEntradaLote> ConferenciaPalletEntradaLotes { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual ContaContabil ContaContabil1 { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual ICollection<DepositoPlantaLocalizacaoRegra> DepositoPlantaLocalizacaoRegras { get; set; }
        public virtual ICollection<Estoque> Estoques { get; set; }
        public virtual GrupoDeProduto GrupoDeProduto { get; set; }
        public virtual ICollection<InventarioContagemProduto> InventarioContagemProdutoes { get; set; }
        public virtual ICollection<Lote> Lotes { get; set; }
        public virtual UnidadeDeMedida UnidadeDeMedida { get; set; }
        public virtual ICollection<ProdutoClienteAvaria> ProdutoClienteAvarias { get; set; }
        public virtual ICollection<ProdutoClienteRegra> ProdutoClienteRegras { get; set; }
        public virtual ICollection<ProdutoEmbalagem> ProdutoEmbalagems { get; set; }
        public virtual ICollection<ProdutoEstrutura> ProdutoEstruturas { get; set; }
        public virtual ICollection<ProdutoEstrutura> ProdutoEstruturas1 { get; set; }
        public virtual ICollection<RomaneioDocumentoConferencia> RomaneioDocumentoConferencias { get; set; }
    }
}
