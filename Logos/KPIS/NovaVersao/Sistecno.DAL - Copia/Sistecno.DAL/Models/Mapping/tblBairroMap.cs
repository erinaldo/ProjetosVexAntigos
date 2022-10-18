using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class tblBairroMap : EntityTypeConfiguration<tblBairro>
    {
        public tblBairroMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Codigo, t.Descricao, t.UF, t.CodigoCidade });

            // Properties
            this.Property(t => t.Codigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(72);

            this.Property(t => t.BairroAbreviado)
                .HasMaxLength(36);

            this.Property(t => t.UF)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.CodigoCidade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("tblBairros");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.BairroAbreviado).HasColumnName("BairroAbreviado");
            this.Property(t => t.UF).HasColumnName("UF");
            this.Property(t => t.CodigoCidade).HasColumnName("CodigoCidade");
        }
    }
}
