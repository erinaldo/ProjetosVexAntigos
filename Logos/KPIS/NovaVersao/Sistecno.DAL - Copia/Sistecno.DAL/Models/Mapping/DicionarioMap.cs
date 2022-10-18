using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DicionarioMap : EntityTypeConfiguration<Dicionario>
    {
        public DicionarioMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TABELA, t.CAMPO });

            // Properties
            this.Property(t => t.TABELA)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CAMPO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OBRIGATORIO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CONTROLADO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Dicionario");
            this.Property(t => t.TABELA).HasColumnName("TABELA");
            this.Property(t => t.CAMPO).HasColumnName("CAMPO");
            this.Property(t => t.OBRIGATORIO).HasColumnName("OBRIGATORIO");
            this.Property(t => t.CONTROLADO).HasColumnName("CONTROLADO");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
