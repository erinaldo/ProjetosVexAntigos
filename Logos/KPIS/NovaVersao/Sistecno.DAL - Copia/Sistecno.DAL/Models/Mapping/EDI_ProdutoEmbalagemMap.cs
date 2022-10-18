using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_ProdutoEmbalagemMap : EntityTypeConfiguration<EDI_ProdutoEmbalagem>
    {
        public EDI_ProdutoEmbalagemMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EDI_Chave, t.IDProdutoEmbalagem, t.IDProdutoCliente, t.IDProduto, t.UnidadeDoCliente });

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.IDProdutoEmbalagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDProdutoCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDProduto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Conteudo)
                .HasMaxLength(60);

            this.Property(t => t.UnidadeDoCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UnidadeDeMedida)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("EDI_ProdutoEmbalagem");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDProdutoEmbalagem).HasColumnName("IDProdutoEmbalagem");
            this.Property(t => t.IDProdutoCliente).HasColumnName("IDProdutoCliente");
            this.Property(t => t.IDProduto).HasColumnName("IDProduto");
            this.Property(t => t.IDProdutoInterno).HasColumnName("IDProdutoInterno");
            this.Property(t => t.Conteudo).HasColumnName("Conteudo");
            this.Property(t => t.UnidadeDoCliente).HasColumnName("UnidadeDoCliente");
            this.Property(t => t.UnidadeLogistica).HasColumnName("UnidadeLogistica");
            this.Property(t => t.UnidadeDeMedida).HasColumnName("UnidadeDeMedida");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
        }
    }
}
