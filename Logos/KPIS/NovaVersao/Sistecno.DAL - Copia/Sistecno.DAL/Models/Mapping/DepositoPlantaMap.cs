using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DepositoPlantaMap : EntityTypeConfiguration<DepositoPlanta>
    {
        public DepositoPlantaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDepositoPlanta);

            // Properties
            this.Property(t => t.IDDepositoPlanta)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("DepositoPlanta");
            this.Property(t => t.IDDepositoPlanta).HasColumnName("IDDepositoPlanta");
            this.Property(t => t.IDDeposito).HasColumnName("IDDeposito");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.AreaTotal).HasColumnName("AreaTotal");
            this.Property(t => t.AreaUtil).HasColumnName("AreaUtil");
            this.Property(t => t.Largura).HasColumnName("Largura");
            this.Property(t => t.Profundidade).HasColumnName("Profundidade");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.LocalLarguraPadrao).HasColumnName("LocalLarguraPadrao");
            this.Property(t => t.LocalProfundidadePadrao).HasColumnName("LocalProfundidadePadrao");
            this.Property(t => t.LocalAlturaPadrao).HasColumnName("LocalAlturaPadrao");
            this.Property(t => t.LocalCapacidadePadrao).HasColumnName("LocalCapacidadePadrao");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasRequired(t => t.Deposito)
                .WithMany(t => t.DepositoPlantas)
                .HasForeignKey(d => d.IDDeposito);

        }
    }
}
