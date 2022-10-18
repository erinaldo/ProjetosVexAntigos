using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DepositoPlantaLeiauteMap : EntityTypeConfiguration<DepositoPlantaLeiaute>
    {
        public DepositoPlantaLeiauteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDepositoPlantaLeiaute);

            // Properties
            this.Property(t => t.IDDepositoPlantaLeiaute)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.TipoCliente)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Inicio)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Fim)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("DepositoPlantaLeiaute");
            this.Property(t => t.IDDepositoPlantaLeiaute).HasColumnName("IDDepositoPlantaLeiaute");
            this.Property(t => t.IDDepositoPlanta).HasColumnName("IDDepositoPlanta");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.TipoCliente).HasColumnName("TipoCliente");
            this.Property(t => t.Inicio).HasColumnName("Inicio");
            this.Property(t => t.Fim).HasColumnName("Fim");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.DepositoPlanta)
                .WithMany(t => t.DepositoPlantaLeiautes)
                .HasForeignKey(d => d.IDDepositoPlanta);

        }
    }
}
