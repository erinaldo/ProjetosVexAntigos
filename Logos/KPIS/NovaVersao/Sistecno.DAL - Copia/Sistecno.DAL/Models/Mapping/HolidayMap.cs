using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class HolidayMap : EntityTypeConfiguration<Holiday>
    {
        public HolidayMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BusinessCenterCode, t.Date });

            // Properties
            this.Property(t => t.BusinessCenterCode)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("Holiday", "Calendar");
            this.Property(t => t.BusinessCenterCode).HasColumnName("BusinessCenterCode");
            this.Property(t => t.Date).HasColumnName("Date");
        }
    }
}
