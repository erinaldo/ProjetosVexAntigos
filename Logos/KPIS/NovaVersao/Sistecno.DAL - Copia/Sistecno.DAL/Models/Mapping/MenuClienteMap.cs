using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MenuClienteMap : EntityTypeConfiguration<MenuCliente>
    {
        public MenuClienteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdMenuCliente);

            // Properties
            this.Property(t => t.IdMenuCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("MenuCliente");
            this.Property(t => t.IdMenuCliente).HasColumnName("IdMenuCliente");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.IdModuloOpcao).HasColumnName("IdModuloOpcao");
            this.Property(t => t.Nome).HasColumnName("Nome");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.MenuClientes)
                .HasForeignKey(d => d.IdCliente);
            this.HasRequired(t => t.ModuloOpcao)
                .WithMany(t => t.MenuClientes)
                .HasForeignKey(d => d.IdModuloOpcao);

        }
    }
}
