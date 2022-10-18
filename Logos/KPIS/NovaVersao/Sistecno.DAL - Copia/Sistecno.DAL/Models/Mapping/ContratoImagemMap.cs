using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContratoImagemMap : EntityTypeConfiguration<ContratoImagem>
    {
        public ContratoImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContratoImagem);

            // Properties
            this.Property(t => t.IdContratoImagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ContratoImagem");
            this.Property(t => t.IdContratoImagem).HasColumnName("IdContratoImagem");
            this.Property(t => t.IdContrato).HasColumnName("IdContrato");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Imagem).HasColumnName("Imagem");

            // Relationships
            this.HasRequired(t => t.Contrato)
                .WithMany(t => t.ContratoImagems)
                .HasForeignKey(d => d.IdContrato);

        }
    }
}
