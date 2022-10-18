using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_AgrupamentoRegiaoMap : EntityTypeConfiguration<ZID_AgrupamentoRegiao>
    {
        public ZID_AgrupamentoRegiaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_AgrupamentoRegiao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
