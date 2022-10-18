using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_AgrupamentoMap : EntityTypeConfiguration<ZID_Agrupamento>
    {
        public ZID_AgrupamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_Agrupamento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
