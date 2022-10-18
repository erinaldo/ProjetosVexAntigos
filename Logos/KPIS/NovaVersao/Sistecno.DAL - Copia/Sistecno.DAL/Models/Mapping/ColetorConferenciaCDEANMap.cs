using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ColetorConferenciaCDEANMap : EntityTypeConfiguration<ColetorConferenciaCDEAN>
    {
        public ColetorConferenciaCDEANMap()
        {
            // Primary Key
            this.HasKey(t => t.IdColetorConferenciaCDEAN);

            // Properties
            this.Property(t => t.CodigoBarras)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UnidadeVenda)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.DescricaoUnidadeVenda)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ColetorConferenciaCDEAN");
            this.Property(t => t.IdColetorConferenciaCDEAN).HasColumnName("IdColetorConferenciaCDEAN");
            this.Property(t => t.CodigoBarras).HasColumnName("CodigoBarras");
            this.Property(t => t.UnidadeVenda).HasColumnName("UnidadeVenda");
            this.Property(t => t.DescricaoUnidadeVenda).HasColumnName("DescricaoUnidadeVenda");
            this.Property(t => t.QuantidadeUnidadeVenda).HasColumnName("QuantidadeUnidadeVenda");
            this.Property(t => t.UltimaAtualizacao).HasColumnName("UltimaAtualizacao");
        }
    }
}
