using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoTabelaRegiaoItemMap : EntityTypeConfiguration<VeiculoTabelaRegiaoItem>
    {
        public VeiculoTabelaRegiaoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdVeiculoTabelaRegiaoItem);

            // Properties
            this.Property(t => t.IdVeiculoTabelaRegiaoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VeiculoTabelaRegiaoItem");
            this.Property(t => t.IdVeiculoTabelaRegiaoItem).HasColumnName("IdVeiculoTabelaRegiaoItem");
            this.Property(t => t.IdVeiculoTabelaRegiao).HasColumnName("IdVeiculoTabelaRegiao");
            this.Property(t => t.IdRegiaoItem).HasColumnName("IdRegiaoItem");
            this.Property(t => t.KM).HasColumnName("KM");
            this.Property(t => t.ValorPorEntrega).HasColumnName("ValorPorEntrega");
            this.Property(t => t.Adicional).HasColumnName("Adicional");
            this.Property(t => t.Pedagio).HasColumnName("Pedagio");
            this.Property(t => t.Cadastro).HasColumnName("Cadastro");
            this.Property(t => t.UltimaAlteracao).HasColumnName("UltimaAlteracao");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");

            // Relationships
            this.HasRequired(t => t.VeiculoTabelaRegiao)
                .WithMany(t => t.VeiculoTabelaRegiaoItems)
                .HasForeignKey(d => d.IdVeiculoTabelaRegiao);

        }
    }
}
