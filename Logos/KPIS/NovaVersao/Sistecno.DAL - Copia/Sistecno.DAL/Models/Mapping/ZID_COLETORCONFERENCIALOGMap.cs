using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_COLETORCONFERENCIALOGMap : EntityTypeConfiguration<ZID_COLETORCONFERENCIALOG>
    {
        public ZID_COLETORCONFERENCIALOGMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_COLETORCONFERENCIALOG");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
