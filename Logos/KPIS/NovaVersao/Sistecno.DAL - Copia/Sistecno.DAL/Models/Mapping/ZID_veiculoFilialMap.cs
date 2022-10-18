using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_veiculoFilialMap : EntityTypeConfiguration<ZID_veiculoFilial>
    {
        public ZID_veiculoFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_veiculoFilial");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
