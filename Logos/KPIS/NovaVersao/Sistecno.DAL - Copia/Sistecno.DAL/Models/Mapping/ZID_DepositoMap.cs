using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DepositoMap : EntityTypeConfiguration<ZID_Deposito>
    {
        public ZID_DepositoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_Deposito");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
