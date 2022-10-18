using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDProduto);

            // Properties
            this.Property(t => t.IDProduto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodigoDeBarras)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Especie)
                .IsRequired()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("Produto");
            this.Property(t => t.IDProduto).HasColumnName("IDProduto");
            this.Property(t => t.CodigoDeBarras).HasColumnName("CodigoDeBarras");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.Largura).HasColumnName("Largura");
            this.Property(t => t.Comprimento).HasColumnName("Comprimento");
            this.Property(t => t.PesoLiquido).HasColumnName("PesoLiquido");
            this.Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            this.Property(t => t.Especie).HasColumnName("Especie");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
        }
    }
}