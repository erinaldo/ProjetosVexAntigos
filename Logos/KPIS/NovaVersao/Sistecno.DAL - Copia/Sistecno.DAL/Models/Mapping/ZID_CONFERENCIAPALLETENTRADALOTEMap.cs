using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CONFERENCIAPALLETENTRADALOTEMap : EntityTypeConfiguration<ZID_CONFERENCIAPALLETENTRADALOTE>
    {
        public ZID_CONFERENCIAPALLETENTRADALOTEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CONFERENCIAPALLETENTRADALOTE");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
