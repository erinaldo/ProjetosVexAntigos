using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class InventarioContagemProdutoMap : EntityTypeConfiguration<InventarioContagemProduto>
    {
        public InventarioContagemProdutoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdInventarioContagemProduto);

            // Properties
            this.Property(t => t.IdInventarioContagemProduto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Referencia)
                .HasMaxLength(30);

            this.Property(t => t.Observacao)
                .HasMaxLength(100);

            this.Property(t => t.Status)
                .HasMaxLength(100);

            this.Property(t => t.Situacao)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("InventarioContagemProduto");
            this.Property(t => t.IdInventarioContagemProduto).HasColumnName("IdInventarioContagemProduto");
            this.Property(t => t.IdInventarioContagem).HasColumnName("IdInventarioContagem");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.IdProdutoEmbalagem).HasColumnName("IdProdutoEmbalagem");
            this.Property(t => t.IdDepositoPlantaLocalizacao).HasColumnName("IdDepositoPlantaLocalizacao");
            this.Property(t => t.IdUnidadeDeArmazenagem).HasColumnName("IdUnidadeDeArmazenagem");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Validade).HasColumnName("Validade");
            this.Property(t => t.Referencia).HasColumnName("Referencia");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
            this.Property(t => t.Lastro).HasColumnName("Lastro");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.Excedente).HasColumnName("Excedente");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");

            // Relationships
            this.HasRequired(t => t.DepositoPlantaLocalizacao)
                .WithMany(t => t.InventarioContagemProdutoes)
                .HasForeignKey(d => d.IdDepositoPlantaLocalizacao);
            this.HasRequired(t => t.InventarioContagem)
                .WithMany(t => t.InventarioContagemProdutoes)
                .HasForeignKey(d => d.IdInventarioContagem);
            this.HasOptional(t => t.ProdutoCliente)
                .WithMany(t => t.InventarioContagemProdutoes)
                .HasForeignKey(d => d.IdProdutoCliente);
            this.HasOptional(t => t.ProdutoEmbalagem)
                .WithMany(t => t.InventarioContagemProdutoes)
                .HasForeignKey(d => d.IdProdutoEmbalagem);
            this.HasOptional(t => t.UnidadeDeArmazenagem)
                .WithMany(t => t.InventarioContagemProdutoes)
                .HasForeignKey(d => d.IdUnidadeDeArmazenagem);
            this.HasOptional(t => t.Usuario)
                .WithMany(t => t.InventarioContagemProdutoes)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
