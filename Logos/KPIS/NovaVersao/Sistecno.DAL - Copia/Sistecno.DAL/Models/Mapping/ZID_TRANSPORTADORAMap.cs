using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TRANSPORTADORAMap : EntityTypeConfiguration<ZID_TRANSPORTADORA>
    {
        public ZID_TRANSPORTADORAMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TRANSPORTADORA");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
