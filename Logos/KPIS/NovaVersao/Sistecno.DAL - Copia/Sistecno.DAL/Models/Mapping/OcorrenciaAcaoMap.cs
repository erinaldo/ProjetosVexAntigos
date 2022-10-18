using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OcorrenciaAcaoMap : EntityTypeConfiguration<OcorrenciaAcao>
    {
        public OcorrenciaAcaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDOcorrenciaAcao);

            // Properties
            this.Property(t => t.IDOcorrenciaAcao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Acao)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("OcorrenciaAcao");
            this.Property(t => t.IDOcorrenciaAcao).HasColumnName("IDOcorrenciaAcao");
            this.Property(t => t.Acao).HasColumnName("Acao");
        }
    }
}
