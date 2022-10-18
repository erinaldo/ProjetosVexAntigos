using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class GrandeUsuarioMap : EntityTypeConfiguration<GrandeUsuario>
    {
        public GrandeUsuarioMap()
        {
            // Primary Key
            this.HasKey(t => t.IDGrandeUsuario);

            // Properties
            this.Property(t => t.IDGrandeUsuario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(80);

            this.Property(t => t.Cep)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.Abreviatura)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("GrandeUsuario");
            this.Property(t => t.IDGrandeUsuario).HasColumnName("IDGrandeUsuario");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Cep).HasColumnName("Cep");
            this.Property(t => t.Abreviatura).HasColumnName("Abreviatura");
        }
    }
}
