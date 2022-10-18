using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            this.Avisoes = new List<Aviso>();
            this.ClienteDivisaos = new List<ClienteDivisao>();
            this.ClienteEDIs = new List<ClienteEDI>();
            this.ClienteFilials = new List<ClienteFilial>();
            this.ClienteMetas = new List<ClienteMeta>();
            this.ClienteSetorFilials = new List<ClienteSetorFilial>();
            this.UsuarioDeTabelaDeFretes = new List<UsuarioDeTabelaDeFrete>();
            this.ClienteTipoDeMaterials = new List<ClienteTipoDeMaterial>();
            this.DepositoPlantaLocalizacaos = new List<DepositoPlantaLocalizacao>();
            this.DepositoPlantaLocalizacaoRegras = new List<DepositoPlantaLocalizacaoRegra>();
            this.DTFaturamentoClientes = new List<DTFaturamentoCliente>();
            this.GrupoDeProdutoes = new List<GrupoDeProduto>();
            this.MenuClientes = new List<MenuCliente>();
            this.Regiaos = new List<Regiao>();
            this.RepresentanteClientes = new List<RepresentanteCliente>();
            this.Sobras = new List<Sobra>();
            this.Tituloes = new List<Titulo>();
            this.UsuarioClientes = new List<UsuarioCliente>();
            this.UsuarioDeTabelaDeFretes1 = new List<UsuarioDeTabelaDeFrete>();
        }

        public int IDCliente { get; set; }
        public Nullable<int> IDContaContabilCredito { get; set; }
        public Nullable<int> IDContaContabilDebito { get; set; }
        public Nullable<int> IDCentroDeCustoCredito { get; set; }
        public Nullable<int> IDCentroDeCustoDebito { get; set; }
        public Nullable<int> IDCfop { get; set; }
        public int CodigoDoCliente { get; set; }
        public int CodigoDoClienteFilial { get; set; }
        public Nullable<int> IDRamoDeAtividade { get; set; }
        public string SeguroProprio { get; set; }
        public string Bloqueado { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public string CodigoDeBarras { get; set; }
        public string FretePadrao { get; set; }
        public string AgrupaDocumentos { get; set; }
        public Nullable<decimal> LimiteDeCredito { get; set; }
        public string MetodoDeArmazenagem { get; set; }
        public string SiglaDoCliente { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> IDFilialPadraoInternet { get; set; }
        public Nullable<int> IDContaContabil { get; set; }
        public Nullable<int> IDCentroDeCusto { get; set; }
        public string ComposicaoDoCodigo { get; set; }
        public Nullable<int> PrazoDePagamento { get; set; }
        public string HorarioDeCorte { get; set; }
        public string UniRecebimento { get; set; }
        public Nullable<int> PrazoDeEntregaNoCD { get; set; }
        public Nullable<int> IdTes { get; set; }
        public Nullable<int> PrazoCorte { get; set; }
        public Nullable<int> DiaExpiracaoLimite { get; set; }
        public string NFCodigoCliente { get; set; }
        public string SistranWeb { get; set; }
        public Nullable<int> IcmsFixo { get; set; }
        public Nullable<int> ValidadeDeCota { get; set; }
        public Nullable<decimal> FatorDeCubagem { get; set; }
        public Nullable<int> IdTesCfop { get; set; }
        public string VerificaSaldoPedido { get; set; }
        public Nullable<int> PrazoCorteD { get; set; }
        public Nullable<decimal> Recebimento { get; set; }
        public Nullable<decimal> Expedicao { get; set; }
        public string Expedicao_Unidade { get; set; }
        public string contrato { get; set; }
        public string Recebto_Unidade { get; set; }
        public Nullable<int> IdOcorrenciaSerie { get; set; }
        public string FaturamentoPorCte { get; set; }
        public string SerieNFE { get; set; }
        public string EmiteNotaFiscalServicoDeTransporte { get; set; }
        public string Averbacao { get; set; }
        public Nullable<int> Lastro { get; set; }
        public Nullable<int> Altura { get; set; }
        public virtual ICollection<Aviso> Avisoes { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual Cfop Cfop { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual RamoDeAtividade RamoDeAtividade { get; set; }
        public virtual ICollection<ClienteDivisao> ClienteDivisaos { get; set; }
        public virtual ICollection<ClienteEDI> ClienteEDIs { get; set; }
        public virtual ICollection<ClienteFilial> ClienteFilials { get; set; }
        public virtual ICollection<ClienteMeta> ClienteMetas { get; set; }
        public virtual ClienteRegra ClienteRegra { get; set; }
        public virtual ICollection<ClienteSetorFilial> ClienteSetorFilials { get; set; }
        public virtual ICollection<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFretes { get; set; }
        public virtual ICollection<ClienteTipoDeMaterial> ClienteTipoDeMaterials { get; set; }
        public virtual ICollection<DepositoPlantaLocalizacao> DepositoPlantaLocalizacaos { get; set; }
        public virtual ICollection<DepositoPlantaLocalizacaoRegra> DepositoPlantaLocalizacaoRegras { get; set; }
        public virtual ICollection<DTFaturamentoCliente> DTFaturamentoClientes { get; set; }
        public virtual ICollection<GrupoDeProduto> GrupoDeProdutoes { get; set; }
        public virtual ICollection<MenuCliente> MenuClientes { get; set; }
        public virtual ICollection<Regiao> Regiaos { get; set; }
        public virtual ICollection<RepresentanteCliente> RepresentanteClientes { get; set; }
        public virtual ICollection<Sobra> Sobras { get; set; }
        public virtual ICollection<Titulo> Tituloes { get; set; }
        public virtual ICollection<UsuarioCliente> UsuarioClientes { get; set; }
        public virtual ICollection<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFretes1 { get; set; }
    }
}
