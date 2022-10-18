using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DTROMANEIOMap : EntityTypeConfiguration<ZID_DTROMANEIO>
    {
        public ZID_DTROMANEIOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DTROMANEIO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
