using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UnidadeDeNegocioMap : EntityTypeConfiguration<UnidadeDeNegocio>
    {
        public UnidadeDeNegocioMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUnidadeDeNegocios);

            // Properties
            this.Property(t => t.IdUnidadeDeNegocios)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UnidadeDeNegocios)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UnidadeDeNegocios");
            this.Property(t => t.IdUnidadeDeNegocios).HasColumnName("IdUnidadeDeNegocios");
            this.Property(t => t.UnidadeDeNegocios).HasColumnName("UnidadeDeNegocios");
        }
    }
}
