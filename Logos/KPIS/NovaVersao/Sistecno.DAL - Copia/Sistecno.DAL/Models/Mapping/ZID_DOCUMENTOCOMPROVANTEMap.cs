using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DOCUMENTOCOMPROVANTEMap : EntityTypeConfiguration<ZID_DOCUMENTOCOMPROVANTE>
    {
        public ZID_DOCUMENTOCOMPROVANTEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DOCUMENTOCOMPROVANTE");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
