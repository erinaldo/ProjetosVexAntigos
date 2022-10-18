using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ESTADO1Map : EntityTypeConfiguration<ESTADO1>
    {
        public ESTADO1Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CD_UF, t.DS_UF_SIGLA, t.DS_UF_NOME });

            // Properties
            this.Property(t => t.CD_UF)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DS_UF_SIGLA)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.DS_UF_NOME)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODIGO_IBGE)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ESTADOS");
            this.Property(t => t.CD_UF).HasColumnName("CD_UF");
            this.Property(t => t.DS_UF_SIGLA).HasColumnName("DS_UF_SIGLA");
            this.Property(t => t.DS_UF_NOME).HasColumnName("DS_UF_NOME");
            this.Property(t => t.CODIGO_IBGE).HasColumnName("CODIGO_IBGE");
        }
    }
}
