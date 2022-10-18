using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BAIRRO1Map : EntityTypeConfiguration<BAIRRO1>
    {
        public BAIRRO1Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CD_BAIRRO, t.CD_CIDADE, t.DS_BAIRRO_NOME });

            // Properties
            this.Property(t => t.CD_BAIRRO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CD_CIDADE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DS_BAIRRO_NOME)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("BAIRROS");
            this.Property(t => t.CD_BAIRRO).HasColumnName("CD_BAIRRO");
            this.Property(t => t.CD_CIDADE).HasColumnName("CD_CIDADE");
            this.Property(t => t.DS_BAIRRO_NOME).HasColumnName("DS_BAIRRO_NOME");
        }
    }
}
