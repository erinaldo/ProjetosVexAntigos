using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MovimentacaoRomaneioItemMap : EntityTypeConfiguration<MovimentacaoRomaneioItem>
    {
        public MovimentacaoRomaneioItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMovimentacaoRomaneioItem);

            // Properties
            this.Property(t => t.IDMovimentacaoRomaneioItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MovimentacaoRomaneioItem");
            this.Property(t => t.IDMovimentacaoRomaneioItem).HasColumnName("IDMovimentacaoRomaneioItem");
            this.Property(t => t.IDMovimentacaoRomaneio).HasColumnName("IDMovimentacaoRomaneio");
            this.Property(t => t.IDUnidadeDeArmazenagemLote).HasColumnName("IDUnidadeDeArmazenagemLote");
            this.Property(t => t.IDUnidadeDeArmazenagemDestino).HasColumnName("IDUnidadeDeArmazenagemDestino");
            this.Property(t => t.IDDepositoPlantaLocalizacaoDestino).HasColumnName("IDDepositoPlantaLocalizacaoDestino");
            this.Property(t => t.IDProdutoEmbalagem).HasColumnName("IDProdutoEmbalagem");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDDocumentoItem).HasColumnName("IDDocumentoItem");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.QuantidadeBaixada).HasColumnName("QuantidadeBaixada");
            this.Property(t => t.DataDeExecucao).HasColumnName("DataDeExecucao");
            this.Property(t => t.QuantidadeUnidadeEstoque).HasColumnName("QuantidadeUnidadeEstoque");
            this.Property(t => t.IDMapa).HasColumnName("IDMapa");

            // Relationships
            this.HasOptional(t => t.DepositoPlantaLocalizacao)
                .WithMany(t => t.MovimentacaoRomaneioItems)
                .HasForeignKey(d => d.IDDepositoPlantaLocalizacaoDestino);
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.MovimentacaoRomaneioItems)
                .HasForeignKey(d => d.IDDocumento);
            this.HasOptional(t => t.DocumentoItem)
                .WithMany(t => t.MovimentacaoRomaneioItems)
                .HasForeignKey(d => d.IDDocumentoItem);
            this.HasOptional(t => t.Mapa)
                .WithMany(t => t.MovimentacaoRomaneioItems)
                .HasForeignKey(d => d.IDMapa);
            this.HasRequired(t => t.MovimentacaoRomaneio)
                .WithMany(t => t.MovimentacaoRomaneioItems)
                .HasForeignKey(d => d.IDMovimentacaoRomaneio);
            this.HasOptional(t => t.ProdutoEmbalagem)
                .WithMany(t => t.MovimentacaoRomaneioItems)
                .HasForeignKey(d => d.IDProdutoEmbalagem);
            this.HasOptional(t => t.UnidadeDeArmazenagem)
                .WithMany(t => t.MovimentacaoRomaneioItems)
                .HasForeignKey(d => d.IDUnidadeDeArmazenagemDestino);
            this.HasRequired(t => t.UnidadeDeArmazenagemLote)
                .WithMany(t => t.MovimentacaoRomaneioItems)
                .HasForeignKey(d => d.IDUnidadeDeArmazenagemLote);
            this.HasOptional(t => t.Usuario)
                .WithMany(t => t.MovimentacaoRomaneioItems)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
