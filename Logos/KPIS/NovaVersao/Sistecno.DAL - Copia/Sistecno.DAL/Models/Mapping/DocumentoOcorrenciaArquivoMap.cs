using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoOcorrenciaArquivoMap : EntityTypeConfiguration<DocumentoOcorrenciaArquivo>
    {
        public DocumentoOcorrenciaArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoOcorrenciaArquivo);

            // Properties
            this.Property(t => t.IDDocumentoOcorrenciaArquivo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoOcorrenciaArquivo");
            this.Property(t => t.IDDocumentoOcorrenciaArquivo).HasColumnName("IDDocumentoOcorrenciaArquivo");
            this.Property(t => t.IDDocumentoOcorrencia).HasColumnName("IDDocumentoOcorrencia");
            this.Property(t => t.Arquivo).HasColumnName("Arquivo");

            // Relationships
            this.HasRequired(t => t.DocumentoOcorrencia)
                .WithMany(t => t.DocumentoOcorrenciaArquivoes)
                .HasForeignKey(d => d.IDDocumentoOcorrencia);

        }
    }
}
