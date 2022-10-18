using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_GRANDE_USUARIOMap : EntityTypeConfiguration<LOG_GRANDE_USUARIO>
    {
        public LOG_GRANDE_USUARIOMap()
        {
            // Primary Key
            this.HasKey(t => new { t.GRU_NU, t.UFE_SG, t.LOC_NU, t.BAI_NU, t.GRU_NO, t.GRU_ENDERECO, t.CEP });

            // Properties
            this.Property(t => t.GRU_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UFE_SG)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.LOC_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BAI_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GRU_NO)
                .IsRequired()
                .HasMaxLength(72);

            this.Property(t => t.GRU_ENDERECO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CEP)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.GRU_NO_ABREV)
                .HasMaxLength(36);

            // Table & Column Mappings
            this.ToTable("LOG_GRANDE_USUARIO");
            this.Property(t => t.GRU_NU).HasColumnName("GRU_NU");
            this.Property(t => t.UFE_SG).HasColumnName("UFE_SG");
            this.Property(t => t.LOC_NU).HasColumnName("LOC_NU");
            this.Property(t => t.BAI_NU).HasColumnName("BAI_NU");
            this.Property(t => t.LOG_NU).HasColumnName("LOG_NU");
            this.Property(t => t.GRU_NO).HasColumnName("GRU_NO");
            this.Property(t => t.GRU_ENDERECO).HasColumnName("GRU_ENDERECO");
            this.Property(t => t.CEP).HasColumnName("CEP");
            this.Property(t => t.GRU_NO_ABREV).HasColumnName("GRU_NO_ABREV");
        }
    }
}
