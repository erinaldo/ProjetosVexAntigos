using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PlantaMap : EntityTypeConfiguration<Planta>
    {
        public PlantaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDPlanta);

            // Properties
            this.Property(t => t.IDPlanta)
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
            this.ToTable("Planta");
            this.Property(t => t.IDPlanta).HasColumnName("IDPlanta");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Endereco).HasColumnName("Endereco");
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
            this.Property(t => t.IDPLANTALEIAUTE).HasColumnName("IDPLANTALEIAUTE");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Plantas)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
