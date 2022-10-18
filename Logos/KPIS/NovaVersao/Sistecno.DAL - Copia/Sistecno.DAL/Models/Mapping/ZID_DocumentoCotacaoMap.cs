using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoCotacaoMap : EntityTypeConfiguration<ZID_DocumentoCotacao>
    {
        public ZID_DocumentoCotacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoCotacao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
