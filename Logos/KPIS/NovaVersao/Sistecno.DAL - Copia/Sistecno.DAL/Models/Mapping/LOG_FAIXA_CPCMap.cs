using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_FAIXA_CPCMap : EntityTypeConfiguration<LOG_FAIXA_CPC>
    {
        public LOG_FAIXA_CPCMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CPC_NU, t.CPC_INICIAL, t.CPC_FINAL });

            // Properties
            this.Property(t => t.CPC_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CPC_INICIAL)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.CPC_FINAL)
                .IsRequired()
                .HasMaxLength(6);

            // Table & Column Mappings
            this.ToTable("LOG_FAIXA_CPC");
            this.Property(t => t.CPC_NU).HasColumnName("CPC_NU");
            this.Property(t => t.CPC_INICIAL).HasColumnName("CPC_INICIAL");
            this.Property(t => t.CPC_FINAL).HasColumnName("CPC_FINAL");
        }
    }
}
