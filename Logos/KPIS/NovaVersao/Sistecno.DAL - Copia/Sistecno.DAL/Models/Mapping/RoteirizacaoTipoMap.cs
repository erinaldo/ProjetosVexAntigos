using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RoteirizacaoTipoMap : EntityTypeConfiguration<RoteirizacaoTipo>
    {
        public RoteirizacaoTipoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRoteirizacaoTipo);

            // Properties
            this.Property(t => t.IDRoteirizacaoTipo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("RoteirizacaoTipo");
            this.Property(t => t.IDRoteirizacaoTipo).HasColumnName("IDRoteirizacaoTipo");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
