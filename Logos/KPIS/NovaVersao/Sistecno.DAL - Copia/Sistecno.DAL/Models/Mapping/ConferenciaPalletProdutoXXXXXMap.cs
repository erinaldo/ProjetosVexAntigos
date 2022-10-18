using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConferenciaPalletProdutoXXXXXMap : EntityTypeConfiguration<ConferenciaPalletProdutoXXXXX>
    {
        public ConferenciaPalletProdutoXXXXXMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConferenciaPalletProduto);

            // Properties
            this.Property(t => t.IdConferenciaPalletProduto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("ConferenciaPalletProdutoXXXXX");
            this.Property(t => t.IdConferenciaPalletProduto).HasColumnName("IdConferenciaPalletProduto");
            this.Property(t => t.IdConferenciaPallet).HasColumnName("IdConferenciaPallet");
            this.Property(t => t.IdProdutoEmbalagem).HasColumnName("IdProdutoEmbalagem");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Tipo).HasColumnName("Tipo");

            // Relationships
            this.HasRequired(t => t.ConferenciaPallet)
                .WithMany(t => t.ConferenciaPalletProdutoXXXXXes)
                .HasForeignKey(d => d.IdConferenciaPallet);
            this.HasRequired(t => t.ProdutoEmbalagem)
                .WithMany(t => t.ConferenciaPalletProdutoXXXXXes)
                .HasForeignKey(d => d.IdProdutoEmbalagem);

        }
    }
}
