using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EDI_BairroMap : EntityTypeConfiguration<ZID_EDI_Bairro>
    {
        public ZID_EDI_BairroMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EDI_Bairro");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
