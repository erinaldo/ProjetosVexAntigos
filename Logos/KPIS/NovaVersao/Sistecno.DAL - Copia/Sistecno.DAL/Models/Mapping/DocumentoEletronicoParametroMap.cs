using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoEletronicoParametroMap : EntityTypeConfiguration<DocumentoEletronicoParametro>
    {
        public DocumentoEletronicoParametroMap()
        {
            // Primary Key
            this.HasKey(t => t.IdParametroCte);

            // Properties
            this.Property(t => t.IdParametroCte)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoEletronico)
                .HasMaxLength(20);

            this.Property(t => t.Certificado)
                .HasMaxLength(500);

            this.Property(t => t.TipoDeConfiguracao)
                .HasMaxLength(50);

            this.Property(t => t.Ambiente)
                .HasMaxLength(50);

            this.Property(t => t.ArquivoServidoresHom)
                .HasMaxLength(50);

            this.Property(t => t.ArquivoServidoresProd)
                .HasMaxLength(50);

            this.Property(t => t.TipoCertificado)
                .HasMaxLength(50);

            this.Property(t => t.VersaoManual)
                .HasMaxLength(50);

            this.Property(t => t.IgnoreInvalidCertificates)
                .HasMaxLength(3);

            this.Property(t => t.DiretorioLog)
                .HasMaxLength(50);

            this.Property(t => t.DiretorioTemplates)
                .HasMaxLength(50);

            this.Property(t => t.ValidarEsquemaAntesEnvio)
                .HasMaxLength(3);

            this.Property(t => t.DiretorioEsquemas)
                .HasMaxLength(50);

            this.Property(t => t.MappingFileName)
                .HasMaxLength(50);

            this.Property(t => t.FraseHomologacao)
                .HasMaxLength(50);

            this.Property(t => t.FraseContingencia)
                .HasMaxLength(50);

            this.Property(t => t.ModeloRetrato)
                .HasMaxLength(50);

            this.Property(t => t.LogotipoEmitente)
                .HasMaxLength(50);

            this.Property(t => t.Modelo)
                .HasMaxLength(10);

            this.Property(t => t.Serie)
                .HasMaxLength(10);

            this.Property(t => t.AnexarPDF)
                .HasMaxLength(3);

            this.Property(t => t.Usuario)
                .HasMaxLength(50);

            this.Property(t => t.senha)
                .HasMaxLength(50);

            this.Property(t => t.ConexaoSegura)
                .HasMaxLength(3);

            this.Property(t => t.Proxy)
                .HasMaxLength(50);

            this.Property(t => t.ModeloPaisagem)
                .HasMaxLength(50);

            this.Property(t => t.EmailServidor)
                .HasMaxLength(50);

            this.Property(t => t.EmailRemetente)
                .HasMaxLength(50);

            this.Property(t => t.EmailAssunto)
                .HasMaxLength(50);

            this.Property(t => t.EmailMensagem)
                .HasMaxLength(500);

            this.Property(t => t.EmailUsuario)
                .HasMaxLength(50);

            this.Property(t => t.EmailSenha)
                .HasMaxLength(50);

            this.Property(t => t.EmailDestinatario)
                .HasMaxLength(50);

            this.Property(t => t.EmailCCo)
                .HasMaxLength(50);

            this.Property(t => t.EmailCC)
                .HasMaxLength(50);

            this.Property(t => t.EmailAutenticacao)
                .HasMaxLength(3);

            this.Property(t => t.UF)
                .HasMaxLength(2);

            this.Property(t => t.cnpj)
                .HasMaxLength(15);

            this.Property(t => t.DiretorioXml)
                .HasMaxLength(50);

            this.Property(t => t.PinCode)
                .HasMaxLength(50);

            this.Property(t => t.LineDelimiters)
                .HasMaxLength(1);

            this.Property(t => t.TipoDeImpressao)
                .HasMaxLength(1);

            this.Property(t => t.TipoDeEmissao)
                .HasMaxLength(1);

            this.Property(t => t.Cidade)
                .HasMaxLength(200);

            this.Property(t => t.BrasaoPrefeitura)
                .HasMaxLength(200);

            this.Property(t => t.InscricaoMunicipal)
                .HasMaxLength(30);

            this.Property(t => t.RNTRC)
                .HasMaxLength(30);

            this.Property(t => t.DiretorioPdf)
                .HasMaxLength(50);

            this.Property(t => t.ModoOperacao)
                .HasMaxLength(10);

            this.Property(t => t.NomeSetup)
                .HasMaxLength(60);

            this.Property(t => t.VersaoSetup)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("DocumentoEletronicoParametro");
            this.Property(t => t.IdParametroCte).HasColumnName("IdParametroCte");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.TipoEletronico).HasColumnName("TipoEletronico");
            this.Property(t => t.Certificado).HasColumnName("Certificado");
            this.Property(t => t.TipoDeConfiguracao).HasColumnName("TipoDeConfiguracao");
            this.Property(t => t.Ambiente).HasColumnName("Ambiente");
            this.Property(t => t.ArquivoServidoresHom).HasColumnName("ArquivoServidoresHom");
            this.Property(t => t.ArquivoServidoresProd).HasColumnName("ArquivoServidoresProd");
            this.Property(t => t.TipoCertificado).HasColumnName("TipoCertificado");
            this.Property(t => t.VersaoManual).HasColumnName("VersaoManual");
            this.Property(t => t.IgnoreInvalidCertificates).HasColumnName("IgnoreInvalidCertificates");
            this.Property(t => t.MaxSizeLoteEnvio).HasColumnName("MaxSizeLoteEnvio");
            this.Property(t => t.DiretorioLog).HasColumnName("DiretorioLog");
            this.Property(t => t.DiretorioTemplates).HasColumnName("DiretorioTemplates");
            this.Property(t => t.ValidarEsquemaAntesEnvio).HasColumnName("ValidarEsquemaAntesEnvio");
            this.Property(t => t.DiretorioEsquemas).HasColumnName("DiretorioEsquemas");
            this.Property(t => t.MappingFileName).HasColumnName("MappingFileName");
            this.Property(t => t.FraseHomologacao).HasColumnName("FraseHomologacao");
            this.Property(t => t.FraseContingencia).HasColumnName("FraseContingencia");
            this.Property(t => t.ModeloRetrato).HasColumnName("ModeloRetrato");
            this.Property(t => t.LogotipoEmitente).HasColumnName("LogotipoEmitente");
            this.Property(t => t.Modelo).HasColumnName("Modelo");
            this.Property(t => t.Serie).HasColumnName("Serie");
            this.Property(t => t.AnexarPDF).HasColumnName("AnexarPDF");
            this.Property(t => t.TimeOut).HasColumnName("TimeOut");
            this.Property(t => t.Usuario).HasColumnName("Usuario");
            this.Property(t => t.senha).HasColumnName("senha");
            this.Property(t => t.ConexaoSegura).HasColumnName("ConexaoSegura");
            this.Property(t => t.Proxy).HasColumnName("Proxy");
            this.Property(t => t.ModeloPaisagem).HasColumnName("ModeloPaisagem");
            this.Property(t => t.QtdeCopias).HasColumnName("QtdeCopias");
            this.Property(t => t.EmailServidor).HasColumnName("EmailServidor");
            this.Property(t => t.EmailRemetente).HasColumnName("EmailRemetente");
            this.Property(t => t.EmailAssunto).HasColumnName("EmailAssunto");
            this.Property(t => t.EmailMensagem).HasColumnName("EmailMensagem");
            this.Property(t => t.EmailUsuario).HasColumnName("EmailUsuario");
            this.Property(t => t.EmailSenha).HasColumnName("EmailSenha");
            this.Property(t => t.EmailTimeOut).HasColumnName("EmailTimeOut");
            this.Property(t => t.EmailDestinatario).HasColumnName("EmailDestinatario");
            this.Property(t => t.EmailCCo).HasColumnName("EmailCCo");
            this.Property(t => t.EmailCC).HasColumnName("EmailCC");
            this.Property(t => t.EmailPorta).HasColumnName("EmailPorta");
            this.Property(t => t.EmailAutenticacao).HasColumnName("EmailAutenticacao");
            this.Property(t => t.UF).HasColumnName("UF");
            this.Property(t => t.cnpj).HasColumnName("cnpj");
            this.Property(t => t.DiretorioXml).HasColumnName("DiretorioXml");
            this.Property(t => t.ViasDeImpressao).HasColumnName("ViasDeImpressao");
            this.Property(t => t.PinCode).HasColumnName("PinCode");
            this.Property(t => t.LineDelimiters).HasColumnName("LineDelimiters");
            this.Property(t => t.TipoDeImpressao).HasColumnName("TipoDeImpressao");
            this.Property(t => t.TipoDeEmissao).HasColumnName("TipoDeEmissao");
            this.Property(t => t.Cidade).HasColumnName("Cidade");
            this.Property(t => t.BrasaoPrefeitura).HasColumnName("BrasaoPrefeitura");
            this.Property(t => t.InscricaoMunicipal).HasColumnName("InscricaoMunicipal");
            this.Property(t => t.RNTRC).HasColumnName("RNTRC");
            this.Property(t => t.DiretorioPdf).HasColumnName("DiretorioPdf");
            this.Property(t => t.ModoOperacao).HasColumnName("ModoOperacao");
            this.Property(t => t.NomeSetup).HasColumnName("NomeSetup");
            this.Property(t => t.VersaoSetup).HasColumnName("VersaoSetup");
        }
    }
}
