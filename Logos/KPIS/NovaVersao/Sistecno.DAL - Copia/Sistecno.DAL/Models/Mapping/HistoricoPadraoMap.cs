using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class HistoricoPadraoMap : EntityTypeConfiguration<HistoricoPadrao>
    {
        public HistoricoPadraoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdHistoricoPadrao);

            // Properties
            this.Property(t => t.IdHistoricoPadrao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HistoricoPadrao1)
                .HasMaxLength(100);

            this.Property(t => t.TipoDeLancamento)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("HistoricoPadrao");
            this.Property(t => t.IdHistoricoPadrao).HasColumnName("IdHistoricoPadrao");
            this.Property(t => t.HistoricoPadrao1).HasColumnName("HistoricoPadrao");
            this.Property(t => t.TipoDeLancamento).HasColumnName("TipoDeLancamento");
        }
    }
}
