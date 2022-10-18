using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DT
    {
        public DT()
        {
            this.DTRomaneios = new List<DTRomaneio>();
            this.DtCheques = new List<DtCheque>();
            this.DTContaCorrentes = new List<DTContaCorrente>();
            this.DTFaturamentoes = new List<DTFaturamento>();
            this.DTHistoricoes = new List<DTHistorico>();
            this.Portarias = new List<Portaria>();
            this.Rastreamentoes = new List<Rastreamento>();
            this.RPCIDocumentoes = new List<RPCIDocumento>();
            this.TituloDocumentoes = new List<TituloDocumento>();
        }

        public int IDDT { get; set; }
        public int IDFilial { get; set; }
        public int Numero { get; set; }
        public int IDDTTipo { get; set; }
        public Nullable<int> IDFilialDestino { get; set; }
        public Nullable<int> IDTransportadora { get; set; }
        public Nullable<int> IDRedespacho { get; set; }
        public Nullable<int> IDPrimeiroVeiculo { get; set; }
        public Nullable<int> IDSegundoVeiculo { get; set; }
        public Nullable<int> IDPrimeiroMotorista { get; set; }
        public Nullable<int> IDSegundoMotorista { get; set; }
        public Nullable<int> IDCadastroTitular { get; set; }
        public Nullable<int> IDProprietarioPrimeiroVeiculo { get; set; }
        public Nullable<int> IDProprietarioSegundoVeiculo { get; set; }
        public Nullable<int> IDUsuarioEmitiu { get; set; }
        public Nullable<int> IDUsuarioBaixou { get; set; }
        public Nullable<int> IDModal { get; set; }
        public Nullable<int> IDTipoDeMonitoramento { get; set; }
        public Nullable<int> IDTipoDeEscolta { get; set; }
        public Nullable<int> IDEmpresaEscolta { get; set; }
        public Nullable<int> IDEmpresaMonitoramento { get; set; }
        public Nullable<int> IDCidadeDeOrigem { get; set; }
        public Nullable<int> IDCidadeDeDestino { get; set; }
        public Nullable<int> IDRpci { get; set; }
        public Nullable<int> IdRastreador { get; set; }
        public Nullable<System.DateTime> Cadastro { get; set; }
        public Nullable<System.DateTime> Emissao { get; set; }
        public Nullable<System.DateTime> Baixado { get; set; }
        public Nullable<System.DateTime> DataDeSaida { get; set; }
        public string HoraDeSaida { get; set; }
        public Nullable<System.DateTime> DataDeChegada { get; set; }
        public string HoraDeChegada { get; set; }
        public string Lacres { get; set; }
        public Nullable<decimal> PalletsExpedido { get; set; }
        public Nullable<decimal> PalletsChep { get; set; }
        public Nullable<decimal> PalletsPbr { get; set; }
        public Nullable<decimal> ValorDaNota { get; set; }
        public Nullable<decimal> Volumes { get; set; }
        public Nullable<decimal> PesoLiquido { get; set; }
        public Nullable<decimal> PesoCubado { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public Nullable<decimal> ValorFreteCtr { get; set; }
        public Nullable<decimal> ValorIcmsCtr { get; set; }
        public Nullable<decimal> CreditoValorDoServico { get; set; }
        public Nullable<decimal> CreditoAgenciamento { get; set; }
        public Nullable<decimal> CreditoPedagio { get; set; }
        public Nullable<decimal> CreditoCarga { get; set; }
        public Nullable<decimal> CreditoDescarga { get; set; }
        public Nullable<decimal> CreditoDiaria { get; set; }
        public Nullable<decimal> CreditoColeta { get; set; }
        public Nullable<decimal> CreditoEntrega { get; set; }
        public Nullable<decimal> CreditoAjudante { get; set; }
        public Nullable<decimal> CreditoAdicional { get; set; }
        public Nullable<decimal> CreditoOutros { get; set; }
        public Nullable<decimal> DebitoSeguro { get; set; }
        public Nullable<decimal> DebitoOutros { get; set; }
        public Nullable<decimal> DebitoAdiantamento { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<decimal> SaldoAReceber { get; set; }
        public Nullable<decimal> KMInicial { get; set; }
        public Nullable<decimal> KMFinal { get; set; }
        public Nullable<decimal> KMTotal { get; set; }
        public string Ativo { get; set; }
        public string Impresso { get; set; }
        public string Situacao { get; set; }
        public string Andamento { get; set; }
        public Nullable<int> idPortaria { get; set; }
        public Nullable<decimal> CreditosForaDoCalculo { get; set; }
        public Nullable<decimal> DebitosForaDoCalculo { get; set; }
        public string SituacaoImpressao { get; set; }
        public Nullable<decimal> VolumesComFator { get; set; }
        public Nullable<int> IDAgregado { get; set; }
        public Nullable<decimal> Entrega { get; set; }
        public Nullable<decimal> Grupos { get; set; }
        public Nullable<decimal> Setor { get; set; }
        public Nullable<int> Chapatex { get; set; }
        public string SituacaoFaturamento { get; set; }
        public Nullable<int> IdDtTipoRE { get; set; }
        public string SerieDT { get; set; }
        public Nullable<int> NumeroMDFe { get; set; }
        public string Origem { get; set; }
        public string Escolta { get; set; }
        public string NumeroLiberacao { get; set; }
        public string DescricaoRota { get; set; }
        public virtual Cadastro Cadastro1 { get; set; }
        public virtual Cadastro Cadastro2 { get; set; }
        public virtual Cadastro Cadastro3 { get; set; }
        public virtual Cadastro Cadastro4 { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Cidade Cidade1 { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Filial Filial1 { get; set; }
        public virtual Motorista Motorista { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual Proprietario Proprietario { get; set; }
        public virtual Proprietario Proprietario1 { get; set; }
        public virtual Motorista Motorista1 { get; set; }
        public virtual Transportadora Transportadora { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual Veiculo Veiculo1 { get; set; }
        public virtual ICollection<DTRomaneio> DTRomaneios { get; set; }
        public virtual DT DT1 { get; set; }
        public virtual DT DT2 { get; set; }
        public virtual DTTipo DTTipo { get; set; }
        public virtual Modal Modal { get; set; }
        public virtual Rastreador Rastreador { get; set; }
        public virtual RPCI RPCI { get; set; }
        public virtual TipoDeEscolta TipoDeEscolta { get; set; }
        public virtual TipoDeMonitoramento TipoDeMonitoramento { get; set; }
        public virtual ICollection<DtCheque> DtCheques { get; set; }
        public virtual ICollection<DTContaCorrente> DTContaCorrentes { get; set; }
        public virtual ICollection<DTFaturamento> DTFaturamentoes { get; set; }
        public virtual ICollection<DTHistorico> DTHistoricoes { get; set; }
        public virtual ICollection<Portaria> Portarias { get; set; }
        public virtual ICollection<Rastreamento> Rastreamentoes { get; set; }
        public virtual ICollection<RPCIDocumento> RPCIDocumentoes { get; set; }
        public virtual ICollection<TituloDocumento> TituloDocumentoes { get; set; }
    }
}
