using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TituloImagemMap : EntityTypeConfiguration<TituloImagem>
    {
        public TituloImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTituloImagem);

            // Properties
            this.Property(t => t.IdTituloImagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TituloImagem");
            this.Property(t => t.IdTituloImagem).HasColumnName("IdTituloImagem");
            this.Property(t => t.IdTitulo).HasColumnName("IdTitulo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Imagem).HasColumnName("Imagem");

            // Relationships
            this.HasOptional(t => t.Titulo)
                .WithMany(t => t.TituloImagems)
                .HasForeignKey(d => d.IdTitulo);

        }
    }
}
