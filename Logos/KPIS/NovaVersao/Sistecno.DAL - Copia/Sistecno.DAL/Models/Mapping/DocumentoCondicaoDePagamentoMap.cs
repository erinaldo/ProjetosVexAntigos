using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoCondicaoDePagamentoMap : EntityTypeConfiguration<DocumentoCondicaoDePagamento>
    {
        public DocumentoCondicaoDePagamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoCondicaoDePagamento);

            // Properties
            this.Property(t => t.IdDocumentoCondicaoDePagamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoCondicaoDePagamento");
            this.Property(t => t.IdDocumentoCondicaoDePagamento).HasColumnName("IdDocumentoCondicaoDePagamento");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Vencimento).HasColumnName("Vencimento");
            this.Property(t => t.Valor).HasColumnName("Valor");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoCondicaoDePagamentoes)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
