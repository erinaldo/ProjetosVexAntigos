using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConferenciaPalletEntradaLoteMap : EntityTypeConfiguration<ConferenciaPalletEntradaLote>
    {
        public ConferenciaPalletEntradaLoteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConferenciaPalletEntradaLote);

            // Properties
            this.Property(t => t.IdConferenciaPalletEntradaLote)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Lote)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("ConferenciaPalletEntradaLote");
            this.Property(t => t.IdConferenciaPalletEntradaLote).HasColumnName("IdConferenciaPalletEntradaLote");
            this.Property(t => t.IdConferenciaPallet).HasColumnName("IdConferenciaPallet");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.IdProduto).HasColumnName("IdProduto");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Fator).HasColumnName("Fator");
            this.Property(t => t.Lote).HasColumnName("Lote");
            this.Property(t => t.Validade).HasColumnName("Validade");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");

            // Relationships
            this.HasRequired(t => t.ConferenciaPallet)
                .WithMany(t => t.ConferenciaPalletEntradaLotes)
                .HasForeignKey(d => d.IdConferenciaPallet);
            this.HasOptional(t => t.Produto)
                .WithMany(t => t.ConferenciaPalletEntradaLotes)
                .HasForeignKey(d => d.IdProduto);
            this.HasRequired(t => t.ProdutoCliente)
                .WithMany(t => t.ConferenciaPalletEntradaLotes)
                .HasForeignKey(d => d.IdProdutoCliente);

        }
    }
}
