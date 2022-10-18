using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoEmbalagemMap : EntityTypeConfiguration<ZID_DocumentoEmbalagem>
    {
        public ZID_DocumentoEmbalagemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoEmbalagem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
