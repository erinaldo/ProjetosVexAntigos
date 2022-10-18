using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_PortariaVisitanteMap : EntityTypeConfiguration<ZID_PortariaVisitante>
    {
        public ZID_PortariaVisitanteMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_PortariaVisitante");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
