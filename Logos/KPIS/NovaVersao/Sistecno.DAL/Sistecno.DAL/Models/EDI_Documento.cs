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
    
    public partial class EDI_Documento
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public string NomeBairro { get; set; }
        public string NomeCidade { get; set; }
        public string UF { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public Nullable<int> IDFilial { get; set; }
        public Nullable<int> IDFilialAtual { get; set; }
        public Nullable<int> IDProprietarioDocumento { get; set; }
        public Nullable<int> IDDocumentoOcorrencia { get; set; }
        public Nullable<int> IDCondicaoDePagamento { get; set; }
        public string TipoDeDocumento { get; set; }
        public string TipoDeServico { get; set; }
        public string Serie { get; set; }
        public Nullable<int> Numero { get; set; }
        public string AnoMes { get; set; }
        public string NumeroOriginal { get; set; }
        public Nullable<int> IDModal { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public Nullable<int> IDRemetente { get; set; }
        public Nullable<int> IDDestinatario { get; set; }
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
    }
}
