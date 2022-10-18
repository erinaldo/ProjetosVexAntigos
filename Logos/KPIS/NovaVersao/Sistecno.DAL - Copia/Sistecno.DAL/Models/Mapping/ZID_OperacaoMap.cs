using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_OperacaoMap : EntityTypeConfiguration<ZID_Operacao>
    {
        public ZID_OperacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_Operacao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
