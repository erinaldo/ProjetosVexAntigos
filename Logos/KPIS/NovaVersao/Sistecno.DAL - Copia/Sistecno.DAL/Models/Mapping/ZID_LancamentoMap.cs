using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_LancamentoMap : EntityTypeConfiguration<ZID_Lancamento>
    {
        public ZID_LancamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_Lancamento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
