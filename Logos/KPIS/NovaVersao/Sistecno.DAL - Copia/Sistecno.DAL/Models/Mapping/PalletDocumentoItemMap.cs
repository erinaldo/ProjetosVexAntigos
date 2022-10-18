using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PalletDocumentoItemMap : EntityTypeConfiguration<PalletDocumentoItem>
    {
        public PalletDocumentoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPalletDocumentoItem);

            // Properties
            this.Property(t => t.IdPalletDocumentoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PalletDocumentoItem");
            this.Property(t => t.IdPalletDocumentoItem).HasColumnName("IdPalletDocumentoItem");
            this.Property(t => t.IdPalletDocumento).HasColumnName("IdPalletDocumento");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.IdProduto).HasColumnName("IdProduto");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.idProdutoEmbalagemPallet).HasColumnName("idProdutoEmbalagemPallet");

            // Relationships
            this.HasOptional(t => t.DocumentoItem)
                .WithMany(t => t.PalletDocumentoItems)
                .HasForeignKey(d => d.IdDocumentoItem);
            this.HasRequired(t => t.PalletDocumento)
                .WithMany(t => t.PalletDocumentoItems)
                .HasForeignKey(d => d.IdPalletDocumento);
            this.HasOptional(t => t.Produto)
                .WithMany(t => t.PalletDocumentoItems)
                .HasForeignKey(d => d.IdProduto);

        }
    }
}
