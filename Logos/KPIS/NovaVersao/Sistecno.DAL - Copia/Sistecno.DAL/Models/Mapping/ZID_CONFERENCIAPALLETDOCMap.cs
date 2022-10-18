using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CONFERENCIAPALLETDOCMap : EntityTypeConfiguration<ZID_CONFERENCIAPALLETDOC>
    {
        public ZID_CONFERENCIAPALLETDOCMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CONFERENCIAPALLETDOC");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
