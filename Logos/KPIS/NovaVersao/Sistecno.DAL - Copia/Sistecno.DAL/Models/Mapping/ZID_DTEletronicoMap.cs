using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DTEletronicoMap : EntityTypeConfiguration<ZID_DTEletronico>
    {
        public ZID_DTEletronicoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DTEletronico");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
