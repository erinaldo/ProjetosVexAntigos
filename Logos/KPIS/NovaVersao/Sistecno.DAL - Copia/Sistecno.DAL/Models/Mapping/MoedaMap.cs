using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MoedaMap : EntityTypeConfiguration<Moeda>
    {
        public MoedaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMoeda);

            // Properties
            this.Property(t => t.IDMoeda)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Simbolo)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.SimboloMonetario)
                .HasMaxLength(5);

            this.Property(t => t.UtilizadaNasCotacoes)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Moeda");
            this.Property(t => t.IDMoeda).HasColumnName("IDMoeda");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Simbolo).HasColumnName("Simbolo");
            this.Property(t => t.SimboloMonetario).HasColumnName("SimboloMonetario");
            this.Property(t => t.IDPais).HasColumnName("IDPais");
            this.Property(t => t.UtilizadaNasCotacoes).HasColumnName("UtilizadaNasCotacoes");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");

            // Relationships
            this.HasOptional(t => t.Pai)
                .WithMany(t => t.Moedas)
                .HasForeignKey(d => d.IDPais);

        }
    }
}
