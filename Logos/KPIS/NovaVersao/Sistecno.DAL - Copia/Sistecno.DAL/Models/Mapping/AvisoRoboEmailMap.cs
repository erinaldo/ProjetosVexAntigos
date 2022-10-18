using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AvisoRoboEmailMap : EntityTypeConfiguration<AvisoRoboEmail>
    {
        public AvisoRoboEmailMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDAvisoKPI, t.Nome, t.Email, t.Horas });

            // Properties
            this.Property(t => t.IDAvisoKPI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Horas)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AvisoRoboEmail");
            this.Property(t => t.IDAvisoKPI).HasColumnName("IDAvisoKPI");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Horas).HasColumnName("Horas");
            this.Property(t => t.Reports).HasColumnName("Reports");
        }
    }
}
