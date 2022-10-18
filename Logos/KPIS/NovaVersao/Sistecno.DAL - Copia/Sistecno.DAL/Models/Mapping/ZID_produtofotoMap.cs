using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_produtofotoMap : EntityTypeConfiguration<ZID_produtofoto>
    {
        public ZID_produtofotoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_produtofoto");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
