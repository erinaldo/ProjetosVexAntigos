using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoObjetoMap : EntityTypeConfiguration<DocumentoObjeto>
    {
        public DocumentoObjetoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoObjeto);

            // Properties
            this.Property(t => t.IdDocumentoObjeto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Objeto)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Situacao)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentoObjeto");
            this.Property(t => t.IdDocumentoObjeto).HasColumnName("IdDocumentoObjeto");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Objeto).HasColumnName("Objeto");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.Volumes).HasColumnName("Volumes");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoObjetoes)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
