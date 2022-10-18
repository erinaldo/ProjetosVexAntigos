using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CIDADEMap : EntityTypeConfiguration<ZID_CIDADE>
    {
        public ZID_CIDADEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CIDADE");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
