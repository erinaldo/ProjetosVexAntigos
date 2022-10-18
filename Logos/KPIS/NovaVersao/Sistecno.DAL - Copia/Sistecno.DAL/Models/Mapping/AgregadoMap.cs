using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AgregadoMap : EntityTypeConfiguration<Agregado>
    {
        public AgregadoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAgregado);

            // Properties
            this.Property(t => t.IdAgregado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Agregado");
            this.Property(t => t.IdAgregado).HasColumnName("IdAgregado");
            this.Property(t => t.IdContaContabil).HasColumnName("IdContaContabil");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithOptional(t => t.Agregado);

        }
    }
}
