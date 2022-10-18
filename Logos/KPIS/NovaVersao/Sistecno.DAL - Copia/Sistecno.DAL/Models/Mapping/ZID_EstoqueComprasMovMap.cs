using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EstoqueComprasMovMap : EntityTypeConfiguration<ZID_EstoqueComprasMov>
    {
        public ZID_EstoqueComprasMovMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EstoqueComprasMov");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
