using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroImagemMap : EntityTypeConfiguration<CadastroImagem>
    {
        public CadastroImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroImagem);

            // Properties
            this.Property(t => t.IDCadastroImagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            this.Property(t => t.TipoImagem)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("CadastroImagem");
            this.Property(t => t.IDCadastroImagem).HasColumnName("IDCadastroImagem");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.Imagem).HasColumnName("Imagem");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.TipoImagem).HasColumnName("TipoImagem");

            // Relationships
            this.HasOptional(t => t.Cadastro)
                .WithMany(t => t.CadastroImagems)
                .HasForeignKey(d => d.IDCadastro);

        }
    }
}
