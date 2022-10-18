using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Documento
    {
        public Documento()
        {
            this.AgendamentoDocumentoes = new List<AgendamentoDocumento>();
            this.ColetorConferencias = new List<ColetorConferencia>();
            this.ColetorConferenciaLogs = new List<ColetorConferenciaLog>();
            this.ConferenciaPalletDocs = new List<ConferenciaPalletDoc>();
            this.DocumentoAFaturars = new List<DocumentoAFaturar>();
            this.DocumentoAguardandoCTRCs = new List<DocumentoAguardandoCTRC>();
            this.DocumentoCfops = new List<DocumentoCfop>();
            this.DocumentoComprovantes = new List<DocumentoComprovante>();
            this.DocumentoComprovantes1 = new List<DocumentoComprovante>();
            this.DocumentoCondicaoDePagamentoes = new List<DocumentoCondicaoDePagamento>();
            this.DocumentoCubagems = new List<DocumentoCubagem>();
            this.DocumentoEdis = new List<DocumentoEdi>();
            this.DocumentoEletronicoes = new List<DocumentoEletronico>();
            this.DocumentoEmbalagems = new List<DocumentoEmbalagem>();
            this.DocumentoFilials = new List<DocumentoFilial>();
            this.DocumentoImagems = new List<DocumentoImagem>();
            this.DocumentoFretes = new List<DocumentoFrete>();
            this.DocumentoItems = new List<DocumentoItem>();
            this.DocumentoNotaFiscals = new List<DocumentoNotaFiscal>();
            this.DocumentoNotaFiscals1 = new List<DocumentoNotaFiscal>();
            this.DocumentoObjetoes = new List<DocumentoObjeto>();
            this.DocumentoObservacaos = new List<DocumentoObservacao>();
            this.DocumentoOcorrencias = new List<DocumentoOcorrencia>();
            this.DocumentoOrcamentoes = new List<DocumentoOrcamento>();
            this.DocumentoRecebimentoes = new List<DocumentoRecebimento>();
            this.DocumentoReclamacaos = new List<DocumentoReclamacao>();
            this.DocumentoRelacionadoes = new List<DocumentoRelacionado>();
            this.DocumentoRelacionadoes1 = new List<DocumentoRelacionado>();
            this.DocumentoReprogramadoes = new List<DocumentoReprogramado>();
            this.DTFaturamentoClientes = new List<DTFaturamentoCliente>();
            this.DTFaturamentoClienteDocumentoes = new List<DTFaturamentoClienteDocumento>();
            this.PalletDocumentoes = new List<PalletDocumento>();
            this.DocumentoCDAs = new List<DocumentoCDA>();
            this.LabelPickings = new List<LabelPicking>();
            this.Lotes = new List<Lote>();
            this.MovimentacaoRomaneioItems = new List<MovimentacaoRomaneioItem>();
            this.RomaneioDocumentoes = new List<RomaneioDocumento>();
            this.RomaneioDocumentoes1 = new List<RomaneioDocumento>();
            this.RomaneioDocumentoConferencias = new List<RomaneioDocumentoConferencia>();
            this.RomaneioDocumentoFretes = new List<RomaneioDocumentoFrete>();
            this.RPCIDocumentoes = new List<RPCIDocumento>();
            this.TituloDocumentoes = new List<TituloDocumento>();
        }

        public int IDDocumento { get; set; }
        public int IDFilial { get; set; }
        public int IDFilialAtual { get; set; }
        public Nullable<int> IDProprietarioDocumento { get; set; }
        public Nullable<int> IDDocumentoOcorrencia { get; set; }
        public Nullable<int> IDCondicaoDePagamento { get; set; }
        public string TipoDeDocumento { get; set; }
        public string TipoDeServico { get; set; }
        public string Serie { get; set; }
        public Nullable<int> Numero { get; set; }
        public string AnoMes { get; set; }
        public string NumeroOriginal { get; set; }
        public int IDModal { get; set; }
        public int IDCliente { get; set; }
        public int IDRemetente { get; set; }
        public int IDDestinatario { get; set; }
        public Nullable<int> IDConsignatario { get; set; }
        public Nullable<int> IDGrupoDeProduto { get; set; }
        public Nullable<int> IDRedespachante { get; set; }
        public Nullable<int> IDClienteDivisao { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public Nullable<int> IDTes { get; set; }
        public Nullable<int> IDTesCFOP { get; set; }
        public string ClasseCFOP { get; set; }
        public string Origem { get; set; }
        public string EntradaSaida { get; set; }
        public Nullable<System.DateTime> DataDoMovimento { get; set; }
        public Nullable<System.DateTime> DataDeEmissao { get; set; }
        public Nullable<System.DateTime> DataDeEntrada { get; set; }
        public Nullable<System.DateTime> DataPlanejadaOriginal { get; set; }
        public Nullable<System.DateTime> DataPlanejada { get; set; }
        public Nullable<System.DateTime> DataDeSaida { get; set; }
        public Nullable<System.DateTime> PrevisaoDeRecebimento { get; set; }
        public Nullable<System.DateTime> PrevisaoDeSaida { get; set; }
        public Nullable<System.DateTime> DataDaUltimaOcorrencia { get; set; }
        public Nullable<System.DateTime> DataDeConclusao { get; set; }
        public Nullable<System.DateTime> DataDeCancelamento { get; set; }
        public Nullable<decimal> PesoLiquido { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public Nullable<decimal> PesoCubado { get; set; }
        public Nullable<decimal> PesoCalculado { get; set; }
        public Nullable<decimal> MetragemCubica { get; set; }
        public Nullable<decimal> Volumes { get; set; }
        public string CifFob { get; set; }
        public string Natureza { get; set; }
        public string Especie { get; set; }
        public string Impresso { get; set; }
        public Nullable<decimal> ValorDaNota { get; set; }
        public Nullable<decimal> ValorDasMercadorias { get; set; }
        public Nullable<decimal> ValorDoServico { get; set; }
        public Nullable<decimal> ValorDoSeguro { get; set; }
        public Nullable<decimal> ValorDoICMS { get; set; }
        public Nullable<decimal> ValorDoIPI { get; set; }
        public Nullable<decimal> BaseDoIPI { get; set; }
        public Nullable<decimal> BaseDoICMS { get; set; }
        public Nullable<decimal> ValorDoICMSSubst { get; set; }
        public Nullable<decimal> BaseDoICMSSubst { get; set; }
        public Nullable<decimal> ValorDeDesconto { get; set; }
        public string Endereco { get; set; }
        public string EnderecoNumero { get; set; }
        public string EnderecoComplemento { get; set; }
        public Nullable<int> IDEnderecoBairro { get; set; }
        public Nullable<int> IDEnderecoCidade { get; set; }
        public string EnderecoCep { get; set; }
        public string Ativo { get; set; }
        public string DocumentoDoCliente { get; set; }
        public string DocumentoDoClienteSerie { get; set; }
        public string PagarReceber { get; set; }
        public string Status { get; set; }
        public string CodigoDoRecExp { get; set; }
        public string CodigoDeBarrasRecExp { get; set; }
        public string CodigoDoRecExpImpresso { get; set; }
        public Nullable<System.DateTime> DataDoRecExp { get; set; }
        public Nullable<System.DateTime> DataGeracaoDoArquivo { get; set; }
        public string NomeDoArquivo { get; set; }
        public string DocumentoDoCliente1 { get; set; }
        public string DocumentoDoCliente2 { get; set; }
        public Nullable<decimal> IcmsSuframa { get; set; }
        public string Enviado { get; set; }
        public Nullable<int> idOcorrenciaAndamento { get; set; }
        public Nullable<int> Prazo { get; set; }
        public Nullable<decimal> PrazoUtilizado { get; set; }
        public Nullable<int> IdCDA { get; set; }
        public string CepColeta { get; set; }
        public string Coleta { get; set; }
        public Nullable<System.DateTime> DataDeReprogramacao { get; set; }
        public Nullable<decimal> FatorDeCubagemCalculado { get; set; }
        public Nullable<int> IdCidadeColeta { get; set; }
        public Nullable<int> IdBairroColeta { get; set; }
        public Nullable<int> IdRomaneio { get; set; }
        public string Paletizado { get; set; }
        public Nullable<int> QuantidadeDePallets { get; set; }
        public Nullable<decimal> VolumesComFator { get; set; }
        public string EnderecoColeta { get; set; }
        public Nullable<int> IdEnderecoUf { get; set; }
        public Nullable<int> IdColetaUf { get; set; }
        public Nullable<int> IDModalidade { get; set; }
        public Nullable<int> IdMotivo { get; set; }
        public Nullable<int> IdFilialDestino { get; set; }
        public string Adiantamento { get; set; }
        public Nullable<decimal> DiasOcorrenciaCliente { get; set; }
        public string DocumentoDoCliente3 { get; set; }
        public string DocumentodoCliente4 { get; set; }
        public Nullable<decimal> ValorDoISS { get; set; }
        public Nullable<int> IdUsuarioDeTabelaDeFrete { get; set; }
        public Nullable<System.DateTime> PrevisaoDeEmbarque { get; set; }
        public Nullable<int> IdTransportadora { get; set; }
        public Nullable<int> IdVeiculo { get; set; }
        public Nullable<decimal> ValorDescontoServico { get; set; }
        public Nullable<int> QuantidadeFeriados { get; set; }
        public Nullable<int> QuantidadeSabDom { get; set; }
        public Nullable<int> PrazoUtilizadoCerto { get; set; }
        public Nullable<int> PrazoUtilizadoCorrido { get; set; }
        public Nullable<int> IDTipoDeMovimento { get; set; }
        public Nullable<decimal> ValorPis { get; set; }
        public Nullable<decimal> ValorCofins { get; set; }
        public string ModeloDocumento { get; set; }
        public string TipoDeFrete { get; set; }
        public Nullable<decimal> ValorDoFrete { get; set; }
        public Nullable<decimal> PisSubstituicao { get; set; }
        public Nullable<decimal> CofinsSubstituicao { get; set; }
        public string SituacaoDocumento { get; set; }
        public string ChaveNFReferencia { get; set; }
        public string SubSerie { get; set; }
        public Nullable<int> IdContaContabilFilial { get; set; }
        public string SituacaoTributariaPIS { get; set; }
        public string SituacaoTributariaCOFINS { get; set; }
        public string BaseDoCredito { get; set; }
        public Nullable<decimal> BaseCalculoPIS { get; set; }
        public Nullable<decimal> BaseCalculoCOFINS { get; set; }
        public Nullable<decimal> AliquotaPIS { get; set; }
        public Nullable<decimal> AliquotaCOFINS { get; set; }
        public Nullable<int> IdContaContabilPIS { get; set; }
        public Nullable<int> IdContaContabilCOFINS { get; set; }
        public string CentroDeCustoCliente { get; set; }
        public string CST_Icms { get; set; }
        public string NomeDoArquivo1 { get; set; }
        public Nullable<System.DateTime> DataDeAgendamento { get; set; }
        public string FreteConciliado { get; set; }
        public string NomeDoArquivo2 { get; set; }
        public Nullable<int> NumeroDocumentoEletronico { get; set; }
        public string TPServico { get; set; }
        public string CteTipoDeCte { get; set; }
        public string CteTipoDeServico { get; set; }
        public string CteFormaDeEmissao { get; set; }
        public string CteModal { get; set; }
        public Nullable<decimal> ValorDoDesconto { get; set; }
        public string DocumentoEspecial { get; set; }
        public Nullable<int> IdTipoDeOperacao { get; set; }
        public Nullable<int> IDExpedidor { get; set; }
        public string PeriodoAgendamento { get; set; }
        public Nullable<int> IdVeiculoTipo { get; set; }
        public Nullable<decimal> TaxaDescargaPallet { get; set; }
        public Nullable<decimal> TaxaDescargaPeso { get; set; }
        public string DocumentoAverbado { get; set; }
        public Nullable<System.DateTime> DataDeAverbacao { get; set; }
        public string TipoAgendamento { get; set; }
        public Nullable<System.DateTime> DataInicialEntrega { get; set; }
        public string HoraInicialEntrega { get; set; }
        public Nullable<System.DateTime> DataFinalEntrega { get; set; }
        public string HoraFinalEntrega { get; set; }
        public string EnviadoComprovei { get; set; }
        public Nullable<decimal> vFCPUFDest { get; set; }
        public Nullable<decimal> vICMSUFDest { get; set; }
        public Nullable<decimal> vICMSUFRemet { get; set; }
        public virtual ICollection<AgendamentoDocumento> AgendamentoDocumentoes { get; set; }
        public virtual ICollection<ColetorConferencia> ColetorConferencias { get; set; }
        public virtual ICollection<ColetorConferenciaLog> ColetorConferenciaLogs { get; set; }
        public virtual ICollection<ConferenciaPalletDoc> ConferenciaPalletDocs { get; set; }
        public virtual DocumentoOcorrencia DocumentoOcorrencia { get; set; }
        public virtual ICollection<DocumentoAFaturar> DocumentoAFaturars { get; set; }
        public virtual ICollection<DocumentoAguardandoCTRC> DocumentoAguardandoCTRCs { get; set; }
        public virtual ICollection<DocumentoCfop> DocumentoCfops { get; set; }
        public virtual ICollection<DocumentoComprovante> DocumentoComprovantes { get; set; }
        public virtual ICollection<DocumentoComprovante> DocumentoComprovantes1 { get; set; }
        public virtual ICollection<DocumentoCondicaoDePagamento> DocumentoCondicaoDePagamentoes { get; set; }
        public virtual ICollection<DocumentoCubagem> DocumentoCubagems { get; set; }
        public virtual ICollection<DocumentoEdi> DocumentoEdis { get; set; }
        public virtual ICollection<DocumentoEletronico> DocumentoEletronicoes { get; set; }
        public virtual ICollection<DocumentoEmbalagem> DocumentoEmbalagems { get; set; }
        public virtual ICollection<DocumentoFilial> DocumentoFilials { get; set; }
        public virtual ICollection<DocumentoImagem> DocumentoImagems { get; set; }
        public virtual ICollection<DocumentoFrete> DocumentoFretes { get; set; }
        public virtual ICollection<DocumentoItem> DocumentoItems { get; set; }
        public virtual ICollection<DocumentoNotaFiscal> DocumentoNotaFiscals { get; set; }
        public virtual ICollection<DocumentoNotaFiscal> DocumentoNotaFiscals1 { get; set; }
        public virtual ICollection<DocumentoObjeto> DocumentoObjetoes { get; set; }
        public virtual ICollection<DocumentoObservacao> DocumentoObservacaos { get; set; }
        public virtual ICollection<DocumentoOcorrencia> DocumentoOcorrencias { get; set; }
        public virtual ICollection<DocumentoOrcamento> DocumentoOrcamentoes { get; set; }
        public virtual ICollection<DocumentoRecebimento> DocumentoRecebimentoes { get; set; }
        public virtual ICollection<DocumentoReclamacao> DocumentoReclamacaos { get; set; }
        public virtual ICollection<DocumentoRelacionado> DocumentoRelacionadoes { get; set; }
        public virtual ICollection<DocumentoRelacionado> DocumentoRelacionadoes1 { get; set; }
        public virtual ICollection<DocumentoReprogramado> DocumentoReprogramadoes { get; set; }
        public virtual ICollection<DTFaturamentoCliente> DTFaturamentoClientes { get; set; }
        public virtual ICollection<DTFaturamentoClienteDocumento> DTFaturamentoClienteDocumentoes { get; set; }
        public virtual ICollection<PalletDocumento> PalletDocumentoes { get; set; }
        public virtual ICollection<DocumentoCDA> DocumentoCDAs { get; set; }
        public virtual ICollection<LabelPicking> LabelPickings { get; set; }
        public virtual ICollection<Lote> Lotes { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItems { get; set; }
        public virtual ICollection<RomaneioDocumento> RomaneioDocumentoes { get; set; }
        public virtual ICollection<RomaneioDocumento> RomaneioDocumentoes1 { get; set; }
        public virtual ICollection<RomaneioDocumentoConferencia> RomaneioDocumentoConferencias { get; set; }
        public virtual ICollection<RomaneioDocumentoFrete> RomaneioDocumentoFretes { get; set; }
        public virtual ICollection<RPCIDocumento> RPCIDocumentoes { get; set; }
        public virtual ICollection<TituloDocumento> TituloDocumentoes { get; set; }
    }
}
