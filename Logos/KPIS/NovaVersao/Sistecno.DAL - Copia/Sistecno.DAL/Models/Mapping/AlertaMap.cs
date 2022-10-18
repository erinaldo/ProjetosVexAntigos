using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AlertaMap : EntityTypeConfiguration<Alerta>
    {
        public AlertaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDAlerta);

            // Properties
            this.Property(t => t.IDAlerta)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Rotina)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Alerta");
            this.Property(t => t.IDAlerta).HasColumnName("IDAlerta");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Rotina).HasColumnName("Rotina");
            this.Property(t => t.ExecutarEmMinutos).HasColumnName("ExecutarEmMinutos");
        }
    }
}
