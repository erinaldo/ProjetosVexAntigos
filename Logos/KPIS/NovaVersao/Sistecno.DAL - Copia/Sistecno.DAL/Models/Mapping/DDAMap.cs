using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DDAMap : EntityTypeConfiguration<DDA>
    {
        public DDAMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDDA);

            // Properties
            this.Property(t => t.IdDDA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HIdentificacaoDoRegistro)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HIdentificacaoDaEmpresa)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.HTipoInscricaoEmpresaPagadora)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HCnpjCpfClientePagador)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.HEmpresaPagadora)
                .IsFixedLength()
                .HasMaxLength(40);

            this.Property(t => t.HTipoDeServico)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.HCodigoDeOrigemDoArquivo)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.HNumeroDoRetorno)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.HHoraDoArquivo)
                .IsFixedLength()
                .HasMaxLength(6);

            this.Property(t => t.HTipoDeProcessamento)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HReservadoEmpresa)
                .IsFixedLength()
                .HasMaxLength(74);

            this.Property(t => t.HReservadoBanco)
                .IsFixedLength()
                .HasMaxLength(217);

            this.Property(t => t.HNumeroDaListaDeDebito)
                .IsFixedLength()
                .HasMaxLength(9);

            this.Property(t => t.HReservadoBanco1)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.HNumeroSequencialDoRegistro)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.TIdentificacaoDoRegistro)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TTipoDeInscricaoDoFornecedor)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TCnpjCpf)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.TNomeDoFornecedor)
                .IsFixedLength()
                .HasMaxLength(30);

            this.Property(t => t.TEnderecoDoFornecedor)
                .IsFixedLength()
                .HasMaxLength(40);

            this.Property(t => t.TCepDoFornecedor)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.TCodigoDoBancoFornecedor)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.TCodigoDaAgenciaFornecedor)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.TDigitoDaAgenciaDoFornecedor)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TContaCorrenteDoFornecedor)
                .IsFixedLength()
                .HasMaxLength(13);

            this.Property(t => t.TDigitoDaContaCorrenteDoFornecedor)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TNumeroDoPagamento)
                .IsFixedLength()
                .HasMaxLength(16);

            this.Property(t => t.TCarteira)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.TNossoNumero)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.TSeuNumero)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.TFatorDeVencimento)
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.TTipoDeDocumento)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TNumeroDaNotaFiscal)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TSerieDaNotaFiscal)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TSituacaoDoAgendamento)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TInformacaoDeRetorno)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TTipoDeMovimento)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TEnderecoDoSacadorAvalista)
                .IsFixedLength()
                .HasMaxLength(40);

            this.Property(t => t.TNomeDoSacadorAvalista)
                .IsFixedLength()
                .HasMaxLength(40);

            this.Property(t => t.TNivelDeInformacaoDeRetorno)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TComplemento)
                .IsFixedLength()
                .HasMaxLength(40);

            this.Property(t => t.TCodigoDaAreaDaEmpresa)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TUsoDaEmpresa)
                .IsFixedLength()
                .HasMaxLength(35);

            this.Property(t => t.TCodigoDeLancamento)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.TTipoDeContaDoFornecedor)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TContaComplementar)
                .IsFixedLength()
                .HasMaxLength(7);

            this.Property(t => t.TSequencialDoRegistro)
                .IsFixedLength()
                .HasMaxLength(6);

            this.Property(t => t.LIdentificacaoDoRegistro)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LQuantidadeDeRegistro)
                .IsFixedLength()
                .HasMaxLength(6);

            this.Property(t => t.LSequencialDoRegistro)
                .IsFixedLength()
                .HasMaxLength(6);

            // Table & Column Mappings
            this.ToTable("DDA");
            this.Property(t => t.IdDDA).HasColumnName("IdDDA");
            this.Property(t => t.IdFornecedor).HasColumnName("IdFornecedor");
            this.Property(t => t.IdSacado).HasColumnName("IdSacado");
            this.Property(t => t.HIdentificacaoDoRegistro).HasColumnName("HIdentificacaoDoRegistro");
            this.Property(t => t.HIdentificacaoDaEmpresa).HasColumnName("HIdentificacaoDaEmpresa");
            this.Property(t => t.HTipoInscricaoEmpresaPagadora).HasColumnName("HTipoInscricaoEmpresaPagadora");
            this.Property(t => t.HCnpjCpfClientePagador).HasColumnName("HCnpjCpfClientePagador");
            this.Property(t => t.HEmpresaPagadora).HasColumnName("HEmpresaPagadora");
            this.Property(t => t.HTipoDeServico).HasColumnName("HTipoDeServico");
            this.Property(t => t.HCodigoDeOrigemDoArquivo).HasColumnName("HCodigoDeOrigemDoArquivo");
            this.Property(t => t.HNumeroDoRetorno).HasColumnName("HNumeroDoRetorno");
            this.Property(t => t.HDataDoArquivo).HasColumnName("HDataDoArquivo");
            this.Property(t => t.HHoraDoArquivo).HasColumnName("HHoraDoArquivo");
            this.Property(t => t.HTipoDeProcessamento).HasColumnName("HTipoDeProcessamento");
            this.Property(t => t.HReservadoEmpresa).HasColumnName("HReservadoEmpresa");
            this.Property(t => t.HReservadoBanco).HasColumnName("HReservadoBanco");
            this.Property(t => t.HNumeroDaListaDeDebito).HasColumnName("HNumeroDaListaDeDebito");
            this.Property(t => t.HReservadoBanco1).HasColumnName("HReservadoBanco1");
            this.Property(t => t.HNumeroSequencialDoRegistro).HasColumnName("HNumeroSequencialDoRegistro");
            this.Property(t => t.TIdentificacaoDoRegistro).HasColumnName("TIdentificacaoDoRegistro");
            this.Property(t => t.TTipoDeInscricaoDoFornecedor).HasColumnName("TTipoDeInscricaoDoFornecedor");
            this.Property(t => t.TCnpjCpf).HasColumnName("TCnpjCpf");
            this.Property(t => t.TNomeDoFornecedor).HasColumnName("TNomeDoFornecedor");
            this.Property(t => t.TEnderecoDoFornecedor).HasColumnName("TEnderecoDoFornecedor");
            this.Property(t => t.TCepDoFornecedor).HasColumnName("TCepDoFornecedor");
            this.Property(t => t.TCodigoDoBancoFornecedor).HasColumnName("TCodigoDoBancoFornecedor");
            this.Property(t => t.TCodigoDaAgenciaFornecedor).HasColumnName("TCodigoDaAgenciaFornecedor");
            this.Property(t => t.TDigitoDaAgenciaDoFornecedor).HasColumnName("TDigitoDaAgenciaDoFornecedor");
            this.Property(t => t.TContaCorrenteDoFornecedor).HasColumnName("TContaCorrenteDoFornecedor");
            this.Property(t => t.TDigitoDaContaCorrenteDoFornecedor).HasColumnName("TDigitoDaContaCorrenteDoFornecedor");
            this.Property(t => t.TNumeroDoPagamento).HasColumnName("TNumeroDoPagamento");
            this.Property(t => t.TCarteira).HasColumnName("TCarteira");
            this.Property(t => t.TNossoNumero).HasColumnName("TNossoNumero");
            this.Property(t => t.TSeuNumero).HasColumnName("TSeuNumero");
            this.Property(t => t.TVencimento).HasColumnName("TVencimento");
            this.Property(t => t.TEmissao).HasColumnName("TEmissao");
            this.Property(t => t.TDataLimiteParaDesconto).HasColumnName("TDataLimiteParaDesconto");
            this.Property(t => t.TFatorDeVencimento).HasColumnName("TFatorDeVencimento");
            this.Property(t => t.TValorDoDocumento).HasColumnName("TValorDoDocumento");
            this.Property(t => t.TValorDePagamento).HasColumnName("TValorDePagamento");
            this.Property(t => t.TValorDoDesconto).HasColumnName("TValorDoDesconto");
            this.Property(t => t.TValorDoAcrescimo).HasColumnName("TValorDoAcrescimo");
            this.Property(t => t.TTipoDeDocumento).HasColumnName("TTipoDeDocumento");
            this.Property(t => t.TNumeroDaNotaFiscal).HasColumnName("TNumeroDaNotaFiscal");
            this.Property(t => t.TSerieDaNotaFiscal).HasColumnName("TSerieDaNotaFiscal");
            this.Property(t => t.TDataParaEfetivacaoDoPagamento).HasColumnName("TDataParaEfetivacaoDoPagamento");
            this.Property(t => t.TSituacaoDoAgendamento).HasColumnName("TSituacaoDoAgendamento");
            this.Property(t => t.TInformacaoDeRetorno).HasColumnName("TInformacaoDeRetorno");
            this.Property(t => t.TTipoDeMovimento).HasColumnName("TTipoDeMovimento");
            this.Property(t => t.TEnderecoDoSacadorAvalista).HasColumnName("TEnderecoDoSacadorAvalista");
            this.Property(t => t.TNomeDoSacadorAvalista).HasColumnName("TNomeDoSacadorAvalista");
            this.Property(t => t.TNivelDeInformacaoDeRetorno).HasColumnName("TNivelDeInformacaoDeRetorno");
            this.Property(t => t.TComplemento).HasColumnName("TComplemento");
            this.Property(t => t.TCodigoDaAreaDaEmpresa).HasColumnName("TCodigoDaAreaDaEmpresa");
            this.Property(t => t.TUsoDaEmpresa).HasColumnName("TUsoDaEmpresa");
            this.Property(t => t.TCodigoDeLancamento).HasColumnName("TCodigoDeLancamento");
            this.Property(t => t.TTipoDeContaDoFornecedor).HasColumnName("TTipoDeContaDoFornecedor");
            this.Property(t => t.TContaComplementar).HasColumnName("TContaComplementar");
            this.Property(t => t.TSequencialDoRegistro).HasColumnName("TSequencialDoRegistro");
            this.Property(t => t.LIdentificacaoDoRegistro).HasColumnName("LIdentificacaoDoRegistro");
            this.Property(t => t.LQuantidadeDeRegistro).HasColumnName("LQuantidadeDeRegistro");
            this.Property(t => t.LValorTotalDePagamento).HasColumnName("LValorTotalDePagamento");
            this.Property(t => t.LSequencialDoRegistro).HasColumnName("LSequencialDoRegistro");
        }
    }
}
