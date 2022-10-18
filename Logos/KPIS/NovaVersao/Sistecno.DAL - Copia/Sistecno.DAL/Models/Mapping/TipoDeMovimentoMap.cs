using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TipoDeMovimentoMap : EntityTypeConfiguration<TipoDeMovimento>
    {
        public TipoDeMovimentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTipoDeMovimento);

            // Properties
            this.Property(t => t.IdTipoDeMovimento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            this.Property(t => t.EntradaSaida)
                .HasMaxLength(10);

            this.Property(t => t.TipoDeDocumento)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TipoDeMovimento");
            this.Property(t => t.IdTipoDeMovimento).HasColumnName("IdTipoDeMovimento");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.EntradaSaida).HasColumnName("EntradaSaida");
            this.Property(t => t.TipoDeDocumento).HasColumnName("TipoDeDocumento");
        }
    }
}
