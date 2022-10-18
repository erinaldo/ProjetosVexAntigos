using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoEletronicoParametro
    {
        public int IdParametroCte { get; set; }
        public Nullable<int> IdFilial { get; set; }
        public string TipoEletronico { get; set; }
        public string Certificado { get; set; }
        public string TipoDeConfiguracao { get; set; }
        public string Ambiente { get; set; }
        public string ArquivoServidoresHom { get; set; }
        public string ArquivoServidoresProd { get; set; }
        public string TipoCertificado { get; set; }
        public string VersaoManual { get; set; }
        public string IgnoreInvalidCertificates { get; set; }
        public Nullable<int> MaxSizeLoteEnvio { get; set; }
        public string DiretorioLog { get; set; }
        public string DiretorioTemplates { get; set; }
        public string ValidarEsquemaAntesEnvio { get; set; }
        public string DiretorioEsquemas { get; set; }
        public string MappingFileName { get; set; }
        public string FraseHomologacao { get; set; }
        public string FraseContingencia { get; set; }
        public string ModeloRetrato { get; set; }
        public string LogotipoEmitente { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string AnexarPDF { get; set; }
        public Nullable<int> TimeOut { get; set; }
        public string Usuario { get; set; }
        public string senha { get; set; }
        public string ConexaoSegura { get; set; }
        public string Proxy { get; set; }
        public string ModeloPaisagem { get; set; }
        public Nullable<int> QtdeCopias { get; set; }
        public string EmailServidor { get; set; }
        public string EmailRemetente { get; set; }
        public string EmailAssunto { get; set; }
        public string EmailMensagem { get; set; }
        public string EmailUsuario { get; set; }
        public string EmailSenha { get; set; }
        public Nullable<int> EmailTimeOut { get; set; }
        public string EmailDestinatario { get; set; }
        public string EmailCCo { get; set; }
        public string EmailCC { get; set; }
        public Nullable<int> EmailPorta { get; set; }
        public string EmailAutenticacao { get; set; }
        public string UF { get; set; }
        public string cnpj { get; set; }
        public string DiretorioXml { get; set; }
        public Nullable<int> ViasDeImpressao { get; set; }
        public string PinCode { get; set; }
        public string LineDelimiters { get; set; }
        public string TipoDeImpressao { get; set; }
        public string TipoDeEmissao { get; set; }
        public string Cidade { get; set; }
        public string BrasaoPrefeitura { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string RNTRC { get; set; }
        public string DiretorioPdf { get; set; }
        public string ModoOperacao { get; set; }
        public string NomeSetup { get; set; }
        public string VersaoSetup { get; set; }
    }
}
