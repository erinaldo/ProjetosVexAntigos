using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_clientedivisaoMap : EntityTypeConfiguration<ZID_clientedivisao>
    {
        public ZID_clientedivisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_clientedivisao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
