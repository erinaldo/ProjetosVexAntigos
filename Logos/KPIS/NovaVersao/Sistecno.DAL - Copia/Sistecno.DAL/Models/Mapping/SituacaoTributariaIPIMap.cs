using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class SituacaoTributariaIPIMap : EntityTypeConfiguration<SituacaoTributariaIPI>
    {
        public SituacaoTributariaIPIMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Codigo, t.Descricao });

            // Properties
            this.Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SituacaoTributariaIPI");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
