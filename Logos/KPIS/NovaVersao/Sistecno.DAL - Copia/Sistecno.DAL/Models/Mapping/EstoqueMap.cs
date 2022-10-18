using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EstoqueMap : EntityTypeConfiguration<Estoque>
    {
        public EstoqueMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEstoque);

            // Properties
            this.Property(t => t.IDEstoque)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Estoque");
            this.Property(t => t.IDEstoque).HasColumnName("IDEstoque");
            this.Property(t => t.IDProdutoCliente).HasColumnName("IDProdutoCliente");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.ValorEmEstoque).HasColumnName("ValorEmEstoque");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Estoques)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.ProdutoCliente)
                .WithMany(t => t.Estoques)
                .HasForeignKey(d => d.IDProdutoCliente);

        }
    }
}
