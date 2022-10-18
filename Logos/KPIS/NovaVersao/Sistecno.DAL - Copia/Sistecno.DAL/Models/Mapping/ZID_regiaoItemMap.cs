using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_regiaoItemMap : EntityTypeConfiguration<ZID_regiaoItem>
    {
        public ZID_regiaoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_regiaoItem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
