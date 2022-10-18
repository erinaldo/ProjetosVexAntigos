using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CIDADE1Map : EntityTypeConfiguration<CIDADE1>
    {
        public CIDADE1Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CD_CIDADE, t.DS_CIDADE_NOME });

            // Properties
            this.Property(t => t.CD_CIDADE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DS_CIDADE_NOME)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CODIGO_IBGE)
                .HasMaxLength(10);

            this.Property(t => t.UF)
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("CIDADES");
            this.Property(t => t.CD_UF).HasColumnName("CD_UF");
            this.Property(t => t.CD_CIDADE).HasColumnName("CD_CIDADE");
            this.Property(t => t.DS_CIDADE_NOME).HasColumnName("DS_CIDADE_NOME");
            this.Property(t => t.CD_AREA).HasColumnName("CD_AREA");
            this.Property(t => t.CODIGO_IBGE).HasColumnName("CODIGO_IBGE");
            this.Property(t => t.UF).HasColumnName("UF");
        }
    }
}
