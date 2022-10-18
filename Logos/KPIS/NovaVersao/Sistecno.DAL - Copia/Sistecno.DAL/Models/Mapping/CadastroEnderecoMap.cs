using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroEnderecoMap : EntityTypeConfiguration<CadastroEndereco>
    {
        public CadastroEnderecoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroEndereco);

            // Properties
            this.Property(t => t.IDCadastroEndereco)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeEndereco)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.Endereco)
                .HasMaxLength(60);

            this.Property(t => t.Numero)
                .HasMaxLength(10);

            this.Property(t => t.Complemento)
                .HasMaxLength(60);

            this.Property(t => t.Cep)
                .HasMaxLength(8);

            this.Property(t => t.CnpjCpf)
                .HasMaxLength(20);

            this.Property(t => t.RazaoSocialNome)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("CadastroEndereco");
            this.Property(t => t.IDCadastroEndereco).HasColumnName("IDCadastroEndereco");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.TipoDeEndereco).HasColumnName("TipoDeEndereco");
            this.Property(t => t.Endereco).HasColumnName("Endereco");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Complemento).HasColumnName("Complemento");
            this.Property(t => t.Cep).HasColumnName("Cep");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.IDBairro).HasColumnName("IDBairro");
            this.Property(t => t.CnpjCpf).HasColumnName("CnpjCpf");
            this.Property(t => t.RazaoSocialNome).HasColumnName("RazaoSocialNome");

            // Relationships
            this.HasOptional(t => t.Bairro)
                .WithMany(t => t.CadastroEnderecoes)
                .HasForeignKey(d => d.IDBairro);
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.CadastroEnderecoes)
                .HasForeignKey(d => d.IDCadastro);
            this.HasOptional(t => t.Cidade)
                .WithMany(t => t.CadastroEnderecoes)
                .HasForeignKey(d => d.IDCidade);

        }
    }
}
