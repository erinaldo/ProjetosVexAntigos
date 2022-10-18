using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_COLETORCONFERENCIAVOLUMEMap : EntityTypeConfiguration<ZID_COLETORCONFERENCIAVOLUME>
    {
        public ZID_COLETORCONFERENCIAVOLUMEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_COLETORCONFERENCIAVOLUME");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
