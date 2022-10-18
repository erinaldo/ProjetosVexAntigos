using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EmailPendenteMap : EntityTypeConfiguration<EmailPendente>
    {
        public EmailPendenteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdEmailPendente);

            // Properties
            this.Property(t => t.Assunto)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("EmailPendente");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IdEmailPendente).HasColumnName("IdEmailPendente");
            this.Property(t => t.Conteudo).HasColumnName("Conteudo");
            this.Property(t => t.DataHoraEnvio).HasColumnName("DataHoraEnvio");
            this.Property(t => t.Assunto).HasColumnName("Assunto");
            this.Property(t => t.CC).HasColumnName("CC");
        }
    }
}
