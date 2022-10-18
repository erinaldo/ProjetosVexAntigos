using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_UsuarioCompraMap : EntityTypeConfiguration<ZID_UsuarioCompra>
    {
        public ZID_UsuarioCompraMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_UsuarioCompra");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
