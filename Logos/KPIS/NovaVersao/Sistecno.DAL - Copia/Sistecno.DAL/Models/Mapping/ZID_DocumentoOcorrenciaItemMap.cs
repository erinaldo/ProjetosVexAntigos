using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoOcorrenciaItemMap : EntityTypeConfiguration<ZID_DocumentoOcorrenciaItem>
    {
        public ZID_DocumentoOcorrenciaItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoOcorrenciaItem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
