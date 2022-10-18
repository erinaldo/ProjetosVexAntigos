using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoOrcamentoMap : EntityTypeConfiguration<ZID_DocumentoOrcamento>
    {
        public ZID_DocumentoOrcamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoOrcamento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
