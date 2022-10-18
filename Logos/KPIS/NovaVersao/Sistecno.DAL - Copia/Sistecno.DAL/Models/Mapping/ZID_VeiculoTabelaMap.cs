using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_VeiculoTabelaMap : EntityTypeConfiguration<ZID_VeiculoTabela>
    {
        public ZID_VeiculoTabelaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_VeiculoTabela");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
