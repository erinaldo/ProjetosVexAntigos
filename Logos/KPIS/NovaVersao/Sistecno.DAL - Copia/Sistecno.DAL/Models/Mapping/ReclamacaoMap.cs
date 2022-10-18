using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ReclamacaoMap : EntityTypeConfiguration<Reclamacao>
    {
        public ReclamacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdReclamacao);

            // Properties
            this.Property(t => t.IdReclamacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Reclamacao1)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Reclamacao");
            this.Property(t => t.IdReclamacao).HasColumnName("IdReclamacao");
            this.Property(t => t.Reclamacao1).HasColumnName("Reclamacao");
        }
    }
}
