using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_FAIXA_BAIRROMap : EntityTypeConfiguration<LOG_FAIXA_BAIRRO>
    {
        public LOG_FAIXA_BAIRROMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BAI_NU, t.FCB_CEP_INI, t.FCB_CEP_FIM });

            // Properties
            this.Property(t => t.BAI_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FCB_CEP_INI)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.FCB_CEP_FIM)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("LOG_FAIXA_BAIRRO");
            this.Property(t => t.BAI_NU).HasColumnName("BAI_NU");
            this.Property(t => t.FCB_CEP_INI).HasColumnName("FCB_CEP_INI");
            this.Property(t => t.FCB_CEP_FIM).HasColumnName("FCB_CEP_FIM");
        }
    }
}
