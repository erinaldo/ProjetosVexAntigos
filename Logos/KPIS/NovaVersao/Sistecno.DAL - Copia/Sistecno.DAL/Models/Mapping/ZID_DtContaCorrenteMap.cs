using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DtContaCorrenteMap : EntityTypeConfiguration<ZID_DtContaCorrente>
    {
        public ZID_DtContaCorrenteMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DtContaCorrente");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
