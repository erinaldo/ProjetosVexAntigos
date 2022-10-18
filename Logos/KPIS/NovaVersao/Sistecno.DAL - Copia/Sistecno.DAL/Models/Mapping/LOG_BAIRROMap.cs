using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_BAIRROMap : EntityTypeConfiguration<LOG_BAIRRO>
    {
        public LOG_BAIRROMap()
        {
            // Primary Key
            this.HasKey(t => t.BAI_NU);

            // Properties
            this.Property(t => t.BAI_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UFE_SG)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.BAI_NO)
                .IsRequired()
                .HasMaxLength(72);

            this.Property(t => t.BAI_NO_ABREV)
                .HasMaxLength(36);

            // Table & Column Mappings
            this.ToTable("LOG_BAIRRO");
            this.Property(t => t.BAI_NU).HasColumnName("BAI_NU");
            this.Property(t => t.UFE_SG).HasColumnName("UFE_SG");
            this.Property(t => t.LOC_NU).HasColumnName("LOC_NU");
            this.Property(t => t.BAI_NO).HasColumnName("BAI_NO");
            this.Property(t => t.BAI_NO_ABREV).HasColumnName("BAI_NO_ABREV");
            this.Property(t => t.Incluido).HasColumnName("Incluido");

            // Relationships
            this.HasRequired(t => t.LOG_LOCALIDADE)
                .WithMany(t => t.LOG_BAIRRO)
                .HasForeignKey(d => d.LOC_NU);

        }
    }
}
