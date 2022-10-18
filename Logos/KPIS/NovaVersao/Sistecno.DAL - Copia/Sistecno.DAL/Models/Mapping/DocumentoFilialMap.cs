using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoFilialMap : EntityTypeConfiguration<DocumentoFilial>
    {
        public DocumentoFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoFilial);

            // Properties
            this.Property(t => t.IDDocumentoFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Situacao)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("DocumentoFilial");
            this.Property(t => t.IDDocumentoFilial).HasColumnName("IDDocumentoFilial");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDRegiaoItem).HasColumnName("IDRegiaoItem");
            this.Property(t => t.IdRegiaoItemFilial).HasColumnName("IdRegiaoItemFilial");
            this.Property(t => t.IdRegiaoItemCliente).HasColumnName("IdRegiaoItemCliente");
            this.Property(t => t.IdRegiaoItemTransportador).HasColumnName("IdRegiaoItemTransportador");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IdSetor).HasColumnName("IdSetor");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoFilials)
                .HasForeignKey(d => d.IDDocumento);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.DocumentoFilials)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
