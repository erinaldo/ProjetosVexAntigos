using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ESTOQUEMap : EntityTypeConfiguration<ZID_ESTOQUE>
    {
        public ZID_ESTOQUEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ESTOQUE");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
