using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TESCFOPMap : EntityTypeConfiguration<ZID_TESCFOP>
    {
        public ZID_TESCFOPMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TESCFOP");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
