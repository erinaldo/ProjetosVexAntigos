using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CONFERENCIAPALLETENTRADAMap : EntityTypeConfiguration<ZID_CONFERENCIAPALLETENTRADA>
    {
        public ZID_CONFERENCIAPALLETENTRADAMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CONFERENCIAPALLETENTRADA");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
