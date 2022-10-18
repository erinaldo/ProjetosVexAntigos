using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_USUARIOGRADECAMPOMap : EntityTypeConfiguration<ZID_USUARIOGRADECAMPO>
    {
        public ZID_USUARIOGRADECAMPOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_USUARIOGRADECAMPO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
