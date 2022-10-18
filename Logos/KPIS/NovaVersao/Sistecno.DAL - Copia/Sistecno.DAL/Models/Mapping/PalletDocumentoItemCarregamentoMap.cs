using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PalletDocumentoItemCarregamentoMap : EntityTypeConfiguration<PalletDocumentoItemCarregamento>
    {
        public PalletDocumentoItemCarregamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPalletDocumentoItemCarregamento);

            // Properties
            this.Property(t => t.IdPalletDocumentoItemCarregamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PalletDocumentoItemCarregamento");
            this.Property(t => t.IdPalletDocumentoItemCarregamento).HasColumnName("IdPalletDocumentoItemCarregamento");
            this.Property(t => t.IdPalletDocumento).HasColumnName("IdPalletDocumento");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.IdProduto).HasColumnName("IdProduto");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
        }
    }
}
