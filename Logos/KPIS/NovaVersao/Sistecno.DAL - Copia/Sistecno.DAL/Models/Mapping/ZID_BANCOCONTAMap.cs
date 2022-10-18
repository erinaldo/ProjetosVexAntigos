using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_BANCOCONTAMap : EntityTypeConfiguration<ZID_BANCOCONTA>
    {
        public ZID_BANCOCONTAMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_BANCOCONTA");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
