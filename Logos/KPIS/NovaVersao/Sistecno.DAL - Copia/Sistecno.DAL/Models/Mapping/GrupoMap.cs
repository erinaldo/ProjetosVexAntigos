using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class GrupoMap : EntityTypeConfiguration<Grupo>
    {
        public GrupoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDGrupo);

            // Properties
            this.Property(t => t.IDGrupo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Ativo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Grupo");
            this.Property(t => t.IDGrupo).HasColumnName("IDGrupo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
        }
    }
}
