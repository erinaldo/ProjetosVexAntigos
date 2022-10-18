using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaDeEmailMap : EntityTypeConfiguration<ContaDeEmail>
    {
        public ContaDeEmailMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContaDeEmail);

            // Properties
            this.Property(t => t.IdContaDeEmail)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Operacao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.De)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.DeApelido)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Senha)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SMTP)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Para)
                .HasMaxLength(100);

            this.Property(t => t.CCopia)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("ContaDeEmail");
            this.Property(t => t.IdContaDeEmail).HasColumnName("IdContaDeEmail");
            this.Property(t => t.Operacao).HasColumnName("Operacao");
            this.Property(t => t.De).HasColumnName("De");
            this.Property(t => t.DeApelido).HasColumnName("DeApelido");
            this.Property(t => t.Senha).HasColumnName("Senha");
            this.Property(t => t.SMTP).HasColumnName("SMTP");
            this.Property(t => t.Porta).HasColumnName("Porta");
            this.Property(t => t.Para).HasColumnName("Para");
            this.Property(t => t.CCopia).HasColumnName("CCopia");
        }
    }
}
