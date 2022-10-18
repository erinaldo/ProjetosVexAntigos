using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LogRoboAcertoNotaMap : EntityTypeConfiguration<LogRoboAcertoNota>
    {
        public LogRoboAcertoNotaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdLogRoboAcertoNota);

            // Properties
            this.Property(t => t.TipoServicoAnterior)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SerieAnterior)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LogRoboAcertoNota");
            this.Property(t => t.IdLogRoboAcertoNota).HasColumnName("IdLogRoboAcertoNota");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.TipoServicoAnterior).HasColumnName("TipoServicoAnterior");
            this.Property(t => t.SerieAnterior).HasColumnName("SerieAnterior");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
        }
    }
}
