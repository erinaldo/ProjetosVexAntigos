using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TituloDocumentoMap : EntityTypeConfiguration<TituloDocumento>
    {
        public TituloDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTituloDocumento);

            // Properties
            this.Property(t => t.IdTituloDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("TituloDocumento");
            this.Property(t => t.IdTituloDocumento).HasColumnName("IdTituloDocumento");
            this.Property(t => t.IdTitulo).HasColumnName("IdTitulo");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.DataProtocolo).HasColumnName("DataProtocolo");
            this.Property(t => t.IdDt).HasColumnName("IdDt");
            this.Property(t => t.IdRpci).HasColumnName("IdRpci");
            this.Property(t => t.IdContaDespesa).HasColumnName("IdContaDespesa");
            this.Property(t => t.IdContrato).HasColumnName("IdContrato");

            // Relationships
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.TituloDocumentoes)
                .HasForeignKey(d => d.IdDocumento);
            this.HasOptional(t => t.DT)
                .WithMany(t => t.TituloDocumentoes)
                .HasForeignKey(d => d.IdDt);
            this.HasOptional(t => t.RPCI)
                .WithMany(t => t.TituloDocumentoes)
                .HasForeignKey(d => d.IdRpci);
            this.HasRequired(t => t.Titulo)
                .WithMany(t => t.TituloDocumentoes)
                .HasForeignKey(d => d.IdTitulo);

        }
    }
}
