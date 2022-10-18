using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TituloDuplicataRemessaMap : EntityTypeConfiguration<TituloDuplicataRemessa>
    {
        public TituloDuplicataRemessaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTituloDuplicataRemessa);

            // Properties
            this.Property(t => t.IdTituloDuplicataRemessa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Situacao)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("TituloDuplicataRemessa");
            this.Property(t => t.IdTituloDuplicataRemessa).HasColumnName("IdTituloDuplicataRemessa");
            this.Property(t => t.IdTituloDuplicata).HasColumnName("IdTituloDuplicata");
            this.Property(t => t.IDBancoOcorrenciaRemessa).HasColumnName("IDBancoOcorrenciaRemessa");
            this.Property(t => t.DataMovimento).HasColumnName("DataMovimento");
            this.Property(t => t.DataDeEnvio).HasColumnName("DataDeEnvio");
            this.Property(t => t.IdUsuarioQueEnviou).HasColumnName("IdUsuarioQueEnviou");
            this.Property(t => t.Situacao).HasColumnName("Situacao");

            // Relationships
            this.HasRequired(t => t.BancoOcorrenciaRemessa)
                .WithMany(t => t.TituloDuplicataRemessas)
                .HasForeignKey(d => d.IDBancoOcorrenciaRemessa);
            this.HasRequired(t => t.TituloDuplicata)
                .WithMany(t => t.TituloDuplicataRemessas)
                .HasForeignKey(d => d.IdTituloDuplicata);

        }
    }
}
