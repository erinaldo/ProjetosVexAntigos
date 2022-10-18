using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioOpcaoMap : EntityTypeConfiguration<UsuarioOpcao>
    {
        public UsuarioOpcaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuarioOpcao);

            // Properties
            this.Property(t => t.IDUsuarioOpcao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Permissao)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("UsuarioOpcao");
            this.Property(t => t.IDUsuarioOpcao).HasColumnName("IDUsuarioOpcao");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDModuloOpcao).HasColumnName("IDModuloOpcao");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.Permissao).HasColumnName("Permissao");

            // Relationships
            this.HasRequired(t => t.ModuloOpcao)
                .WithMany(t => t.UsuarioOpcaos)
                .HasForeignKey(d => d.IDModuloOpcao);

        }
    }
}
