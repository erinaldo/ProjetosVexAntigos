using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProdutosVivoMap : EntityTypeConfiguration<ProdutosVivo>
    {
        public ProdutosVivoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SALDODIVISAODISPONIVEL, t.RODOVIARIO, t.AERIO });

            // Properties
            this.Property(t => t.CODIGODOCLIENTE)
                .HasMaxLength(20);

            this.Property(t => t.Codigo)
                .HasMaxLength(20);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            this.Property(t => t.PERECIVEL)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.VALIDADE)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.SALDODIVISAODISPONIVEL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Conteudo)
                .HasMaxLength(60);

            this.Property(t => t.UnidadeDeMedida)
                .HasMaxLength(5);

            this.Property(t => t.RODOVIARIO)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.AERIO)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("ProdutosVivo");
            this.Property(t => t.linha).HasColumnName("linha");
            this.Property(t => t.FOTO).HasColumnName("FOTO");
            this.Property(t => t.CODIGODOCLIENTE).HasColumnName("CODIGODOCLIENTE");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.PERECIVEL).HasColumnName("PERECIVEL");
            this.Property(t => t.VALIDADE).HasColumnName("VALIDADE");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");
            this.Property(t => t.SALDODIVISAODISPONIVEL).HasColumnName("SALDODIVISAODISPONIVEL");
            this.Property(t => t.Conteudo).HasColumnName("Conteudo");
            this.Property(t => t.UnidadeDeMedida).HasColumnName("UnidadeDeMedida");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.Largura).HasColumnName("Largura");
            this.Property(t => t.Comprimento).HasColumnName("Comprimento");
            this.Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            this.Property(t => t.PesoLiquido).HasColumnName("PesoLiquido");
            this.Property(t => t.RODOVIARIO).HasColumnName("RODOVIARIO");
            this.Property(t => t.AERIO).HasColumnName("AERIO");
            this.Property(t => t.DataLimiteDeUso).HasColumnName("DataLimiteDeUso");
        }
    }
}
