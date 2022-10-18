using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PalletDocumentoMap : EntityTypeConfiguration<PalletDocumento>
    {
        public PalletDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPalletDocumento);

            // Properties
            this.Property(t => t.IdPalletDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PalletDocumento");
            this.Property(t => t.IdPalletDocumento).HasColumnName("IdPalletDocumento");
            this.Property(t => t.IdPallet).HasColumnName("IdPallet");
            this.Property(t => t.IdUADocumento).HasColumnName("IdUADocumento");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdRomaneio).HasColumnName("IdRomaneio");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Inicio).HasColumnName("Inicio");
            this.Property(t => t.Final).HasColumnName("Final");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.PalletDocumentoes)
                .HasForeignKey(d => d.IdDocumento);
            this.HasRequired(t => t.Pallet)
                .WithMany(t => t.PalletDocumentoes)
                .HasForeignKey(d => d.IdPallet);
            this.HasRequired(t => t.UnidadeDeArmazenagem)
                .WithMany(t => t.PalletDocumentoes)
                .HasForeignKey(d => d.IdUADocumento);
            this.HasOptional(t => t.Romaneio)
                .WithMany(t => t.PalletDocumentoes)
                .HasForeignKey(d => d.IdRomaneio);

        }
    }
}
