using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DepositoMap : EntityTypeConfiguration<Deposito>
    {
        public DepositoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDeposito);

            // Properties
            this.Property(t => t.IDDeposito)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Endereco)
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Deposito");
            this.Property(t => t.IDDeposito).HasColumnName("IDDeposito");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Endereco).HasColumnName("Endereco");
            this.Property(t => t.AreaTotal).HasColumnName("AreaTotal");
            this.Property(t => t.AreaUtil).HasColumnName("AreaUtil");
            this.Property(t => t.Largura).HasColumnName("Largura");
            this.Property(t => t.Profundidade).HasColumnName("Profundidade");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Depositoes)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
