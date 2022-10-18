using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModuloMenuMap : EntityTypeConfiguration<ModuloMenu>
    {
        public ModuloMenuMap()
        {
            // Primary Key
            this.HasKey(t => t.IDModuloMenu);

            // Properties
            this.Property(t => t.IDModuloMenu)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Parametro)
                .HasMaxLength(20);

            this.Property(t => t.Ativo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ModuloMenu");
            this.Property(t => t.IDModuloMenu).HasColumnName("IDModuloMenu");
            this.Property(t => t.IDModulo).HasColumnName("IDModulo");
            this.Property(t => t.IDModuloOpcao).HasColumnName("IDModuloOpcao");
            this.Property(t => t.Parametro).HasColumnName("Parametro");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.IDParente).HasColumnName("IDParente");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasOptional(t => t.ModuloOpcao)
                .WithMany(t => t.ModuloMenus)
                .HasForeignKey(d => d.IDModuloOpcao);

        }
    }
}
