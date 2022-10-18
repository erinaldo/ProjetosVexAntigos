using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaDespesaImagemMap : EntityTypeConfiguration<ContaDespesaImagem>
    {
        public ContaDespesaImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContaDespesaImagem);

            // Properties
            this.Property(t => t.IdContaDespesaImagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ContaDespesaImagem");
            this.Property(t => t.IdContaDespesaImagem).HasColumnName("IdContaDespesaImagem");
            this.Property(t => t.IdContaDespesa).HasColumnName("IdContaDespesa");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Imagem).HasColumnName("Imagem");

            // Relationships
            this.HasRequired(t => t.ContaDespesa)
                .WithMany(t => t.ContaDespesaImagems)
                .HasForeignKey(d => d.IdContaDespesa);

        }
    }
}
