using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_MAPAMap : EntityTypeConfiguration<ZID_MAPA>
    {
        public ZID_MAPAMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_MAPA");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
