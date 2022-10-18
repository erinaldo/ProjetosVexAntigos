using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioPerfilMap : EntityTypeConfiguration<UsuarioPerfil>
    {
        public UsuarioPerfilMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuarioPerfil);

            // Properties
            this.Property(t => t.IDUsuarioPerfil)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UsuarioPerfil");
            this.Property(t => t.IDUsuarioPerfil).HasColumnName("IDUsuarioPerfil");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDPerfil).HasColumnName("IDPerfil");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.UsuarioPerfils)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.UsuarioPerfils)
                .HasForeignKey(d => d.IDUsuario);
            this.HasRequired(t => t.Usuario1)
                .WithMany(t => t.UsuarioPerfils1)
                .HasForeignKey(d => d.IDPerfil);

        }
    }
}
