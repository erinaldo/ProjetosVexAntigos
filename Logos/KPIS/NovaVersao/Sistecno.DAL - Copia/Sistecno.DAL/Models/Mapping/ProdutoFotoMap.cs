using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProdutoFotoMap : EntityTypeConfiguration<ProdutoFoto>
    {
        public ProdutoFotoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDProdutoFoto);

            // Properties
            this.Property(t => t.IDProdutoFoto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProdutoFoto");
            this.Property(t => t.IDProdutoFoto).HasColumnName("IDProdutoFoto");
            this.Property(t => t.IDProduto).HasColumnName("IDProduto");
            this.Property(t => t.Foto).HasColumnName("Foto");

            // Relationships
            this.HasRequired(t => t.Produto)
                .WithMany(t => t.ProdutoFotoes)
                .HasForeignKey(d => d.IDProduto);

        }
    }
}
