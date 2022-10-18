using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_contacontabilMap : EntityTypeConfiguration<ZID_contacontabil>
    {
        public ZID_contacontabilMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_contacontabil");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
