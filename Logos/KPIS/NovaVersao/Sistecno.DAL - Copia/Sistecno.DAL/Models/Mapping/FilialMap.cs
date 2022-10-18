using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FilialMap : EntityTypeConfiguration<Filial>
    {
        public FilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDFilial);

            // Properties
            this.Property(t => t.IDFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PermiteCNPJErrado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PermiteIEErrada)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CodigoDipam)
                .HasMaxLength(4);

            this.Property(t => t.DadosLogisticosObrigatorio)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PermiteDestinatarioDiferenteRE)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NomeCertificado)
                .HasMaxLength(300);

            this.Property(t => t.TipoCertificado)
                .HasMaxLength(10);

            this.Property(t => t.ObrigaEnviarEmailDT)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.EmitirNotaFiscalServico)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.DT_ExigePortaria)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.EmiteNotaFiscalServicoDeTransporte)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CalcularFreteDT)
                .HasMaxLength(3);

            this.Property(t => t.IcmsSuspenso)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NomeComprovei)
                .HasMaxLength(50);

            this.Property(t => t.Sigla)
                .HasMaxLength(10);

            this.Property(t => t.GerenteNome)
                .HasMaxLength(60);

            this.Property(t => t.GerenteFone)
                .HasMaxLength(30);

            this.Property(t => t.GerenteCelular)
                .HasMaxLength(30);

            this.Property(t => t.GerenteEmail)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Filial");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDEmpresa).HasColumnName("IDEmpresa");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.IDContaContabilCredito).HasColumnName("IDContaContabilCredito");
            this.Property(t => t.IDContaContabilDebito).HasColumnName("IDContaContabilDebito");
            this.Property(t => t.IDCentroDeCustoCredito).HasColumnName("IDCentroDeCustoCredito");
            this.Property(t => t.IDCentroDeCustoDebito).HasColumnName("IDCentroDeCustoDebito");
            this.Property(t => t.NumeroDaFilial).HasColumnName("NumeroDaFilial");
            this.Property(t => t.Unidade).HasColumnName("Unidade");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.PermiteCNPJErrado).HasColumnName("PermiteCNPJErrado");
            this.Property(t => t.PermiteIEErrada).HasColumnName("PermiteIEErrada");
            this.Property(t => t.FusoHorario).HasColumnName("FusoHorario");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.CodigoContabil).HasColumnName("CodigoContabil");
            this.Property(t => t.CodigoDipam).HasColumnName("CodigoDipam");
            this.Property(t => t.DadosLogisticosObrigatorio).HasColumnName("DadosLogisticosObrigatorio");
            this.Property(t => t.PermiteDestinatarioDiferenteRE).HasColumnName("PermiteDestinatarioDiferenteRE");
            this.Property(t => t.QtdeDTAberto).HasColumnName("QtdeDTAberto");
            this.Property(t => t.IdCadastroCompra).HasColumnName("IdCadastroCompra");
            this.Property(t => t.NomeCertificado).HasColumnName("NomeCertificado");
            this.Property(t => t.TipoCertificado).HasColumnName("TipoCertificado");
            this.Property(t => t.ObrigaEnviarEmailDT).HasColumnName("ObrigaEnviarEmailDT");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.EmitirNotaFiscalServico).HasColumnName("EmitirNotaFiscalServico");
            this.Property(t => t.DT_ExigePortaria).HasColumnName("DT_ExigePortaria");
            this.Property(t => t.EmiteNotaFiscalServicoDeTransporte).HasColumnName("EmiteNotaFiscalServicoDeTransporte");
            this.Property(t => t.Imposto).HasColumnName("Imposto");
            this.Property(t => t.Seguro).HasColumnName("Seguro");
            this.Property(t => t.TaxaAdministrativa).HasColumnName("TaxaAdministrativa");
            this.Property(t => t.TaxaDeTranferencia).HasColumnName("TaxaDeTranferencia");
            this.Property(t => t.CalcularFreteDT).HasColumnName("CalcularFreteDT");
            this.Property(t => t.TaxaDeOcupacaoDoVeiculo).HasColumnName("TaxaDeOcupacaoDoVeiculo");
            this.Property(t => t.IcmsSuspenso).HasColumnName("IcmsSuspenso");
            this.Property(t => t.NomeComprovei).HasColumnName("NomeComprovei");
            this.Property(t => t.Sigla).HasColumnName("Sigla");
            this.Property(t => t.PercentualLucratividadeExigitoPorDT).HasColumnName("PercentualLucratividadeExigitoPorDT");
            this.Property(t => t.GerenteNome).HasColumnName("GerenteNome");
            this.Property(t => t.GerenteFone).HasColumnName("GerenteFone");
            this.Property(t => t.GerenteCelular).HasColumnName("GerenteCelular");
            this.Property(t => t.GerenteEmail).HasColumnName("GerenteEmail");
            this.Property(t => t.GerenteFoto).HasColumnName("GerenteFoto");
            this.Property(t => t.PercentualMaximoFrete).HasColumnName("PercentualMaximoFrete");
            this.Property(t => t.Gerente).HasColumnName("Gerente");
            this.Property(t => t.Assistente).HasColumnName("Assistente");
            this.Property(t => t.LiderOperacional).HasColumnName("LiderOperacional");
            this.Property(t => t.Conferente).HasColumnName("Conferente");
            this.Property(t => t.Separador).HasColumnName("Separador");
            this.Property(t => t.Limpeza).HasColumnName("Limpeza");
            this.Property(t => t.Outros).HasColumnName("Outros");
            this.Property(t => t.Empilhador).HasColumnName("Empilhador");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.Filials)
                .HasForeignKey(d => d.IDCadastro);
            this.HasOptional(t => t.CentroDeCusto)
                .WithMany(t => t.Filials)
                .HasForeignKey(d => d.IDCentroDeCustoCredito);
            this.HasOptional(t => t.CentroDeCusto1)
                .WithMany(t => t.Filials1)
                .HasForeignKey(d => d.IDCentroDeCustoDebito);
            this.HasOptional(t => t.ContaContabil)
                .WithMany(t => t.Filials)
                .HasForeignKey(d => d.IDContaContabilCredito);
            this.HasOptional(t => t.ContaContabil1)
                .WithMany(t => t.Filials1)
                .HasForeignKey(d => d.IDContaContabilDebito);
            this.HasRequired(t => t.Empresa)
                .WithMany(t => t.Filials)
                .HasForeignKey(d => d.IDEmpresa);

        }
    }
}
