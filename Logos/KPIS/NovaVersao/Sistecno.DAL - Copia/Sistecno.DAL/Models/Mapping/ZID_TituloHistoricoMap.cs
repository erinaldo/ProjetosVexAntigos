using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TituloHistoricoMap : EntityTypeConfiguration<ZID_TituloHistorico>
    {
        public ZID_TituloHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TituloHistorico");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
