using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LancamentoOcorrenciaMap : EntityTypeConfiguration<LancamentoOcorrencia>
    {
        public LancamentoOcorrenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdLancamentoOcorrencia);

            // Properties
            this.Property(t => t.IdLancamentoOcorrencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Ocorrencia)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.ProgramaOrigem)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LancamentoOcorrencia");
            this.Property(t => t.IdLancamentoOcorrencia).HasColumnName("IdLancamentoOcorrencia");
            this.Property(t => t.IdLancamento).HasColumnName("IdLancamento");
            this.Property(t => t.Ocorrencia).HasColumnName("Ocorrencia");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.DataDaOcorrencia).HasColumnName("DataDaOcorrencia");
            this.Property(t => t.ProgramaOrigem).HasColumnName("ProgramaOrigem");
        }
    }
}
