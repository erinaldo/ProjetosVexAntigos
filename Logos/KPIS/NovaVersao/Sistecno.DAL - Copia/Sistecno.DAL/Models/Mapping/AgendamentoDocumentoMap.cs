using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AgendamentoDocumentoMap : EntityTypeConfiguration<AgendamentoDocumento>
    {
        public AgendamentoDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAgendamentoDocumento);

            // Properties
            this.Property(t => t.IdAgendamentoDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusDoDocumento)
                .HasMaxLength(150);

            this.Property(t => t.SituacaoDoDocumento)
                .HasMaxLength(150);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AgendamentoDocumento");
            this.Property(t => t.IdAgendamentoDocumento).HasColumnName("IdAgendamentoDocumento");
            this.Property(t => t.IdAgendamento).HasColumnName("IdAgendamento");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.StatusDoDocumento).HasColumnName("StatusDoDocumento");
            this.Property(t => t.SituacaoDoDocumento).HasColumnName("SituacaoDoDocumento");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
            this.Property(t => t.DataGeracaoDoArquivo).HasColumnName("DataGeracaoDoArquivo");

            // Relationships
            this.HasRequired(t => t.Agendamento)
                .WithMany(t => t.AgendamentoDocumentoes)
                .HasForeignKey(d => d.IdAgendamento);
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.AgendamentoDocumentoes)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
