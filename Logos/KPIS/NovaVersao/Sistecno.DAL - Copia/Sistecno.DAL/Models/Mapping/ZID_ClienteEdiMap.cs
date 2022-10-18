using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ClienteEdiMap : EntityTypeConfiguration<ZID_ClienteEdi>
    {
        public ZID_ClienteEdiMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ClienteEdi");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
