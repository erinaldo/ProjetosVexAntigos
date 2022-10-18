using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ClienteFilialMap : EntityTypeConfiguration<ZID_ClienteFilial>
    {
        public ZID_ClienteFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ClienteFilial");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
