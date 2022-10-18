using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UnidadeDeArmazenagemAgrupMap : EntityTypeConfiguration<UnidadeDeArmazenagemAgrup>
    {
        public UnidadeDeArmazenagemAgrupMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUnidadeDeArmazenagemAgrup);

            // Properties
            this.Property(t => t.IDUnidadeDeArmazenagemAgrup)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UnidadeDeArmazenagemAgrup");
            this.Property(t => t.IDUnidadeDeArmazenagemAgrup).HasColumnName("IDUnidadeDeArmazenagemAgrup");
            this.Property(t => t.IDUnidadeDeArmazenagem).HasColumnName("IDUnidadeDeArmazenagem");
            this.Property(t => t.Agrupamento).HasColumnName("Agrupamento");

            // Relationships
            this.HasRequired(t => t.UnidadeDeArmazenagem)
                .WithMany(t => t.UnidadeDeArmazenagemAgrups)
                .HasForeignKey(d => d.IDUnidadeDeArmazenagem);

        }
    }
}
