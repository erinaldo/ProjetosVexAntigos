using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TabelaDeFreteMap : EntityTypeConfiguration<ZID_TabelaDeFrete>
    {
        public ZID_TabelaDeFreteMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TabelaDeFrete");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
