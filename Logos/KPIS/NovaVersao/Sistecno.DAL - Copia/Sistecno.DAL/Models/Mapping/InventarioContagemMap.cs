using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class InventarioContagemMap : EntityTypeConfiguration<InventarioContagem>
    {
        public InventarioContagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdinventarioContagem);

            // Properties
            this.Property(t => t.IdinventarioContagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            this.Property(t => t.Status)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("InventarioContagem");
            this.Property(t => t.IdinventarioContagem).HasColumnName("IdinventarioContagem");
            this.Property(t => t.Idinventario).HasColumnName("Idinventario");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.Inventario)
                .WithMany(t => t.InventarioContagems)
                .HasForeignKey(d => d.Idinventario);

        }
    }
}
