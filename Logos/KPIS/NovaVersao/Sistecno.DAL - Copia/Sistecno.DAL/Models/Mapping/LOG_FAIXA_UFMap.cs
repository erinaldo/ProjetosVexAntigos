using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_FAIXA_UFMap : EntityTypeConfiguration<LOG_FAIXA_UF>
    {
        public LOG_FAIXA_UFMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UFE_SG, t.UFE_CEP_INI, t.UFE_CEP_FIM });

            // Properties
            this.Property(t => t.UFE_SG)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.UFE_CEP_INI)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.UFE_CEP_FIM)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("LOG_FAIXA_UF");
            this.Property(t => t.UFE_SG).HasColumnName("UFE_SG");
            this.Property(t => t.UFE_CEP_INI).HasColumnName("UFE_CEP_INI");
            this.Property(t => t.UFE_CEP_FIM).HasColumnName("UFE_CEP_FIM");
        }
    }
}
