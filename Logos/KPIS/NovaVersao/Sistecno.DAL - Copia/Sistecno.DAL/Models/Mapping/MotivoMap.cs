using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MotivoMap : EntityTypeConfiguration<Motivo>
    {
        public MotivoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.idMotivo, t.IdCliente });

            // Properties
            this.Property(t => t.idMotivo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Motivo1)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Motivo");
            this.Property(t => t.idMotivo).HasColumnName("idMotivo");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.Motivo1).HasColumnName("Motivo");
        }
    }
}
