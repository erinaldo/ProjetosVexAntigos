using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_LOGRADOUROMap : EntityTypeConfiguration<LOG_LOGRADOURO>
    {
        public LOG_LOGRADOUROMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LOG_NU, t.UFE_SG, t.LOC_NU, t.LOG_NO, t.CEP, t.TLO_TX });

            // Properties
            this.Property(t => t.LOG_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UFE_SG)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.LOC_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LOG_NO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.LOG_COMPLEMENTO)
                .HasMaxLength(100);

            this.Property(t => t.CEP)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.TLO_TX)
                .IsRequired()
                .HasMaxLength(36);

            this.Property(t => t.LOG_STA_TLO)
                .HasMaxLength(1);

            this.Property(t => t.LOG_NO_ABREV)
                .HasMaxLength(36);

            // Table & Column Mappings
            this.ToTable("LOG_LOGRADOURO");
            this.Property(t => t.LOG_NU).HasColumnName("LOG_NU");
            this.Property(t => t.UFE_SG).HasColumnName("UFE_SG");
            this.Property(t => t.LOC_NU).HasColumnName("LOC_NU");
            this.Property(t => t.BAI_NU_INI).HasColumnName("BAI_NU_INI");
            this.Property(t => t.BAI_NU_FIM).HasColumnName("BAI_NU_FIM");
            this.Property(t => t.LOG_NO).HasColumnName("LOG_NO");
            this.Property(t => t.LOG_COMPLEMENTO).HasColumnName("LOG_COMPLEMENTO");
            this.Property(t => t.CEP).HasColumnName("CEP");
            this.Property(t => t.TLO_TX).HasColumnName("TLO_TX");
            this.Property(t => t.LOG_STA_TLO).HasColumnName("LOG_STA_TLO");
            this.Property(t => t.LOG_NO_ABREV).HasColumnName("LOG_NO_ABREV");
            this.Property(t => t.Incluido).HasColumnName("Incluido");
        }
    }
}
