using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DepositoPlantaLocalizacaoRegraMap : EntityTypeConfiguration<DepositoPlantaLocalizacaoRegra>
    {
        public DepositoPlantaLocalizacaoRegraMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDepositoPlantaLocalizacaoRegra);

            // Properties
            this.Property(t => t.IdDepositoPlantaLocalizacaoRegra)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DepositoPlantaLocalizacaoRegra");
            this.Property(t => t.IdDepositoPlantaLocalizacaoRegra).HasColumnName("IdDepositoPlantaLocalizacaoRegra");
            this.Property(t => t.IdDepositoPlantaLocalizacao).HasColumnName("IdDepositoPlantaLocalizacao");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.idGrupoDeProduto).HasColumnName("idGrupoDeProduto");

            // Relationships
            this.HasOptional(t => t.Cliente)
                .WithMany(t => t.DepositoPlantaLocalizacaoRegras)
                .HasForeignKey(d => d.IdCliente);
            this.HasRequired(t => t.DepositoPlantaLocalizacao)
                .WithMany(t => t.DepositoPlantaLocalizacaoRegras)
                .HasForeignKey(d => d.IdDepositoPlantaLocalizacao);
            this.HasRequired(t => t.DepositoPlantaLocalizacaoRegra2)
                .WithOptional(t => t.DepositoPlantaLocalizacaoRegra1);
            this.HasOptional(t => t.GrupoDeProduto)
                .WithMany(t => t.DepositoPlantaLocalizacaoRegras)
                .HasForeignKey(d => d.idGrupoDeProduto);
            this.HasOptional(t => t.ProdutoCliente)
                .WithMany(t => t.DepositoPlantaLocalizacaoRegras)
                .HasForeignKey(d => d.IdProdutoCliente);

        }
    }
}
