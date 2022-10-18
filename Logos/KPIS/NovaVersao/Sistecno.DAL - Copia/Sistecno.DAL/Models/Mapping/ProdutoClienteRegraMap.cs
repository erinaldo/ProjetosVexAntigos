using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProdutoClienteRegraMap : EntityTypeConfiguration<ProdutoClienteRegra>
    {
        public ProdutoClienteRegraMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdProdutoClienteRegra, t.IdProdutoCliente, t.IdDepositoPlantaLocalizacao, t.TipoDeRegra });

            // Properties
            this.Property(t => t.IdProdutoClienteRegra)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdProdutoCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdDepositoPlantaLocalizacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeRegra)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("ProdutoClienteRegra");
            this.Property(t => t.IdProdutoClienteRegra).HasColumnName("IdProdutoClienteRegra");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.IdDepositoPlantaLocalizacao).HasColumnName("IdDepositoPlantaLocalizacao");
            this.Property(t => t.TipoDeRegra).HasColumnName("TipoDeRegra");

            // Relationships
            this.HasRequired(t => t.DepositoPlantaLocalizacao)
                .WithMany(t => t.ProdutoClienteRegras)
                .HasForeignKey(d => d.IdDepositoPlantaLocalizacao);
            this.HasRequired(t => t.ProdutoCliente)
                .WithMany(t => t.ProdutoClienteRegras)
                .HasForeignKey(d => d.IdProdutoCliente);

        }
    }
}
