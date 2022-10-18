using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ColetorConferenciaItemMap : EntityTypeConfiguration<ColetorConferenciaItem>
    {
        public ColetorConferenciaItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdColetorConferenciaItem);

            // Properties
            this.Property(t => t.IdColetorConferenciaItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodigoDeBarras)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ColetorConferenciaItem");
            this.Property(t => t.IdColetorConferenciaItem).HasColumnName("IdColetorConferenciaItem");
            this.Property(t => t.IdColetorConferencia).HasColumnName("IdColetorConferencia");
            this.Property(t => t.CodigoDeBarras).HasColumnName("CodigoDeBarras");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.DataHora).HasColumnName("DataHora");

            // Relationships
            this.HasRequired(t => t.ColetorConferencia)
                .WithMany(t => t.ColetorConferenciaItems)
                .HasForeignKey(d => d.IdColetorConferencia);

        }
    }
}
