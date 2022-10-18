using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_RASTREAMENTOMap : EntityTypeConfiguration<ZID_RASTREAMENTO>
    {
        public ZID_RASTREAMENTOMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_RASTREAMENTO");
            this.Property(t => t.Id).HasColumnName("Id");
        }
    }
}
