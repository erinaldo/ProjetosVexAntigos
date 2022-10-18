using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_GAIOLACONFERENCIAMap : EntityTypeConfiguration<ZID_GAIOLACONFERENCIA>
    {
        public ZID_GAIOLACONFERENCIAMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_GAIOLACONFERENCIA");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
