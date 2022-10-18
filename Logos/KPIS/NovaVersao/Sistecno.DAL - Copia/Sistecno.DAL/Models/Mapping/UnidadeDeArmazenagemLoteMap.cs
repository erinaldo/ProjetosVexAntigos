using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UnidadeDeArmazenagemLoteMap : EntityTypeConfiguration<UnidadeDeArmazenagemLote>
    {
        public UnidadeDeArmazenagemLoteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUnidadeDeArmazenagemLote);

            // Properties
            this.Property(t => t.IDUnidadeDeArmazenagemLote)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Divisao)
                .HasMaxLength(15);

            this.Property(t => t.obs)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("UnidadeDeArmazenagemLote");
            this.Property(t => t.IDUnidadeDeArmazenagemLote).HasColumnName("IDUnidadeDeArmazenagemLote");
            this.Property(t => t.IDUnidadeDeArmazenagem).HasColumnName("IDUnidadeDeArmazenagem");
            this.Property(t => t.IDLote).HasColumnName("IDLote");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.Lastro).HasColumnName("Lastro");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.Divisao).HasColumnName("Divisao");
            this.Property(t => t.SaldoPicking).HasColumnName("SaldoPicking");
            this.Property(t => t.obs).HasColumnName("obs");

            // Relationships
            this.HasRequired(t => t.UnidadeDeArmazenagem)
                .WithMany(t => t.UnidadeDeArmazenagemLotes)
                .HasForeignKey(d => d.IDUnidadeDeArmazenagem);

        }
    }
}
