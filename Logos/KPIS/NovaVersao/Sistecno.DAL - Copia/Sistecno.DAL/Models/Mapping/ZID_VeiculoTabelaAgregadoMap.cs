using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_VeiculoTabelaAgregadoMap : EntityTypeConfiguration<ZID_VeiculoTabelaAgregado>
    {
        public ZID_VeiculoTabelaAgregadoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_VeiculoTabelaAgregado");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
