using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_FAIXA_UOPMap : EntityTypeConfiguration<LOG_FAIXA_UOP>
    {
        public LOG_FAIXA_UOPMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UOP_NU, t.FNC_INICIAL, t.FNC_FINAL });

            // Properties
            this.Property(t => t.UOP_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FNC_INICIAL)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.FNC_FINAL)
                .IsRequired()
                .HasMaxLength(6);

            // Table & Column Mappings
            this.ToTable("LOG_FAIXA_UOP");
            this.Property(t => t.UOP_NU).HasColumnName("UOP_NU");
            this.Property(t => t.FNC_INICIAL).HasColumnName("FNC_INICIAL");
            this.Property(t => t.FNC_FINAL).HasColumnName("FNC_FINAL");
        }
    }
}
