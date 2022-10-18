using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TituloMap : EntityTypeConfiguration<Titulo>
    {
        public TituloMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTitulo);

            // Properties
            this.Property(t => t.IDTitulo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.NumeroOriginal)
                .HasMaxLength(20);

            this.Property(t => t.Serie)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.PagarReceber)
                .IsRequired()
                .HasMaxLength(7);

            this.Property(t => t.Impresso)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Origem)
                .HasMaxLength(50);

            this.Property(t => t.Previsao)
                .HasMaxLength(3);

            this.Property(t => t.TipoDeEntrada)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Titulo");
            this.Property(t => t.IDTitulo).HasColumnName("IDTitulo");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.IDFornecedor).HasColumnName("IDFornecedor");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDCondicaoDePagamento).HasColumnName("IDCondicaoDePagamento");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.NumeroOriginal).HasColumnName("NumeroOriginal");
            this.Property(t => t.Serie).HasColumnName("Serie");
            this.Property(t => t.PagarReceber).HasColumnName("PagarReceber");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Impresso).HasColumnName("Impresso");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.AliquotaMulta).HasColumnName("AliquotaMulta");
            this.Property(t => t.AliquotaJurosDiario).HasColumnName("AliquotaJurosDiario");
            this.Property(t => t.Desconto).HasColumnName("Desconto");
            this.Property(t => t.DataDeEmissao).HasColumnName("DataDeEmissao");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Origem).HasColumnName("Origem");
            this.Property(t => t.DataProtocolo).HasColumnName("DataProtocolo");
            this.Property(t => t.Previsao).HasColumnName("Previsao");
            this.Property(t => t.Parcelas).HasColumnName("Parcelas");
            this.Property(t => t.IdTipoDeTitulo).HasColumnName("IdTipoDeTitulo");
            this.Property(t => t.DataHoraInclusao).HasColumnName("DataHoraInclusao");
            this.Property(t => t.TipoDeEntrada).HasColumnName("TipoDeEntrada");
            this.Property(t => t.IdContaContabil).HasColumnName("IdContaContabil");

            // Relationships
            this.HasOptional(t => t.Cliente)
                .WithMany(t => t.Tituloes)
                .HasForeignKey(d => d.IDCliente);
            this.HasOptional(t => t.CondicaoDePagamento)
                .WithMany(t => t.Tituloes)
                .HasForeignKey(d => d.IDCondicaoDePagamento);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Tituloes)
                .HasForeignKey(d => d.IDFilial);
            this.HasOptional(t => t.Fornecedor)
                .WithMany(t => t.Tituloes)
                .HasForeignKey(d => d.IDFornecedor);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Tituloes)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
