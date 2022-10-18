using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConferenciaItemNaoCadastradoMap : EntityTypeConfiguration<ConferenciaItemNaoCadastrado>
    {
        public ConferenciaItemNaoCadastradoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConferenciaItemNaoCadastrado);

            // Properties
            this.Property(t => t.IdConferenciaItemNaoCadastrado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodigoDeBarras)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ConferenciaItemNaoCadastrado");
            this.Property(t => t.IdConferenciaItemNaoCadastrado).HasColumnName("IdConferenciaItemNaoCadastrado");
            this.Property(t => t.IdConferencia).HasColumnName("IdConferencia");
            this.Property(t => t.CodigoDeBarras).HasColumnName("CodigoDeBarras");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");

            // Relationships
            this.HasRequired(t => t.Conferencia)
                .WithMany(t => t.ConferenciaItemNaoCadastradoes)
                .HasForeignKey(d => d.IdConferencia);

        }
    }
}
