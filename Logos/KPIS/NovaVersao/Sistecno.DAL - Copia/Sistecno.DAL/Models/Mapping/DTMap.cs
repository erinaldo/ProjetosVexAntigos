using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTMap : EntityTypeConfiguration<DT>
    {
        public DTMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDT);

            // Properties
            this.Property(t => t.IDDT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HoraDeSaida)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.HoraDeChegada)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.Lacres)
                .HasMaxLength(100);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Impresso)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Situacao)
                .HasMaxLength(30);

            this.Property(t => t.Andamento)
                .HasMaxLength(30);

            this.Property(t => t.SituacaoImpressao)
                .HasMaxLength(30);

            this.Property(t => t.SituacaoFaturamento)
                .HasMaxLength(50);

            this.Property(t => t.SerieDT)
                .HasMaxLength(10);

            this.Property(t => t.Origem)
                .HasMaxLength(20);

            this.Property(t => t.Escolta)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NumeroLiberacao)
                .HasMaxLength(20);

            this.Property(t => t.DescricaoRota)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("DT");
            this.Property(t => t.IDDT).HasColumnName("IDDT");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.IDDTTipo).HasColumnName("IDDTTipo");
            this.Property(t => t.IDFilialDestino).HasColumnName("IDFilialDestino");
            this.Property(t => t.IDTransportadora).HasColumnName("IDTransportadora");
            this.Property(t => t.IDRedespacho).HasColumnName("IDRedespacho");
            this.Property(t => t.IDPrimeiroVeiculo).HasColumnName("IDPrimeiroVeiculo");
            this.Property(t => t.IDSegundoVeiculo).HasColumnName("IDSegundoVeiculo");
            this.Property(t => t.IDPrimeiroMotorista).HasColumnName("IDPrimeiroMotorista");
            this.Property(t => t.IDSegundoMotorista).HasColumnName("IDSegundoMotorista");
            this.Property(t => t.IDCadastroTitular).HasColumnName("IDCadastroTitular");
            this.Property(t => t.IDProprietarioPrimeiroVeiculo).HasColumnName("IDProprietarioPrimeiroVeiculo");
            this.Property(t => t.IDProprietarioSegundoVeiculo).HasColumnName("IDProprietarioSegundoVeiculo");
            this.Property(t => t.IDUsuarioEmitiu).HasColumnName("IDUsuarioEmitiu");
            this.Property(t => t.IDUsuarioBaixou).HasColumnName("IDUsuarioBaixou");
            this.Property(t => t.IDModal).HasColumnName("IDModal");
            this.Property(t => t.IDTipoDeMonitoramento).HasColumnName("IDTipoDeMonitoramento");
            this.Property(t => t.IDTipoDeEscolta).HasColumnName("IDTipoDeEscolta");
            this.Property(t => t.IDEmpresaEscolta).HasColumnName("IDEmpresaEscolta");
            this.Property(t => t.IDEmpresaMonitoramento).HasColumnName("IDEmpresaMonitoramento");
            this.Property(t => t.IDCidadeDeOrigem).HasColumnName("IDCidadeDeOrigem");
            this.Property(t => t.IDCidadeDeDestino).HasColumnName("IDCidadeDeDestino");
            this.Property(t => t.IDRpci).HasColumnName("IDRpci");
            this.Property(t => t.IdRastreador).HasColumnName("IdRastreador");
            this.Property(t => t.Cadastro).HasColumnName("Cadastro");
            this.Property(t => t.Emissao).HasColumnName("Emissao");
            this.Property(t => t.Baixado).HasColumnName("Baixado");
            this.Property(t => t.DataDeSaida).HasColumnName("DataDeSaida");
            this.Property(t => t.HoraDeSaida).HasColumnName("HoraDeSaida");
            this.Property(t => t.DataDeChegada).HasColumnName("DataDeChegada");
            this.Property(t => t.HoraDeChegada).HasColumnName("HoraDeChegada");
            this.Property(t => t.Lacres).HasColumnName("Lacres");
            this.Property(t => t.PalletsExpedido).HasColumnName("PalletsExpedido");
            this.Property(t => t.PalletsChep).HasColumnName("PalletsChep");
            this.Property(t => t.PalletsPbr).HasColumnName("PalletsPbr");
            this.Property(t => t.ValorDaNota).HasColumnName("ValorDaNota");
            this.Property(t => t.Volumes).HasColumnName("Volumes");
            this.Property(t => t.PesoLiquido).HasColumnName("PesoLiquido");
            this.Property(t => t.PesoCubado).HasColumnName("PesoCubado");
            this.Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            this.Property(t => t.ValorFreteCtr).HasColumnName("ValorFreteCtr");
            this.Property(t => t.ValorIcmsCtr).HasColumnName("ValorIcmsCtr");
            this.Property(t => t.CreditoValorDoServico).HasColumnName("CreditoValorDoServico");
            this.Property(t => t.CreditoAgenciamento).HasColumnName("CreditoAgenciamento");
            this.Property(t => t.CreditoPedagio).HasColumnName("CreditoPedagio");
            this.Property(t => t.CreditoCarga).HasColumnName("CreditoCarga");
            this.Property(t => t.CreditoDescarga).HasColumnName("CreditoDescarga");
            this.Property(t => t.CreditoDiaria).HasColumnName("CreditoDiaria");
            this.Property(t => t.CreditoColeta).HasColumnName("CreditoColeta");
            this.Property(t => t.CreditoEntrega).HasColumnName("CreditoEntrega");
            this.Property(t => t.CreditoAjudante).HasColumnName("CreditoAjudante");
            this.Property(t => t.CreditoAdicional).HasColumnName("CreditoAdicional");
            this.Property(t => t.CreditoOutros).HasColumnName("CreditoOutros");
            this.Property(t => t.DebitoSeguro).HasColumnName("DebitoSeguro");
            this.Property(t => t.DebitoOutros).HasColumnName("DebitoOutros");
            this.Property(t => t.DebitoAdiantamento).HasColumnName("DebitoAdiantamento");
            this.Property(t => t.SubTotal).HasColumnName("SubTotal");
            this.Property(t => t.SaldoAReceber).HasColumnName("SaldoAReceber");
            this.Property(t => t.KMInicial).HasColumnName("KMInicial");
            this.Property(t => t.KMFinal).HasColumnName("KMFinal");
            this.Property(t => t.KMTotal).HasColumnName("KMTotal");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Impresso).HasColumnName("Impresso");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Andamento).HasColumnName("Andamento");
            this.Property(t => t.idPortaria).HasColumnName("idPortaria");
            this.Property(t => t.CreditosForaDoCalculo).HasColumnName("CreditosForaDoCalculo");
            this.Property(t => t.DebitosForaDoCalculo).HasColumnName("DebitosForaDoCalculo");
            this.Property(t => t.SituacaoImpressao).HasColumnName("SituacaoImpressao");
            this.Property(t => t.VolumesComFator).HasColumnName("VolumesComFator");
            this.Property(t => t.IDAgregado).HasColumnName("IDAgregado");
            this.Property(t => t.Entrega).HasColumnName("Entrega");
            this.Property(t => t.Grupos).HasColumnName("Grupos");
            this.Property(t => t.Setor).HasColumnName("Setor");
            this.Property(t => t.Chapatex).HasColumnName("Chapatex");
            this.Property(t => t.SituacaoFaturamento).HasColumnName("SituacaoFaturamento");
            this.Property(t => t.IdDtTipoRE).HasColumnName("IdDtTipoRE");
            this.Property(t => t.SerieDT).HasColumnName("SerieDT");
            this.Property(t => t.NumeroMDFe).HasColumnName("NumeroMDFe");
            this.Property(t => t.Origem).HasColumnName("Origem");
            this.Property(t => t.Escolta).HasColumnName("Escolta");
            this.Property(t => t.NumeroLiberacao).HasColumnName("NumeroLiberacao");
            this.Property(t => t.DescricaoRota).HasColumnName("DescricaoRota");

            // Relationships
            this.HasOptional(t => t.Cadastro1)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDRedespacho);
            this.HasOptional(t => t.Cadastro2)
                .WithMany(t => t.DTs1)
                .HasForeignKey(d => d.IDEmpresaEscolta);
            this.HasOptional(t => t.Cadastro3)
                .WithMany(t => t.DTs2)
                .HasForeignKey(d => d.IDEmpresaMonitoramento);
            this.HasOptional(t => t.Cadastro4)
                .WithMany(t => t.DTs3)
                .HasForeignKey(d => d.IDCadastroTitular);
            this.HasOptional(t => t.Cidade)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDCidadeDeDestino);
            this.HasOptional(t => t.Cidade1)
                .WithMany(t => t.DTs1)
                .HasForeignKey(d => d.IDCidadeDeOrigem);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDFilial);
            this.HasOptional(t => t.Filial1)
                .WithMany(t => t.DTs1)
                .HasForeignKey(d => d.IDFilialDestino);
            this.HasOptional(t => t.Motorista)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDPrimeiroMotorista);
            this.HasOptional(t => t.Veiculo)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDPrimeiroVeiculo);
            this.HasOptional(t => t.Proprietario)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDProprietarioPrimeiroVeiculo);
            this.HasOptional(t => t.Proprietario1)
                .WithMany(t => t.DTs1)
                .HasForeignKey(d => d.IDProprietarioSegundoVeiculo);
            this.HasOptional(t => t.Motorista1)
                .WithMany(t => t.DTs1)
                .HasForeignKey(d => d.IDSegundoMotorista);
            this.HasOptional(t => t.Transportadora)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDTransportadora);
            this.HasOptional(t => t.Usuario)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDUsuarioBaixou);
            this.HasOptional(t => t.Usuario1)
                .WithMany(t => t.DTs1)
                .HasForeignKey(d => d.IDUsuarioEmitiu);
            this.HasOptional(t => t.Veiculo1)
                .WithMany(t => t.DTs1)
                .HasForeignKey(d => d.IDSegundoVeiculo);
            this.HasRequired(t => t.DT2)
                .WithOptional(t => t.DT1);
            this.HasRequired(t => t.DTTipo)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDDTTipo);
            this.HasOptional(t => t.Modal)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDModal);
            this.HasOptional(t => t.Rastreador)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IdRastreador);
            this.HasOptional(t => t.RPCI)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDRpci);
            this.HasOptional(t => t.TipoDeEscolta)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDTipoDeEscolta);
            this.HasOptional(t => t.TipoDeMonitoramento)
                .WithMany(t => t.DTs)
                .HasForeignKey(d => d.IDTipoDeMonitoramento);

        }
    }
}
