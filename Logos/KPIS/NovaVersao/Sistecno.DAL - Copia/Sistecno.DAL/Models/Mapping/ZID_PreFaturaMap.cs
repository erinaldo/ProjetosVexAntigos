using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_PreFaturaMap : EntityTypeConfiguration<ZID_PreFatura>
    {
        public ZID_PreFaturaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_PreFatura");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
