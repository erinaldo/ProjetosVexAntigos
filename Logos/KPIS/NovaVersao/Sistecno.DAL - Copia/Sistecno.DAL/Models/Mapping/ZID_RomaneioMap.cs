using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_RomaneioMap : EntityTypeConfiguration<ZID_Romaneio>
    {
        public ZID_RomaneioMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_Romaneio");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
