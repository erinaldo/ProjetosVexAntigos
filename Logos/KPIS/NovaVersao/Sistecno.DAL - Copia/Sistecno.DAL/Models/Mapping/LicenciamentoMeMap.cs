using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LicenciamentoMeMap : EntityTypeConfiguration<LicenciamentoMe>
    {
        public LicenciamentoMeMap()
        {
            // Primary Key
            this.HasKey(t => t.IdLicenciamentoMes);

            // Properties
            this.Property(t => t.IdLicenciamentoMes)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FinalDaPlaca)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.Mes)
                .IsRequired()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("LicenciamentoMes");
            this.Property(t => t.IdLicenciamentoMes).HasColumnName("IdLicenciamentoMes");
            this.Property(t => t.IdLicenciamento).HasColumnName("IdLicenciamento");
            this.Property(t => t.FinalDaPlaca).HasColumnName("FinalDaPlaca");
            this.Property(t => t.Mes).HasColumnName("Mes");

            // Relationships
            this.HasRequired(t => t.Licenciamento)
                .WithMany(t => t.LicenciamentoMes)
                .HasForeignKey(d => d.IdLicenciamento);

        }
    }
}
