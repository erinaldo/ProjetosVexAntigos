using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioCotaMap : EntityTypeConfiguration<UsuarioCota>
    {
        public UsuarioCotaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuarioCota);

            // Properties
            this.Property(t => t.IdUsuarioCota)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SiglaProduto)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("UsuarioCota");
            this.Property(t => t.IdUsuarioCota).HasColumnName("IdUsuarioCota");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.SiglaProduto).HasColumnName("SiglaProduto");
            this.Property(t => t.ValorCota).HasColumnName("ValorCota");
        }
    }
}
