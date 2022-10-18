using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TipoDeItemMap : EntityTypeConfiguration<TipoDeItem>
    {
        public TipoDeItemMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TipoDeItem1, t.Descricao });

            // Properties
            this.Property(t => t.TipoDeItem1)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TipoDeItem");
            this.Property(t => t.TipoDeItem1).HasColumnName("TipoDeItem");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
