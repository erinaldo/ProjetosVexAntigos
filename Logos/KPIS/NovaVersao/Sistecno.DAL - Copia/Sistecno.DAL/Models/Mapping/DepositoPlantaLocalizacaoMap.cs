using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DepositoPlantaLocalizacaoMap : EntityTypeConfiguration<DepositoPlantaLocalizacao>
    {
        public DepositoPlantaLocalizacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDepositoPlantaLocalizacao);

            // Properties
            this.Property(t => t.IDDepositoPlantaLocalizacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.MultiplosProdutos)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.TipoDeMovimentacao)
                .HasMaxLength(10);

            this.Property(t => t.Ativo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("DepositoPlantaLocalizacao");
            this.Property(t => t.IDDepositoPlantaLocalizacao).HasColumnName("IDDepositoPlantaLocalizacao");
            this.Property(t => t.IDDepositoPlanta).HasColumnName("IDDepositoPlanta");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Largura).HasColumnName("Largura");
            this.Property(t => t.Profundidade).HasColumnName("Profundidade");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.CapacidadeEmKg).HasColumnName("CapacidadeEmKg");
            this.Property(t => t.MultiplosProdutos).HasColumnName("MultiplosProdutos");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.TipoDeMovimentacao).HasColumnName("TipoDeMovimentacao");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.OrdemArmazenagem).HasColumnName("OrdemArmazenagem");

            // Relationships
            this.HasOptional(t => t.Cliente)
                .WithMany(t => t.DepositoPlantaLocalizacaos)
                .HasForeignKey(d => d.IDCliente);
            this.HasRequired(t => t.DepositoPlanta)
                .WithMany(t => t.DepositoPlantaLocalizacaos)
                .HasForeignKey(d => d.IDDepositoPlanta);

        }
    }
}
