using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TituloItemMap : EntityTypeConfiguration<TituloItem>
    {
        public TituloItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTituloItem);

            // Properties
            this.Property(t => t.IdTituloItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("TituloItem");
            this.Property(t => t.IdTituloItem).HasColumnName("IdTituloItem");
            this.Property(t => t.IdTitulo).HasColumnName("IdTitulo");
            this.Property(t => t.Parcela).HasColumnName("Parcela");
            this.Property(t => t.Valor).HasColumnName("Valor");
        }
    }
}
