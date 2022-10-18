using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EdiPlanilhaDetalheMap : EntityTypeConfiguration<ZID_EdiPlanilhaDetalhe>
    {
        public ZID_EdiPlanilhaDetalheMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EdiPlanilhaDetalhe");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
