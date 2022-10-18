using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CONFERENCIAPALLETDOCVOLMap : EntityTypeConfiguration<ZID_CONFERENCIAPALLETDOCVOL>
    {
        public ZID_CONFERENCIAPALLETDOCVOLMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CONFERENCIAPALLETDOCVOL");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
