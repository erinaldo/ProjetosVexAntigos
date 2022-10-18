using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class GrandeUsuarioEnderecoMap : EntityTypeConfiguration<GrandeUsuarioEndereco>
    {
        public GrandeUsuarioEnderecoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDGrandeUsuario);

            // Properties
            this.Property(t => t.IDGrandeUsuario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Preposicao)
                .HasMaxLength(10);

            this.Property(t => t.Titulo)
                .HasMaxLength(80);

            this.Property(t => t.Rua)
                .HasMaxLength(80);

            this.Property(t => t.NumeroDoLote)
                .HasMaxLength(11);

            this.Property(t => t.Complemento1)
                .HasMaxLength(40);

            this.Property(t => t.Numero1)
                .HasMaxLength(11);

            this.Property(t => t.Complemento2)
                .HasMaxLength(40);

            this.Property(t => t.Numero2)
                .HasMaxLength(11);

            this.Property(t => t.TipoUnidadeOcupacao)
                .HasMaxLength(40);

            this.Property(t => t.NumeroUnidadeOcupacao)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("GrandeUsuarioEndereco");
            this.Property(t => t.IDGrandeUsuario).HasColumnName("IDGrandeUsuario");
            this.Property(t => t.IDRua).HasColumnName("IDRua");
            this.Property(t => t.Preposicao).HasColumnName("Preposicao");
            this.Property(t => t.Titulo).HasColumnName("Titulo");
            this.Property(t => t.Rua).HasColumnName("Rua");
            this.Property(t => t.NumeroDoLote).HasColumnName("NumeroDoLote");
            this.Property(t => t.Complemento1).HasColumnName("Complemento1");
            this.Property(t => t.Numero1).HasColumnName("Numero1");
            this.Property(t => t.Complemento2).HasColumnName("Complemento2");
            this.Property(t => t.Numero2).HasColumnName("Numero2");
            this.Property(t => t.TipoUnidadeOcupacao).HasColumnName("TipoUnidadeOcupacao");
            this.Property(t => t.NumeroUnidadeOcupacao).HasColumnName("NumeroUnidadeOcupacao");

            // Relationships
            this.HasRequired(t => t.GrandeUsuario)
                .WithOptional(t => t.GrandeUsuarioEndereco);

        }
    }
}
