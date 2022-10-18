using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ReposicaoRogeMap : EntityTypeConfiguration<ZID_ReposicaoRoge>
    {
        public ZID_ReposicaoRogeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ReposicaoRoge");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
