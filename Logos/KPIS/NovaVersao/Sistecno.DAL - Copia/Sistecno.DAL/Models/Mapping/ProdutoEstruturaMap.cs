using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProdutoEstruturaMap : EntityTypeConfiguration<ProdutoEstrutura>
    {
        public ProdutoEstruturaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdProdutoEstrutura);

            // Properties
            this.Property(t => t.IdProdutoEstrutura)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProdutoEstrutura");
            this.Property(t => t.IdProdutoEstrutura).HasColumnName("IdProdutoEstrutura");
            this.Property(t => t.IDProdutoClientePai).HasColumnName("IDProdutoClientePai");
            this.Property(t => t.IdProdutoClienteFilho).HasColumnName("IdProdutoClienteFilho");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IdProdutoEmbalagem).HasColumnName("IdProdutoEmbalagem");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.Perda).HasColumnName("Perda");

            // Relationships
            this.HasRequired(t => t.ProdutoCliente)
                .WithMany(t => t.ProdutoEstruturas)
                .HasForeignKey(d => d.IdProdutoClienteFilho);
            this.HasRequired(t => t.ProdutoCliente1)
                .WithMany(t => t.ProdutoEstruturas1)
                .HasForeignKey(d => d.IDProdutoClientePai);

        }
    }
}
