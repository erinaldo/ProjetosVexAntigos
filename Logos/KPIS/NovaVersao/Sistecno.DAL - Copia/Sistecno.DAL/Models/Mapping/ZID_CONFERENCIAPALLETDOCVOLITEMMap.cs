using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CONFERENCIAPALLETDOCVOLITEMMap : EntityTypeConfiguration<ZID_CONFERENCIAPALLETDOCVOLITEM>
    {
        public ZID_CONFERENCIAPALLETDOCVOLITEMMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CONFERENCIAPALLETDOCVOLITEM");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
