using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TipoDeTituloMap : EntityTypeConfiguration<TipoDeTitulo>
    {
        public TipoDeTituloMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTipoDeTitulo);

            // Properties
            this.Property(t => t.IdTipoDeTitulo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            this.Property(t => t.Tipo)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TipoDeTitulo");
            this.Property(t => t.IdTipoDeTitulo).HasColumnName("IdTipoDeTitulo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
        }
    }
}
