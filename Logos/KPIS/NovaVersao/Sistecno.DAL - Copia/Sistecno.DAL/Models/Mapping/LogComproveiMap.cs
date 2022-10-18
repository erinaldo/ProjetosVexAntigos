using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LogComproveiMap : EntityTypeConfiguration<LogComprovei>
    {
        public LogComproveiMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdDocumento, t.Chave, t.DataHora });

            // Properties
            this.Property(t => t.IdDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Chave)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LogComprovei");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
            this.Property(t => t.Resultado).HasColumnName("Resultado");
            this.Property(t => t.XML).HasColumnName("XML");
        }
    }
}
