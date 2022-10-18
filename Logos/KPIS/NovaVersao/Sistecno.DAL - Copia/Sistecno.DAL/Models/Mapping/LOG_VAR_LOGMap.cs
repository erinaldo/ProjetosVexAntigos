using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_VAR_LOGMap : EntityTypeConfiguration<LOG_VAR_LOG>
    {
        public LOG_VAR_LOGMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LOG_NU, t.VLO_NU, t.TLO_TX, t.VLO_TX });

            // Properties
            this.Property(t => t.LOG_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VLO_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TLO_TX)
                .IsRequired()
                .HasMaxLength(36);

            this.Property(t => t.VLO_TX)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("LOG_VAR_LOG");
            this.Property(t => t.LOG_NU).HasColumnName("LOG_NU");
            this.Property(t => t.VLO_NU).HasColumnName("VLO_NU");
            this.Property(t => t.TLO_TX).HasColumnName("TLO_TX");
            this.Property(t => t.VLO_TX).HasColumnName("VLO_TX");
        }
    }
}
