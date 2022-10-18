using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_RPCIMap : EntityTypeConfiguration<ZID_RPCI>
    {
        public ZID_RPCIMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_RPCI");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
