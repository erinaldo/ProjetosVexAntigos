using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroComplementoMap : EntityTypeConfiguration<CadastroComplemento>
    {
        public CadastroComplementoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroComplemento);

            // Properties
            this.Property(t => t.IDCadastroComplemento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Aniversario)
                .HasMaxLength(5);

            this.Property(t => t.Banco)
                .HasMaxLength(5);

            this.Property(t => t.Agencia)
                .HasMaxLength(10);

            this.Property(t => t.AgenciaDigito)
                .HasMaxLength(5);

            this.Property(t => t.Conta)
                .HasMaxLength(10);

            this.Property(t => t.ContaDigito)
                .HasMaxLength(5);

            this.Property(t => t.TipoConta)
                .HasMaxLength(10);

            this.Property(t => t.CnpjCpfFavorecido)
                .HasMaxLength(20);

            this.Property(t => t.NomeFavorecido)
                .HasMaxLength(60);

            this.Property(t => t.InscricaoNoInss)
                .HasMaxLength(20);

            this.Property(t => t.InscricaoPIS)
                .HasMaxLength(20);

            this.Property(t => t.VinculoFavorecido)
                .HasMaxLength(60);

            this.Property(t => t.Aposentado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Contratado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.OrgaoExpedicaoRG)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CadastroComplemento");
            this.Property(t => t.IDCadastroComplemento).HasColumnName("IDCadastroComplemento");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.IDBanco).HasColumnName("IDBanco");
            this.Property(t => t.Aniversario).HasColumnName("Aniversario");
            this.Property(t => t.Dependentes).HasColumnName("Dependentes");
            this.Property(t => t.Banco).HasColumnName("Banco");
            this.Property(t => t.Agencia).HasColumnName("Agencia");
            this.Property(t => t.AgenciaDigito).HasColumnName("AgenciaDigito");
            this.Property(t => t.Conta).HasColumnName("Conta");
            this.Property(t => t.ContaDigito).HasColumnName("ContaDigito");
            this.Property(t => t.TipoConta).HasColumnName("TipoConta");
            this.Property(t => t.CnpjCpfFavorecido).HasColumnName("CnpjCpfFavorecido");
            this.Property(t => t.NomeFavorecido).HasColumnName("NomeFavorecido");
            this.Property(t => t.InscricaoNoInss).HasColumnName("InscricaoNoInss");
            this.Property(t => t.InscricaoPIS).HasColumnName("InscricaoPIS");
            this.Property(t => t.ValorPensaoAlimenticia).HasColumnName("ValorPensaoAlimenticia");
            this.Property(t => t.VinculoFavorecido).HasColumnName("VinculoFavorecido");
            this.Property(t => t.Aposentado).HasColumnName("Aposentado");
            this.Property(t => t.Contratado).HasColumnName("Contratado");
            this.Property(t => t.DataExpedicaoRG).HasColumnName("DataExpedicaoRG");
            this.Property(t => t.OrgaoExpedicaoRG).HasColumnName("OrgaoExpedicaoRG");
            this.Property(t => t.UltimaComprovacaoEndereco).HasColumnName("UltimaComprovacaoEndereco");

            // Relationships
            this.HasOptional(t => t.Banco1)
                .WithMany(t => t.CadastroComplementoes)
                .HasForeignKey(d => d.IDBanco);
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.CadastroComplementoes)
                .HasForeignKey(d => d.IDCadastro);

        }
    }
}
