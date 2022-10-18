using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UnidadeDeArmazenagemMap : EntityTypeConfiguration<UnidadeDeArmazenagem>
    {
        public UnidadeDeArmazenagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUnidadeDeArmazenagem);

            // Properties
            this.Property(t => t.IDUnidadeDeArmazenagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UnidadeDeArmazenagem");
            this.Property(t => t.IDUnidadeDeArmazenagem).HasColumnName("IDUnidadeDeArmazenagem");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDDepositoPlantaLocalizacao).HasColumnName("IDDepositoPlantaLocalizacao");
            this.Property(t => t.Impressao).HasColumnName("Impressao");

            // Relationships
            this.HasRequired(t => t.DepositoPlantaLocalizacao)
                .WithMany(t => t.UnidadeDeArmazenagems)
                .HasForeignKey(d => d.IDDepositoPlantaLocalizacao);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.UnidadeDeArmazenagems)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
