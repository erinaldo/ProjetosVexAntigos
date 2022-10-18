using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOGRADOUROMap : EntityTypeConfiguration<LOGRADOURO>
    {
        public LOGRADOUROMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CD_LOGRADOURO, t.CD_BAIRRO, t.DS_LOGRADOURO_NOME, t.NO_LOGRADOURO_CEP });

            // Properties
            this.Property(t => t.CD_LOGRADOURO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CD_BAIRRO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CD_TIPO_LOGRADOUROS)
                .HasMaxLength(50);

            this.Property(t => t.DS_LOGRADOURO_NOME)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.NO_LOGRADOURO_CEP)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("LOGRADOUROS");
            this.Property(t => t.CD_LOGRADOURO).HasColumnName("CD_LOGRADOURO");
            this.Property(t => t.CD_BAIRRO).HasColumnName("CD_BAIRRO");
            this.Property(t => t.CD_TIPO_LOGRADOUROS).HasColumnName("CD_TIPO_LOGRADOUROS");
            this.Property(t => t.DS_LOGRADOURO_NOME).HasColumnName("DS_LOGRADOURO_NOME");
            this.Property(t => t.NO_LOGRADOURO_CEP).HasColumnName("NO_LOGRADOURO_CEP");
            this.Property(t => t.CD_ID_CIDADE).HasColumnName("CD_ID_CIDADE");
        }
    }
}
