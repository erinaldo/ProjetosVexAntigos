using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OcorrenciaAndamentoMap : EntityTypeConfiguration<OcorrenciaAndamento>
    {
        public OcorrenciaAndamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdOcorrenciaAndamento);

            // Properties
            this.Property(t => t.IdOcorrenciaAndamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("OcorrenciaAndamento");
            this.Property(t => t.IdOcorrenciaAndamento).HasColumnName("IdOcorrenciaAndamento");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
