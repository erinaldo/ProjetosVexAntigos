using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_MOVIMENTACAOITEMMap : EntityTypeConfiguration<ZID_MOVIMENTACAOITEM>
    {
        public ZID_MOVIMENTACAOITEMMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_MOVIMENTACAOITEM");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
