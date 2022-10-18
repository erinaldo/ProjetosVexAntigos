using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_COLETORCONFERENCIAITEMMap : EntityTypeConfiguration<ZID_COLETORCONFERENCIAITEM>
    {
        public ZID_COLETORCONFERENCIAITEMMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_COLETORCONFERENCIAITEM");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
