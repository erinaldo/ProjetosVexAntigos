using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_VAR_BAIMap : EntityTypeConfiguration<LOG_VAR_BAI>
    {
        public LOG_VAR_BAIMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BAI_NU, t.VDB_NU, t.VDB_TX });

            // Properties
            this.Property(t => t.BAI_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VDB_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VDB_TX)
                .IsRequired()
                .HasMaxLength(72);

            // Table & Column Mappings
            this.ToTable("LOG_VAR_BAI");
            this.Property(t => t.BAI_NU).HasColumnName("BAI_NU");
            this.Property(t => t.VDB_NU).HasColumnName("VDB_NU");
            this.Property(t => t.VDB_TX).HasColumnName("VDB_TX");
        }
    }
}
