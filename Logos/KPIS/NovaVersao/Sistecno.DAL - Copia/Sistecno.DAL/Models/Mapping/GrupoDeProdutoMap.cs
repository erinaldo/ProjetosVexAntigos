using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class GrupoDeProdutoMap : EntityTypeConfiguration<GrupoDeProduto>
    {
        public GrupoDeProdutoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDGrupoDeProduto);

            // Properties
            this.Property(t => t.IDGrupoDeProduto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CodigoNoCliente)
                .HasMaxLength(20);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("GrupoDeProduto");
            this.Property(t => t.IDGrupoDeProduto).HasColumnName("IDGrupoDeProduto");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.CodigoNoCliente).HasColumnName("CodigoNoCliente");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.ProximoNumero).HasColumnName("ProximoNumero");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IDParente).HasColumnName("IDParente");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.GrupoDeProdutoes)
                .HasForeignKey(d => d.IDCliente);
            this.HasOptional(t => t.GrupoDeProduto2)
                .WithMany(t => t.GrupoDeProduto1)
                .HasForeignKey(d => d.IDParente);

        }
    }
}
