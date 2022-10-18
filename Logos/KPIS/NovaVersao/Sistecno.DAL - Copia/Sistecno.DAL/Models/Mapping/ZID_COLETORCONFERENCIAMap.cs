using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_COLETORCONFERENCIAMap : EntityTypeConfiguration<ZID_COLETORCONFERENCIA>
    {
        public ZID_COLETORCONFERENCIAMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_COLETORCONFERENCIA");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
