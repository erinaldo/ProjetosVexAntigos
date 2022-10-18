using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ParametroFluxoDeCaixaMap : EntityTypeConfiguration<ZID_ParametroFluxoDeCaixa>
    {
        public ZID_ParametroFluxoDeCaixaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ParametroFluxoDeCaixa");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
