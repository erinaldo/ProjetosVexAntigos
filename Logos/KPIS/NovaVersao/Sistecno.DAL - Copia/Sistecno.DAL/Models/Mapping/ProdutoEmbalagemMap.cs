using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProdutoEmbalagemMap : EntityTypeConfiguration<ProdutoEmbalagem>
    {
        public ProdutoEmbalagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDProdutoEmbalagem);

            // Properties
            this.Property(t => t.IDProdutoEmbalagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Conteudo)
                .HasMaxLength(60);

            this.Property(t => t.UnidadeDeMedida)
                .HasMaxLength(5);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ArquivoQueAlterou)
                .HasMaxLength(50);

            this.Property(t => t.OperadorParaConversao)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DumEan)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ProdutoEmbalagem");
            this.Property(t => t.IDProdutoEmbalagem).HasColumnName("IDProdutoEmbalagem");
            this.Property(t => t.IDProdutoCliente).HasColumnName("IDProdutoCliente");
            this.Property(t => t.IDProduto).HasColumnName("IDProduto");
            this.Property(t => t.IDProdutoInterno).HasColumnName("IDProdutoInterno");
            this.Property(t => t.Conteudo).HasColumnName("Conteudo");
            this.Property(t => t.UnidadeDoCliente).HasColumnName("UnidadeDoCliente");
            this.Property(t => t.UnidadeLogistica).HasColumnName("UnidadeLogistica");
            this.Property(t => t.UnidadeDeMedida).HasColumnName("UnidadeDeMedida");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.UniFor).HasColumnName("UniFor");
            this.Property(t => t.UniCli).HasColumnName("UniCli");
            this.Property(t => t.DataUltimaAlteracao).HasColumnName("DataUltimaAlteracao");
            this.Property(t => t.ArquivoQueAlterou).HasColumnName("ArquivoQueAlterou");
            this.Property(t => t.OperadorParaConversao).HasColumnName("OperadorParaConversao");
            this.Property(t => t.DumEan).HasColumnName("DumEan");
            this.Property(t => t.DadosLogisticos).HasColumnName("DadosLogisticos");

            // Relationships
            this.HasRequired(t => t.Produto)
                .WithMany(t => t.ProdutoEmbalagems)
                .HasForeignKey(d => d.IDProduto);
            this.HasRequired(t => t.ProdutoCliente)
                .WithMany(t => t.ProdutoEmbalagems)
                .HasForeignKey(d => d.IDProdutoCliente);
            this.HasOptional(t => t.ProdutoEmbalagem2)
                .WithMany(t => t.ProdutoEmbalagem1)
                .HasForeignKey(d => d.IDProdutoInterno);

        }
    }
}
