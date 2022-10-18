using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoRecebimentoMap : EntityTypeConfiguration<ZID_DocumentoRecebimento>
    {
        public ZID_DocumentoRecebimentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoRecebimento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
