using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_USUARIOGRADEMap : EntityTypeConfiguration<ZID_USUARIOGRADE>
    {
        public ZID_USUARIOGRADEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_USUARIOGRADE");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}