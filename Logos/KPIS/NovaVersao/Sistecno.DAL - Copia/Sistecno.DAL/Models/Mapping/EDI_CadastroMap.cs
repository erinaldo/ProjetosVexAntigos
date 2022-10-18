using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_CadastroMap : EntityTypeConfiguration<EDI_Cadastro>
    {
        public EDI_CadastroMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EDI_Chave, t.IDCadastro, t.CnpjCpf, t.RazaoSocialNome, t.Endereco, t.IDCidade });

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.Bairro)
                .HasMaxLength(80);

            this.Property(t => t.Cidade)
                .HasMaxLength(80);

            this.Property(t => t.UF)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.IDCadastro)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CnpjCpf)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.InscricaoRG)
                .HasMaxLength(20);

            this.Property(t => t.RazaoSocialNome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.FantasiaApelido)
                .HasMaxLength(30);

            this.Property(t => t.Endereco)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Numero)
                .HasMaxLength(10);

            this.Property(t => t.Complemento)
                .HasMaxLength(60);

            this.Property(t => t.IDCidade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Cep)
                .HasMaxLength(8);

            this.Property(t => t.CnpjCpfErrado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.InscricaoErrada)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CEPValido)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Aniversario)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("EDI_Cadastro");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.Bairro).HasColumnName("Bairro");
            this.Property(t => t.Cidade).HasColumnName("Cidade");
            this.Property(t => t.UF).HasColumnName("UF");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.CnpjCpf).HasColumnName("CnpjCpf");
            this.Property(t => t.InscricaoRG).HasColumnName("InscricaoRG");
            this.Property(t => t.RazaoSocialNome).HasColumnName("RazaoSocialNome");
            this.Property(t => t.FantasiaApelido).HasColumnName("FantasiaApelido");
            this.Property(t => t.Endereco).HasColumnName("Endereco");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Complemento).HasColumnName("Complemento");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.IDBairro).HasColumnName("IDBairro");
            this.Property(t => t.Cep).HasColumnName("Cep");
            this.Property(t => t.CnpjCpfErrado).HasColumnName("CnpjCpfErrado");
            this.Property(t => t.InscricaoErrada).HasColumnName("InscricaoErrada");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.CEPValido).HasColumnName("CEPValido");
            this.Property(t => t.Aniversario).HasColumnName("Aniversario");
            this.Property(t => t.idedi_cadastro).HasColumnName("idedi_cadastro");
        }
    }
}
