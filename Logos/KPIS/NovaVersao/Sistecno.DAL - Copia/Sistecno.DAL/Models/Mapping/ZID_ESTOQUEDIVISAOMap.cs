using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ESTOQUEDIVISAOMap : EntityTypeConfiguration<ZID_ESTOQUEDIVISAO>
    {
        public ZID_ESTOQUEDIVISAOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ESTOQUEDIVISAO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
