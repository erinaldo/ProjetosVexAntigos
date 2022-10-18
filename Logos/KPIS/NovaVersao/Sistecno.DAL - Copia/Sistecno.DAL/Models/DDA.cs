using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DDA
    {
        public DDA()
        {
            this.DDAImagems = new List<DDAImagem>();
            this.TituloDuplicatas = new List<TituloDuplicata>();
        }

        public int IdDDA { get; set; }
        public Nullable<int> IdFornecedor { get; set; }
        public Nullable<int> IdSacado { get; set; }
        public string HIdentificacaoDoRegistro { get; set; }
        public string HIdentificacaoDaEmpresa { get; set; }
        public string HTipoInscricaoEmpresaPagadora { get; set; }
        public string HCnpjCpfClientePagador { get; set; }
        public string HEmpresaPagadora { get; set; }
        public string HTipoDeServico { get; set; }
        public string HCodigoDeOrigemDoArquivo { get; set; }
        public string HNumeroDoRetorno { get; set; }
        public Nullable<System.DateTime> HDataDoArquivo { get; set; }
        public string HHoraDoArquivo { get; set; }
        public string HTipoDeProcessamento { get; set; }
        public string HReservadoEmpresa { get; set; }
        public string HReservadoBanco { get; set; }
        public string HNumeroDaListaDeDebito { get; set; }
        public string HReservadoBanco1 { get; set; }
        public string HNumeroSequencialDoRegistro { get; set; }
        public string TIdentificacaoDoRegistro { get; set; }
        public string TTipoDeInscricaoDoFornecedor { get; set; }
        public string TCnpjCpf { get; set; }
        public string TNomeDoFornecedor { get; set; }
        public string TEnderecoDoFornecedor { get; set; }
        public string TCepDoFornecedor { get; set; }
        public string TCodigoDoBancoFornecedor { get; set; }
        public string TCodigoDaAgenciaFornecedor { get; set; }
        public string TDigitoDaAgenciaDoFornecedor { get; set; }
        public string TContaCorrenteDoFornecedor { get; set; }
        public string TDigitoDaContaCorrenteDoFornecedor { get; set; }
        public string TNumeroDoPagamento { get; set; }
        public string TCarteira { get; set; }
        public string TNossoNumero { get; set; }
        public string TSeuNumero { get; set; }
        public Nullable<System.DateTime> TVencimento { get; set; }
        public Nullable<System.DateTime> TEmissao { get; set; }
        public Nullable<System.DateTime> TDataLimiteParaDesconto { get; set; }
        public string TFatorDeVencimento { get; set; }
        public Nullable<decimal> TValorDoDocumento { get; set; }
        public Nullable<decimal> TValorDePagamento { get; set; }
        public Nullable<decimal> TValorDoDesconto { get; set; }
        public Nullable<decimal> TValorDoAcrescimo { get; set; }
        public string TTipoDeDocumento { get; set; }
        public string TNumeroDaNotaFiscal { get; set; }
        public string TSerieDaNotaFiscal { get; set; }
        public Nullable<System.DateTime> TDataParaEfetivacaoDoPagamento { get; set; }
        public string TSituacaoDoAgendamento { get; set; }
        public string TInformacaoDeRetorno { get; set; }
        public string TTipoDeMovimento { get; set; }
        public string TEnderecoDoSacadorAvalista { get; set; }
        public string TNomeDoSacadorAvalista { get; set; }
        public string TNivelDeInformacaoDeRetorno { get; set; }
        public string TComplemento { get; set; }
        public string TCodigoDaAreaDaEmpresa { get; set; }
        public string TUsoDaEmpresa { get; set; }
        public string TCodigoDeLancamento { get; set; }
        public string TTipoDeContaDoFornecedor { get; set; }
        public string TContaComplementar { get; set; }
        public string TSequencialDoRegistro { get; set; }
        public string LIdentificacaoDoRegistro { get; set; }
        public string LQuantidadeDeRegistro { get; set; }
        public Nullable<decimal> LValorTotalDePagamento { get; set; }
        public string LSequencialDoRegistro { get; set; }
        public virtual ICollection<DDAImagem> DDAImagems { get; set; }
        public virtual ICollection<TituloDuplicata> TituloDuplicatas { get; set; }
    }
}