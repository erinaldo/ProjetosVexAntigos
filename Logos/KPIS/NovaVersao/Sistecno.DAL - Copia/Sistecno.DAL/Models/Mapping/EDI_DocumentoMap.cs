using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_DocumentoMap : EntityTypeConfiguration<EDI_Documento>
    {
        public EDI_DocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.EDI_Chave);

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.EDI_Motivo)
                .HasMaxLength(500);

            this.Property(t => t.NomeBairro)
                .HasMaxLength(80);

            this.Property(t => t.NomeCidade)
                .HasMaxLength(80);

            this.Property(t => t.UF)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TipoDeDocumento)
                .HasMaxLength(20);

            this.Property(t => t.TipoDeServico)
                .HasMaxLength(20);

            this.Property(t => t.Serie)
                .HasMaxLength(20);

            this.Property(t => t.AnoMes)
                .IsFixedLength()
                .HasMaxLength(7);

            this.Property(t => t.NumeroOriginal)
                .HasMaxLength(20);

            this.Property(t => t.ClasseCFOP)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Origem)
                .HasMaxLength(10);

            this.Property(t => t.EntradaSaida)
                .HasMaxLength(10);

            this.Property(t => t.CifFob)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Natureza)
                .HasMaxLength(50);

            this.Property(t => t.Especie)
                .HasMaxLength(50);

            this.Property(t => t.Impresso)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Endereco)
                .HasMaxLength(60);

            this.Property(t => t.EnderecoNumero)
                .HasMaxLength(10);

            this.Property(t => t.EnderecoComplemento)
                .HasMaxLength(60);

            this.Property(t => t.EnderecoCep)
                .HasMaxLength(8);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.DocumentoDoCliente)
                .HasMaxLength(15);

            this.Property(t => t.DocumentoDoClienteSerie)
                .HasMaxLength(15);

            this.Property(t => t.PagarReceber)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Status)
                .HasMaxLength(20);

            this.Property(t => t.CodigoDoRecExp)
                .HasMaxLength(20);

            this.Property(t => t.CodigoDeBarrasRecExp)
                .HasMaxLength(20);

            this.Property(t => t.CodigoDoRecExpImpresso)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(50);

            this.Property(t => t.DocumentoDoCliente1)
                .HasMaxLength(50);

            this.Property(t => t.DocumentoDoCliente2)
                .HasMaxLength(50);

            this.Property(t => t.Enviado)
                .HasMaxLength(3);

            this.Property(t => t.CepColeta)
                .HasMaxLength(8);

            this.Property(t => t.Coleta)
                .HasMaxLength(3);

            this.Property(t => t.Paletizado)
                .HasMaxLength(3);

            this.Property(t => t.EnderecoColeta)
                .HasMaxLength(100);

            this.Property(t => t.Adiantamento)
                .HasMaxLength(3);

            this.Property(t => t.DocumentoDoCliente3)
                .HasMaxLength(50);

            this.Property(t => t.DocumentodoCliente4)
                .HasMaxLength(100);

            this.Property(t => t.ModeloDocumento)
                .HasMaxLength(2);

            this.Property(t => t.TipoDeFrete)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SituacaoDocumento)
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("EDI_Documento");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.NomeBairro).HasColumnName("NomeBairro");
            this.Property(t => t.NomeCidade).HasColumnName("NomeCidade");
            this.Property(t => t.UF).HasColumnName("UF");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDFilialAtual).HasColumnName("IDFilialAtual");
            this.Property(t => t.IDProprietarioDocumento).HasColumnName("IDProprietarioDocumento");
            this.Property(t => t.IDDocumentoOcorrencia).HasColumnName("IDDocumentoOcorrencia");
            this.Property(t => t.IDCondicaoDePagamento).HasColumnName("IDCondicaoDePagamento");
            this.Property(t => t.TipoDeDocumento).HasColumnName("TipoDeDocumento");
            this.Property(t => t.TipoDeServico).HasColumnName("TipoDeServico");
            this.Property(t => t.Serie).HasColumnName("Serie");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.AnoMes).HasColumnName("AnoMes");
            this.Property(t => t.NumeroOriginal).HasColumnName("NumeroOriginal");
            this.Property(t => t.IDModal).HasColumnName("IDModal");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.IDRemetente).HasColumnName("IDRemetente");
            this.Property(t => t.IDDestinatario).HasColumnName("IDDestinatario");
            this.Property(t => t.IDConsignatario).HasColumnName("IDConsignatario");
            this.Property(t => t.IDGrupoDeProduto).HasColumnName("IDGrupoDeProduto");
            this.Property(t => t.IDRedespachante).HasColumnName("IDRedespachante");
            this.Property(t => t.IDClienteDivisao).HasColumnName("IDClienteDivisao");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDTes).HasColumnName("IDTes");
            this.Property(t => t.IDTesCFOP).HasColumnName("IDTesCFOP");
            this.Property(t => t.ClasseCFOP).HasColumnName("ClasseCFOP");
            this.Property(t => t.Origem).HasColumnName("Origem");
            this.Property(t => t.EntradaSaida).HasColumnName("EntradaSaida");
            this.Property(t => t.DataDoMovimento).HasColumnName("DataDoMovimento");
            this.Property(t => t.DataDeEmissao).HasColumnName("DataDeEmissao");
            this.Property(t => t.DataDeEntrada).HasColumnName("DataDeEntrada");
            this.Property(t => t.DataPlanejadaOriginal).HasColumnName("DataPlanejadaOriginal");
            this.Property(t => t.DataPlanejada).HasColumnName("DataPlanejada");
            this.Property(t => t.DataDeSaida).HasColumnName("DataDeSaida");
            this.Property(t => t.PrevisaoDeRecebimento).HasColumnName("PrevisaoDeRecebimento");
            this.Property(t => t.PrevisaoDeSaida).HasColumnName("PrevisaoDeSaida");
            this.Property(t => t.DataDaUltimaOcorrencia).HasColumnName("DataDaUltimaOcorrencia");
            this.Property(t => t.DataDeConclusao).HasColumnName("DataDeConclusao");
            this.Property(t => t.DataDeCancelamento).HasColumnName("DataDeCancelamento");
            this.Property(t => t.PesoLiquido).HasColumnName("PesoLiquido");
            this.Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            this.Property(t => t.PesoCubado).HasColumnName("PesoCubado");
            this.Property(t => t.PesoCalculado).HasColumnName("PesoCalculado");
            this.Property(t => t.MetragemCubica).HasColumnName("MetragemCubica");
            this.Property(t => t.Volumes).HasColumnName("Volumes");
            this.Property(t => t.CifFob).HasColumnName("CifFob");
            this.Property(t => t.Natureza).HasColumnName("Natureza");
            this.Property(t => t.Especie).HasColumnName("Especie");
            this.Property(t => t.Impresso).HasColumnName("Impresso");
            this.Property(t => t.ValorDaNota).HasColumnName("ValorDaNota");
            this.Property(t => t.ValorDasMercadorias).HasColumnName("ValorDasMercadorias");
            this.Property(t => t.ValorDoServico).HasColumnName("ValorDoServico");
            this.Property(t => t.ValorDoSeguro).HasColumnName("ValorDoSeguro");
            this.Property(t => t.ValorDoICMS).HasColumnName("ValorDoICMS");
            this.Property(t => t.ValorDoIPI).HasColumnName("ValorDoIPI");
            this.Property(t => t.BaseDoIPI).HasColumnName("BaseDoIPI");
            this.Property(t => t.BaseDoICMS).HasColumnName("BaseDoICMS");
            this.Property(t => t.ValorDoICMSSubst).HasColumnName("ValorDoICMSSubst");
            this.Property(t => t.BaseDoICMSSubst).HasColumnName("BaseDoICMSSubst");
            this.Property(t => t.ValorDeDesconto).HasColumnName("ValorDeDesconto");
            this.Property(t => t.Endereco).HasColumnName("Endereco");
            this.Property(t => t.EnderecoNumero).HasColumnName("EnderecoNumero");
            this.Property(t => t.EnderecoComplemento).HasColumnName("EnderecoComplemento");
            this.Property(t => t.IDEnderecoBairro).HasColumnName("IDEnderecoBairro");
            this.Property(t => t.IDEnderecoCidade).HasColumnName("IDEnderecoCidade");
            this.Property(t => t.EnderecoCep).HasColumnName("EnderecoCep");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.DocumentoDoCliente).HasColumnName("DocumentoDoCliente");
            this.Property(t => t.DocumentoDoClienteSerie).HasColumnName("DocumentoDoClienteSerie");
            this.Property(t => t.PagarReceber).HasColumnName("PagarReceber");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CodigoDoRecExp).HasColumnName("CodigoDoRecExp");
            this.Property(t => t.CodigoDeBarrasRecExp).HasColumnName("CodigoDeBarrasRecExp");
            this.Property(t => t.CodigoDoRecExpImpresso).HasColumnName("CodigoDoRecExpImpresso");
            this.Property(t => t.DataDoRecExp).HasColumnName("DataDoRecExp");
            this.Property(t => t.DataGeracaoDoArquivo).HasColumnName("DataGeracaoDoArquivo");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
            this.Property(t => t.DocumentoDoCliente1).HasColumnName("DocumentoDoCliente1");
            this.Property(t => t.DocumentoDoCliente2).HasColumnName("DocumentoDoCliente2");
            this.Property(t => t.IcmsSuframa).HasColumnName("IcmsSuframa");
            this.Property(t => t.Enviado).HasColumnName("Enviado");
            this.Property(t => t.idOcorrenciaAndamento).HasColumnName("idOcorrenciaAndamento");
            this.Property(t => t.Prazo).HasColumnName("Prazo");
            this.Property(t => t.PrazoUtilizado).HasColumnName("PrazoUtilizado");
            this.Property(t => t.IdCDA).HasColumnName("IdCDA");
            this.Property(t => t.CepColeta).HasColumnName("CepColeta");
            this.Property(t => t.Coleta).HasColumnName("Coleta");
            this.Property(t => t.DataDeReprogramacao).HasColumnName("DataDeReprogramacao");
            this.Property(t => t.FatorDeCubagemCalculado).HasColumnName("FatorDeCubagemCalculado");
            this.Property(t => t.IdCidadeColeta).HasColumnName("IdCidadeColeta");
            this.Property(t => t.IdBairroColeta).HasColumnName("IdBairroColeta");
            this.Property(t => t.IdRomaneio).HasColumnName("IdRomaneio");
            this.Property(t => t.Paletizado).HasColumnName("Paletizado");
            this.Property(t => t.QuantidadeDePallets).HasColumnName("QuantidadeDePallets");
            this.Property(t => t.VolumesComFator).HasColumnName("VolumesComFator");
            this.Property(t => t.EnderecoColeta).HasColumnName("EnderecoColeta");
            this.Property(t => t.IdEnderecoUf).HasColumnName("IdEnderecoUf");
            this.Property(t => t.IdColetaUf).HasColumnName("IdColetaUf");
            this.Property(t => t.IDModalidade).HasColumnName("IDModalidade");
            this.Property(t => t.IdMotivo).HasColumnName("IdMotivo");
            this.Property(t => t.IdFilialDestino).HasColumnName("IdFilialDestino");
            this.Property(t => t.Adiantamento).HasColumnName("Adiantamento");
            this.Property(t => t.DiasOcorrenciaCliente).HasColumnName("DiasOcorrenciaCliente");
            this.Property(t => t.DocumentoDoCliente3).HasColumnName("DocumentoDoCliente3");
            this.Property(t => t.DocumentodoCliente4).HasColumnName("DocumentodoCliente4");
            this.Property(t => t.ValorDoISS).HasColumnName("ValorDoISS");
            this.Property(t => t.IdUsuarioDeTabelaDeFrete).HasColumnName("IdUsuarioDeTabelaDeFrete");
            this.Property(t => t.PrevisaoDeEmbarque).HasColumnName("PrevisaoDeEmbarque");
            this.Property(t => t.IdTransportadora).HasColumnName("IdTransportadora");
            this.Property(t => t.IdVeiculo).HasColumnName("IdVeiculo");
            this.Property(t => t.ValorDescontoServico).HasColumnName("ValorDescontoServico");
            this.Property(t => t.QuantidadeFeriados).HasColumnName("QuantidadeFeriados");
            this.Property(t => t.QuantidadeSabDom).HasColumnName("QuantidadeSabDom");
            this.Property(t => t.PrazoUtilizadoCerto).HasColumnName("PrazoUtilizadoCerto");
            this.Property(t => t.PrazoUtilizadoCorrido).HasColumnName("PrazoUtilizadoCorrido");
            this.Property(t => t.IDTipoDeMovimento).HasColumnName("IDTipoDeMovimento");
            this.Property(t => t.ValorPis).HasColumnName("ValorPis");
            this.Property(t => t.ValorCofins).HasColumnName("ValorCofins");
            this.Property(t => t.ModeloDocumento).HasColumnName("ModeloDocumento");
            this.Property(t => t.TipoDeFrete).HasColumnName("TipoDeFrete");
            this.Property(t => t.ValorDoFrete).HasColumnName("ValorDoFrete");
            this.Property(t => t.PisSubstituicao).HasColumnName("PisSubstituicao");
            this.Property(t => t.CofinsSubstituicao).HasColumnName("CofinsSubstituicao");
            this.Property(t => t.SituacaoDocumento).HasColumnName("SituacaoDocumento");
        }
    }
}
