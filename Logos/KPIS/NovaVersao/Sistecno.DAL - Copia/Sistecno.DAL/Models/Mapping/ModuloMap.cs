using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModuloMap : EntityTypeConfiguration<Modulo>
    {
        public ModuloMap()
        {
            // Primary Key
            this.HasKey(t => t.IDModulo);

            // Properties
            this.Property(t => t.IDModulo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Tipo)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Modulo");
            this.Property(t => t.IDModulo).HasColumnName("IDModulo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
        }
    }
}
