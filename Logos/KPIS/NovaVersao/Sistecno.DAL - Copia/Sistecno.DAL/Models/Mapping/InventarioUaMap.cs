using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class InventarioUaMap : EntityTypeConfiguration<InventarioUa>
    {
        public InventarioUaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdInventarioUa);

            // Properties
            this.Property(t => t.IdInventarioUa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Status)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("InventarioUa");
            this.Property(t => t.IdInventarioUa).HasColumnName("IdInventarioUa");
            this.Property(t => t.IdInventarioContagem).HasColumnName("IdInventarioContagem");
            this.Property(t => t.IdUnidadeDeArmazenagemLote).HasColumnName("IdUnidadeDeArmazenagemLote");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.InventarioContagem)
                .WithMany(t => t.InventarioUas)
                .HasForeignKey(d => d.IdInventarioContagem);
            this.HasRequired(t => t.UnidadeDeArmazenagemLote)
                .WithMany(t => t.InventarioUas)
                .HasForeignKey(d => d.IdUnidadeDeArmazenagemLote);

        }
    }
}
