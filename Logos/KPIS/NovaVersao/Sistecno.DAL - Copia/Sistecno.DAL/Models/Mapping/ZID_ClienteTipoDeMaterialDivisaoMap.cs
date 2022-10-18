using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ClienteTipoDeMaterialDivisaoMap : EntityTypeConfiguration<ZID_ClienteTipoDeMaterialDivisao>
    {
        public ZID_ClienteTipoDeMaterialDivisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ClienteTipoDeMaterialDivisao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
