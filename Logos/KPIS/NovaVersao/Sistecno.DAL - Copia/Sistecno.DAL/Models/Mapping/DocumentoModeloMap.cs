using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoModeloMap : EntityTypeConfiguration<DocumentoModelo>
    {
        public DocumentoModeloMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoModelo);

            // Properties
            this.Property(t => t.IdDocumentoModelo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Modelo)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("DocumentoModelo");
            this.Property(t => t.IdDocumentoModelo).HasColumnName("IdDocumentoModelo");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Modelo).HasColumnName("Modelo");
        }
    }
}
