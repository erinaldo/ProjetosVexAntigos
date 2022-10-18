using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RequisicaoDeMaterialItemMap : EntityTypeConfiguration<RequisicaoDeMaterialItem>
    {
        public RequisicaoDeMaterialItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRequisicaoDeMaterialItem);

            // Properties
            this.Property(t => t.IdRequisicaoDeMaterialItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Andamento)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Observacao)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("RequisicaoDeMaterialItem");
            this.Property(t => t.IdRequisicaoDeMaterialItem).HasColumnName("IdRequisicaoDeMaterialItem");
            this.Property(t => t.IdRequisicaoDeMaterial).HasColumnName("IdRequisicaoDeMaterial");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.QuantidadeSolicitada).HasColumnName("QuantidadeSolicitada");
            this.Property(t => t.QuantidadeAtendida).HasColumnName("QuantidadeAtendida");
            this.Property(t => t.Andamento).HasColumnName("Andamento");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.IdCentroDeCusto).HasColumnName("IdCentroDeCusto");

            // Relationships
            this.HasRequired(t => t.RequisicaoDeMaterial)
                .WithMany(t => t.RequisicaoDeMaterialItems)
                .HasForeignKey(d => d.IdRequisicaoDeMaterial);

        }
    }
}
