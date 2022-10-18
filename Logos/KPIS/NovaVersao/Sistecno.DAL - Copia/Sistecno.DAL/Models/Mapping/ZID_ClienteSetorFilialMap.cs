using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ClienteSetorFilialMap : EntityTypeConfiguration<ZID_ClienteSetorFilial>
    {
        public ZID_ClienteSetorFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ClienteSetorFilial");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
