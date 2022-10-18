using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_usuarioopcaoMap : EntityTypeConfiguration<ZID_usuarioopcao>
    {
        public ZID_usuarioopcaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_usuarioopcao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
