using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LogMetodoMap : EntityTypeConfiguration<LogMetodo>
    {
        public LogMetodoMap()
        {
            // Primary Key
            this.HasKey(t => t.idLogMetodo);

            // Properties
            this.Property(t => t.NomeMetodo)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.TempoGasto)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LogMetodo");
            this.Property(t => t.NomeMetodo).HasColumnName("NomeMetodo");
            this.Property(t => t.DataHoraInicio).HasColumnName("DataHoraInicio");
            this.Property(t => t.DataHoraTermino).HasColumnName("DataHoraTermino");
            this.Property(t => t.TempoGasto).HasColumnName("TempoGasto");
            this.Property(t => t.Obs).HasColumnName("Obs");
            this.Property(t => t.idLogMetodo).HasColumnName("idLogMetodo");
            this.Property(t => t.idUsuario).HasColumnName("idUsuario");
        }
    }
}
