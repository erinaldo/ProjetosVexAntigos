using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioDeTabelaDeFreteMap : EntityTypeConfiguration<UsuarioDeTabelaDeFrete>
    {
        public UsuarioDeTabelaDeFreteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuarioDeTabelaDeFrete);

            // Properties
            this.Property(t => t.IDUsuarioDeTabelaDeFrete)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeTabela)
                .HasMaxLength(12);

            this.Property(t => t.Descricao)
                .HasMaxLength(200);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("UsuarioDeTabelaDeFrete");
            this.Property(t => t.IDUsuarioDeTabelaDeFrete).HasColumnName("IDUsuarioDeTabelaDeFrete");
            this.Property(t => t.IDTabelaDeFrete).HasColumnName("IDTabelaDeFrete");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.IDTransportadora).HasColumnName("IDTransportadora");
            this.Property(t => t.IDVeiculoFilial).HasColumnName("IDVeiculoFilial");
            this.Property(t => t.IDFilialTabela).HasColumnName("IDFilialTabela");
            this.Property(t => t.IDVeiculoTipo).HasColumnName("IDVeiculoTipo");
            this.Property(t => t.TipoDeTabela).HasColumnName("TipoDeTabela");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.DataDesativacao).HasColumnName("DataDesativacao");

            // Relationships
            this.HasOptional(t => t.Cliente)
                .WithMany(t => t.UsuarioDeTabelaDeFretes)
                .HasForeignKey(d => d.IDCliente);
            this.HasOptional(t => t.Cliente1)
                .WithMany(t => t.UsuarioDeTabelaDeFretes1)
                .HasForeignKey(d => d.IDCliente);
            this.HasOptional(t => t.Filial)
                .WithMany(t => t.UsuarioDeTabelaDeFretes)
                .HasForeignKey(d => d.IDFilial);
            this.HasOptional(t => t.Filial1)
                .WithMany(t => t.UsuarioDeTabelaDeFretes1)
                .HasForeignKey(d => d.IDFilialTabela);
            this.HasRequired(t => t.TabelaDeFrete)
                .WithMany(t => t.UsuarioDeTabelaDeFretes)
                .HasForeignKey(d => d.IDTabelaDeFrete);
            this.HasOptional(t => t.Transportadora)
                .WithMany(t => t.UsuarioDeTabelaDeFretes)
                .HasForeignKey(d => d.IDTransportadora);
            this.HasOptional(t => t.VeiculoFilial)
                .WithMany(t => t.UsuarioDeTabelaDeFretes)
                .HasForeignKey(d => d.IDVeiculoFilial);
            this.HasOptional(t => t.VeiculoTipo)
                .WithMany(t => t.UsuarioDeTabelaDeFretes)
                .HasForeignKey(d => d.IDVeiculoTipo);

        }
    }
}
