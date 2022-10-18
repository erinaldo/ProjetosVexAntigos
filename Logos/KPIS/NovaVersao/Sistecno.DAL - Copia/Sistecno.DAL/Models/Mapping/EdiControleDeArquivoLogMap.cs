using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EdiControleDeArquivoLogMap : EntityTypeConfiguration<EdiControleDeArquivoLog>
    {
        public EdiControleDeArquivoLogMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdEdiControleDeArquivoLog, t.IdEdiControleDeArquivo });

            // Properties
            this.Property(t => t.IdEdiControleDeArquivoLog)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdEdiControleDeArquivo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeRegistro)
                .HasMaxLength(50);

            this.Property(t => t.Chave)
                .HasMaxLength(50);

            this.Property(t => t.Ocorrencia)
                .HasMaxLength(200);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("EdiControleDeArquivoLog");
            this.Property(t => t.IdEdiControleDeArquivoLog).HasColumnName("IdEdiControleDeArquivoLog");
            this.Property(t => t.IdEdiControleDeArquivo).HasColumnName("IdEdiControleDeArquivo");
            this.Property(t => t.TipoDeRegistro).HasColumnName("TipoDeRegistro");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.Linha).HasColumnName("Linha");
            this.Property(t => t.Ocorrencia).HasColumnName("Ocorrencia");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
        }
    }
}
