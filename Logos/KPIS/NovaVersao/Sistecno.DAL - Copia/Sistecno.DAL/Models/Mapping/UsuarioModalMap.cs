using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioModalMap : EntityTypeConfiguration<UsuarioModal>
    {
        public UsuarioModalMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuarioModal);

            // Properties
            this.Property(t => t.IdUsuarioModal)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UsuarioModal");
            this.Property(t => t.IdUsuarioModal).HasColumnName("IdUsuarioModal");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdModal).HasColumnName("IdModal");
        }
    }
}
