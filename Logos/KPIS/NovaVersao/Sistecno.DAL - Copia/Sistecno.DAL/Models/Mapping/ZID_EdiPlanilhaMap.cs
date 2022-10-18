using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EdiPlanilhaMap : EntityTypeConfiguration<ZID_EdiPlanilha>
    {
        public ZID_EdiPlanilhaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EdiPlanilha");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
