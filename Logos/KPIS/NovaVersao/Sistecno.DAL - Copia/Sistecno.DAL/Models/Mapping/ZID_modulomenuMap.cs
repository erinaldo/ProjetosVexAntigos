using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_modulomenuMap : EntityTypeConfiguration<ZID_modulomenu>
    {
        public ZID_modulomenuMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_modulomenu");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
