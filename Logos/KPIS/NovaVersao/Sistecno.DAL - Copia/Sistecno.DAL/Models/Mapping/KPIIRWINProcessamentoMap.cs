using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class KPIIRWINProcessamentoMap : EntityTypeConfiguration<KPIIRWINProcessamento>
    {
        public KPIIRWINProcessamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdKpiIrwinProcessamento);

            // Properties
            this.Property(t => t.IdKpiIrwinProcessamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("KPIIRWINProcessamento");
            this.Property(t => t.IdKpiIrwinProcessamento).HasColumnName("IdKpiIrwinProcessamento");
            this.Property(t => t.UltimoProcessamento).HasColumnName("UltimoProcessamento");
        }
    }
}
