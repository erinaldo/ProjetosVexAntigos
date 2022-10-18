using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTTipoMap : EntityTypeConfiguration<DTTipo>
    {
        public DTTipoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDTTipo);

            // Properties
            this.Property(t => t.IDDTTipo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DTTipo");
            this.Property(t => t.IDDTTipo).HasColumnName("IDDTTipo");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
