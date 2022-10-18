using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CidadeFaixaDeCepMap : EntityTypeConfiguration<CidadeFaixaDeCep>
    {
        public CidadeFaixaDeCepMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCidadeFaixaDeCep);

            // Properties
            this.Property(t => t.IDCidadeFaixaDeCep)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CepInicial)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.CepFinal)
                .IsFixedLength()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("CidadeFaixaDeCep");
            this.Property(t => t.IDCidadeFaixaDeCep).HasColumnName("IDCidadeFaixaDeCep");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.CepInicial).HasColumnName("CepInicial");
            this.Property(t => t.CepFinal).HasColumnName("CepFinal");
        }
    }
}
