using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaContabilMap : EntityTypeConfiguration<ContaContabil>
    {
        public ContaContabilMap()
        {
            // Primary Key
            this.HasKey(t => t.IDContaContabil);

            // Properties
            this.Property(t => t.IDContaContabil)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Conta)
                .HasMaxLength(50);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CodigoReduzido)
                .HasMaxLength(50);

            this.Property(t => t.TipoDeConta)
                .HasMaxLength(10);

            this.Property(t => t.Redutora)
                .HasMaxLength(3);

            this.Property(t => t.SpedFiscal)
                .HasMaxLength(3);

            this.Property(t => t.Receita)
                .HasMaxLength(3);

            this.Property(t => t.Despesa)
                .HasMaxLength(3);

            this.Property(t => t.ContaSefaz)
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            this.Property(t => t.TipoDeNumerario)
                .HasMaxLength(20);

            this.Property(t => t.DRE)
                .HasMaxLength(1);

            this.Property(t => t.PlanoReferencial)
                .HasMaxLength(5);

            this.Property(t => t.FCont)
                .HasMaxLength(1);

            this.Property(t => t.ContaFCont)
                .HasMaxLength(56);

            this.Property(t => t.Sistema)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ContaContabil");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");
            this.Property(t => t.IDEmpresa).HasColumnName("IDEmpresa");
            this.Property(t => t.Conta).HasColumnName("Conta");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.CodigoReduzido).HasColumnName("CodigoReduzido");
            this.Property(t => t.IDParente).HasColumnName("IDParente");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.TipoDeConta).HasColumnName("TipoDeConta");
            this.Property(t => t.Redutora).HasColumnName("Redutora");
            this.Property(t => t.Grau).HasColumnName("Grau");
            this.Property(t => t.SaldoInicial).HasColumnName("SaldoInicial");
            this.Property(t => t.SpedFiscal).HasColumnName("SpedFiscal");
            this.Property(t => t.Receita).HasColumnName("Receita");
            this.Property(t => t.Despesa).HasColumnName("Despesa");
            this.Property(t => t.ContaSefaz).HasColumnName("ContaSefaz");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.TipoDeNumerario).HasColumnName("TipoDeNumerario");
            this.Property(t => t.DRE).HasColumnName("DRE");
            this.Property(t => t.PlanoReferencial).HasColumnName("PlanoReferencial");
            this.Property(t => t.FCont).HasColumnName("FCont");
            this.Property(t => t.ContaFCont).HasColumnName("ContaFCont");
            this.Property(t => t.Sistema).HasColumnName("Sistema");

            // Relationships
            this.HasOptional(t => t.ContaContabil2)
                .WithMany(t => t.ContaContabil1)
                .HasForeignKey(d => d.IDParente);
            this.HasRequired(t => t.Empresa)
                .WithMany(t => t.ContaContabils)
                .HasForeignKey(d => d.IDEmpresa);

        }
    }
}
