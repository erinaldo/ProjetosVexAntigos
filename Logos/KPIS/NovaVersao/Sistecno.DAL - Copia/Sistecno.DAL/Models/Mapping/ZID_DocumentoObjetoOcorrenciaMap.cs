using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoObjetoOcorrenciaMap : EntityTypeConfiguration<ZID_DocumentoObjetoOcorrencia>
    {
        public ZID_DocumentoObjetoOcorrenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoObjetoOcorrencia");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
