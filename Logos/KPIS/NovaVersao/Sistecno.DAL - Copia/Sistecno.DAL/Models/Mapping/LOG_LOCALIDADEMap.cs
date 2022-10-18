using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_LOCALIDADEMap : EntityTypeConfiguration<LOG_LOCALIDADE>
    {
        public LOG_LOCALIDADEMap()
        {
            // Primary Key
            this.HasKey(t => t.LOC_NU);

            // Properties
            this.Property(t => t.LOC_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UFE_SG)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.LOC_NO)
                .IsRequired()
                .HasMaxLength(72);

            this.Property(t => t.CEP)
                .HasMaxLength(8);

            this.Property(t => t.LOC_IN_SIT)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.LOC_IN_TIPO_LOC)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.LOC_NO_ABREV)
                .HasMaxLength(36);

            // Table & Column Mappings
            this.ToTable("LOG_LOCALIDADE");
            this.Property(t => t.LOC_NU).HasColumnName("LOC_NU");
            this.Property(t => t.UFE_SG).HasColumnName("UFE_SG");
            this.Property(t => t.LOC_NO).HasColumnName("LOC_NO");
            this.Property(t => t.CEP).HasColumnName("CEP");
            this.Property(t => t.LOC_IN_SIT).HasColumnName("LOC_IN_SIT");
            this.Property(t => t.LOC_IN_TIPO_LOC).HasColumnName("LOC_IN_TIPO_LOC");
            this.Property(t => t.LOC_NU_SUB).HasColumnName("LOC_NU_SUB");
            this.Property(t => t.LOC_NO_ABREV).HasColumnName("LOC_NO_ABREV");
            this.Property(t => t.MUN_NU).HasColumnName("MUN_NU");
            this.Property(t => t.Incluido).HasColumnName("Incluido");
        }
    }
}
