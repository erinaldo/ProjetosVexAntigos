using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_NUM_SECMap : EntityTypeConfiguration<LOG_NUM_SEC>
    {
        public LOG_NUM_SECMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LOG_NU, t.SEC_NU_INI, t.SEC_NU_FIM, t.SEC_IN_LADO });

            // Properties
            this.Property(t => t.LOG_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEC_NU_INI)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SEC_NU_FIM)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SEC_IN_LADO)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("LOG_NUM_SEC");
            this.Property(t => t.LOG_NU).HasColumnName("LOG_NU");
            this.Property(t => t.SEC_NU_INI).HasColumnName("SEC_NU_INI");
            this.Property(t => t.SEC_NU_FIM).HasColumnName("SEC_NU_FIM");
            this.Property(t => t.SEC_IN_LADO).HasColumnName("SEC_IN_LADO");
        }
    }
}
