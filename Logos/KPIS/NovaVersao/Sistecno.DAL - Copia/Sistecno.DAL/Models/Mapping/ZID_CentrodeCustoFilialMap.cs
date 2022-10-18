using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CentrodeCustoFilialMap : EntityTypeConfiguration<ZID_CentrodeCustoFilial>
    {
        public ZID_CentrodeCustoFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CentrodeCustoFilial");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
