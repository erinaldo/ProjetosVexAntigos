using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioClienteMap : EntityTypeConfiguration<UsuarioCliente>
    {
        public UsuarioClienteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuarioCliente);

            // Properties
            this.Property(t => t.IDUsuarioCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UsuarioCliente");
            this.Property(t => t.IDUsuarioCliente).HasColumnName("IDUsuarioCliente");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.UsuarioClientes)
                .HasForeignKey(d => d.IDCliente);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.UsuarioClientes)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
