using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FrotaMap : EntityTypeConfiguration<Frota>
    {
        public FrotaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDFrota);

            // Properties
            this.Property(t => t.IDFrota)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Frota");
            this.Property(t => t.IDFrota).HasColumnName("IDFrota");
            this.Property(t => t.NumeroDaFrota).HasColumnName("NumeroDaFrota");

            // Relationships
            this.HasRequired(t => t.Veiculo)
                .WithOptional(t => t.Frota);

        }
    }
}
