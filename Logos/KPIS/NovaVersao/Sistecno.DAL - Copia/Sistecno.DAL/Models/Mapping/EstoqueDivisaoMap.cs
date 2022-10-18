using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EstoqueDivisaoMap : EntityTypeConfiguration<EstoqueDivisao>
    {
        public EstoqueDivisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEstoqueDivisao);

            // Properties
            this.Property(t => t.IDEstoqueDivisao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("EstoqueDivisao");
            this.Property(t => t.IDEstoqueDivisao).HasColumnName("IDEstoqueDivisao");
            this.Property(t => t.IDEstoque).HasColumnName("IDEstoque");
            this.Property(t => t.IDClienteDivisao).HasColumnName("IDClienteDivisao");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.SaldoBaseExterna).HasColumnName("SaldoBaseExterna");
            this.Property(t => t.PercentualRateio).HasColumnName("PercentualRateio");
            this.Property(t => t.data_limite).HasColumnName("data_limite");
            this.Property(t => t.PercentualRateioCda).HasColumnName("PercentualRateioCda");
            this.Property(t => t.Inventariado).HasColumnName("Inventariado");

            // Relationships
            this.HasRequired(t => t.ClienteDivisao)
                .WithMany(t => t.EstoqueDivisaos)
                .HasForeignKey(d => d.IDClienteDivisao);
            this.HasRequired(t => t.Estoque)
                .WithMany(t => t.EstoqueDivisaos)
                .HasForeignKey(d => d.IDEstoque);

        }
    }
}
