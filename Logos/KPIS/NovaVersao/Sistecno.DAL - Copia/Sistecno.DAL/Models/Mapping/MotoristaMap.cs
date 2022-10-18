using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MotoristaMap : EntityTypeConfiguration<Motorista>
    {
        public MotoristaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMotorista);

            // Properties
            this.Property(t => t.IDMotorista)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CarteiraDeHabilitacao)
                .HasMaxLength(30);

            this.Property(t => t.Categoria)
                .HasMaxLength(10);

            this.Property(t => t.IDCidadeNascimento)
                .HasMaxLength(60);

            this.Property(t => t.NomeDoPai)
                .HasMaxLength(60);

            this.Property(t => t.NomeDaMae)
                .HasMaxLength(60);

            this.Property(t => t.Conjuge)
                .HasMaxLength(60);

            this.Property(t => t.EstadoCivil)
                .HasMaxLength(30);

            this.Property(t => t.VinculoComAEmpresa)
                .HasMaxLength(20);

            this.Property(t => t.NumeroPancard)
                .HasMaxLength(20);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Liberado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.MOPP)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NumeroRegistroCNH)
                .HasMaxLength(50);

            this.Property(t => t.NumeroInss)
                .HasMaxLength(50);

            this.Property(t => t.Origem)
                .HasMaxLength(10);

            this.Property(t => t.RecolheINSS)
                .HasMaxLength(3);

            this.Property(t => t.RecolheIRRF)
                .HasMaxLength(3);

            this.Property(t => t.RecolheSESTSENAT)
                .HasMaxLength(3);

            this.Property(t => t.LocalRG)
                .HasMaxLength(3);

            this.Property(t => t.LocalEmissaoRG)
                .HasMaxLength(30);

            this.Property(t => t.Senha)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Motorista");
            this.Property(t => t.IDMotorista).HasColumnName("IDMotorista");
            this.Property(t => t.CarteiraDeHabilitacao).HasColumnName("CarteiraDeHabilitacao");
            this.Property(t => t.ValidadeDaHabilitacao).HasColumnName("ValidadeDaHabilitacao");
            this.Property(t => t.DataDaPrimeiraHabilitacao).HasColumnName("DataDaPrimeiraHabilitacao");
            this.Property(t => t.Categoria).HasColumnName("Categoria");
            this.Property(t => t.DataDeNascimento).HasColumnName("DataDeNascimento");
            this.Property(t => t.IDCidadeNascimento).HasColumnName("IDCidadeNascimento");
            this.Property(t => t.NomeDoPai).HasColumnName("NomeDoPai");
            this.Property(t => t.NomeDaMae).HasColumnName("NomeDaMae");
            this.Property(t => t.Conjuge).HasColumnName("Conjuge");
            this.Property(t => t.VitimaDeRouboQuantidade).HasColumnName("VitimaDeRouboQuantidade");
            this.Property(t => t.SofreuAcidadeQuantidade).HasColumnName("SofreuAcidadeQuantidade");
            this.Property(t => t.EstadoCivil).HasColumnName("EstadoCivil");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.CarregamentoAutorizadoAte).HasColumnName("CarregamentoAutorizadoAte");
            this.Property(t => t.VencimentoNoGerenciadorDeRisco).HasColumnName("VencimentoNoGerenciadorDeRisco");
            this.Property(t => t.AliquotaSestSenat).HasColumnName("AliquotaSestSenat");
            this.Property(t => t.VinculoComAEmpresa).HasColumnName("VinculoComAEmpresa");
            this.Property(t => t.NumeroPancard).HasColumnName("NumeroPancard");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Liberado).HasColumnName("Liberado");
            this.Property(t => t.MOPP).HasColumnName("MOPP");
            this.Property(t => t.AguardandoLiberacao).HasColumnName("AguardandoLiberacao");
            this.Property(t => t.VencimentoPancary).HasColumnName("VencimentoPancary");
            this.Property(t => t.VencimentoBrasilrisk).HasColumnName("VencimentoBrasilrisk");
            this.Property(t => t.VencimentoBuonny).HasColumnName("VencimentoBuonny");
            this.Property(t => t.DataDeBloqueio).HasColumnName("DataDeBloqueio");
            this.Property(t => t.NumeroRegistroCNH).HasColumnName("NumeroRegistroCNH");
            this.Property(t => t.NumeroInss).HasColumnName("NumeroInss");
            this.Property(t => t.Origem).HasColumnName("Origem");
            this.Property(t => t.RecolheINSS).HasColumnName("RecolheINSS");
            this.Property(t => t.RecolheIRRF).HasColumnName("RecolheIRRF");
            this.Property(t => t.RecolheSESTSENAT).HasColumnName("RecolheSESTSENAT");
            this.Property(t => t.LocalRG).HasColumnName("LocalRG");
            this.Property(t => t.LocalEmissaoRG).HasColumnName("LocalEmissaoRG");
            this.Property(t => t.Senha).HasColumnName("Senha");
        }
    }
}
