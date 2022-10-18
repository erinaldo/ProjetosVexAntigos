using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_VeiculoTabelaRegiaoMap : EntityTypeConfiguration<ZID_VeiculoTabelaRegiao>
    {
        public ZID_VeiculoTabelaRegiaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_VeiculoTabelaRegiao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
