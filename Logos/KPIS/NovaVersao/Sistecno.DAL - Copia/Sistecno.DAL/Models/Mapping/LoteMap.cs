using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LoteMap : EntityTypeConfiguration<Lote>
    {
        public LoteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDLote);

            // Properties
            this.Property(t => t.IDLote)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Referencia)
                .HasMaxLength(30);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Observacao)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Lote");
            this.Property(t => t.IDLote).HasColumnName("IDLote");
            this.Property(t => t.IDEstoque).HasColumnName("IDEstoque");
            this.Property(t => t.IDProdutoCliente).HasColumnName("IDProdutoCliente");
            this.Property(t => t.IDProdutoEmbalagem).HasColumnName("IDProdutoEmbalagem");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IdRomaneio).HasColumnName("IdRomaneio");
            this.Property(t => t.Validade).HasColumnName("Validade");
            this.Property(t => t.DataDeEntrada).HasColumnName("DataDeEntrada");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");
            this.Property(t => t.Referencia).HasColumnName("Referencia");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.SaldoNFEntrada).HasColumnName("SaldoNFEntrada");

            // Relationships
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.Lotes)
                .HasForeignKey(d => d.IDDocumento);
            this.HasOptional(t => t.Estoque)
                .WithMany(t => t.Lotes)
                .HasForeignKey(d => d.IDEstoque);
            this.HasOptional(t => t.ProdutoCliente)
                .WithMany(t => t.Lotes)
                .HasForeignKey(d => d.IDProdutoCliente);
            this.HasOptional(t => t.ProdutoEmbalagem)
                .WithMany(t => t.Lotes)
                .HasForeignKey(d => d.IDProdutoEmbalagem);
            this.HasOptional(t => t.Romaneio)
                .WithMany(t => t.Lotes)
                .HasForeignKey(d => d.IdRomaneio);

        }
    }
}
