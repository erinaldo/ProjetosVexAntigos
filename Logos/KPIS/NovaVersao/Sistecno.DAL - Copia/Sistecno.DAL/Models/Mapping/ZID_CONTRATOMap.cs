using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CONTRATOMap : EntityTypeConfiguration<ZID_CONTRATO>
    {
        public ZID_CONTRATOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CONTRATO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
