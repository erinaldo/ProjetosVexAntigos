using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_BAIRROMap : EntityTypeConfiguration<ZID_BAIRRO>
    {
        public ZID_BAIRROMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_BAIRRO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
