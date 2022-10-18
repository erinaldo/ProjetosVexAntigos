using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ResultadoNFENFMap : EntityTypeConfiguration<ResultadoNFENF>
    {
        public ResultadoNFENFMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDEntrada, t.IDSaida, t.Cidade, t.UF, t.Estado, t.NFEIDCliente, t.ProdutoCodigo, t.UFSaida, t.NFEIDFilial, t.NFSAtivo, t.NFEAtivo });

            // Properties
            this.Property(t => t.IDEntrada)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDSaida)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.remetente)
                .HasMaxLength(60);

            this.Property(t => t.destinatario)
                .HasMaxLength(60);

            this.Property(t => t.Cidade)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.UF)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Estado)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.NFEIDCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProdutoCodigo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProdutoDescricao)
                .HasMaxLength(60);

            this.Property(t => t.UFSaida)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.NFEIDFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NFSEntradaSaida)
                .HasMaxLength(10);

            this.Property(t => t.NFSAtivo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NFEEntradaSaida)
                .HasMaxLength(10);

            this.Property(t => t.NFEAtivo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ResultadoNFENFS");
            this.Property(t => t.IDEntrada).HasColumnName("IDEntrada");
            this.Property(t => t.NFEntrada).HasColumnName("NFEntrada");
            this.Property(t => t.NFEDataDeEmissao).HasColumnName("NFEDataDeEmissao");
            this.Property(t => t.NFEValorDoICMS).HasColumnName("NFEValorDoICMS");
            this.Property(t => t.NFSaida).HasColumnName("NFSaida");
            this.Property(t => t.IDSaida).HasColumnName("IDSaida");
            this.Property(t => t.NFSDataDeEmissao).HasColumnName("NFSDataDeEmissao");
            this.Property(t => t.remetente).HasColumnName("remetente");
            this.Property(t => t.destinatario).HasColumnName("destinatario");
            this.Property(t => t.Cidade).HasColumnName("Cidade");
            this.Property(t => t.UF).HasColumnName("UF");
            this.Property(t => t.Estado).HasColumnName("Estado");
            this.Property(t => t.NFEIDCliente).HasColumnName("NFEIDCliente");
            this.Property(t => t.Pedido).HasColumnName("Pedido");
            this.Property(t => t.ProdutoCodigo).HasColumnName("ProdutoCodigo");
            this.Property(t => t.ProdutoDescricao).HasColumnName("ProdutoDescricao");
            this.Property(t => t.NFSDataDoMovimento).HasColumnName("NFSDataDoMovimento");
            this.Property(t => t.Aliquota).HasColumnName("Aliquota");
            this.Property(t => t.UFSaida).HasColumnName("UFSaida");
            this.Property(t => t.NFSValorDoICMS).HasColumnName("NFSValorDoICMS");
            this.Property(t => t.NFSValorDaNota).HasColumnName("NFSValorDaNota");
            this.Property(t => t.NFSPesoBruto).HasColumnName("NFSPesoBruto");
            this.Property(t => t.NFSMetragemCubica).HasColumnName("NFSMetragemCubica");
            this.Property(t => t.NFSVolumes).HasColumnName("NFSVolumes");
            this.Property(t => t.NFEIDFilial).HasColumnName("NFEIDFilial");
            this.Property(t => t.NFSEntradaSaida).HasColumnName("NFSEntradaSaida");
            this.Property(t => t.NFSAtivo).HasColumnName("NFSAtivo");
            this.Property(t => t.NFEEntradaSaida).HasColumnName("NFEEntradaSaida");
            this.Property(t => t.NFEAtivo).HasColumnName("NFEAtivo");
        }
    }
}
