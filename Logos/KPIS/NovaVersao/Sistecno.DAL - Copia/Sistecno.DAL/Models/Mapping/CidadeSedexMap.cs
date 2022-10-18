using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CidadeSedexMap : EntityTypeConfiguration<CidadeSedex>
    {
        public CidadeSedexMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCidadeSedex);

            // Properties
            this.Property(t => t.IdCidadeSedex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CidadeSedex");
            this.Property(t => t.IdCidadeSedex).HasColumnName("IdCidadeSedex");
            this.Property(t => t.IdModal).HasColumnName("IdModal");
            this.Property(t => t.IdCidade).HasColumnName("IdCidade");
        }
    }
}
