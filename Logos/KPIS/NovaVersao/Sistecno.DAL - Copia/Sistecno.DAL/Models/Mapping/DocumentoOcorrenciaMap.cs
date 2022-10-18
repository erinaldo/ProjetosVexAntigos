using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoOcorrenciaMap : EntityTypeConfiguration<DocumentoOcorrencia>
    {
        public DocumentoOcorrenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoOcorrencia);

            // Properties
            this.Property(t => t.IDDocumentoOcorrencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(300);

            this.Property(t => t.Pessoa)
                .HasMaxLength(50);

            this.Property(t => t.CpfRg)
                .HasMaxLength(20);

            this.Property(t => t.Sistema)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Senha)
                .HasMaxLength(50);

            this.Property(t => t.ArquivoDeIntegracao)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentoOcorrencia");
            this.Property(t => t.IDDocumentoOcorrencia).HasColumnName("IDDocumentoOcorrencia");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDOcorrencia).HasColumnName("IDOcorrencia");
            this.Property(t => t.IDConhecimento).HasColumnName("IDConhecimento");
            this.Property(t => t.IDRomaneio).HasColumnName("IDRomaneio");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.DataOcorrencia).HasColumnName("DataOcorrencia");
            this.Property(t => t.DataLancamento).HasColumnName("DataLancamento");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Pessoa).HasColumnName("Pessoa");
            this.Property(t => t.CpfRg).HasColumnName("CpfRg");
            this.Property(t => t.Sistema).HasColumnName("Sistema");
            this.Property(t => t.Senha).HasColumnName("Senha");
            this.Property(t => t.IdOcorrenciaAndamento).HasColumnName("IdOcorrenciaAndamento");
            this.Property(t => t.ArquivoDeIntegracao).HasColumnName("ArquivoDeIntegracao");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.IdOcorrenciaComprovei).HasColumnName("IdOcorrenciaComprovei");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoOcorrencias)
                .HasForeignKey(d => d.IDDocumento);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.DocumentoOcorrencias)
                .HasForeignKey(d => d.IDFilial);
            this.HasOptional(t => t.Ocorrencia)
                .WithMany(t => t.DocumentoOcorrencias)
                .HasForeignKey(d => d.IDOcorrencia);
            this.HasOptional(t => t.Romaneio)
                .WithMany(t => t.DocumentoOcorrencias)
                .HasForeignKey(d => d.IDRomaneio);

        }
    }
}
