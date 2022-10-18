using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EnquadramentoIPIMap : EntityTypeConfiguration<EnquadramentoIPI>
    {
        public EnquadramentoIPIMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Codigo, t.GrupoCST, t.Descricao });

            // Properties
            this.Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.GrupoCST)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("EnquadramentoIPI");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.GrupoCST).HasColumnName("GrupoCST");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
