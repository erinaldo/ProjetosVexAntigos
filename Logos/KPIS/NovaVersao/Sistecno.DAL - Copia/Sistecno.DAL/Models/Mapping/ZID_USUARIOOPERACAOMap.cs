using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_USUARIOOPERACAOMap : EntityTypeConfiguration<ZID_USUARIOOPERACAO>
    {
        public ZID_USUARIOOPERACAOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_USUARIOOPERACAO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
