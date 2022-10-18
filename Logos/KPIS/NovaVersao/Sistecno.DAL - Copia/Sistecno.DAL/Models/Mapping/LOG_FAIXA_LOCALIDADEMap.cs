using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_FAIXA_LOCALIDADEMap : EntityTypeConfiguration<LOG_FAIXA_LOCALIDADE>
    {
        public LOG_FAIXA_LOCALIDADEMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LOC_NU, t.LOC_CEP_INI, t.LOC_CEP_FIM });

            // Properties
            this.Property(t => t.LOC_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LOC_CEP_INI)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.LOC_CEP_FIM)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("LOG_FAIXA_LOCALIDADE");
            this.Property(t => t.LOC_NU).HasColumnName("LOC_NU");
            this.Property(t => t.LOC_CEP_INI).HasColumnName("LOC_CEP_INI");
            this.Property(t => t.LOC_CEP_FIM).HasColumnName("LOC_CEP_FIM");
            this.Property(t => t.Incluido).HasColumnName("Incluido");

            // Relationships
            this.HasRequired(t => t.LOG_LOCALIDADE)
                .WithMany(t => t.LOG_FAIXA_LOCALIDADE)
                .HasForeignKey(d => d.LOC_NU);

        }
    }
}
