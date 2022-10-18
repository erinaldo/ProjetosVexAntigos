using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoImpostoMap : EntityTypeConfiguration<ZID_DocumentoImposto>
    {
        public ZID_DocumentoImpostoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoImposto");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
