using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioCompraMap : EntityTypeConfiguration<UsuarioCompra>
    {
        public UsuarioCompraMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuarioCompra);

            // Properties
            this.Property(t => t.IdUsuarioCompra)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Ativo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("UsuarioCompra");
            this.Property(t => t.IdUsuarioCompra).HasColumnName("IdUsuarioCompra");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdUnidadeFuncional).HasColumnName("IdUnidadeFuncional");
            this.Property(t => t.LimiteDeCompra).HasColumnName("LimiteDeCompra");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.LimiteDeCompraMinimo).HasColumnName("LimiteDeCompraMinimo");

            // Relationships
            this.HasOptional(t => t.UnidadeFuncional)
                .WithMany(t => t.UsuarioCompras)
                .HasForeignKey(d => d.IdUnidadeFuncional);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.UsuarioCompras)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
