using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_VAR_LOCMap : EntityTypeConfiguration<LOG_VAR_LOC>
    {
        public LOG_VAR_LOCMap()
        {
            // Primary Key
            this.HasKey(t => new { t.VAL_NU, t.VAL_TX });

            // Properties
            this.Property(t => t.VAL_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VAL_TX)
                .IsRequired()
                .HasMaxLength(72);

            // Table & Column Mappings
            this.ToTable("LOG_VAR_LOC");
            this.Property(t => t.LOC_NU).HasColumnName("LOC_NU");
            this.Property(t => t.VAL_NU).HasColumnName("VAL_NU");
            this.Property(t => t.VAL_TX).HasColumnName("VAL_TX");
        }
    }
}
