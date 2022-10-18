using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEmpresa);

            // Properties
            this.Property(t => t.IDEmpresa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.PermiteCNPJErrado)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PermiteIEErrada)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.FormatacaoDaContaContabil)
                .HasMaxLength(20);

            this.Property(t => t.FormatacaoDoCentroDeCusto)
                .HasMaxLength(20);

            this.Property(t => t.CodigoDaLicenca)
                .HasMaxLength(255);

            this.Property(t => t.AcessosSimultaneos)
                .HasMaxLength(50);

            this.Property(t => t.Permissao)
                .HasMaxLength(50);

            this.Property(t => t.MinSegProtecaoTela)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.OptanteSimples)
                .HasMaxLength(3);

            this.Property(t => t.CSOSN)
                .HasMaxLength(3);

            this.Property(t => t.RNTRC)
                .HasMaxLength(20);

            this.Property(t => t.RegimeApuracaoImposto)
                .HasMaxLength(15);

            this.Property(t => t.LocalGerarArquivoAverbacao)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Empresa");
            this.Property(t => t.IDEmpresa).HasColumnName("IDEmpresa");
            this.Property(t => t.IDGrupo).HasColumnName("IDGrupo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.PermiteCNPJErrado).HasColumnName("PermiteCNPJErrado");
            this.Property(t => t.PermiteIEErrada).HasColumnName("PermiteIEErrada");
            this.Property(t => t.FormatacaoDaContaContabil).HasColumnName("FormatacaoDaContaContabil");
            this.Property(t => t.FormatacaoDoCentroDeCusto).HasColumnName("FormatacaoDoCentroDeCusto");
            this.Property(t => t.CodigoDaLicenca).HasColumnName("CodigoDaLicenca");
            this.Property(t => t.AcessosSimultaneos).HasColumnName("AcessosSimultaneos");
            this.Property(t => t.Permissao).HasColumnName("Permissao");
            this.Property(t => t.MinSegProtecaoTela).HasColumnName("MinSegProtecaoTela");
            this.Property(t => t.ExpirarSenha).HasColumnName("ExpirarSenha");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.OptanteSimples).HasColumnName("OptanteSimples");
            this.Property(t => t.CSOSN).HasColumnName("CSOSN");
            this.Property(t => t.RNTRC).HasColumnName("RNTRC");
            this.Property(t => t.RegimeApuracaoImposto).HasColumnName("RegimeApuracaoImposto");
            this.Property(t => t.Pis).HasColumnName("Pis");
            this.Property(t => t.Cofins).HasColumnName("Cofins");
            this.Property(t => t.LimitadorIcms).HasColumnName("LimitadorIcms");
            this.Property(t => t.AliquotaSimples).HasColumnName("AliquotaSimples");
            this.Property(t => t.LocalGerarArquivoAverbacao).HasColumnName("LocalGerarArquivoAverbacao");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithOptional(t => t.Empresa);
            this.HasOptional(t => t.Grupo)
                .WithMany(t => t.Empresas)
                .HasForeignKey(d => d.IDGrupo);

        }
    }
}
