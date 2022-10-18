using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_VeiculoTabelaRegiaoItemMap : EntityTypeConfiguration<ZID_VeiculoTabelaRegiaoItem>
    {
        public ZID_VeiculoTabelaRegiaoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_VeiculoTabelaRegiaoItem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
