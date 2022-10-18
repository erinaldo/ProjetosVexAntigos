using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_AgendaRecebimentoMap : EntityTypeConfiguration<ZID_AgendaRecebimento>
    {
        public ZID_AgendaRecebimentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_AgendaRecebimento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
