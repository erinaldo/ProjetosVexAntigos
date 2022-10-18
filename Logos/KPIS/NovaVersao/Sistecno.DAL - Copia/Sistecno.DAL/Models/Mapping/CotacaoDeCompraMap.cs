using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CotacaoDeCompraMap : EntityTypeConfiguration<CotacaoDeCompra>
    {
        public CotacaoDeCompraMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCotacaoDeCompra);

            // Properties
            this.Property(t => t.IdCotacaoDeCompra)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            this.Property(t => t.Adiantamento)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("CotacaoDeCompra");
            this.Property(t => t.IdCotacaoDeCompra).HasColumnName("IdCotacaoDeCompra");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.IdUsuarioCompra).HasColumnName("IdUsuarioCompra");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Adiantamento).HasColumnName("Adiantamento");
        }
    }
}
