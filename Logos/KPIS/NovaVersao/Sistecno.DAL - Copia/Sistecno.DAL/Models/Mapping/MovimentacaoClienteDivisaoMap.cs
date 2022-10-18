using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MovimentacaoClienteDivisaoMap : EntityTypeConfiguration<MovimentacaoClienteDivisao>
    {
        public MovimentacaoClienteDivisaoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Data, t.IdProdutoCliente, t.IdClienteDivisao, t.Saldo });

            // Properties
            this.Property(t => t.IdProdutoCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdClienteDivisao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Saldo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MovimentacaoClienteDivisao");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.IdClienteDivisao).HasColumnName("IdClienteDivisao");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
        }
    }
}
