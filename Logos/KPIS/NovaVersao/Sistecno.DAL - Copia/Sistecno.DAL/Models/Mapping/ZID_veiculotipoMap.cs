using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_veiculotipoMap : EntityTypeConfiguration<ZID_veiculotipo>
    {
        public ZID_veiculotipoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_veiculotipo");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
