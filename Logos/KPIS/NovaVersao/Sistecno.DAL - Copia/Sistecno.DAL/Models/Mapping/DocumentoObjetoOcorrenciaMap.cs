using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoObjetoOcorrenciaMap : EntityTypeConfiguration<DocumentoObjetoOcorrencia>
    {
        public DocumentoObjetoOcorrenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoObjetoOcorrencia);

            // Properties
            this.Property(t => t.IdDocumentoObjetoOcorrencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Local)
                .HasMaxLength(50);

            this.Property(t => t.Uf)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Cidade)
                .HasMaxLength(50);

            this.Property(t => t.Hora)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.Destino)
                .HasMaxLength(50);

            this.Property(t => t.UfDestino)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.CidadeDestino)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentoObjetoOcorrencia");
            this.Property(t => t.IdDocumentoObjetoOcorrencia).HasColumnName("IdDocumentoObjetoOcorrencia");
            this.Property(t => t.IdDocumentoObjeto).HasColumnName("IdDocumentoObjeto");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.DataHoraAtualizacao).HasColumnName("DataHoraAtualizacao");
            this.Property(t => t.Local).HasColumnName("Local");
            this.Property(t => t.Uf).HasColumnName("Uf");
            this.Property(t => t.Cidade).HasColumnName("Cidade");
            this.Property(t => t.Hora).HasColumnName("Hora");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Destino).HasColumnName("Destino");
            this.Property(t => t.UfDestino).HasColumnName("UfDestino");
            this.Property(t => t.CidadeDestino).HasColumnName("CidadeDestino");

            // Relationships
            this.HasRequired(t => t.DocumentoObjeto)
                .WithMany(t => t.DocumentoObjetoOcorrencias)
                .HasForeignKey(d => d.IdDocumentoObjeto);

        }
    }
}
