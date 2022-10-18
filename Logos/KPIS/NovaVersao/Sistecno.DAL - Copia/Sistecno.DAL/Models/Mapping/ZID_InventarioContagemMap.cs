using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_InventarioContagemMap : EntityTypeConfiguration<ZID_InventarioContagem>
    {
        public ZID_InventarioContagemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_InventarioContagem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
