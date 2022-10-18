using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ServicoMap : EntityTypeConfiguration<Servico>
    {
        public ServicoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDServico);

            // Properties
            this.Property(t => t.IDServico)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Servico");
            this.Property(t => t.IDServico).HasColumnName("IDServico");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
