using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EstoqueComprasMovMap : EntityTypeConfiguration<EstoqueComprasMov>
    {
        public EstoqueComprasMovMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEstoqueComprasMov);

            // Properties
            this.Property(t => t.IDEstoqueComprasMov)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EntradaSaida)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("EstoqueComprasMov");
            this.Property(t => t.IDEstoqueComprasMov).HasColumnName("IDEstoqueComprasMov");
            this.Property(t => t.IDRequisicaoDeMaterial).HasColumnName("IDRequisicaoDeMaterial");
            this.Property(t => t.IDRequisicaoDeMaterialItem).HasColumnName("IDRequisicaoDeMaterialItem");
            this.Property(t => t.IDEstoque).HasColumnName("IDEstoque");
            this.Property(t => t.IDLote).HasColumnName("IDLote");
            this.Property(t => t.IDUnidadeDeArmazenagemLote).HasColumnName("IDUnidadeDeArmazenagemLote");
            this.Property(t => t.DataDeMovimentacao).HasColumnName("DataDeMovimentacao");
            this.Property(t => t.EntradaSaida).HasColumnName("EntradaSaida");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");

            // Relationships
            this.HasRequired(t => t.Estoque)
                .WithMany(t => t.EstoqueComprasMovs)
                .HasForeignKey(d => d.IDEstoque);
            this.HasRequired(t => t.Lote)
                .WithMany(t => t.EstoqueComprasMovs)
                .HasForeignKey(d => d.IDLote);
            this.HasRequired(t => t.RequisicaoDeMaterial)
                .WithMany(t => t.EstoqueComprasMovs)
                .HasForeignKey(d => d.IDRequisicaoDeMaterial);
            this.HasRequired(t => t.RequisicaoDeMaterialItem)
                .WithMany(t => t.EstoqueComprasMovs)
                .HasForeignKey(d => d.IDRequisicaoDeMaterialItem);
            this.HasRequired(t => t.UnidadeDeArmazenagemLote)
                .WithMany(t => t.EstoqueComprasMovs)
                .HasForeignKey(d => d.IDUnidadeDeArmazenagemLote);

        }
    }
}
