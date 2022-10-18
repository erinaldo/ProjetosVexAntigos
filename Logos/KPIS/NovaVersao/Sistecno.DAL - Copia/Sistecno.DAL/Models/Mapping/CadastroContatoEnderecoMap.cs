using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroContatoEnderecoMap : EntityTypeConfiguration<CadastroContatoEndereco>
    {
        public CadastroContatoEnderecoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroContatoEndereco);

            // Properties
            this.Property(t => t.IDCadastroContatoEndereco)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Endereco)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CadastroContatoEndereco");
            this.Property(t => t.IDCadastroContatoEndereco).HasColumnName("IDCadastroContatoEndereco");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.IDCadastroTipoDeContato).HasColumnName("IDCadastroTipoDeContato");
            this.Property(t => t.Endereco).HasColumnName("Endereco");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.CadastroContatoEnderecoes)
                .HasForeignKey(d => d.IDCadastro);
            this.HasRequired(t => t.CadastroTipoDeContato)
                .WithMany(t => t.CadastroContatoEnderecoes)
                .HasForeignKey(d => d.IDCadastroTipoDeContato);

        }
    }
}
