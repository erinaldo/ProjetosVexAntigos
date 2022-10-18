using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_LANCAMENTOCONTABILMap : EntityTypeConfiguration<ZID_LANCAMENTOCONTABIL>
    {
        public ZID_LANCAMENTOCONTABILMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_LANCAMENTOCONTABIL");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
