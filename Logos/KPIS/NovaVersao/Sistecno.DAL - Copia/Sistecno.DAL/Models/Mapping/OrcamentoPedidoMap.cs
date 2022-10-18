using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OrcamentoPedidoMap : EntityTypeConfiguration<OrcamentoPedido>
    {
        public OrcamentoPedidoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdOrcamentoPedido);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrcamentoPedido");
            this.Property(t => t.IdOrcamentoPedido).HasColumnName("IdOrcamentoPedido");
            this.Property(t => t.IdOrcamento).HasColumnName("IdOrcamento");
            this.Property(t => t.IdCidade).HasColumnName("IdCidade");
            this.Property(t => t.IdModal).HasColumnName("IdModal");
            this.Property(t => t.IdPlanilha).HasColumnName("IdPlanilha");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.PesoLiquido).HasColumnName("PesoLiquido");
            this.Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            this.Property(t => t.PesoCubado).HasColumnName("PesoCubado");
            this.Property(t => t.MetragemCubica).HasColumnName("MetragemCubica");
            this.Property(t => t.Frete).HasColumnName("Frete");
            this.Property(t => t.BaseDeCalculo).HasColumnName("BaseDeCalculo");
            this.Property(t => t.Aliquota).HasColumnName("Aliquota");
            this.Property(t => t.Icms).HasColumnName("Icms");
            this.Property(t => t.FreteTotal).HasColumnName("FreteTotal");

            // Relationships
            this.HasRequired(t => t.Orcamento)
                .WithMany(t => t.OrcamentoPedidoes)
                .HasForeignKey(d => d.IdOrcamento);

        }
    }
}
