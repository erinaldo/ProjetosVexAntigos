using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MovimentacaoMap : EntityTypeConfiguration<Movimentacao>
    {
        public MovimentacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMovimentacao);

            // Properties
            this.Property(t => t.IDMovimentacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Modo)
                .HasMaxLength(10);

            this.Property(t => t.EstoqueProcessado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Tipo)
                .HasMaxLength(15);

            this.Property(t => t.Motivo)
                .HasMaxLength(15);

            this.Property(t => t.Observacao)
                .HasMaxLength(500);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.MapaGerado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PedidoNotaFiscal)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Movimentacao");
            this.Property(t => t.IDMovimentacao).HasColumnName("IDMovimentacao");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Modo).HasColumnName("Modo");
            this.Property(t => t.EstoqueProcessado).HasColumnName("EstoqueProcessado");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Motivo).HasColumnName("Motivo");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.MapaGerado).HasColumnName("MapaGerado");
            this.Property(t => t.PedidoNotaFiscal).HasColumnName("PedidoNotaFiscal");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Movimentacaos)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Movimentacaos)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
