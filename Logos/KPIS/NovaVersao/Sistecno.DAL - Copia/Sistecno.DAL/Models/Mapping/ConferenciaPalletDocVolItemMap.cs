using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConferenciaPalletDocVolItemMap : EntityTypeConfiguration<ConferenciaPalletDocVolItem>
    {
        public ConferenciaPalletDocVolItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConferenciaPalletDocVolItem);

            // Properties
            this.Property(t => t.IdConferenciaPalletDocVolItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Lote)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("ConferenciaPalletDocVolItem");
            this.Property(t => t.IdConferenciaPalletDocVolItem).HasColumnName("IdConferenciaPalletDocVolItem");
            this.Property(t => t.IdConferenciaPalletDocVol).HasColumnName("IdConferenciaPalletDocVol");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.IdProduto).HasColumnName("IdProduto");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Fator).HasColumnName("Fator");
            this.Property(t => t.Validade).HasColumnName("Validade");
            this.Property(t => t.Lote).HasColumnName("Lote");

            // Relationships
            this.HasRequired(t => t.ConferenciaPalletDocVol)
                .WithMany(t => t.ConferenciaPalletDocVolItems)
                .HasForeignKey(d => d.IdConferenciaPalletDocVol);
            this.HasOptional(t => t.Produto)
                .WithMany(t => t.ConferenciaPalletDocVolItems)
                .HasForeignKey(d => d.IdProduto);
            this.HasRequired(t => t.ProdutoCliente)
                .WithMany(t => t.ConferenciaPalletDocVolItems)
                .HasForeignKey(d => d.IdProdutoCliente);

        }
    }
}
