using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RetornoComproveiMap : EntityTypeConfiguration<RetornoComprovei>
    {
        public RetornoComproveiMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRetornoComprovei);

            // Properties
            this.Property(t => t.Chave)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Ocorrencia)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("RetornoComprovei");
            this.Property(t => t.IdRetornoComprovei).HasColumnName("IdRetornoComprovei");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.DataDaOcorrencia).HasColumnName("DataDaOcorrencia");
            this.Property(t => t.Ocorrencia).HasColumnName("Ocorrencia");
            this.Property(t => t.IdOcorrenciaComprovei).HasColumnName("IdOcorrenciaComprovei");
            this.Property(t => t.Foto).HasColumnName("Foto");
            this.Property(t => t.IdDocumentoOcorrencia).HasColumnName("IdDocumentoOcorrencia");
            this.Property(t => t.Processado).HasColumnName("Processado");
            this.Property(t => t.HorarioRecebimento).HasColumnName("HorarioRecebimento");
            this.Property(t => t.HorarioProcessamento).HasColumnName("HorarioProcessamento");
        }
    }
}
