using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MenuSiteMap : EntityTypeConfiguration<MenuSite>
    {
        public MenuSiteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdMenuSite);

            // Properties
            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            this.Property(t => t.Visao)
                .HasMaxLength(20);

            this.Property(t => t.ICONE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MenuSite");
            this.Property(t => t.IdMenuSite).HasColumnName("IdMenuSite");
            this.Property(t => t.IdModuloOpcao).HasColumnName("IdModuloOpcao");
            this.Property(t => t.IdParente).HasColumnName("IdParente");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Visao).HasColumnName("Visao");
            this.Property(t => t.ICONE).HasColumnName("ICONE");
        }
    }
}
