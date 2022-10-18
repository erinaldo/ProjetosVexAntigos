using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioMap : EntityTypeConfiguration<Romaneio>
    {
        public RomaneioMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRomaneio);

            // Properties
            this.Property(t => t.IDRomaneio)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Divisao)
                .HasMaxLength(15);

            this.Property(t => t.Conferencia)
                .IsFixedLength()
                .HasMaxLength(11);

            this.Property(t => t.Observacao1)
                .HasMaxLength(200);

            this.Property(t => t.Situacao)
                .HasMaxLength(10);

            this.Property(t => t.Andamento)
                .HasMaxLength(30);

            this.Property(t => t.GerarRomaneioDeTransportes)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(50);

            this.Property(t => t.TipoDeCarga)
                .HasMaxLength(15);

            this.Property(t => t.SeparadoPor)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ObsAprovacao)
                .HasMaxLength(200);

            this.Property(t => t.Lacres)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Romaneio");
            this.Property(t => t.IDRomaneio).HasColumnName("IDRomaneio");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDPortaria).HasColumnName("IDPortaria");
            this.Property(t => t.IDMovimentacao).HasColumnName("IDMovimentacao");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDRomaneioOrigem).HasColumnName("IDRomaneioOrigem");
            this.Property(t => t.IDDepositoPlantaLocalizacao).HasColumnName("IDDepositoPlantaLocalizacao");
            this.Property(t => t.IdRomaneioRelacionado).HasColumnName("IdRomaneioRelacionado");
            this.Property(t => t.Emissao).HasColumnName("Emissao");
            this.Property(t => t.Liberacao).HasColumnName("Liberacao");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Divisao).HasColumnName("Divisao");
            this.Property(t => t.Conferencia).HasColumnName("Conferencia");
            this.Property(t => t.Observacao1).HasColumnName("Observacao1");
            this.Property(t => t.Observacao2).HasColumnName("Observacao2");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Andamento).HasColumnName("Andamento");
            this.Property(t => t.DataPlanejada).HasColumnName("DataPlanejada");
            this.Property(t => t.GerarRomaneioDeTransportes).HasColumnName("GerarRomaneioDeTransportes");
            this.Property(t => t.DataGeracaoDoArquivo).HasColumnName("DataGeracaoDoArquivo");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
            this.Property(t => t.TipoDeCarga).HasColumnName("TipoDeCarga");
            this.Property(t => t.InicioDoRecebimento).HasColumnName("InicioDoRecebimento");
            this.Property(t => t.FinalDoRecebimento).HasColumnName("FinalDoRecebimento");
            this.Property(t => t.LiberacaoDocRecebimento).HasColumnName("LiberacaoDocRecebimento");
            this.Property(t => t.InicioSeparacao).HasColumnName("InicioSeparacao");
            this.Property(t => t.FinalDaSeparacao).HasColumnName("FinalDaSeparacao");
            this.Property(t => t.LiberacaoDocSeparacao).HasColumnName("LiberacaoDocSeparacao");
            this.Property(t => t.QtdePaleteExpedido).HasColumnName("QtdePaleteExpedido");
            this.Property(t => t.QtdePaleteRecebido).HasColumnName("QtdePaleteRecebido");
            this.Property(t => t.IdRegiao).HasColumnName("IdRegiao");
            this.Property(t => t.PalletsChep).HasColumnName("PalletsChep");
            this.Property(t => t.PalletsPbr).HasColumnName("PalletsPbr");
            this.Property(t => t.OrdemDeCarregamento).HasColumnName("OrdemDeCarregamento");
            this.Property(t => t.ValorDaNota).HasColumnName("ValorDaNota");
            this.Property(t => t.Volumes).HasColumnName("Volumes");
            this.Property(t => t.VolumesComFator).HasColumnName("VolumesComFator");
            this.Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            this.Property(t => t.PesoCubado).HasColumnName("PesoCubado");
            this.Property(t => t.MetragemCubica).HasColumnName("MetragemCubica");
            this.Property(t => t.IcmsIss).HasColumnName("IcmsIss");
            this.Property(t => t.FreteCredito).HasColumnName("FreteCredito");
            this.Property(t => t.FreteDebito).HasColumnName("FreteDebito");
            this.Property(t => t.idFilialDestino).HasColumnName("idFilialDestino");
            this.Property(t => t.IdCadastro).HasColumnName("IdCadastro");
            this.Property(t => t.IdVeiculoTipo).HasColumnName("IdVeiculoTipo");
            this.Property(t => t.Entrega).HasColumnName("Entrega");
            this.Property(t => t.Grupos).HasColumnName("Grupos");
            this.Property(t => t.Setor).HasColumnName("Setor");
            this.Property(t => t.Chapatex).HasColumnName("Chapatex");
            this.Property(t => t.SeparadoPor).HasColumnName("SeparadoPor");
            this.Property(t => t.IdPlaca).HasColumnName("IdPlaca");
            this.Property(t => t.IdVeiculo).HasColumnName("IdVeiculo");
            this.Property(t => t.PercentualLucratividadeDT).HasColumnName("PercentualLucratividadeDT");
            this.Property(t => t.PercentualOcopacaoVeiculo).HasColumnName("PercentualOcopacaoVeiculo");
            this.Property(t => t.Custos).HasColumnName("Custos");
            this.Property(t => t.Liquido).HasColumnName("Liquido");
            this.Property(t => t.IdUsuarioAprovou).HasColumnName("IdUsuarioAprovou");
            this.Property(t => t.DataAprovacao).HasColumnName("DataAprovacao");
            this.Property(t => t.ObsAprovacao).HasColumnName("ObsAprovacao");
            this.Property(t => t.IdMotorista).HasColumnName("IdMotorista");
            this.Property(t => t.PercentualMaximoFrete).HasColumnName("PercentualMaximoFrete");
            this.Property(t => t.Lacres).HasColumnName("Lacres");

            // Relationships
            this.HasOptional(t => t.DepositoPlantaLocalizacao)
                .WithMany(t => t.Romaneios)
                .HasForeignKey(d => d.IDDepositoPlantaLocalizacao);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Romaneios)
                .HasForeignKey(d => d.IDFilial);
            this.HasOptional(t => t.Filial1)
                .WithMany(t => t.Romaneios1)
                .HasForeignKey(d => d.idFilialDestino);
            this.HasOptional(t => t.Movimentacao)
                .WithMany(t => t.Romaneios)
                .HasForeignKey(d => d.IDMovimentacao);
            this.HasOptional(t => t.Portaria)
                .WithMany(t => t.Romaneios)
                .HasForeignKey(d => d.IDPortaria);
            this.HasOptional(t => t.Regiao)
                .WithMany(t => t.Romaneios)
                .HasForeignKey(d => d.IdRegiao);
            this.HasOptional(t => t.Romaneio2)
                .WithMany(t => t.Romaneio1)
                .HasForeignKey(d => d.IDRomaneioOrigem);
            this.HasOptional(t => t.Romaneio3)
                .WithMany(t => t.Romaneio11)
                .HasForeignKey(d => d.IdRomaneioRelacionado);

        }
    }
}
