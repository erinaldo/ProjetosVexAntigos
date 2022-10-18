using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CONFERENCIAPALLETMap : EntityTypeConfiguration<ZID_CONFERENCIAPALLET>
    {
        public ZID_CONFERENCIAPALLETMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CONFERENCIAPALLET");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
