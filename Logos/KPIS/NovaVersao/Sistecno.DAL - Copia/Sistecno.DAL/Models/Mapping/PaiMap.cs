using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PaiMap : EntityTypeConfiguration<Pai>
    {
        public PaiMap()
        {
            // Primary Key
            this.HasKey(t => t.IDPais);

            // Properties
            this.Property(t => t.IDPais)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Sigla)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Sigla1)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.CodigoDoBacen)
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("Pais");
            this.Property(t => t.IDPais).HasColumnName("IDPais");
            this.Property(t => t.Sigla).HasColumnName("Sigla");
            this.Property(t => t.Sigla1).HasColumnName("Sigla1");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.CodigoDoBacen).HasColumnName("CodigoDoBacen");
        }
    }
}
