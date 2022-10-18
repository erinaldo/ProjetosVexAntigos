using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioDocumentoMap : EntityTypeConfiguration<RomaneioDocumento>
    {
        public RomaneioDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRomaneioDocumento);

            // Properties
            this.Property(t => t.IDRomaneioDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Status)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("RomaneioDocumento");
            this.Property(t => t.IDRomaneioDocumento).HasColumnName("IDRomaneioDocumento");
            this.Property(t => t.IDRomaneio).HasColumnName("IDRomaneio");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.Volumes).HasColumnName("Volumes");
            this.Property(t => t.Peso).HasColumnName("Peso");
            this.Property(t => t.PesoCubado).HasColumnName("PesoCubado");
            this.Property(t => t.Cubagem).HasColumnName("Cubagem");
            this.Property(t => t.ValorDoDocumento).HasColumnName("ValorDoDocumento");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IdDocumentoVerificado).HasColumnName("IdDocumentoVerificado");
            this.Property(t => t.ValorDoFrete).HasColumnName("ValorDoFrete");
            this.Property(t => t.ValorDoFreteIcmsIss).HasColumnName("ValorDoFreteIcmsIss");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.RomaneioDocumentoes)
                .HasForeignKey(d => d.IDDocumento);
            this.HasOptional(t => t.Documento1)
                .WithMany(t => t.RomaneioDocumentoes1)
                .HasForeignKey(d => d.IdDocumentoVerificado);
            this.HasRequired(t => t.Romaneio)
                .WithMany(t => t.RomaneioDocumentoes)
                .HasForeignKey(d => d.IDRomaneio);

        }
    }
}
