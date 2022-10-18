using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoObservacaoMap : EntityTypeConfiguration<ZID_DocumentoObservacao>
    {
        public ZID_DocumentoObservacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoObservacao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
