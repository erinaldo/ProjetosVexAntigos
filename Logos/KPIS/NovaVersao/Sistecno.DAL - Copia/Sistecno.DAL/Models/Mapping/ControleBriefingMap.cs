using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ControleBriefingMap : EntityTypeConfiguration<ControleBriefing>
    {
        public ControleBriefingMap()
        {
            // Primary Key
            this.HasKey(t => t.IdControleBriefing);

            // Properties
            this.Property(t => t.IdControleBriefing)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ControleBriefing");
            this.Property(t => t.IdControleBriefing).HasColumnName("IdControleBriefing");
            this.Property(t => t.Briefing).HasColumnName("Briefing");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
        }
    }
}
