using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoMigradoMap : EntityTypeConfiguration<DocumentoMigrado>
    {
        public DocumentoMigradoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoMigrado);

            // Properties
            this.Property(t => t.IdDocumentoMigrado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Conhecimento)
                .HasMaxLength(12);

            // Table & Column Mappings
            this.ToTable("DocumentoMigrado");
            this.Property(t => t.IdDocumentoMigrado).HasColumnName("IdDocumentoMigrado");
            this.Property(t => t.Conhecimento).HasColumnName("Conhecimento");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.NotaFiscal).HasColumnName("NotaFiscal");
        }
    }
}
