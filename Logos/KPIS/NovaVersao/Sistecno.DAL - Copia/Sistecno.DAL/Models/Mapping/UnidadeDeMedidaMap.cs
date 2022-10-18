using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UnidadeDeMedidaMap : EntityTypeConfiguration<UnidadeDeMedida>
    {
        public UnidadeDeMedidaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUnidadeDeMedida);

            // Properties
            this.Property(t => t.IDUnidadeDeMedida)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Unidade)
                .HasMaxLength(20);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UnidadeDeMedida");
            this.Property(t => t.IDUnidadeDeMedida).HasColumnName("IDUnidadeDeMedida");
            this.Property(t => t.Unidade).HasColumnName("Unidade");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Decimais).HasColumnName("Decimais");
        }
    }
}
