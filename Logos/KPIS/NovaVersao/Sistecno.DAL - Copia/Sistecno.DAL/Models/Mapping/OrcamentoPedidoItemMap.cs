using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OrcamentoPedidoItemMap : EntityTypeConfiguration<OrcamentoPedidoItem>
    {
        public OrcamentoPedidoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdOrcamentoPedidoItem);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrcamentoPedidoItem");
            this.Property(t => t.IdOrcamentoPedidoItem).HasColumnName("IdOrcamentoPedidoItem");
            this.Property(t => t.IdOrcamentoPedido).HasColumnName("IdOrcamentoPedido");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");
            this.Property(t => t.TotalDoItem).HasColumnName("TotalDoItem");
            this.Property(t => t.PesoLiquido).HasColumnName("PesoLiquido");
            this.Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            this.Property(t => t.MetragemCubica).HasColumnName("MetragemCubica");
            this.Property(t => t.PesoCubado).HasColumnName("PesoCubado");

            // Relationships
            this.HasRequired(t => t.OrcamentoPedido)
                .WithMany(t => t.OrcamentoPedidoItems)
                .HasForeignKey(d => d.IdOrcamentoPedido);

        }
    }
}
