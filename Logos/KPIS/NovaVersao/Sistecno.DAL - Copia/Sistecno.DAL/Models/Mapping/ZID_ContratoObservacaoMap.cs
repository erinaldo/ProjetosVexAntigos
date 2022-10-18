using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ContratoObservacaoMap : EntityTypeConfiguration<ZID_ContratoObservacao>
    {
        public ZID_ContratoObservacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ContratoObservacao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
