using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoEletronicoMap : EntityTypeConfiguration<ZID_DocumentoEletronico>
    {
        public ZID_DocumentoEletronicoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoEletronico");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
