using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class movimentacaocliente_bkpMap : EntityTypeConfiguration<movimentacaocliente_bkp>
    {
        public movimentacaocliente_bkpMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdMovimentoCliente, t.Data, t.IdCliente, t.IdProdutoCliente, t.IdUnidadeDeArmazenagem, t.IdUnidadeDeArmazenagemLote, t.IdDepositoPlantaLocalizacao, t.SaldoDeEntrada, t.Saldo });

            // Properties
            this.Property(t => t.IdMovimentoCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdProdutoCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdUnidadeDeArmazenagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdUnidadeDeArmazenagemLote)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdDepositoPlantaLocalizacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SaldoDeEntrada)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Saldo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .HasMaxLength(20);

            this.Property(t => t.UsouSaldoMinimo)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("movimentacaocliente_bkp");
            this.Property(t => t.IdMovimentoCliente).HasColumnName("IdMovimentoCliente");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.IdUnidadeDeArmazenagem).HasColumnName("IdUnidadeDeArmazenagem");
            this.Property(t => t.IdUnidadeDeArmazenagemLote).HasColumnName("IdUnidadeDeArmazenagemLote");
            this.Property(t => t.IdDepositoPlantaLocalizacao).HasColumnName("IdDepositoPlantaLocalizacao");
            this.Property(t => t.SaldoDeEntrada).HasColumnName("SaldoDeEntrada");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.UsouSaldoMinimo).HasColumnName("UsouSaldoMinimo");
            this.Property(t => t.OcupaQtosPallets).HasColumnName("OcupaQtosPallets");
            this.Property(t => t.ValorEmEstoque).HasColumnName("ValorEmEstoque");
            this.Property(t => t.Valorunitario).HasColumnName("Valorunitario");
        }
    }
}
