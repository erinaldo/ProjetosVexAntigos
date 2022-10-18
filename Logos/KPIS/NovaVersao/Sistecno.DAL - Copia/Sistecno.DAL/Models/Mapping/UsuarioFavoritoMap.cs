using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioFavoritoMap : EntityTypeConfiguration<UsuarioFavorito>
    {
        public UsuarioFavoritoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuarioFavoritos);

            // Properties
            this.Property(t => t.IDUsuarioFavoritos)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UsuarioFavoritos");
            this.Property(t => t.IDUsuarioFavoritos).HasColumnName("IDUsuarioFavoritos");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDModuloOpcao).HasColumnName("IDModuloOpcao");
            this.Property(t => t.Ordem).HasColumnName("Ordem");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.UsuarioFavoritos)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
