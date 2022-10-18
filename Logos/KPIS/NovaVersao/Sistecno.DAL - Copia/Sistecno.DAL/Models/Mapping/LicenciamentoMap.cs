using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LicenciamentoMap : EntityTypeConfiguration<Licenciamento>
    {
        public LicenciamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdLicenciamento);

            // Properties
            this.Property(t => t.IdLicenciamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Licenciamento");
            this.Property(t => t.IdLicenciamento).HasColumnName("IdLicenciamento");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
        }
    }
}
