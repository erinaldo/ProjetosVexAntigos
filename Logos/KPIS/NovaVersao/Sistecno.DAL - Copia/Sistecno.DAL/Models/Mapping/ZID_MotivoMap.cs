using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_MotivoMap : EntityTypeConfiguration<ZID_Motivo>
    {
        public ZID_MotivoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_Motivo");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
