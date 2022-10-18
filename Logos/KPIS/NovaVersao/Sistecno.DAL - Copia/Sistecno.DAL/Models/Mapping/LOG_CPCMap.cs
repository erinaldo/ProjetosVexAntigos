using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_CPCMap : EntityTypeConfiguration<LOG_CPC>
    {
        public LOG_CPCMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CPC_NU, t.UFE_SG, t.LOC_NU, t.CPC_NO, t.CPC_ENDERECO, t.CEP });

            // Properties
            this.Property(t => t.CPC_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UFE_SG)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.LOC_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CPC_NO)
                .IsRequired()
                .HasMaxLength(72);

            this.Property(t => t.CPC_ENDERECO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CEP)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("LOG_CPC");
            this.Property(t => t.CPC_NU).HasColumnName("CPC_NU");
            this.Property(t => t.UFE_SG).HasColumnName("UFE_SG");
            this.Property(t => t.LOC_NU).HasColumnName("LOC_NU");
            this.Property(t => t.CPC_NO).HasColumnName("CPC_NO");
            this.Property(t => t.CPC_ENDERECO).HasColumnName("CPC_ENDERECO");
            this.Property(t => t.CEP).HasColumnName("CEP");
        }
    }
}
